// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using PlatynUI.Extension.Win32.UiAutomation.Client;
using PlatynUI.Extension.Win32.UiAutomation.Core;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using IAdapter = PlatynUI.Runtime.Core.IAdapter;
using IAttribute = PlatynUI.Runtime.Core.IAttribute;
using INode = PlatynUI.Runtime.Core.INode;

namespace PlatynUI.Extension.Win32.UiAutomation;

public partial class ElementNode(INode? parent, IUIAutomationElement element) : INode, IAdapter, IElement
{
    public INode? Parent { get; } = parent;
    public IUIAutomationElement Element { get; } = element;

    List<INode>? _children = null;
    public IList<INode> Children => _children ??= GetChildren();

    private List<INode> GetChildren()
    {
        var result = new List<INode>();
        foreach (var e in Element.EnumerateChildren(Automation.RawViewWalker, true))
        {
            result.Add(new ElementNode(this, e));
        }
        return result;
    }

    public string LocalName => Element.GetCurrentControlTypeName();

    public string NamespaceURI => Namespaces.Raw;

    Dictionary<string, IAttribute>? _attributes;
    public IDictionary<string, IAttribute> Attributes => _attributes ??= GetAttributes();

    public bool IsEnabled => Element.CurrentIsEnabled != 0;

    public bool IsVisible => Element.CurrentIsOffscreen == 0;

    public bool IsInView => IsVisible;

    public bool TopLevelParentIsActive => throw new NotImplementedException();

    public Rect BoundingRectangle => Element.CurrentBoundingRectangle.ToRect();

    public Rect VisibleRectangle => BoundingRectangle;

    public Point? DefaultClickPosition
    {
        get
        {
            // if (Element.GetClickablePoint(out var point) != 0)
            // {   Console.WriteLine($"Element.GetClickablePoint {point.ToPoint()}");
            //     return point.ToPoint();
            // }
            var r = BoundingRectangle;
            return new Point(r.X + r.Width / 2, r.Y + r.Height / 2);
        }
    }

    public string Id => Element.CurrentAutomationId;

    public string Name => Element.CurrentName;

    public string Role => Element.GetCurrentControlTypeName();

    public string ClassName => Element.CurrentClassName;

    public string[] SupportedRoles => [Role, "Control", "Element"]; // TODO: use UIAutomation to get the roles

    public string Type => "element"; // TODO: use UIAutomation to get the type

    public string[] SupportedTypes => ["element"]; // TODO: use UIAutomation to get the type

    public string FrameworkId => Element.CurrentFrameworkId;

    public string RuntimeId => throw new NotImplementedException();

    static readonly object InvalidValue = new();

    private Dictionary<string, IAttribute> GetAttributes()
    {
        var result = new Dictionary<string, IAttribute>();
        Automation.PropertyIdAndName[] props =
        [
            new Automation.PropertyIdAndName(-1, "Role"),
            new Automation.PropertyIdAndName(-2, "ProcessName"),
            new Automation.PropertyIdAndName(-3, "Technology"),
            new Automation.PropertyIdAndName(-4, "IsTopLevel"),
            new Automation.PropertyIdAndName(-5, "DefaultClickPosition"),
            .. Automation.GetSupportedPropertyIdAndNames(Element),
        ];

        result = props
            .Select(x => new Runtime.Core.Attribute(x.Name, () => GetPropertyValue(x)) as IAttribute)
            .Where(x => x.Value != InvalidValue)
            .OrderBy(x => x.Name)
            .ToDictionary(x => x.Name);

        return result;
    }

    private object? GetPropertyValue(Automation.PropertyIdAndName attribute)
    {
        try
        {
            switch (attribute.Id)
            {
                case -1:
                    if (attribute.Name == "Role")
                        return Role;

                    return null;
                case -2:
                    if (attribute.Name == "ProcessName")
                    {
                        return System.Diagnostics.Process.GetProcessById(Element.CurrentProcessId).ProcessName;
                    }

                    return null;
                case -3:
                    if (attribute.Name == "Technology")
                    {
                        return "UIAutomation";
                    }

                    return null;
                case -4:
                    if (attribute.Name == "IsTopLevel")
                    {
                        if (Element.CurrentNativeWindowHandle == IntPtr.Zero)
                        {
                            return false;
                        }

                        return (HWND)Element.CurrentNativeWindowHandle
                            == PInvoke.GetAncestor((HWND)Element.CurrentNativeWindowHandle, GET_ANCESTOR_FLAGS.GA_ROOT);
                    }
                    return null;
                case -5:
                    if (attribute.Name == "DefaultClickPosition")
                    {
                        return DefaultClickPosition;
                    }

                    return null;
            }

            var value = Element.GetCurrentPropertyValueEx(attribute.Id, 1);
            if (Automation.UiAutomation.CheckNotSupported(value) != 0)
            {
                return InvalidValue;
            }

            if (
                attribute.Id == UIA_PropertyIds.UIA_BoundingRectanglePropertyId
                && value is double[] r and { Length: 4 }
            )
            {
                return new Rect(r[0], r[1], r[2], r[3]);
            }
            else if (
                (
                    attribute.Id == UIA_PropertyIds.UIA_ClickablePointPropertyId
                    || attribute.Id == UIA_PropertyIds.UIA_CenterPointPropertyId
                ) && value is double[] p and { Length: 2 }
            )
            {
                return new Point(p[0], p[1]);
            }

            return value;
        }
        catch (Exception e)
        {
            return $"<Error>: {e.Message}";
        }
    }

    public INode Clone()
    {
        return new ElementNode(Parent, Element);
    }

    public bool IsSamePosition(INode other)
    {
        return other is ElementNode node && Automation.CompareElements(Element, node.Element);
    }

    public void Invalidate()
    {
        _children = null;
    }

    public bool IsValid()
    {
        try
        {
            return Element.CurrentProcessId != 0;
        }
        catch
        {
            return false;
        }
    }

    public bool TryEnsureVisible()
    {
        return Element.CurrentIsOffscreen == 0;
    }

    public bool TryEnsureApplicationIsReady()
    {
        try
        {
            return System.Diagnostics.Process.GetProcessById(Element.CurrentProcessId).WaitForInputIdle(5);
        }
        catch
        {
            return true;
        }
    }

    public bool TryEnsureToplevelParentIsActive()
    {
        return Helper.TryEnsureTopLevelParentIsActive(Element);
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
            _ => null,
        };
    }

    public bool has_focus => Element.CurrentHasKeyboardFocus != 0;

    public bool try_ensure_focused()
    {
        if (has_focus)
        {
            return true;
        }

        Element.SetFocus();

        return has_focus;
    }
}
