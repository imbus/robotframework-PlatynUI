namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_set_dashes_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("xcb_gcontext_t")]
    public uint gc;

    [NativeTypeName("uint16_t")]
    public ushort dash_offset;

    [NativeTypeName("uint16_t")]
    public ushort dashes_len;
}
