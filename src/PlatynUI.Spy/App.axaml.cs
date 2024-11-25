// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PlatynUI.Spy.ViewModels;
using PlatynUI.Spy.Views;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace PlatynUI.Spy;

public partial class App : Application
{
    private CompositionContainer? _container;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        // Set up MEF
        var catalog = new AggregateCatalog();
        catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
        catalog.Catalogs.Add(new AssemblyCatalog(typeof(PlatynUI.Platform.Win32.MouseDevice).Assembly));
        
        _container = new CompositionContainer(catalog);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var viewModel = _container!.GetExportedValue<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow { DataContext = viewModel };
        }

        base.OnFrameworkInitializationCompleted();
    }
}