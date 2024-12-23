namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_reparent_window_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("xcb_window_t")]
    public uint window;

    [NativeTypeName("xcb_window_t")]
    public uint parent;

    [NativeTypeName("int16_t")]
    public short x;

    [NativeTypeName("int16_t")]
    public short y;
}
