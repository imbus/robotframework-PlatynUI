namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_set_access_control_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte mode;

    [NativeTypeName("uint16_t")]
    public ushort length;
}
