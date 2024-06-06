namespace PlatynUI.Technology.UiAutomation;

using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

public enum MouseButton : int
{
    Left = 0,
    Right = 1,
    Middle = 2,

    X1 = 3,
    //X2
}

public class Point(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public override string ToString() => $"({X}, {Y})";
}

public class Size(double width, double height)
{
    public double Width { get; set; } = width;
    public double Height { get; set; } = height;

    public override string ToString() => $"({Width}, {Height})";
}

public class Rect(double x, double y, double width, double height)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;
    public double Width { get; set; } = width;
    public double Height { get; set; } = height;

    public override string ToString() => $"({X}, {Y}, {Width}, {Height})";
}

public static class MouseDevice
{
    public static double GetDoubleClickTime()
    {
        return PInvoke.GetDoubleClickTime() / 1000.0;
    }

    public static Size GetDoubleClickSize()
    {
        return new Size(
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXDOUBLECLK),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYDOUBLECLK)
        );
    }

    public static Point GetPosition()
    {
        CURSORINFO cursorInfo = new() { cbSize = (uint)Marshal.SizeOf(typeof(CURSORINFO)) };

        PInvoke.GetCursorInfo(ref cursorInfo);
        return new Point(cursorInfo.ptScreenPos.X, cursorInfo.ptScreenPos.Y);
    }

    public static Point ScreenToMouseCoords(Point pt)
    {
        var screenRect = new Rect(
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_XVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_YVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXVIRTUALSCREEN),
            PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYVIRTUALSCREEN)
        );

        return ScreenToMouseCoords(pt, screenRect);
    }

    public static Point ScreenToMouseCoords(Point point, Rect desktopRect)
    {
        return new Point(
            ushort.MaxValue * (point.X - desktopRect.X + 0.5) / desktopRect.Width,
            ushort.MaxValue * (point.Y - desktopRect.Y + 0.5) / desktopRect.Height
        );
    }

    public static void Move(double x, double y)
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

    public static void Press(MouseButton button) => Press((int)button);

    public static void Press(int button)
    {
        var flags = button switch
        {
            0 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTDOWN,
            1 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTDOWN,
            2 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEDOWN,
            3 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_XDOWN,
            _ => throw new NotSupportedException(),
        };
        SendMouseInput(0, 0, 0, flags);
    }

    public static void Release(MouseButton button) => Release((int)button);

    public static void Release(int button)
    {
        var flags = button switch
        {
            0 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTUP,
            1 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTUP,
            2 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEUP,
            3 => MOUSE_EVENT_FLAGS.MOUSEEVENTF_XUP,
            _ => throw new NotSupportedException(),
        };
        SendMouseInput(0, 0, 0, flags);
    }

    private static void SendMouseInput(int dx, int dy, uint mouseData, MOUSE_EVENT_FLAGS dwFlags)
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
                    dwExtraInfo = (nuint)PInvoke.GetMessageExtraInfo().Value
                }
            }
        };

        PInvoke.SendInput(new Span<INPUT>(ref inputs), Marshal.SizeOf(inputs));
    }
}
