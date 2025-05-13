using System.Diagnostics;
using System.IO.Pipes;
using PlatynUI.Provider.Core;
using StreamJsonRpc;

namespace PlatynUI.Provider.Server
{
    public class ProviderServer(IApplicationInfoAsync applicationInfo, INodeInfoAsync nodeInfo)
    {
        IApplicationInfoAsync ApplicationInfo { get; } = applicationInfo;
        INodeInfoAsync NodeInfo { get; } = nodeInfo;

        async Task RespondToRpcRequestsAsync(Stream stream, int clientId)
        {
            Debug.WriteLine(
                $"Connection request #{clientId} received. Spinning off an async Task to cater to requests."
            );

            var jsonRpc = new JsonRpc(stream);
            jsonRpc.AddLocalRpcTarget(ApplicationInfo);
            jsonRpc.AddLocalRpcTarget(NodeInfo);

            jsonRpc.StartListening();

            Debug.WriteLine($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");

            await jsonRpc.Completion;

            Debug.WriteLine($"Connection #{clientId} terminated.");
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            string pipeName = PipeHelper.BuildPipeName(Environment.ProcessId);

            using var mutex = new Mutex(true, pipeName);

            int clientId = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.WriteLine("Cancellation requested. Exiting the loop.");
                    break;
                }

                Debug.WriteLine("Waiting for client to make a connection...");

                var stream = new NamedPipeServerStream(
                    pipeName,
                    PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous
                );
                try
                {
                    await stream.WaitForConnectionAsync(cancellationToken);
                    _ = RespondToRpcRequestsAsync(stream, ++clientId);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error while waiting for connection: {e}");
                }
            }
        }
    }
}
