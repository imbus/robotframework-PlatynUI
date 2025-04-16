// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel.Composition;
using System.Diagnostics;
using Nerdbank.Streams;
using PlatynUI.Platform.MacOS.SwiftInterop;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using StreamJsonRpc;

namespace PlatynUI.Platform.MacOS;

internal interface IHighlighterRpcClient
{
    Task<object?> Initialize(int processId);
    Task<object?> Show(double x, double y, double width, double height, double timeout);
    Task<object?> Hide();
    Task Exit();
}

[Export(typeof(IDisplayDevice))]
public class DisplayDevice : IDisplayDevice, IDisposable
{
    private readonly Process _rpcProcess;
    private readonly IHighlighterRpcClient _client;
    private readonly JsonRpc _jsonRpc;
    private bool _disposed;

    [method: ImportingConstructor]
    DisplayDevice()
    {
        string exePath = Path.Combine(
            Path.GetDirectoryName(typeof(DisplayDevice).Assembly.Location)!,
            "runtimes",
            "osx",
            "native",
            "PlatynUI.Platform.MacOS.Highlighter"
        );

        _rpcProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = "--server",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },
        };

        _rpcProcess.Start();

        if (_rpcProcess.HasExited)
        {
            throw new InvalidOperationException(
                $"Failed to start the highlighter process. Exit code: {_rpcProcess.ExitCode}"
            );
        }

        var stdioStream = FullDuplexStream.Splice(
            _rpcProcess.StandardOutput.BaseStream,
            _rpcProcess.StandardInput.BaseStream
        );

        var formatter = new JsonMessageFormatter();

        var messageHandler = new HeaderDelimitedMessageHandler(stdioStream, formatter);
        _jsonRpc = new JsonRpc(messageHandler);
        _jsonRpc.TraceSource.Switch.Level = SourceLevels.All;

        _client = _jsonRpc.Attach<IHighlighterRpcClient>(
            new JsonRpcProxyOptions { ServerRequiresNamedArguments = true }
        );

        _jsonRpc.StartListening();

        _client.Initialize(Environment.ProcessId).GetAwaiter().GetResult();
    }

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _client.Exit();

            if (_rpcProcess != null && !_rpcProcess.HasExited)
            {
                try
                {
                    _rpcProcess.Kill(true);
                }
                catch (Exception) { }
                _rpcProcess.Dispose();
            }
        }

        _disposed = true;
    }

    ~DisplayDevice()
    {
        Dispose(false);
    }

    public Rect GetBoundingRectangle()
    {
        Interop.GetBoundingRectangle(out double x, out double y, out double width, out double height);

        return new Rect(x, y, width, height);
    }

    public void HighlightRect(double x, double y, double width, double height, double time)
    {
        _client.Show(x, y, width, height, time).GetAwaiter().GetResult();
    }
}
