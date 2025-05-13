using System.ComponentModel.Composition;
using PlatynUI.Platform.MacOS.SwiftInterop;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;

namespace PlatynUI.Platform.MacOS;

[Export(typeof(IMouseDevice))]
public class MouseDevice() : IMouseDevice
{
    public Size GetDoubleClickSize()
    {
        double toleranceSize = Interop.GetMouseDoubleClickTolerance();
        return new Size(toleranceSize, toleranceSize);
    }

    public int GetDoubleClickTime()
    {
        return (int)(Interop.GetMouseDoubleClickInterval() * 1000);
    }

    public Point GetPosition()
    {
        Interop.GetMousePosition(out var x, out var y);
        return new Point(x, y);
    }

    public void Move(double x, double y)
    {
        Interop.MouseMove(x, y);
    }

    public void Press(MouseButton button)
    {
        Interop.MousePress((int)button);
    }

    public void Release(MouseButton button)
    {
        Interop.MouseRelease((int)button);
    }
}
