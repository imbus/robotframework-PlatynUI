namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_get_keyboard_mapping_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("xcb_keycode_t")]
    public byte first_keycode;

    [NativeTypeName("uint8_t")]
    public byte count;
}
