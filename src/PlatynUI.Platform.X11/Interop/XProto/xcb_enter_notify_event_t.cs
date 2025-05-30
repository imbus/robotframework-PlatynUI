namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_enter_notify_event_t
{
    [NativeTypeName("uint8_t")]
    public byte response_type;

    [NativeTypeName("uint8_t")]
    public byte detail;

    [NativeTypeName("uint16_t")]
    public ushort sequence;

    [NativeTypeName("xcb_timestamp_t")]
    public uint time;

    [NativeTypeName("xcb_window_t")]
    public uint root;

    [NativeTypeName("xcb_window_t")]
    public uint @event;

    [NativeTypeName("xcb_window_t")]
    public uint child;

    [NativeTypeName("int16_t")]
    public short root_x;

    [NativeTypeName("int16_t")]
    public short root_y;

    [NativeTypeName("int16_t")]
    public short event_x;

    [NativeTypeName("int16_t")]
    public short event_y;

    [NativeTypeName("uint16_t")]
    public ushort state;

    [NativeTypeName("uint8_t")]
    public byte mode;

    [NativeTypeName("uint8_t")]
    public byte same_screen_focus;
}
