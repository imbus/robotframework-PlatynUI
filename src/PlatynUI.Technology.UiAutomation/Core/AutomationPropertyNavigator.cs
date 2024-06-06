namespace PlatynUI.Ui.Technology.UIAutomation.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using PlatynUI.Technology.UiAutomation.Client;

internal class AutomationPropertyNavigator
    : ChildrenNavigatorBase<AutomationElementNavigator, Automation.PropertyIdAndName>
{
    private IReadOnlyList<Automation.PropertyIdAndName>? _children;

    public AutomationPropertyNavigator(AutomationElementNavigator? parent)
        : base(parent)
    {
        ArgumentNullException.ThrowIfNull(parent, nameof(parent));

        Element = parent.Current;
    }

    public IUIAutomationElement? Element { get; }

    protected override IReadOnlyList<Automation.PropertyIdAndName> Children => _children ??= GetChildren();

    private IReadOnlyList<Automation.PropertyIdAndName> GetChildren()
    {
        return Element != null ? Automation.GetSupportedPropertyIdAndNames(Element).ToList() : [];
    }

    public override ChildrenNavigatorBase<AutomationElementNavigator, Automation.PropertyIdAndName> Clone()
    {
        return new AutomationPropertyNavigator(Parent)
        {
            _children = _children,
            CurrentIndex = CurrentIndex,
            IsStarted = IsStarted
        };
    }
}
