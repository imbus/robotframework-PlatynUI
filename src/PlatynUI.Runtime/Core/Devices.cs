// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace PlatynUI.Runtime.Core;

public interface IDisplayDevice : IDisposable
{
    Rect GetBoundingRectangle();
    void HighlightRect(double x, double y, double width, double height, double time);
}

public enum MouseButton : int
{
    Left = 0,
    Right = 1,
    Middle = 2,

    X1 = 3,
    X2,
}

public interface IMouseDevice
{
    int GetDoubleClickTime();
    Size GetDoubleClickSize();
    Point GetPosition();

    void Move(double x, double y);

    void Press(MouseButton button);
    void Release(MouseButton button);
}

public class Keycode(object? key, object? code, bool valid, string? errorText)
{
    public object? Key { get; } = key;
    public object? Code { get; } = code;
    public bool Valid { get; } = valid;
    public string? ErrorText { get; } = errorText;

    public override string ToString()
    {
        return $"{Key} - {Code} - {Valid} - {ErrorText}";
    }
}

public interface IKeyboardDevice
{
    Keycode KeyToKeyCode(object? key);
    bool SendKeyCode(object keyCode, bool pressed);
}
