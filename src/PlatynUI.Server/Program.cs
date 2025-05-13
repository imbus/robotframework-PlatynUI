using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using PlatynUI.JsonRpc;
using PlatynUI.Server.Endpoints;

class Program
{
    enum TransportType
    {
        Stdio,
        Tcp,
        PipeServer,
        UnixServer,
        AttachPipe,
        AttachUnixSocket,
    }

    private static bool _verbose;

    private static void Log(string message)
    {
        if (_verbose)
        {
            Console.Error.WriteLine(message);
        }
    }

    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("JSON-RPC Server")
        {
            new Option<bool>(
                "--stdio",
                "Run the server in `stdio` mode, communicating over standard input and output."
            ),
            new Option<string>(
                "--tcp",
                "Run in `tcp` server mode and listen at the given addresses and ports.\n"
                    + "Examples:\n"
                    + "  7721 - Listen on localhost (IPv4 and IPv6 loopback) at port 7721.\n"
                    + "  127.0.0.1:7721 - Listen on IPv4 address 127.0.0.1 at port 7721.\n"
                    + "  [::1]:7721 - Listen on IPv6 loopback address at port 7721.\n"
                    + "  *:7721 - Listen on all available addresses (IPv4 and IPv6) at port 7721.\n"
                    + "  * - Listen on all available addresses (IPv4 and IPv6) at default port 7720."
            ),
            new Option<string>(
                "--unix-socket-server",
                "Run the server in `unix socket` mode and listen on the specified socket path.\n"
                    + "Example:\n"
                    + "  /tmp/server.sock"
            ),
            new Option<string>(
                "--pipe-server",
                "Run the server in `pipe` mode and listen on the specified named pipe.\n"
                    + "Example:\n"
                    + "  my-pipe-name"
            ),
            new Option<string>(
                "--attach-pipe",
                "Attach to an existing named pipe for communication.\n" + "Example:\n" + "  my-existing-pipe"
            ),
            new Option<string>(
                "--attach-unix-socket",
                "Attach to an existing Unix domain socket for communication.\n" + "Example:\n" + "  /tmp/existing.sock"
            ),
            new Option<bool>("--verbose", "Enable detailed logging output for debugging purposes."),
        };

        rootCommand.Description = "JSON-RPC Server with various transport options";

        rootCommand.Handler = CommandHandler.Create<bool, string, string, string, string, string, bool>(
            async (stdio, tcp, pipeServer, unixSocketServer, attachPipe, attachUnixSocket, verbose) =>
            {
                _verbose = verbose;

                ConfigureLogging(_verbose);

                if (!string.IsNullOrEmpty(attachPipe))
                {
                    await AttachToNamedPipe(attachPipe);
                    return;
                }

                if (!string.IsNullOrEmpty(attachUnixSocket))
                {
                    await AttachToUnixSocket(attachUnixSocket);
                    return;
                }

                var (transportType, transportValue) = DetermineTransport(
                    stdio,
                    tcp,
                    pipeServer,
                    unixSocketServer,
                    attachPipe,
                    attachUnixSocket
                );

                Log($"Using transport: {transportType}");

                await RunServerAsync(transportType, transportValue);
            }
        );

        var result = await rootCommand.InvokeAsync(args);
        if (result != 0)
        {
            Log($"Command failed with exit code {result}.");
        }
        else
        {
            Log("Server stopped successfully.");
        }
        return result;
    }

    private static async Task<int> RunServerAsync(TransportType transportType, string transportValue)
    {
        switch (transportType)
        {
            case TransportType.Stdio:
                await RunStdioServer();
                break;

            case TransportType.Tcp:
                await RunTcpServer(transportValue);
                break;

            case TransportType.PipeServer:
                string pipeNameValue = transportValue ?? "default-pipe";
                await RunPipeServer(pipeNameValue);
                break;

            case TransportType.UnixServer:
                string socketPath = transportValue ?? Path.Combine(Path.GetTempPath(), "platynui_jsonrpc.sock");
                await RunUnixSocketServer(socketPath);
                break;

            case TransportType.AttachPipe:
                await AttachToNamedPipe(transportValue);
                return 0;

            case TransportType.AttachUnixSocket:
                await AttachToUnixSocket(transportValue);
                return 0;

            default:
                throw new InvalidOperationException("Unsupported transport type.");
        }

        return 0;
    }

    private static async Task RunStdioServer()
    {
        Log("Starting Stdio server...");

        var stdout = Console.OpenStandardOutput();
        var stdin = Console.OpenStandardInput();
        await CreateAndRunJsonRpc(stdout, stdin);
    }

    private static async Task RunTcpServer(string tcpOption)
    {
        List<TcpListener> listeners = [];

        if (tcpOption.StartsWith("*", StringComparison.OrdinalIgnoreCase))
        {
            int port = 7720;
            if (tcpOption.Length > 2 && tcpOption[1] == ':')
            {
                var portPart = tcpOption[2..];
                if (!int.TryParse(portPart, out port) || port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
                {
                    throw new ArgumentException("Invalid port specified for '*'. Port must be between 0 and 65535.");
                }
            }

            listeners.Add(new TcpListener(IPAddress.Any, port));
            listeners.Add(new TcpListener(IPAddress.IPv6Any, port));
        }
        else
        {
            var endpoints = tcpOption
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(option =>
                {
                    if (int.TryParse(option, out int port))
                    {
                        if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
                        {
                            throw new ArgumentOutOfRangeException(
                                nameof(tcpOption),
                                "Port must be between 0 and 65535."
                            );
                        }
                        listeners.Add(new TcpListener(IPAddress.Loopback, port));
                        if (Socket.OSSupportsIPv6)
                        {
                            listeners.Add(new TcpListener(IPAddress.IPv6Loopback, port));
                        }
                        return null;
                    }
                    else if (IPEndPoint.TryParse(option, out IPEndPoint? endpoint))
                    {
                        if (endpoint.Port == 0)
                        {
                            endpoint = new IPEndPoint(endpoint.Address, 7720);
                        }
                        return endpoint;
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"Invalid format for address: {option}. Use <address>:<port> or <port>."
                        );
                    }
                })
                .Where(endpoint => endpoint != null)
                .Cast<IPEndPoint>()
                .ToList();

            foreach (var endpoint in endpoints)
            {
                listeners.Add(new TcpListener(endpoint));
            }
        }

        var tasks = listeners.Select(async listener =>
        {
            listener.Start();
            Log($"TCP server is running on {listener.LocalEndpoint}. Waiting for connections...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                client.NoDelay = true;
                Log($"Client connected on {listener.LocalEndpoint}.");

                _ = Task.Run(async () =>
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        await CreateAndRunJsonRpc(stream, stream);
                    }
                    Log($"Client disconnected from {listener.LocalEndpoint}.");
                });
            }
        });

        await Task.WhenAll(tasks);
    }

    private static async Task RunPipeServer(string pipeName)
    {
        Log($"Creating Named Pipe transport with name {pipeName}");

        while (true)
        {
            const string prefix = @"\\.\pipe\";

            string name = pipeName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? pipeName[prefix.Length..]
                : pipeName;

            var pipeServer = new NamedPipeServerStream(
                name,
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly
            );

            Log("Waiting for pipe connection...");

            await pipeServer.WaitForConnectionAsync();
            Log("Pipe client connected.");

            _ = Task.Run(async () =>
            {
                using (pipeServer)
                {
                    await CreateAndRunJsonRpc(pipeServer, pipeServer);
                }
                Log("Pipe client disconnected.");
            });
        }
    }

    private static async Task RunUnixSocketServer(string socketPath)
    {
        Log($"Creating Unix Domain Socket at {socketPath}");

        if (File.Exists(socketPath))
        {
            File.Delete(socketPath);
        }

        var endpoint = new UnixDomainSocketEndPoint(socketPath);
        var listener = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

        listener.Bind(endpoint);
        listener.Listen(10);

        Log("Unix socket server is running. Waiting for connections...");

        try
        {
            while (true)
            {
                var clientSocket = await listener.AcceptAsync();
                Log("Client connected.");

                _ = Task.Run(async () =>
                {
                    using (var networkStream = new NetworkStream(clientSocket, true))
                    {
                        await CreateAndRunJsonRpc(networkStream, networkStream);
                    }
                    Log("Client disconnected.");
                });
            }
        }
        finally
        {
            listener.Close();
            if (File.Exists(socketPath))
            {
                File.Delete(socketPath);
            }
            Log("Unix socket server shut down and socket file deleted.");
        }
    }

    private static async Task AttachToNamedPipe(string pipeName)
    {
        Log($"Attaching to existing Named Pipe: {pipeName}");

        using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
        await pipeClient.ConnectAsync();
        Log("Connected to Named Pipe.");

        await CreateAndRunJsonRpc(pipeClient, pipeClient);
    }

    private static async Task AttachToUnixSocket(string socketPath)
    {
        Log($"Attaching to existing Unix Domain Socket: {socketPath}");

        var clientSocket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        var endpoint = new UnixDomainSocketEndPoint(socketPath);

        await clientSocket.ConnectAsync(endpoint);
        Log("Connected to Unix Domain Socket.");

        using var networkStream = new NetworkStream(clientSocket, true);

        await CreateAndRunJsonRpc(networkStream, networkStream);
    }

    private static (TransportType Type, string Value) DetermineTransport(
        bool useStdio,
        string tcp,
        string pipeServer,
        string unixSocketServer,
        string attachPipe,
        string attachUnixSocket
    )
    {
        int transportCount = 0;
        if (useStdio)
            transportCount++;
        if (!string.IsNullOrEmpty(tcp))
            transportCount++;
        if (!string.IsNullOrEmpty(pipeServer))
            transportCount++;
        if (!string.IsNullOrEmpty(unixSocketServer))
            transportCount++;
        if (!string.IsNullOrEmpty(attachPipe))
            transportCount++;
        if (!string.IsNullOrEmpty(attachUnixSocket))
            transportCount++;

        if (transportCount > 1)
        {
            throw new InvalidOperationException(
                "Invalid transport configuration. Please specify only one transport option."
            );
        }

        if (transportCount == 0)
        {
            useStdio = true;
        }

        TransportType? selectedTransport = null;
        string? transportValue = null;

        if (!string.IsNullOrEmpty(pipeServer))
        {
            selectedTransport = TransportType.PipeServer;
            transportValue = pipeServer;
        }
        else if (useStdio)
        {
            selectedTransport = TransportType.Stdio;
        }
        else if (!string.IsNullOrEmpty(tcp))
        {
            selectedTransport = TransportType.Tcp;
            transportValue = tcp ?? "7720";
        }
        else if (!string.IsNullOrEmpty(unixSocketServer))
        {
            selectedTransport = TransportType.UnixServer;
            transportValue = unixSocketServer;
        }
        else if (!string.IsNullOrEmpty(attachPipe))
        {
            selectedTransport = TransportType.AttachPipe;
            transportValue = attachPipe;
        }
        else if (!string.IsNullOrEmpty(attachUnixSocket))
        {
            selectedTransport = TransportType.AttachUnixSocket;
            transportValue = attachUnixSocket;
        }

        if (selectedTransport == null)
        {
            throw new InvalidOperationException("No valid transport option was specified.");
        }

        return (selectedTransport.Value, transportValue ?? string.Empty);
    }

    private static async Task CreateAndRunJsonRpc(Stream sendingStream, Stream receivingStream)
    {
        Log("Creating JSON-RPC instance...");

        var peer = new JsonRpcPeer(receivingStream, sendingStream)
        {
            JsonSerializerOptions =
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            },
        };

        DisplayDeviceEndPoint.Attach(peer);
        MouseDeviceEndpoint.Attach(peer);

        peer.Start();

        await peer.Completion;
    }

    private static void ConfigureLogging(bool verbose)
    {
        Log(verbose ? "Verbose logging is enabled." : "Verbose logging is disabled.");
    }

    private static bool IsWindowsPlatform()
    {
        return Environment.OSVersion.Platform == PlatformID.Win32NT;
    }

    public static string GetVersion()
    {
        return typeof(Program).Assembly.GetName().Version?.ToString() ?? "1.0.0";
    }
}
