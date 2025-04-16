// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using Avalonia.Metadata;
using Avalonia.Threading;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using ReactiveUI;

namespace PlatynUI.Spy.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyDataErrorInfo
{
    public MainWindowViewModel()
    {
        this.WhenAnyValue(x => x.SelectedNode)
            .Subscribe(node =>
            {
                SelectedNodeAttributes = node?.Attributes ?? [];

                if (node != null && node.Node is IElement element && element.IsVisible && element.IsInView)
                {
                    var r = element.BoundingRectangle;
                    DisplayDevice.HighlightRect(r.X, r.Y, r.Width, r.Height, 2);
                }
            });
        this.WhenAnyValue(x => x.SearchText)
            .Subscribe(text =>
            {
                RemoveErrors(nameof(SearchText));

                if (string.IsNullOrWhiteSpace(text))
                    return;

                try
                {
                    XPathExpression.Compile(text);
                }
                catch (XPathException ex)
                {
                    SetError(nameof(SearchText), ex.Message);
                }
            });
        this.WhenAnyValue(x => x.ResultsSelectedNode)
            .Subscribe(node =>
            {
                if (node != null)
                {
                    var treeNode = FindNodeInTree(node.Node);
                    if (treeNode != null)
                    {
                        foreach (var ancestor in treeNode.GetAncestors())
                        {
                            ancestor.IsExpanded = true;
                        }

                        SelectedNode = null;
                        treeNode.IsSelected = true;
                    }
                }
            });
    }

    public void Refresh()
    {
        SelectedNode?.Refresh();
    }

    [DependsOn(nameof(SelectedNode))]
    public bool CanRefresh()
    {
        return SelectedNode != null;
    }

    public void Highlight()
    {
        var node = SelectedNode;

        if (node == null)
            return;

        SelectedNodeAttributes = node?.Attributes ?? [];

        if (node != null && node.Node is IElement element && element.IsVisible && element.IsInView)
        {
            var r = element.BoundingRectangle;
            DisplayDevice.HighlightRect(r.X, r.Y, r.Width, r.Height, 2);
        }
    }

    [DependsOn(nameof(SelectedNode))]
    public bool CanHighlight()
    {
        return SelectedNode != null;
    }

    public TreeNode[] Root { get; } =
        [new TreeNode(null, Desktop.GetInstance()) { IsExpanded = true, IsSelected = true }];

    private TreeNode? _selectedNode;
    public TreeNode? SelectedNode
    {
        get => _selectedNode;
        set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
    }

    ObservableCollection<TreeNodeAttribute> _selectedNodeAttributes = [];
    public ObservableCollection<TreeNodeAttribute> SelectedNodeAttributes
    {
        get => _selectedNodeAttributes;
        set => this.RaiseAndSetIfChanged(ref _selectedNodeAttributes, value);
    }

    private TreeNode? _resultsSelectedNode;
    public TreeNode? ResultsSelectedNode
    {
        get => _resultsSelectedNode;
        set => this.RaiseAndSetIfChanged(ref _resultsSelectedNode, value);
    }

    public void ClearSearch()
    {
        SearchText = "";
    }

    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public bool CancelSearch()
    {
        if (_searchCancellationTokenSource != null)
        {
            _searchCancellationTokenSource.Cancel();
            _searchCancellationTokenSource = null;
            InSearch = false;
            return true;
        }
        else
        {
            Debug.WriteLine("No search in progress");
        }
        return false;
    }

    private bool _inSearch;
    public bool InSearch
    {
        get => _inSearch;
        set => this.RaiseAndSetIfChanged(ref _inSearch, value);
    }

    private TreeNode? FindNodeInTree(INode node)
    {
        var parents = node.GetAncestors();

        var currentTreeNode = Root[0];

        if (parents.Count > 0 && parents[0].IsSamePosition(Root[0].Node))
        {
            parents.RemoveAt(0);
        }
        foreach (var parentNode in parents)
        {
            var foundTreeNode = currentTreeNode.FindNode(parentNode);

            if (foundTreeNode == null)
            {
                break;
            }

            currentTreeNode = foundTreeNode;
        }

        if (currentTreeNode != null)
        {
            return currentTreeNode!.FindNode(node);
        }

        return currentTreeNode;
    }

    CancellationTokenSource? _searchCancellationTokenSource;

    public async Task SearchAsync()
    {
        CancelSearch();

        SearchResults.Clear();
        if (string.IsNullOrEmpty(SearchText))
            return;

        _searchCancellationTokenSource = new();
        var token = _searchCancellationTokenSource.Token;
        InSearch = true;

        LastError = "";
        try
        {
            await Task.Run(
                () =>
                {
                    var first = true;

                    foreach (var node in Finder.EnumAllNodes(Desktop.GetInstance(), SearchText).Cast<INode>())
                    {
                        token.ThrowIfCancellationRequested();

                        var treeNode = Root.FirstOrDefault(n => n.Node.IsSamePosition(node)) ?? FindNodeInTree(node);
                        //var treeNode = new TreeNode(null, node);

                        if (treeNode != null)
                        {
                            Dispatcher.UIThread.Invoke(
                                () =>
                                {
                                    token.ThrowIfCancellationRequested();
                                    SearchResults.Add(treeNode);
                                },
                                DispatcherPriority.Normal,
                                token
                            );

                            if (first)
                            {
                                first = false;
                                Dispatcher.UIThread.Invoke(
                                    () =>
                                    {
                                        token.ThrowIfCancellationRequested();
                                        ResultsSelectedNode = treeNode;
                                    },
                                    DispatcherPriority.Normal,
                                    token
                                );
                            }
                        }
                    }
                },
                token
            );
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            Debug.WriteLine(ex);
        }
        finally
        {
            InSearch = false;
        }
    }

    private Dictionary<string, List<string>> _errorsByPropertyName = [];
    private static readonly string[] NO_ERRORS = [];

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return _errorsByPropertyName.Values.SelectMany(x => x);
        }

        if (_errorsByPropertyName.TryGetValue(propertyName, out var errorList))
        {
            return errorList;
        }
        return NO_ERRORS;
    }

    protected virtual void SetError(string propertyName, string error)
    {
        if (_errorsByPropertyName.TryGetValue(propertyName, out var errorList))
        {
            if (!errorList.Contains(error))
            {
                errorList.Add(error);
            }
        }
        else
        {
            _errorsByPropertyName.Add(propertyName, [error]);
        }
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected virtual void RemoveErrors(string propertyName)
    {
        if (_errorsByPropertyName.Remove(propertyName))
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public ObservableCollection<TreeNode> SearchResults { get; } = [];

    private string _lastError = "";

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public string LastError
    {
        get => _lastError;
        set => this.RaiseAndSetIfChanged(ref _lastError, value);
    }

    public bool HasErrors => throw new NotImplementedException();
}
