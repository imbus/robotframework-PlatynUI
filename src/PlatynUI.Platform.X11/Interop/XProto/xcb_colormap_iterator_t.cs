namespace PlatynUI.Platform.X11.Interop.XCB;

public unsafe partial struct xcb_colormap_iterator_t
{
    [NativeTypeName("xcb_colormap_t *")]
    public uint* data;

    public int rem;

    public int index;
}
