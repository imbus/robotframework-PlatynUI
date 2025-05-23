namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_map_request_event_t
{
    [NativeTypeName("uint8_t")]
    public byte response_type;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort sequence;

    [NativeTypeName("xcb_window_t")]
    public uint parent;

    [NativeTypeName("xcb_window_t")]
    public uint window;
}
