// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel.Composition;
using PlatynUI.Runtime.Core;
using Attribute = PlatynUI.Runtime.Core.Attribute;

namespace PlatynUI.Runtime;

public class Desktop : INode, IAdapter, IElement
{
    private Desktop()
    {
        PlatynUiExtensions.ComposeParts(this);
    }

    [ImportMany]
    IEnumerable<INodeProvider>? Providers { get; set; }

    public INode? Parent => null;

    private IList<INode>? _children;
    public IList<INode> Children => _children ??= GetChildren();

    protected IList<INode> GetChildren()
    {
        var children = new List<INode>();

        if (Providers == null)
        {
            return children;
        }

        foreach (var item in Providers)
        {
            children.AddRange(item.GetNodes(this));
        }
        return children;
    }

    public string LocalName => "Desktop";

    public string NamespaceURI => Namespaces.Raw;

    Dictionary<string, IAttribute>? _attributes;
    public IDictionary<string, IAttribute> Attributes =>
        _attributes ??= new List<IAttribute>()
        {
            new Attribute("Name", Environment.MachineName),
            new Attribute("Platform", PlatformHelper.GetCurrentPlatform().ToString()),
            new Attribute("Version", Environment.OSVersion.VersionString),
            new Attribute("Role", "Desktop"),
            new Attribute("BoundingRectangle", DisplayDevice.GetBoundingRectangle()),
        }
            .OrderBy(x => x.Name)
            .ToDictionary(x => x.Name);

    public string Id => "Desktop";

    public string Name => "Desktop";

    public string Role => "Desktop";

    public string ClassName => "Desktop";

    public string[] SupportedRoles => ["Desktop", "Element"];

    public string Type => "element";

    public string[] SupportedTypes => ["element"];

    public string FrameworkId => throw new NotImplementedException();

    public string RuntimeId => throw new NotImplementedException();

    public bool IsEnabled => true;

    public bool IsVisible => true;

    public bool IsInView => true;

    public bool TopLevelParentIsActive => throw new NotImplementedException();

    public Rect BoundingRectangle => DisplayDevice.GetBoundingRectangle();

    public Rect VisibleRectangle => DisplayDevice.GetBoundingRectangle();

    public Point? DefaultClickPosition => null;

    private static Desktop? _instance;

    public static Desktop GetInstance() => _instance ??= new Desktop();

    public INode Clone()
    {
        return new Desktop();
    }

    public bool IsSamePosition(INode other)
    {
        return this == other;
    }

    public void Invalidate()
    {
        _children = null;
    }

    public bool IsValid()
    {
        return true;
    }

    public bool TryEnsureVisible()
    {
        return true;
    }

    public bool TryEnsureApplicationIsReady()
    {
        return true;
    }

    public bool TryEnsureToplevelParentIsActive()
    {
        return true;
    }

    public bool TryBringIntoView()
    {
        return true;
    }
}
