namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_create_notify_event_t
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

    [NativeTypeName("int16_t")]
    public short x;

    [NativeTypeName("int16_t")]
    public short y;

    [NativeTypeName("uint16_t")]
    public ushort width;

    [NativeTypeName("uint16_t")]
    public ushort height;

    [NativeTypeName("uint16_t")]
    public ushort border_width;

    [NativeTypeName("uint8_t")]
    public byte override_redirect;

    [NativeTypeName("uint8_t")]
    public byte pad1;
}
