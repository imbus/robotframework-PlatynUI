using System.ComponentModel.Composition;
using System.Runtime.InteropServices;

using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;
using PlatynUI.Platform.X11.Interop.XCB;
using static PlatynUI.Platform.X11.Interop.XCB.XCB;

namespace PlatynUI.Platform.X11;

[Export(typeof(IMouseDevice))]
public class MouseDevice : IMouseDevice
{
    public uint Id { get; private set; }
    public XCBConnection Connection { get; }

    public MouseDevice(XCBConnection connection)
    {
        Connection = connection;

        if (Connection == null)
        {
            throw new InvalidOperationException("Failed to initialize XCB connection.");
        }

        Id = GetRootWindow();
    }

    // In X11, there isn't a direct API to get the double-click size.
    // Some desktop environments like GNOME store these settings in GSettings.
    // For a generic X11 implementation, we'll return a reasonable default value.
    public Size GetDoubleClickSize()
    {
        // Default double-click distance for X11 (varies by desktop environment)
        const int defaultDoubleClickDistance = 5; // pixels

        // Try to read from the X resource database (Xrm) if available
        // This would require interop with Xlib since XCB doesn't expose Xrm functionality

        // For now, return a reasonable default value
        return new Size(defaultDoubleClickDistance, defaultDoubleClickDistance);
    }

    public double GetDoubleClickTime()
    {
        // Default double-click time for X11 (varies by desktop environment)
        const double defaultDoubleClickTimeMs = 400; // milliseconds

        // Try to read from the X resource database (Xrm) if available
        // This would require interop with Xlib since XCB doesn't expose Xrm functionality

        // For now, return a reasonable default value
        return defaultDoubleClickTimeMs / 1000.0; // Convert to seconds
    }

    public unsafe Point GetPosition()
    {
        // Create a cookie for the query pointer request
        var cookie = xcb_query_pointer(Connection, Id);

        // Get the reply for the query pointer request
        xcb_generic_error_t* error = null;
        var reply = xcb_query_pointer_reply(Connection, cookie, &error);

        if (reply == null || error != null)
        {
            throw new InvalidOperationException("Failed to get cursor position.");
        }

        // Extract the pointer position from the reply
        var x = reply->root_x;
        var y = reply->root_y;

        // Free the reply
        Marshal.FreeHGlobal((IntPtr)reply);

        return new Point(x, y);
    }

    public unsafe void Move(double x, double y)
    {
        xcb_test_fake_input(
            Connection,
            type: 6, // 6 corresponds to MotionNotify in X11
            detail: 0,
            time: 0,
            root: Id,
            rootX: (short)x,
            rootY: (short)y,
            deviceid: 0
        );
    }

    public void Press(MouseButton button) => Press((int)button);

    public unsafe void Press(int button)
    {
        var buttonId = button + 1; // In X11, button IDs start at 1 (1=left, 2=middle, 3=right)

        xcb_test_fake_input(
            Connection,
            type: 4, // 4 corresponds to ButtonPress in X11
            detail: (byte)buttonId,
            time: 0,
            root: Id,
            rootX: 0,
            rootY: 0,
            deviceid: 0
        );
    }
    public void Release(MouseButton button) => Release((int)button);

    public unsafe void Release(int button)
    {
        var buttonId = button + 1; // In X11, button IDs start at 1 (1=left, 2=middle, 3=right)

        xcb_test_fake_input(
            Connection,
            type: 5, // 5 corresponds to ButtonRelease in X11
            detail: (byte)buttonId,
            time: 0,
            root: Id,
            rootX: 0,
            rootY: 0,
            deviceid: 0
        );
    }

    private unsafe uint GetRootWindow()
    {
        // Get the setup information for the X server
        var setup = xcb_get_setup(Connection);
        if (setup == null)
        {
            throw new InvalidOperationException("Failed to get X server setup information.");
        }

        // Get the iterator for the screen roots
        var iter = xcb_setup_roots_iterator(setup);

        // Return the root window of the first screen
        return iter.data->root;
    }

    ~MouseDevice()
    {
        // Clean up the XCB connection if necessary
        Connection?.Dispose();
    }
}

