// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel.Composition;
using PlatynUI.Runtime.Core;

namespace PlatynUI.Runtime;

public class DisplayDevice : IDisposable
{
    [Import]
    protected IDisplayDevice? displayDevice;

    private bool _disposed = false;

    static DisplayDevice()
    {
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Reset();
    }

    private DisplayDevice()
    {
        PlatynUiExtensions.ComposeParts(this);
    }

    private static DisplayDevice? _instance;
    private static DisplayDevice Instance => _instance ??= new DisplayDevice();

    public static void Reset()
    {
        if (_instance != null)
        {
            _instance.Dispose();
            _instance = null;
        }
    }

    public static Rect GetBoundingRectangle()
    {
        return Instance.displayDevice?.GetBoundingRectangle() ?? new Rect();
    }

    public static void HighlightRect(double x, double y, double width, double height, double time = 2)
    {
        Instance.displayDevice?.HighlightRect(x, y, width, height, time);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing && displayDevice is IDisposable disposableDevice)
            {
                disposableDevice.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DisplayDevice()
    {
        Dispose(false);
    }
}
