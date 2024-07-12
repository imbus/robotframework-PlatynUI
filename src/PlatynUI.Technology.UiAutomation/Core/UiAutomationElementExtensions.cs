﻿using System.Diagnostics;
using System.Windows;
using PlatynUI.Technology.UiAutomation.Client;

namespace PlatynUI.Technology.UiAutomation.Core;

public static class UiAutomationElementExtensions
{
    public static bool TryGetCurrentPattern<T>(this IUIAutomationElement element, out T? pattern)
        where T : class
    {
        return Automation.TryGetCurrentPattern(element, out pattern);
    }

    public static T? GetCurrentPattern<T>(this IUIAutomationElement element)
        where T : class
    {
        return Automation.GetCurrentPattern<T>(element);
    }

    public static bool TryGetCachedPattern<T>(this IUIAutomationElement element, out T? pattern)
        where T : class
    {
        return Automation.TryGetCachedPattern(element, out pattern);
    }

    public static T? GetCachedPattern<T>(this IUIAutomationElement element)
        where T : class
    {
        return Automation.GetCachedPattern<T>(element);
    }

    public static bool SupportsPatternId(this IUIAutomationElement element, int id)
    {
        return Automation.SupportsPatternId(element, id);
    }

    public static bool SupportsPattern<T>(this IUIAutomationElement element)
        where T : class
    {
        return Automation.SupportsPattern<T>(element);
    }

    public static string[] GetSupportedPatternNames(this IUIAutomationElement element)
    {
        return Automation.GetSupportedPatternNames(element);
    }

    public static int[] GetSupportedPatternIds(this IUIAutomationElement element)
    {
        return Automation.GetSupportedPatternIds(element);
    }

    public static int[] GetSupportedPropertyIds(this IUIAutomationElement element)
    {
        return Automation.GetSupportedPropertyIds(element);
    }

    public static string[] GetSupportedPropertyNames(this IUIAutomationElement element)
    {
        return Automation.GetSupportedPropertyNames(element);
    }

    public static bool IsVirtualized(this IUIAutomationElement element)
    {
        return element.SupportsPatternId(UIA_PatternIds.UIA_VirtualizedItemPatternId);
    }

    public static bool IsItemContainer(this IUIAutomationElement element)
    {
        return element.SupportsPatternId(UIA_PatternIds.UIA_ItemContainerPatternId);
    }

    public static IUIAutomationElement Realize(this IUIAutomationElement element)
    {
        if (element.TryGetCurrentPattern(out IUIAutomationVirtualizedItemPattern? pattern))
        {
            pattern?.Realize();
        }

        return element;
    }

    public static IUIAutomationElement? GetCurrentParent(this IUIAutomationElement element)
    {
        var result = Automation.RawViewWalker.GetParentElement(element);

        if (result != null && Automation.CompareElements(result, Automation.RootElement))
        {
            return null;
        }

        return result;
    }

    public static Rect ToRect(this tagRECT rect)
    {
        return new Rect(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
    }

#pragma warning disable CA1837 // Use 'Environment.ProcessId'
    public static int CurrentProcessId => Process.GetCurrentProcess().Id;
#pragma warning restore CA1837 // Use 'Environment.ProcessId'

    private static IUIAutomationElement? ItemContainerFindItemByProperty(
        IUIAutomationItemContainerPattern pattern,
        IUIAutomationElement? pStartAfter
    )
    {
        try
        {
            return pattern.FindItemByProperty(pStartAfter, 0, null);
        }
        catch
        {
            return null;
        }
    }

    public static IEnumerable<IUIAutomationElement> EnumerateChildren(
        this IUIAutomationElement? element,
        IUIAutomationTreeWalker? walker,
        bool findVirtual = false
    )
    {
        if (element == null || walker == null)
        {
            yield break;
        }

        var r = walker.GetFirstChildElement(element);
        if (r == null)
        {
            yield break;
        }

        r.Realize();
        if (r.CurrentProcessId == CurrentProcessId)
        {
            Debug.WriteLine("Skipping element from the same process");
        }
        else
        {
            yield return r;
        }

        while (r != null)
        {
            r = walker.GetNextSiblingElement(r);
            if (r != null)
            {
                r.Realize();
                if (r.CurrentProcessId == CurrentProcessId)
                {
                    Debug.WriteLine("Skipping element from the same process");
                    continue;
                }
                yield return r;
            }
        }

        if (findVirtual && element.SupportsPatternId(UIA_PatternIds.UIA_ItemContainerPatternId))
        {
            var pattern = element.GetCurrentPattern<IUIAutomationItemContainerPattern>();
            if (pattern == null)
            {
                yield break;
            }

            r = ItemContainerFindItemByProperty(pattern, null);
            if (r == null)
            {
                yield break;
            }

            r.Realize();
            if (r.CurrentProcessId == CurrentProcessId)
            {
                Debug.WriteLine("Skipping element from the same process");
            }
            else
            {
                yield return r;
            }

            while (r != null)
            {
                r = ItemContainerFindItemByProperty(pattern, r);
                if (r != null)
                {
                    if (r.CurrentProcessId == CurrentProcessId)
                    {
                        Debug.WriteLine("Skipping element from the same process");
                        continue;
                    }
                    r.Realize();
                    yield return r;
                }
            }
        }
    }
}
