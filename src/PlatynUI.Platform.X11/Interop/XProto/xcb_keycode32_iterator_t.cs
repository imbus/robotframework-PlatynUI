namespace PlatynUI.Platform.X11.Interop.XCB;

public unsafe partial struct xcb_keycode32_iterator_t
{
    [NativeTypeName("xcb_keycode32_t *")]
    public uint* data;

    public int rem;

    public int index;
}
