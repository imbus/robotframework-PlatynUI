// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel.Composition;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using Tmds.DBus.Protocol;
using Tmds.DBus.SourceGenerator;
using Attribute = PlatynUI.Runtime.Core.Attribute;

[assembly: PlatynUiExtension(supportedPlatforms: [RuntimePlatform.Linux])]

namespace PlatynUI.Extension.Linux.AtSpi2;

[Flags]
enum AtspiState : long
{
    ATSPI_STATE_INVALID = 1L << 0,
    ATSPI_STATE_ACTIVE = 1L << 1,
    ATSPI_STATE_ARMED = 1L << 2,
    ATSPI_STATE_BUSY = 1L << 3,
    ATSPI_STATE_CHECKED = 1L << 4,
    ATSPI_STATE_COLLAPSED = 1L << 5,
    ATSPI_STATE_DEFUNCT = 1L << 6,
    ATSPI_STATE_EDITABLE = 1L << 7,
    ATSPI_STATE_ENABLED = 1L << 8,
    ATSPI_STATE_EXPANDABLE = 1L << 9,
    ATSPI_STATE_EXPANDED = 1L << 10,
    ATSPI_STATE_FOCUSABLE = 1L << 11,
    ATSPI_STATE_FOCUSED = 1L << 12,
    ATSPI_STATE_HAS_TOOLTIP = 1L << 13,
    ATSPI_STATE_HORIZONTAL = 1L << 14,
    ATSPI_STATE_ICONIFIED = 1L << 15,
    ATSPI_STATE_MODAL = 1L << 16,
    ATSPI_STATE_MULTI_LINE = 1L << 17,
    ATSPI_STATE_MULTISELECTABLE = 1L << 18,
    ATSPI_STATE_OPAQUE = 1L << 19,
    ATSPI_STATE_PRESSED = 1L << 20,
    ATSPI_STATE_RESIZABLE = 1L << 21,
    ATSPI_STATE_SELECTABLE = 1L << 22,
    ATSPI_STATE_SELECTED = 1L << 23,
    ATSPI_STATE_SENSITIVE = 1L << 24,
    ATSPI_STATE_SHOWING = 1L << 25,
    ATSPI_STATE_SINGLE_LINE = 1L << 26,
    ATSPI_STATE_STALE = 1L << 27,
    ATSPI_STATE_TRANSIENT = 1L << 28,
    ATSPI_STATE_VERTICAL = 1L << 29,
    ATSPI_STATE_VISIBLE = 1L << 30,
    ATSPI_STATE_MANAGES_DESCENDANTS = 1L << 31,
    ATSPI_STATE_INDETERMINATE = 1L << 32,
    ATSPI_STATE_REQUIRED = 1L << 33,
    ATSPI_STATE_TRUNCATED = 1L << 34,
    ATSPI_STATE_ANIMATED = 1L << 35,
    ATSPI_STATE_INVALID_ENTRY = 1L << 36,
    ATSPI_STATE_SUPPORTS_AUTOCOMPLETION = 1L << 37,
    ATSPI_STATE_SELECTABLE_TEXT = 1L << 38,
    ATSPI_STATE_IS_DEFAULT = 1L << 39,
    ATSPI_STATE_VISITED = 1L << 40,
    ATSPI_STATE_CHECKABLE = 1L << 41,
    ATSPI_STATE_HAS_POPUP = 1L << 42,
    ATSPI_STATE_READ_ONLY = 1L << 43,
    ATSPI_STATE_LAST_DEFINED = 1L << 44,
};

struct ElementReference(string service, ObjectPath path)
{
    public string Service = service;
    public ObjectPath Path = path;

    public override readonly string ToString()
    {
        return $"{Service}:{Path}";
    }

    public static bool operator ==(ElementReference left, ElementReference right)
    {
        return left.Service == right.Service && left.Path == right.Path;
    }

    public static bool operator !=(ElementReference left, ElementReference right)
    {
        return !(left.Service == right.Service && left.Path == right.Path);
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is ElementReference reference && this == reference;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Service, Path);
    }
}

class Adapter(Connection connection, INode? parent, ElementReference elementReference) : INode, IAdapter
{
    private List<INode>? _children;
    private IDictionary<string, IAttribute>? _attributes;
    private string[]? _interfaces = null;

    public INode? Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public IList<INode> Children => _children ??= GetChildren();

    private List<INode> GetChildren()
    {
        var result = new List<INode>();

        var children = Element.GetChildrenAsync().GetAwaiter().GetResult();
        foreach (var child in children)
        {
            var acc = new OrgA11yAtspiAccessibleProxy(connection, child.Item1, child.Item2);
            var interfaces = acc.GetInterfacesAsync().GetAwaiter().GetResult();

            if (interfaces.Contains("org.a11y.atspi.Component"))
            {
                result.Add(new ComponentAdapter(Connection, this, new ElementReference(child.Item1, child.Item2)));
                continue;
            }

            if (interfaces.Contains("org.a11y.atspi.Application"))
            {
                var childCount = acc.GetChildCountPropertyAsync().GetAwaiter().GetResult();
                if (childCount == 0)
                {
                    continue;
                }
                result.Add(new ApplicationAdapter(Connection, this, new ElementReference(child.Item1, child.Item2)));

                continue;
            }

            result.Add(new Adapter(Connection, this, new ElementReference(child.Item1, child.Item2)));
        }
        return result;
    }

    static string ConvertToCamelCase(string input)
    {
        string[] words = input.Trim().Split(' ', '\t');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == 0)
            {
                continue;
            }
            words[i] = char.ToUpper(words[i][0]) + words[i][1..];
        }
        return string.Join("", words);
    }

    public string LocalName
    {
        get
        {
            try
            {
                return ConvertToCamelCase(Element.GetRoleNameAsync().GetAwaiter().GetResult());
            }
            catch (Exception) { }
            return "Unknown";
        }
    }

    public virtual string NamespaceURI => Namespaces.Raw;

    public IDictionary<string, IAttribute> Attributes => _attributes ??= GetAttributes();

    private Dictionary<string, string> GetAttributesDictionary()
    {
        try
        {
            return Element.GetAttributesAsync().GetAwaiter().GetResult();
        }
        catch (Exception) { }
        return [];
    }

    protected AtspiState GetState()
    {
        try
        {
            var state = Element.GetStateAsync().GetAwaiter().GetResult();
            ulong ustate = state[0];
            ustate += (ulong)state[1] << 32;
            return (AtspiState)ustate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return AtspiState.ATSPI_STATE_INVALID;
    }

    protected virtual List<IAttribute> GetAttributesList()
    {
        return
        [
            .. GetAttributesDictionary().Select(x => new Attribute("Native." + x.Key, () => x.Value)),
            new Attribute("Name", () => Element.GetNamePropertyAsync().GetAwaiter().GetResult()),
            new Attribute("Description", () => Element.GetDescriptionPropertyAsync().GetAwaiter().GetResult()),
            new Attribute("Role", () => LocalName),
            new Attribute("RoleId", () => Element.GetRoleAsync().GetAwaiter().GetResult()),
            new Attribute("LocalizedRoleName", () => Element.GetLocalizedRoleNameAsync().GetAwaiter().GetResult()),
            new Attribute("AccessibleId", () => Element.GetAccessibleIdPropertyAsync().GetAwaiter().GetResult()),
            new Attribute("Locale", () => Element.GetLocalePropertyAsync().GetAwaiter().GetResult()),
            //new Attribute("HelpText", () => Element.GetHelpTextPropertyAsync().GetAwaiter().GetResult().ToString()),
            new Attribute("State", () => GetState()),
            new Attribute("GetRelationSetAsync", () => Element.GetRelationSetAsync().GetAwaiter().GetResult()),
            new Attribute("Interfaces", () => Interfaces),
        ];
    }

    private Dictionary<string, IAttribute> GetAttributes()
    {
        return GetAttributesList().OrderBy(x => x.Name).ToDictionary(x => x.Name);
    }

    string[] Interfaces => _interfaces ??= Element.GetInterfacesAsync().GetAwaiter().GetResult();

    public Connection Connection => connection;
    public ElementReference ElementReference { get; } = elementReference;
    public OrgA11yAtspiAccessibleProxy Element { get; } =
        new(connection, elementReference.Service, elementReference.Path);

    public string Id => "";

    public string Name => Element.GetNamePropertyAsync().GetAwaiter().GetResult();

    public string Role => LocalName;

    public string ClassName => "";

    public string[] SupportedRoles => ["Adapter"];

    public string Type => "element";

    public string[] SupportedTypes => ["element"];

    public string FrameworkId => "AtSpi2";

    public string RuntimeId => ElementReference.ToString();

    public INode Clone()
    {
        return new Adapter(Connection, Parent, ElementReference);
    }

    public bool IsSamePosition(INode other)
    {
        if (other is Adapter adapter)
        {
            return ElementReference == adapter.ElementReference;
        }

        return false;
    }

    public void Invalidate()
    {
        _children = null;
        _attributes = null;
    }

    public bool IsValid()
    {
        return GetState() != AtspiState.ATSPI_STATE_INVALID;
    }
}

class ApplicationAdapter(Connection connection, INode? parent, ElementReference elementReference)
    : Adapter(connection, parent, elementReference)
{
    OrgA11yAtspiApplicationProxy Application { get; } =
        new(connection, elementReference.Service, elementReference.Path);

    public override string NamespaceURI => Namespaces.App;

    protected override List<IAttribute> GetAttributesList()
    {
        return
        [
            .. base.GetAttributesList(),
            new Attribute("AtspiVersion", () => Application.GetAtspiVersionPropertyAsync().GetAwaiter().GetResult()),
            new Attribute("Id", () => Application.GetIdPropertyAsync().GetAwaiter().GetResult()),
            new Attribute("Version", () => Application.GetVersionPropertyAsync().GetAwaiter().GetResult()),
            new Attribute("ToolkitName", () => Application.GetToolkitNamePropertyAsync().GetAwaiter().GetResult()),
        ];
    }
}

class ComponentAdapter(Connection connection, INode? parent, ElementReference elementReference)
    : Adapter(connection, parent, elementReference),
        IElement
{
    OrgA11yAtspiComponentProxy Component { get; } = new(connection, elementReference.Service, elementReference.Path);

    public bool IsEnabled => GetState().HasFlag(AtspiState.ATSPI_STATE_ENABLED);

    public bool IsVisible => GetState().HasFlag(AtspiState.ATSPI_STATE_VISIBLE);

    public bool IsInView => GetState().HasFlag(AtspiState.ATSPI_STATE_SHOWING);

    public bool TopLevelParentIsActive => true; // TODO

    public Rect BoundingRectangle
    {
        get
        {
            var (X, Y) = Component.GetPositionAsync(0).GetAwaiter().GetResult();
            var (Width, Height) = Component.GetSizeAsync().GetAwaiter().GetResult();

            return new Rect(X, Y, Width, Height);
        }
    }

    public Rect VisibleRectangle => BoundingRectangle;

    public Point? DefaultClickPosition
    {
        get
        {
            var b = BoundingRectangle;
            return new Point(b.X + b.Width / 2, b.Y + b.Height / 2);
        }
    }

    protected override List<IAttribute> GetAttributesList()
    {
        return
        [
            .. base.GetAttributesList(),
            new Attribute("IsEnabled", () => IsEnabled),
            new Attribute("IsVisible", () => IsVisible),
            new Attribute("IsInView", () => IsInView),
            new Attribute("TopLevelParentIsActive", () => TopLevelParentIsActive),
            new Attribute("BoundingRectangle", () => BoundingRectangle),
            new Attribute("VisibleRectangle", () => VisibleRectangle),
            new Attribute("DefaultClickPosition", () => DefaultClickPosition),
            new Attribute("Extents.Root", () => Component.GetExtentsAsync(0).GetAwaiter().GetResult()),
            new Attribute("Extents.TopLevel", () => Component.GetExtentsAsync(1).GetAwaiter().GetResult()),
            new Attribute("Extents.Parent", () => Component.GetExtentsAsync(2).GetAwaiter().GetResult()),
        ];
    }

    public bool TryEnsureVisible()
    {
        //TODO: throw new NotImplementedException();
        return true;
    }

    public bool TryEnsureApplicationIsReady()
    {
        //TODO: throw new NotImplementedException();
        return true;
    }

    public bool TryEnsureToplevelParentIsActive()
    {
        //TODO: throw new NotImplementedException();
        return true;
    }

    public bool TryBringIntoView()
    {
        return true;
    }

    public virtual object? GetStrategy(string name, bool throwException = true)
    {
        return name switch
        {
            "org.platynui.strategies.Control" => this,
            "org.platynui.strategies.Component" => this,
            "org.platynui.strategies.Text" => this,
            _ => null,
        };
    }

    public bool has_focus =>
        GetState().HasFlag(AtspiState.ATSPI_STATE_FOCUSABLE) && GetState().HasFlag(AtspiState.ATSPI_STATE_FOCUSED);

    public bool try_ensure_focused()
    {
        if (has_focus)
        {
            return true;
        }

        Component.GrabFocusAsync().GetAwaiter().GetResult();

        return has_focus;
    }

    public string text
    {
        get
        {
            var text = new OrgA11yAtspiTextProxy(Connection, ElementReference.Service, ElementReference.Path);
            var l = text.GetCharacterCountPropertyAsync().GetAwaiter().GetResult();
            return text.GetTextAsync(0, l).GetAwaiter().GetResult();
        }
    }
}

[Export(typeof(INodeProvider))]
class NodeProvider : INodeProvider
{
    private Connection? _connection;
    private Adapter? _root;

    Connection Connection => _connection ??= CreateConnection();

    private static Connection CreateConnection()
    {
        var bus = new OrgA11yBusProxy(Connection.Session, "org.a11y.Bus", "/org/a11y/bus");
        var address = bus.GetAddressAsync().GetAwaiter().GetResult();

        return new Connection(new ClientConnectionOptions(address) { AutoConnect = true });
    }

    public IEnumerable<INode> GetNodes(INode parent)
    {
        Root.Invalidate();
        Root.Parent = parent;
        foreach (var e in Root.Children)
        {
            if (e is Adapter a)
                a.Parent = parent;
            yield return e;
        }
    }

    public Adapter Root =>
        _root ??= new Adapter(
            Connection,
            null,
            new ElementReference("org.a11y.atspi.Registry", "/org/a11y/atspi/accessible/root")
        );
}
