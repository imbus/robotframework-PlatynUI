using System.Runtime.InteropServices;

namespace PlatynUI.Platform.X11.Interop.XCB;

public static unsafe partial class XCB
{
    [LibraryImport("libc")]
    public static partial void free(void* buffer);
    // // TODO: implement free
    // public static void free(void* buffer) { }
}
