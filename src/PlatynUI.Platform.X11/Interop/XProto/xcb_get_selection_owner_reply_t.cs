namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_get_selection_owner_reply_t
{
    [NativeTypeName("uint8_t")]
    public byte response_type;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort sequence;

    [NativeTypeName("uint32_t")]
    public uint length;

    [NativeTypeName("xcb_window_t")]
    public uint owner;
}
