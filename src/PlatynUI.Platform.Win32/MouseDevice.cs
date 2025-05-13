// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace PlatynUI.Platform.Win32;

[Export(typeof(IMouseDevice))]
public class MouseDevice() : IMouseDevice
{
    public int GetDoubleClickTime()
    {
        return (int)PInvoke.GetDoubleClickTime();
    }

    public Size GetDoubleClickSize()
    {
        return new Size(
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXDOUBLECLK),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYDOUBLECLK)
        );
    }

    public Point GetPosition()
    {
        CURSORINFO cursorInfo = new() { cbSize = (uint)Marshal.SizeOf(typeof(CURSORINFO)) };

        PInvoke.GetCursorInfo(ref cursorInfo);
        return new Point(cursorInfo.ptScreenPos.X, cursorInfo.ptScreenPos.Y);
    }

    static Point ScreenToMouseCoords(Point pt)
    {
        var screenRect = new Rect(
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_XVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_YVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYVIRTUALSCREEN)
        );

        return ScreenToMouseCoords(pt, screenRect);
    }

    static Point ScreenToMouseCoords(Point point, Rect desktopRect)
    {
        return new Point(
            ushort.MaxValue * (point.X - desktopRect.X + 0.5) / desktopRect.Width,
            ushort.MaxValue * (point.Y - desktopRect.Y + 0.5) / desktopRect.Height
        );
    }

    public void Move(double x, double y)
    {
        Move(new Point(x, y));
    }

    public static void Move(Point p)
    {
        p = ScreenToMouseCoords(p);

        SendMouseInput(
            (int)p.X,
            (int)p.Y,
            0,
            MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE
                | MOUSE_EVENT_FLAGS.MOUSEEVENTF_ABSOLUTE
                | MOUSE_EVENT_FLAGS.MOUSEEVENTF_VIRTUALDESK
        );
    }

    public void Press(MouseButton button) => Press((int)button);

    public static void Press(int button)
    {
        var flags = button switch
        {
            0 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTDOWN,
            1 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEDOWN,
            2 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTDOWN,
            3 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_XDOWN,
            _ => throw new NotSupportedException(),
        };
        SendMouseInput(0, 0, 0, flags);
    }

    public void Release(MouseButton button) => Release((int)button);

    public static void Release(int button)
    {
        var flags = button switch
        {
            0 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTUP,
            1 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEUP,
            2 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTUP,
            3 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_XUP,
            _ => throw new NotSupportedException(),
        };
        SendMouseInput(0, 0, 0, flags);
    }

    private static unsafe void SendMouseInput(int dx, int dy, uint mouseData, MOUSE_EVENT_FLAGS dwFlags)
    {
        var inputs = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dx = dx,
                    dy = dy,
                    mouseData = mouseData,
                    dwFlags = dwFlags,
                    time = 0,
                    dwExtraInfo = (nuint)PInvoke.GetMessageExtraInfo().Value,
                },
            },
        };

        PInvoke.SendInput(1, &inputs, Marshal.SizeOf<INPUT>());
    }
}
