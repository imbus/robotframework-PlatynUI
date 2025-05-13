using System.Buffers;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.Threading;

namespace PlatynUI.JsonRpc;

public enum JsonRpcErrorCode
{
    ParseError = -32700,
    InvalidRequest = -32600,
    MethodNotFound = -32601,
    InvalidParams = -32602,
    InternalError = -32603,
}

// Add JSON-RPC version constant
public static class JsonRpcConstants
{
    public const string JSON_RPC_VERSION = "2.0";
}

public abstract record JsonRpcMessage
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; init; } = JsonRpcConstants.JSON_RPC_VERSION;
}

public record JsonRpcRequest : JsonRpcMessage
{
    [JsonPropertyName("method")]
    public required string Method { get; init; }

    [JsonPropertyName("params")]
    public JsonElement? Params { get; set; } = null;

    [JsonPropertyName("id")]
    public required JsonElement? Id { get; set; }
}

public record JsonRpcNotification : JsonRpcMessage
{
    [JsonPropertyName("method")]
    public required string Method { get; init; }

    [JsonPropertyName("params")]
    public JsonElement? Params { get; set; } = null;
}

public abstract record JsonResponseBase : JsonRpcMessage { }

public record JsonRpcResponse : JsonResponseBase
{
    [JsonPropertyName("result")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public object? Result { get; set; }

    [JsonPropertyName("id")]
    public required JsonElement Id { get; set; }
}

public class JsonRpcError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

public record JsonRpcErrorResponse : JsonResponseBase
{
    [JsonPropertyName("error")]
    public required JsonRpcError Error { get; set; }

    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required JsonElement? Id { get; set; }
}

public class JsonRpcPeer(Stream readerStream, Stream writerStream)
{
    public JsonRpcPeer(Stream stream)
        : this(stream, stream) { }

    public readonly Stream ReaderStream = readerStream;
    public readonly Stream WriterStream = writerStream;

    public JoinableTaskFactory? JoinableTaskFactory { get; set; }

    private readonly ConcurrentDictionary<
        string,
        Func<JsonElement?, JsonSerializerOptions, Task<object?>>
    > requestHandlers = new();
    private readonly ConcurrentDictionary<
        string,
        Func<JsonElement?, JsonSerializerOptions, Task>
    > notificationHandlers = new();

    private readonly ConcurrentDictionary<string, TaskCompletionSource<JsonRpcResponse>> pendingRequests = new();
    private int idCounter = 1;
    private readonly Encoding defaultEncoding = Encoding.UTF8;
    private readonly string defaultContentType = "application/vscode-jsonrpc";

    private Task? messageLoopTask;
    private CancellationTokenSource? cancellationTokenSource;

    public JsonSerializerOptions JsonSerializerOptions { get; set; } =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    private readonly TaskCompletionSource<bool> completionSource = new();

    public Task<bool> Completion
    {
        get { return completionSource.Task; }
    }

    public void Start()
    {
        if (messageLoopTask == null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            messageLoopTask = Task.Run(() => MessageLoopAsync(cancellationTokenSource.Token));
        }
    }

    public void Stop()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = null;
            messageLoopTask = null;
        }
    }

    public void RegisterRequestHandler<T>(string methodName, Func<JsonElement?, JsonSerializerOptions, Task<T>> handler)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        if (requestHandlers.ContainsKey(methodName))
        {
            throw new ArgumentException(
                $"Request handler for method '{methodName}' already exists",
                nameof(methodName)
            );
        }
        requestHandlers[methodName] = async (parameters, options) => await handler(parameters, options);
    }

    public void RegisterNotificationHandler(string methodName, Func<JsonElement?, JsonSerializerOptions, Task> handler)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);
        if (notificationHandlers.ContainsKey(methodName))
        {
            throw new ArgumentException(
                $"Notification handler for method '{methodName}' already exists",
                nameof(methodName)
            );
        }
        notificationHandlers[methodName] = handler;
    }

    private readonly SemaphoreSlim _writeLock = new(1, 1);

    private async Task SendMessageAsync(string json)
    {
        await _writeLock.WaitAsync();
        try
        {
            var bodyBytes = defaultEncoding.GetBytes(json + "\r\n");

            var headerBuilder = new StringBuilder();
            headerBuilder.Append("Content-Length: ").Append(bodyBytes.Length).Append("\r\n");

            if (defaultEncoding != Encoding.UTF8)
            {
                headerBuilder
                    .Append($"Content-Type: application/${defaultContentType}; charset=")
                    .Append(defaultEncoding.WebName)
                    .Append("\r\n");
            }

            headerBuilder.Append("\r\n");
            var header = headerBuilder.ToString();

            var headerLen = header.Length;
            var totalLen = headerLen + bodyBytes.Length;
            var buffer = ArrayPool<byte>.Shared.Rent(totalLen);

            try
            {
                Encoding.ASCII.GetBytes(header, new Span<byte>(buffer, 0, headerLen));

                Buffer.BlockCopy(bodyBytes, 0, buffer, headerLen, bodyBytes.Length);

                await WriterStream.WriteAsync(buffer.AsMemory(0, totalLen));
                await WriterStream.FlushAsync();
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
        finally
        {
            _writeLock.Release();
        }
    }

    protected static void LogError(string message)
    {
        Console.Error.WriteLine(message);
    }

    public void SendNotification(string method, object @params)
    {
        if (JoinableTaskFactory != null)
        {
            JoinableTaskFactory.Run(async () => await SendNotificationAsync(method, @params));
        }
        else
        {
            Task.Run(async () => await SendNotificationAsync(method, @params)).GetAwaiter().GetResult();
        }
    }

    public async Task SendNotificationAsync(string method, object @params)
    {
        var message = new JsonRpcNotification
        {
            Method = method,
            Params = @params != null ? JsonSerializer.SerializeToElement(@params, JsonSerializerOptions) : null,
        };
        var json = JsonSerializer.Serialize(message, JsonSerializerOptions);
        await SendMessageAsync(json);
    }

    public void SendRequest(string method)
    {
        SendRequest(method, null);
    }

    public void SendRequest(string method, object? @params)
    {
        SendRequest<object>(method, @params);
    }

    public T SendRequest<T>(string method)
    {
        return SendRequest<T>(method, null);
    }

    public T SendRequest<T>(string method, object? @params)
    {
        if (JoinableTaskFactory != null)
        {
            return JoinableTaskFactory.Run(async () => await SendRequestAsync<T>(method, @params));
        }
        else
        {
            return Task.Run(async () => await SendRequestAsync<T>(method, @params)).GetAwaiter().GetResult();
        }
    }

    public async Task SendRequestAsync(string method)
    {
        await SendRequestAsync<object>(method, null);
    }

    public async Task SendRequestAsync(string method, object? @params)
    {
        await SendRequestAsync<object>(method, @params);
    }

    public async Task<T> SendRequestAsync<T>(string method)
    {
        return await SendRequestAsync<T>(method, null);
    }

    public async Task<T> SendRequestAsync<T>(string method, object? @params)
    {
        var id = Interlocked.Increment(ref idCounter).ToString();
        var message = new JsonRpcRequest
        {
            Method = method,
            Params = @params != null ? JsonSerializer.SerializeToElement(@params, JsonSerializerOptions) : null,
            Id = JsonSerializer.SerializeToElement(id),
        };
        var json = JsonSerializer.Serialize(message, JsonSerializerOptions);

        var tcs = new TaskCompletionSource<JsonRpcResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
        pendingRequests[id] = tcs;

        await SendMessageAsync(json);

        var response = await tcs.Task;

        if (response.Result is JsonElement jsonElement)
        {
            T? result = jsonElement.Deserialize<T>(JsonSerializerOptions);

            if (result == null && !IsNullable(typeof(T)))
            {
                throw new InvalidOperationException(
                    $"Deserialization resulted in null for non-nullable type {typeof(T).FullName}"
                );
            }

            return result!;
        }

        return (T)default!;
    }

    private static bool IsNullable(Type type)
    {
        return Nullable.GetUnderlyingType(type) != null || !type.IsValueType;
    }

    static (string HeaderName, string HeaderValue) SplitHeader(string headerLine)
    {
        if (string.IsNullOrWhiteSpace(headerLine))
            return (string.Empty, string.Empty);

        var separatorIndex = headerLine.IndexOf(':');
        if (separatorIndex == -1)
            return (headerLine.Trim(), string.Empty);

        var headerName = headerLine[..separatorIndex].Trim();
        var headerValue = headerLine[(separatorIndex + 1)..].Trim();

        return (headerName, headerValue);
    }

    protected async Task<string?> ReadMessageAsync()
    {
        var contentLength = 0;
        var hasContentLength = false;
        var encoding = Encoding.UTF8;

        byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
        try
        {
            var headerBuilder = new StringBuilder(128);
            var inHeaderSection = true;
            var bufferPos = 0;
            var bufferLen = 0;

            while (inHeaderSection)
            {
                if (bufferPos >= bufferLen)
                {
                    bufferPos = 0;
                    bufferLen = await ReaderStream.ReadAsync(buffer, cancellationTokenSource!.Token);

                    if (bufferLen == 0)
                        return null;
                }

                var availableData = buffer.AsSpan(bufferPos, bufferLen - bufferPos);
                var crlfIndex = -1;

                for (var i = 0; i < availableData.Length - 1; i++)
                {
                    if (availableData[i] == '\r' && availableData[i + 1] == '\n')
                    {
                        crlfIndex = i;
                        break;
                    }
                }

                if (crlfIndex >= 0)
                {
                    if (crlfIndex > 0)
                    {
                        var line = Encoding.ASCII.GetString(availableData[..crlfIndex]);
                        headerBuilder.Append(line);
                    }

                    var headerLine = headerBuilder.ToString();
                    headerBuilder.Clear();

                    var (headerName, headerValue) = SplitHeader(headerLine);

                    if (headerLine.Length == 0)
                    {
                        if (hasContentLength)
                            inHeaderSection = false;
                    }
                    else if (headerName.StartsWith("content-length", StringComparison.OrdinalIgnoreCase))
                    {
                        hasContentLength = true;
                        contentLength = int.Parse(headerValue);
                    }
                    else if (headerName.StartsWith("content-type", StringComparison.OrdinalIgnoreCase))
                    {
                        var contentType = headerValue;
                        var charsetToken = contentType
                            .Split(';')
                            .FirstOrDefault(s => s.Trim().StartsWith("charset=", StringComparison.OrdinalIgnoreCase));

                        if (charsetToken != null)
                        {
                            var charset = charsetToken.Split('=')[1].Trim();
                            try
                            {
                                encoding = Encoding.GetEncoding(charset);
                            }
                            catch (ArgumentException)
                            {
                                LogError($"Unsupported encoding: {charset}");
                                encoding = defaultEncoding;
                            }
                        }
                    }

                    bufferPos += crlfIndex + 2;
                }
                else
                {
                    headerBuilder.Append(Encoding.ASCII.GetString(availableData));
                    bufferPos = bufferLen;
                }
            }

            if (contentLength <= 0)
                return null;

            var usePooledBuffer = contentLength > 8192;
            var contentBuffer = usePooledBuffer ? ArrayPool<byte>.Shared.Rent(contentLength) : new byte[contentLength];

            try
            {
                var bytesRead = 0;

                if (bufferPos < bufferLen)
                {
                    var bytesToCopy = Math.Min(bufferLen - bufferPos, contentLength);
                    Buffer.BlockCopy(buffer, bufferPos, contentBuffer, 0, bytesToCopy);
                    bytesRead = bytesToCopy;
                }

                while (bytesRead < contentLength)
                {
                    var read = await ReaderStream.ReadAsync(
                        contentBuffer.AsMemory(bytesRead, contentLength - bytesRead),
                        cancellationTokenSource!.Token
                    );

                    if (read == 0)
                        break;

                    bytesRead += read;
                }

                if (bytesRead == contentLength)
                {
                    return encoding.GetString(contentBuffer, 0, contentLength);
                }

                return null;
            }
            finally
            {
                if (usePooledBuffer)
                {
                    ArrayPool<byte>.Shared.Return(contentBuffer);
                }
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    protected async Task MessageLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var json = await ReadMessageAsync();
                if (json == null)
                    break;

                await HandleIncomingMessageAsync(json);
            }
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            completionSource.TrySetException(e);
            return;
        }

        completionSource.TrySetResult(!cancellationToken.IsCancellationRequested);
    }

    protected async Task HandleIncomingMessageAsync(string json)
    {
        try
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json, JsonSerializerOptions);

            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                var responses = new List<object>();

                foreach (var element in jsonElement.EnumerateArray())
                {
                    var response = await ProcessSingleMessage(element);
                    if (response != null)
                    {
                        responses.Add(response);
                    }
                }

                if (responses.Count > 0)
                {
                    var batchResponse = JsonSerializer.Serialize<object?>(responses, JsonSerializerOptions);
                    await SendMessageAsync(batchResponse);
                }
            }
            else
            {
                var response = await ProcessSingleMessage(jsonElement);
                if (response != null)
                {
                    var singleResponse = JsonSerializer.Serialize<object?>(response, JsonSerializerOptions);
                    await SendMessageAsync(singleResponse);
                }
            }
        }
        catch (JsonException ex)
        {
            await SendMessageAsync(
                JsonSerializer.Serialize(
                    new JsonRpcErrorResponse
                    {
                        Error = new JsonRpcError
                        {
                            Code = (int)JsonRpcErrorCode.ParseError,
                            Message = ex.Message,
                            Data = ex.StackTrace,
                        },
                        Id = null,
                    },
                    JsonSerializerOptions
                )
            );
        }
        catch (Exception ex)
        {
            await SendMessageAsync(
                JsonSerializer.Serialize(
                    new JsonRpcErrorResponse
                    {
                        Error = new JsonRpcError
                        {
                            Code = (int)JsonRpcErrorCode.InvalidRequest,
                            Message = ex.Message,
                            Data = ex.StackTrace,
                        },
                        Id = null,
                    },
                    JsonSerializerOptions
                )
            );
        }
    }

    protected async Task<JsonResponseBase?> ProcessSingleMessage(JsonElement jsonElement)
    {
        // If it's a response (has id and no method), we should be more tolerant of version differences
        bool isResponse = jsonElement.TryGetProperty("id", out var _) && !jsonElement.TryGetProperty("method", out _);

        if (jsonElement.TryGetProperty("jsonrpc", out var versionProperty))
        {
            // Only enforce version check for requests/notifications, not for responses
            if (versionProperty.GetString() != JsonRpcConstants.JSON_RPC_VERSION && !isResponse)
            {
                return new JsonRpcErrorResponse
                {
                    Id = null,
                    Error = new JsonRpcError
                    {
                        Code = (int)JsonRpcErrorCode.InvalidRequest,
                        Message = $"Unsupported JSON-RPC version: {versionProperty.GetString()}",
                    },
                };
            }
        }
        else if (!isResponse) // Only require jsonrpc property for requests/notifications
        {
            // If jsonrpc property is missing entirely
            return new JsonRpcErrorResponse
            {
                Id = null,
                Error = new JsonRpcError
                {
                    Code = (int)JsonRpcErrorCode.InvalidRequest,
                    Message = "Missing required 'jsonrpc' property",
                },
            };
        }

        if (jsonElement.TryGetProperty("id", out var idProperty) && !jsonElement.TryGetProperty("method", out _))
        {
            if (
                idProperty.ValueKind != JsonValueKind.Null
                && pendingRequests.TryRemove(idProperty.ToString(), out var tcs)
            )
            {
                if (jsonElement.TryGetProperty("error", out var errorProperty))
                {
                    var error = errorProperty.Deserialize<JsonRpcError>(JsonSerializerOptions);
                    tcs.SetException(new Exception($"RPC Error {error?.Code}: {error?.Message} ({error?.Data})"));
                }
                else if (jsonElement.TryGetProperty("result", out var resultProperty))
                {
                    var response = new JsonRpcResponse { Id = idProperty, Result = resultProperty };
                    tcs.SetResult(response);
                }
                else
                {
                    tcs.SetException(new Exception("RPC Error: Invalid response format"));
                }
                return null;
            }
            else if (
                idProperty.ValueKind == JsonValueKind.Null
                && jsonElement.TryGetProperty("error", out var errorProperty)
            )
            {
                var error = errorProperty.Deserialize<JsonRpcError>(JsonSerializerOptions);
                LogError($"Received error with null ID: {error?.Code}: {error?.Message} ({error?.Data})");

                return null;
            }
        }
        else if (jsonElement.TryGetProperty("method", out var methodProperty))
        {
            if (
                jsonElement.TryGetProperty("id", out var requestIdProperty)
                && requestIdProperty.ValueKind != JsonValueKind.Null
            )
            {
                var message =
                    jsonElement.Deserialize<JsonRpcRequest>(JsonSerializerOptions)
                    ?? throw new JsonException("Invalid request format");
                return await ProcessRequestAsync(message, requestIdProperty);
            }
            else
            {
                var message =
                    jsonElement.Deserialize<JsonRpcNotification>(JsonSerializerOptions)
                    ?? throw new JsonException("Invalid notification format");
                return await ProcessNotificationAsync(message);
            }
        }

        return null;
    }

    protected async Task<JsonResponseBase> ProcessRequestAsync(JsonRpcRequest message, JsonElement requestId)
    {
        if (requestHandlers.TryGetValue(message.Method, out var handler))
        {
            try
            {
                var result = await handler(message.Params, JsonSerializerOptions);
                return new JsonRpcResponse { Id = requestId, Result = result };
            }
            catch (Exception ex)
            {
                return new JsonRpcErrorResponse
                {
                    Id = requestId,
                    Error = new JsonRpcError
                    {
                        Code = (int)JsonRpcErrorCode.InternalError,
                        Message = ex.Message,
                        Data = ex.StackTrace,
                    },
                };
            }
        }
        else
        {
            return new JsonRpcErrorResponse
            {
                Id = requestId,
                Error = new JsonRpcError
                {
                    Code = (int)JsonRpcErrorCode.MethodNotFound,
                    Message = $"Method for request {message?.Method} not found",
                },
            };
        }
    }

    protected async Task<JsonResponseBase?> ProcessNotificationAsync(JsonRpcNotification message)
    {
        if (notificationHandlers.TryGetValue(message.Method, out var handler))
        {
            try
            {
                await handler(message.Params, JsonSerializerOptions);
                return null;
            }
            catch (Exception ex)
            {
                return new JsonRpcErrorResponse
                {
                    Id = null,
                    Error = new JsonRpcError
                    {
                        Code = (int)JsonRpcErrorCode.InternalError,
                        Message = ex.Message,
                        Data = ex.StackTrace,
                    },
                };
            }
        }
        else
        {
            return new JsonRpcErrorResponse
            {
                Id = null,
                Error = new JsonRpcError
                {
                    Code = (int)JsonRpcErrorCode.MethodNotFound,
                    Message = $"Method for notification {message?.Method} not found",
                },
            };
        }
    }
}
