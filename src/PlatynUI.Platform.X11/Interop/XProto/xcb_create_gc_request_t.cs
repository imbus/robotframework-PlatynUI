namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_create_gc_request_t
{
    [NativeTypeName("uint8_t")]
    public byte major_opcode;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort length;

    [NativeTypeName("xcb_gcontext_t")]
    public uint cid;

    [NativeTypeName("xcb_drawable_t")]
    public uint drawable;

    [NativeTypeName("uint32_t")]
    public uint value_mask;
}
