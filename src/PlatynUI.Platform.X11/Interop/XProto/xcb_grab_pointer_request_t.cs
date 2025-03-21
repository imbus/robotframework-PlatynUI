namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_grab_pointer_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte owner_events;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("xcb_window_t")]
    public uint grab_window;

    [NativeTypeName("uint16_t")]
    public ushort event_mask;

    [NativeTypeName("uint8_t")]
    public byte pointer_mode;

    [NativeTypeName("uint8_t")]
    public byte keyboard_mode;

    [NativeTypeName("xcb_window_t")]
    public uint confine_to;

    [NativeTypeName("xcb_cursor_t")]
    public uint cursor;

    [NativeTypeName("xcb_timestamp_t")]
    public uint time;
}
