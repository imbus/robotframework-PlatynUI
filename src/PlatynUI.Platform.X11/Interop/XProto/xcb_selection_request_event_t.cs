namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_selection_request_event_t
{
    [NativeTypeName("uint8_t")]
    public byte response_type;

    [NativeTypeName("uint8_t")]
    public byte pad0;

    [NativeTypeName("uint16_t")]
    public ushort sequence;

    [NativeTypeName("xcb_timestamp_t")]
    public uint time;

    [NativeTypeName("xcb_window_t")]
    public uint owner;

    [NativeTypeName("xcb_window_t")]
    public uint requestor;

    [NativeTypeName("xcb_atom_t")]
    public uint selection;

    [NativeTypeName("xcb_atom_t")]
    public uint target;

    [NativeTypeName("xcb_atom_t")]
    public uint property;
}
