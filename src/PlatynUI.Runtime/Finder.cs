// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using System.Xml.XPath;
using PlatynUI.Runtime.Core;
using XPathNavigator = PlatynUI.Runtime.Core.XPathNavigator;

namespace PlatynUI.Runtime;

public static class Finder
{
    static readonly XsltContext xsltContext = new();

    public static object? FindSingleNode(INode? parent, string xpath, bool findVirtual = false, bool refresh = true)
    {
        parent ??= Desktop.GetInstance();

        if (refresh)
        {
            parent.Invalidate();
        }

        var navigator = new XPathNavigator(parent, findVirtual, xsltContext.NameTable);
        var expression = XPathExpression.Compile(xpath, xsltContext);

        var node = navigator.SelectSingleNode(expression);

        return node?.UnderlyingObject;
    }

    public static void InvalidateNode(string xpath, bool findVirtual = false)
    {
        var parent = Desktop.GetInstance();

        var navigator = new XPathNavigator(parent, findVirtual, xsltContext.NameTable);
        var expression = XPathExpression.Compile(xpath, xsltContext);

        var node = navigator.SelectSingleNode(expression);

        if (node?.UnderlyingObject is INode element)
        {
            element.Invalidate();
        }
    }

    public static IEnumerable<object?> EnumAllNodes(INode? parent, string xpath, bool findVirtual = false)
    {
        parent ??= Desktop.GetInstance();
        parent.Invalidate();

        var navigator = new XPathNavigator(parent, findVirtual);
        var expression = XPathExpression.Compile(xpath, xsltContext);

        var nodes = navigator.Select(expression);

        foreach (var node in nodes.OfType<System.Xml.XPath.XPathNavigator>())
        {
            if (node?.UnderlyingObject is INode element)
                yield return element;
        }
    }

    public static IList<object?> FindNodes(INode? parent, string xpath, bool findVirtual = false)
    {
        return [.. EnumAllNodes(parent, xpath, findVirtual)];
    }

    public static IEnumerable<object?> Evaluate(INode? parent, string xpath, bool findVirtual = false)
    {
        parent ??= Desktop.GetInstance();
        parent.Invalidate();

        var navigator = new XPathNavigator(parent, findVirtual);
        var expression = XPathExpression.Compile(xpath, xsltContext);

        var nodes = navigator.Evaluate(expression);

        if (nodes is XPathNodeIterator iterator)
        {
            while (iterator.MoveNext())
            {
                if (iterator.Current is System.Xml.XPath.XPathNavigator node)
                {
                    if (node?.UnderlyingObject is INode n)
                    {
                        yield return n;
                    }
                    else
                    {
                        yield return node?.TypedValue;
                    }
                }
            }
        }
        else
        {
            yield return nodes;
        }
    }
}
