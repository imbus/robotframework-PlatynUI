﻿// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using Avalonia.Metadata;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using PlatynUI.Extension.Win32.UiAutomation;
using PlatynUI.Extension.Win32.UiAutomation.Client;
using ReactiveUI;
using Finder = PlatynUI.Runtime.Finder;

namespace PlatynUI.Spy.ViewModels;

[Export]
public class MainWindowViewModel : ViewModelBase, INotifyDataErrorInfo
{
    private readonly IMouseDevice _mouseDevice;
    private readonly IUIAutomation _automation;

    public ObservableCollection<string> Results { get; } = new();
    public IReactiveCommand ClearResultsCommand { get; }

    [ImportingConstructor]
    public MainWindowViewModel(IMouseDevice mouseDevice)
    {
        _mouseDevice = mouseDevice;
        _automation = new CUIAutomation();
        
        ClearResultsCommand = ReactiveCommand.Create(() => Results.Clear());

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

    public TreeNode[] Root { get; } = [new TreeNode(null, Desktop.GetInstance()) { IsExpanded = true, IsSelected = true }];

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

    private bool _isRecording;
    public bool IsRecording
    {
        get => _isRecording;
        set => this.RaiseAndSetIfChanged(ref _isRecording, value);
    }

    public string RecordButtonContent => IsRecording ? "\uE946" : "\uE81D";

    public void HandleCtrlPress()
    {
        var position = _mouseDevice.GetPosition();
        
        try 
        {
            var element = _automation.ElementFromPoint(new tagPOINT { x = (int)position.X, y = (int)position.Y });
            
            if (element == null)
            {
                Results.Add($"No element found at position ({position.X}, {position.Y})");
                return;
            }

            // Skip if it's our own window
            var processId = element.CurrentProcessId;
            if (processId == Process.GetCurrentProcess().Id)
            {
                Results.Add($"Skipping our own window at ({position.X}, {position.Y})");
                return;
            }

            // Get element information
            var role = Helper.GetRole(element);
            var name = element.CurrentName;
            var automationId = element.CurrentAutomationId;
            var className = element.CurrentClassName;
            var runtimeId = Helper.GetRuntimeId(element);

            Results.Add($"Element found at ({position.X}, {position.Y}):");
            Results.Add($"  Role: {role}");
            Results.Add($"  Name: {name}");
            Results.Add($"  AutomationId: {automationId}");
            Results.Add($"  ClassName: {className}");
            Results.Add($"  RuntimeId: {runtimeId}");
        }
        catch (Exception ex)
        {
            Results.Add($"Error capturing element: {ex.Message}");
            Debug.WriteLine($"Element capture error: {ex}");
        }
    }

    public void LogHello()
    {
        IsRecording = !IsRecording;
        Debug.WriteLine($"Recording state changed to: {IsRecording}");
        this.RaisePropertyChanged(nameof(RecordButtonContent));
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

    public void ClearResults()
    {
        SearchResults.Clear();
        LastError = "";
    }
}