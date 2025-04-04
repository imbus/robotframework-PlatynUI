namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_list_fonts_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("uint16_t")]
    public ushort max_names;

    [NativeTypeName("uint16_t")]
    public ushort pattern_len;
}
