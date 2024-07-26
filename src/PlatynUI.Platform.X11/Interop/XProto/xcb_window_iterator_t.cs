namespace PlatynUI.Platform.X11.Interop.XCB;

public unsafe partial struct xcb_window_iterator_t
{
    [NativeTypeName("xcb_window_t *")]
    public uint* data;

    public int rem;

    public int index;
}
