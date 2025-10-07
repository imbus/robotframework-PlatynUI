// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using System.IO.Pipes;
using PlatynUI.Provider.Core;
using PlatynUI.Runtime.Core;
using StreamJsonRpc;

namespace PlatynUI.Extension.Provider.Client;

public class NodeInfoProxy(INodeInfoAsync nodeInfoAsync)
{
    public INodeInfoAsync NodeInfoAsync { get; } = nodeInfoAsync;

    public bool IsValid(ElementReference reference)
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => NodeInfoAsync.IsValidAsync(reference));
    }

    public Task<bool> IsValidAsync(ElementReference reference)
    {
        return NodeInfoAsync.IsValidAsync(reference);
    }

    public string GetLocalName(ElementReference reference)
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => NodeInfoAsync.GetLocalNameAsync(reference));
    }

    public Task<string> GetLocalNameAsync(ElementReference reference)
    {
        return NodeInfoAsync.GetLocalNameAsync(reference);
    }

    public NodeType GetNodeType(ElementReference reference)
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => NodeInfoAsync.GetNodeTypeAsync(reference));
    }

    public Task<NodeType> GetNodeTypeAsync(ElementReference reference)
    {
        return NodeInfoAsync.GetNodeTypeAsync(reference);
    }

    public IList<ElementReference> GetChildren(ElementReference parentReference)
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => NodeInfoAsync.GetChildrenAsync(parentReference));
    }

    public Task<IList<ElementReference>> GetChildrenAsync(ElementReference parentReference)
    {
        return NodeInfoAsync.GetChildrenAsync(parentReference);
    }

    public string[] GetAttributeNames(ElementReference reference)
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => NodeInfoAsync.GetAttributeNamesAsync(reference));
    }

    public Task<string[]> GetAttributeNamesAsync(ElementReference reference)
    {
        return NodeInfoAsync.GetAttributeNamesAsync(reference);
    }

    public object? GetAttributeValue(ElementReference reference, string attributeName)
    {
        return ThreadHelper.JoinableTaskFactory.Run(
            () => NodeInfoAsync.GetAttributeValueAsync(reference, attributeName)
        );
    }

    public Task<object?> GetAttributeValueAsync(ElementReference reference, string attributeName)
    {
        return NodeInfoAsync.GetAttributeValueAsync(reference, attributeName);
    }

    public string GetAttributeType(ElementReference reference, string attributeName)
    {
        return ThreadHelper.JoinableTaskFactory.Run(
            () => NodeInfoAsync.GetAttributeTypeAsync(reference, attributeName)
        );
    }

    public Task<string> GetAttributeTypeAsync(ElementReference reference, string attributeName)
    {
        return NodeInfoAsync.GetAttributeTypeAsync(reference, attributeName);
    }
}

public class ApplicationInfoProxy(IApplicationInfoAsync applicationInfoAsync)
{
    public IApplicationInfoAsync ApplicationInfoAsync { get; } = applicationInfoAsync;

    public ElementReference? GetRoot()
    {
        return ThreadHelper.JoinableTaskFactory.Run(() => ApplicationInfoAsync.GetRootAsync());
    }

    public Task<ElementReference?> GetRootAsync()
    {
        return ApplicationInfoAsync.GetRootAsync();
    }
}

public class ProcessProvider(Process process, string pipeName, INode? parent) : IDisposable
{
    ~ProcessProvider()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _disposed = false;

    public void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Stream?.Dispose();
            JsonRpc?.Dispose();
        }

        _disposed = true;
        Stream = null;
        JsonRpc = null;
    }

    public Process Process { get; } = process;
    public INode? Parent { get; } = parent;

    NamedPipeClientStream? Stream = null;
    JsonRpc? JsonRpc = null;

    private ApplicationInfoProxy? _applicationInfo = null;
    public ApplicationInfoProxy ApplicationInfo =>
        _applicationInfo ?? throw new InvalidOperationException("Not connected");

    private NodeInfoProxy? _nodeInfo = null;
    public NodeInfoProxy NodeInfo => _nodeInfo ?? throw new InvalidOperationException("Not connected");

    public string Technology = "";

    public string PipeName => pipeName;

    public async Task ConnectAsync()
    {
        if (Stream != null)
        {
            return;
        }

        Stream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
        try
        {
            Debug.WriteLine($"Connecting to process {Process.Id} with name {Process.ProcessName} and {PipeName}");
            await Stream.ConnectAsync(2000);
        }
        catch
        {
            Debug.WriteLine(
                $"Failed to connect to process {Process.Id} with name {Process.ProcessName} and {PipeName}"
            );
            Stream = null;
            throw;
        }

        try
        {
            JsonRpc = new JsonRpc(Stream)
            {
                TraceSource = new TraceSource("PlatynUI.Extension.Provider.Client", SourceLevels.All),
            };
            _applicationInfo = new ApplicationInfoProxy(JsonRpc.Attach<IApplicationInfoAsync>());
            _nodeInfo = new NodeInfoProxy(JsonRpc.Attach<INodeInfoAsync>());
            JsonRpc.JoinableTaskFactory = ThreadHelper.JoinableTaskFactory;
            JsonRpc.StartListening();
        }
        catch (Exception e)
        {
            Stream = null;
            JsonRpc = null;
            _applicationInfo = null;
            _nodeInfo = null;

            Debug.WriteLine(
                $"Failed to attach to process {Process.Id} with name {Process.ProcessName} and {PipeName}: {e}"
            );
            throw;
        }
        Debug.WriteLine($"Connected to process {Process.Id} with name {Process.ProcessName}");
    }

    INode? _rootNode = null;

    public INode? GetRootNode()
    {
        _rootNode?.Invalidate();

        if (ApplicationInfo.GetRoot() is ElementReference root)
        {
            _rootNode = new ApplicationNode(Parent, root, this);
        }

        return _rootNode;
    }
}
