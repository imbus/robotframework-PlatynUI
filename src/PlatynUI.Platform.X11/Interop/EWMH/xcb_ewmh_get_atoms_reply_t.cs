namespace PlatynUI.Platform.X11.Interop.XCB;

public unsafe partial struct xcb_ewmh_get_atoms_reply_t
{
    [NativeTypeName("uint32_t")]
    public uint atoms_len;

    [NativeTypeName("xcb_atom_t *")]
    public uint* atoms;

    public xcb_get_property_reply_t* _reply;
}
