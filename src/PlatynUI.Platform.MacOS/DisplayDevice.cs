// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Threading;
using PlatynUI.JsonRpc;
using PlatynUI.Platform.MacOS.SwiftInterop;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;

namespace PlatynUI.Platform.MacOS;

[JsonRpcEndpoint]
internal partial interface IHighlighterRpcClient
{
    [JsonRpcRequest("initialize")]
    void Initialize(int processId);

    [JsonRpcRequest("show")]
    void Show(double x, double y, double width, double height, double timeout);

    [JsonRpcRequest("hide")]
    void Hide();

    [JsonRpcNotification("exit")]
    void Exit();
}

[Export(typeof(IDisplayDevice))]
public class DisplayDevice : IDisplayDevice, IDisposable
{
    private Process? _rpcProcess;
    private IHighlighterRpcClient? _client;
    private JsonRpcPeer? _jsonRpcPeer;
    private bool _disposed;

    [method: ImportingConstructor]
    DisplayDevice() { }

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
            if (_client != null)
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
        }

        _disposed = true;
    }

    ~DisplayDevice()
    {
        Dispose(false);
    }

    IHighlighterRpcClient Client
    {
        get
        {
            if (_client == null)
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

                _jsonRpcPeer = new JsonRpcPeer(
                    _rpcProcess.StandardOutput.BaseStream,
                    _rpcProcess.StandardInput.BaseStream
                )
                {
                    JoinableTaskFactory = new JoinableTaskFactory(new JoinableTaskContext()),
                };

                _client = IHighlighterRpcClient.Attach(_jsonRpcPeer);

                _jsonRpcPeer.Start();

                // Run the initialization in a separate task to avoid blocking the main thread
                _client.Initialize(Environment.ProcessId);
            }

            return _client;
        }
    }

    public Rect GetBoundingRectangle()
    {
        Interop.GetBoundingRectangle(out double x, out double y, out double width, out double height);

        return new Rect(x, y, width, height);
    }

    public void HighlightRect(double x, double y, double width, double height, double time)
    {
        Client.Show(x, y, width, height, time);
    }
}
