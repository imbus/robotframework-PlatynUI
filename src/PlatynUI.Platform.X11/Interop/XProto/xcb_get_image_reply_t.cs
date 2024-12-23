using System.Runtime.CompilerServices;

namespace PlatynUI.Platform.X11.Interop.XCB;

public partial struct xcb_get_image_reply_t
{
    [NativeTypeName("uint8_t")]
    public byte response_type;

    [NativeTypeName("uint8_t")]
    public byte depth;

    [NativeTypeName("uint16_t")]
    public ushort sequence;

    [NativeTypeName("uint32_t")]
    public uint length;

    [NativeTypeName("xcb_visualid_t")]
    public uint visual;

    [NativeTypeName("uint8_t[20]")]
    public _pad0_e__FixedBuffer pad0;

    [InlineArray(20)]
    public partial struct _pad0_e__FixedBuffer
    {
        public byte e0;
    }
}
