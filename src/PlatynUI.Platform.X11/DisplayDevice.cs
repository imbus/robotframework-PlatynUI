// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel.Composition;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;

namespace PlatynUI.Platform.X11;

[Export(typeof(IDisplayDevice))]
[method: ImportingConstructor]
public class DisplayDevice() : IDisplayDevice
{
    public Rect GetBoundingRectangle()
    {
        var size = XCBConnection.Default.ScreenSize;
        return new Rect(0, 0, size.Width, size.Height);
    }

    private Highlighter? _highlighter = null;

    Highlighter Highlighter => _highlighter ??= new(true);

    public void HighlightRect(double x, double y, double width, double height, double time)
    {
        Highlighter.Show(new Rect(x, y, width, height), (int)time * 1000);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _highlighter?.Dispose();
            _highlighter = null;
        }
    }

    ~DisplayDevice()
    {
        Dispose(false);
    }
}
