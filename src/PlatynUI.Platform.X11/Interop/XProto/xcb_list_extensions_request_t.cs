namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_list_extensions_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;
}
