namespace PlatynUI.Platform.X11.Interop.XCB;

public unsafe partial struct xcb_client_message_data_iterator_t
{
    public xcb_client_message_data_t* data;

    public int rem;

    public int index;
}
