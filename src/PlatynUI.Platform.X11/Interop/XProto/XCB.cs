using System.Runtime.InteropServices;

namespace PlatynUI.Platform.X11.Interop.XCB;

public static unsafe partial class XCB
{
    [NativeTypeName("#define XCB_KEY_PRESS 2")]
    public const int XCB_KEY_PRESS = 2;

    [NativeTypeName("#define XCB_KEY_RELEASE 3")]
    public const int XCB_KEY_RELEASE = 3;

    [NativeTypeName("#define XCB_BUTTON_PRESS 4")]
    public const int XCB_BUTTON_PRESS = 4;

    [NativeTypeName("#define XCB_BUTTON_RELEASE 5")]
    public const int XCB_BUTTON_RELEASE = 5;

    [NativeTypeName("#define XCB_MOTION_NOTIFY 6")]
    public const int XCB_MOTION_NOTIFY = 6;

    [NativeTypeName("#define XCB_ENTER_NOTIFY 7")]
    public const int XCB_ENTER_NOTIFY = 7;

    [NativeTypeName("#define XCB_LEAVE_NOTIFY 8")]
    public const int XCB_LEAVE_NOTIFY = 8;

    [NativeTypeName("#define XCB_FOCUS_IN 9")]
    public const int XCB_FOCUS_IN = 9;

    [NativeTypeName("#define XCB_FOCUS_OUT 10")]
    public const int XCB_FOCUS_OUT = 10;

    [NativeTypeName("#define XCB_KEYMAP_NOTIFY 11")]
    public const int XCB_KEYMAP_NOTIFY = 11;

    [NativeTypeName("#define XCB_EXPOSE 12")]
    public const int XCB_EXPOSE = 12;

    [NativeTypeName("#define XCB_GRAPHICS_EXPOSURE 13")]
    public const int XCB_GRAPHICS_EXPOSURE = 13;

    [NativeTypeName("#define XCB_NO_EXPOSURE 14")]
    public const int XCB_NO_EXPOSURE = 14;

    [NativeTypeName("#define XCB_VISIBILITY_NOTIFY 15")]
    public const int XCB_VISIBILITY_NOTIFY = 15;

    [NativeTypeName("#define XCB_CREATE_NOTIFY 16")]
    public const int XCB_CREATE_NOTIFY = 16;

    [NativeTypeName("#define XCB_DESTROY_NOTIFY 17")]
    public const int XCB_DESTROY_NOTIFY = 17;

    [NativeTypeName("#define XCB_UNMAP_NOTIFY 18")]
    public const int XCB_UNMAP_NOTIFY = 18;

    [NativeTypeName("#define XCB_MAP_NOTIFY 19")]
    public const int XCB_MAP_NOTIFY = 19;

    [NativeTypeName("#define XCB_MAP_REQUEST 20")]
    public const int XCB_MAP_REQUEST = 20;

    [NativeTypeName("#define XCB_REPARENT_NOTIFY 21")]
    public const int XCB_REPARENT_NOTIFY = 21;

    [NativeTypeName("#define XCB_CONFIGURE_NOTIFY 22")]
    public const int XCB_CONFIGURE_NOTIFY = 22;

    [NativeTypeName("#define XCB_CONFIGURE_REQUEST 23")]
    public const int XCB_CONFIGURE_REQUEST = 23;

    [NativeTypeName("#define XCB_GRAVITY_NOTIFY 24")]
    public const int XCB_GRAVITY_NOTIFY = 24;

    [NativeTypeName("#define XCB_RESIZE_REQUEST 25")]
    public const int XCB_RESIZE_REQUEST = 25;

    [NativeTypeName("#define XCB_CIRCULATE_NOTIFY 26")]
    public const int XCB_CIRCULATE_NOTIFY = 26;

    [NativeTypeName("#define XCB_CIRCULATE_REQUEST 27")]
    public const int XCB_CIRCULATE_REQUEST = 27;

    [NativeTypeName("#define XCB_PROPERTY_NOTIFY 28")]
    public const int XCB_PROPERTY_NOTIFY = 28;

    [NativeTypeName("#define XCB_SELECTION_CLEAR 29")]
    public const int XCB_SELECTION_CLEAR = 29;

    [NativeTypeName("#define XCB_SELECTION_REQUEST 30")]
    public const int XCB_SELECTION_REQUEST = 30;

    [NativeTypeName("#define XCB_SELECTION_NOTIFY 31")]
    public const int XCB_SELECTION_NOTIFY = 31;

    [NativeTypeName("#define XCB_COLORMAP_NOTIFY 32")]
    public const int XCB_COLORMAP_NOTIFY = 32;

    [NativeTypeName("#define XCB_CLIENT_MESSAGE 33")]
    public const int XCB_CLIENT_MESSAGE = 33;

    [NativeTypeName("#define XCB_MAPPING_NOTIFY 34")]
    public const int XCB_MAPPING_NOTIFY = 34;

    [NativeTypeName("#define XCB_GE_GENERIC 35")]
    public const int XCB_GE_GENERIC = 35;

    [NativeTypeName("#define XCB_REQUEST 1")]
    public const int XCB_REQUEST = 1;

    [NativeTypeName("#define XCB_VALUE 2")]
    public const int XCB_VALUE = 2;

    [NativeTypeName("#define XCB_WINDOW 3")]
    public const int XCB_WINDOW = 3;

    [NativeTypeName("#define XCB_PIXMAP 4")]
    public const int XCB_PIXMAP = 4;

    [NativeTypeName("#define XCB_ATOM 5")]
    public const int XCB_ATOM = 5;

    [NativeTypeName("#define XCB_CURSOR 6")]
    public const int XCB_CURSOR = 6;

    [NativeTypeName("#define XCB_FONT 7")]
    public const int XCB_FONT = 7;

    [NativeTypeName("#define XCB_MATCH 8")]
    public const int XCB_MATCH = 8;

    [NativeTypeName("#define XCB_DRAWABLE 9")]
    public const int XCB_DRAWABLE = 9;

    [NativeTypeName("#define XCB_ACCESS 10")]
    public const int XCB_ACCESS = 10;

    [NativeTypeName("#define XCB_ALLOC 11")]
    public const int XCB_ALLOC = 11;

    [NativeTypeName("#define XCB_COLORMAP 12")]
    public const int XCB_COLORMAP = 12;

    [NativeTypeName("#define XCB_G_CONTEXT 13")]
    public const int XCB_G_CONTEXT = 13;

    [NativeTypeName("#define XCB_ID_CHOICE 14")]
    public const int XCB_ID_CHOICE = 14;

    [NativeTypeName("#define XCB_NAME 15")]
    public const int XCB_NAME = 15;

    [NativeTypeName("#define XCB_LENGTH 16")]
    public const int XCB_LENGTH = 16;

    [NativeTypeName("#define XCB_IMPLEMENTATION 17")]
    public const int XCB_IMPLEMENTATION = 17;

    [NativeTypeName("#define XCB_CREATE_WINDOW 1")]
    public const int XCB_CREATE_WINDOW = 1;

    [NativeTypeName("#define XCB_CHANGE_WINDOW_ATTRIBUTES 2")]
    public const int XCB_CHANGE_WINDOW_ATTRIBUTES = 2;

    [NativeTypeName("#define XCB_GET_WINDOW_ATTRIBUTES 3")]
    public const int XCB_GET_WINDOW_ATTRIBUTES = 3;

    [NativeTypeName("#define XCB_DESTROY_WINDOW 4")]
    public const int XCB_DESTROY_WINDOW = 4;

    [NativeTypeName("#define XCB_DESTROY_SUBWINDOWS 5")]
    public const int XCB_DESTROY_SUBWINDOWS = 5;

    [NativeTypeName("#define XCB_CHANGE_SAVE_SET 6")]
    public const int XCB_CHANGE_SAVE_SET = 6;

    [NativeTypeName("#define XCB_REPARENT_WINDOW 7")]
    public const int XCB_REPARENT_WINDOW = 7;

    [NativeTypeName("#define XCB_MAP_WINDOW 8")]
    public const int XCB_MAP_WINDOW = 8;

    [NativeTypeName("#define XCB_MAP_SUBWINDOWS 9")]
    public const int XCB_MAP_SUBWINDOWS = 9;

    [NativeTypeName("#define XCB_UNMAP_WINDOW 10")]
    public const int XCB_UNMAP_WINDOW = 10;

    [NativeTypeName("#define XCB_UNMAP_SUBWINDOWS 11")]
    public const int XCB_UNMAP_SUBWINDOWS = 11;

    [NativeTypeName("#define XCB_CONFIGURE_WINDOW 12")]
    public const int XCB_CONFIGURE_WINDOW = 12;

    [NativeTypeName("#define XCB_CIRCULATE_WINDOW 13")]
    public const int XCB_CIRCULATE_WINDOW = 13;

    [NativeTypeName("#define XCB_GET_GEOMETRY 14")]
    public const int XCB_GET_GEOMETRY = 14;

    [NativeTypeName("#define XCB_QUERY_TREE 15")]
    public const int XCB_QUERY_TREE = 15;

    [NativeTypeName("#define XCB_INTERN_ATOM 16")]
    public const int XCB_INTERN_ATOM = 16;

    [NativeTypeName("#define XCB_GET_ATOM_NAME 17")]
    public const int XCB_GET_ATOM_NAME = 17;

    [NativeTypeName("#define XCB_CHANGE_PROPERTY 18")]
    public const int XCB_CHANGE_PROPERTY = 18;

    [NativeTypeName("#define XCB_DELETE_PROPERTY 19")]
    public const int XCB_DELETE_PROPERTY = 19;

    [NativeTypeName("#define XCB_GET_PROPERTY 20")]
    public const int XCB_GET_PROPERTY = 20;

    [NativeTypeName("#define XCB_LIST_PROPERTIES 21")]
    public const int XCB_LIST_PROPERTIES = 21;

    [NativeTypeName("#define XCB_SET_SELECTION_OWNER 22")]
    public const int XCB_SET_SELECTION_OWNER = 22;

    [NativeTypeName("#define XCB_GET_SELECTION_OWNER 23")]
    public const int XCB_GET_SELECTION_OWNER = 23;

    [NativeTypeName("#define XCB_CONVERT_SELECTION 24")]
    public const int XCB_CONVERT_SELECTION = 24;

    [NativeTypeName("#define XCB_SEND_EVENT 25")]
    public const int XCB_SEND_EVENT = 25;

    [NativeTypeName("#define XCB_GRAB_POINTER 26")]
    public const int XCB_GRAB_POINTER = 26;

    [NativeTypeName("#define XCB_UNGRAB_POINTER 27")]
    public const int XCB_UNGRAB_POINTER = 27;

    [NativeTypeName("#define XCB_GRAB_BUTTON 28")]
    public const int XCB_GRAB_BUTTON = 28;

    [NativeTypeName("#define XCB_UNGRAB_BUTTON 29")]
    public const int XCB_UNGRAB_BUTTON = 29;

    [NativeTypeName("#define XCB_CHANGE_ACTIVE_POINTER_GRAB 30")]
    public const int XCB_CHANGE_ACTIVE_POINTER_GRAB = 30;

    [NativeTypeName("#define XCB_GRAB_KEYBOARD 31")]
    public const int XCB_GRAB_KEYBOARD = 31;

    [NativeTypeName("#define XCB_UNGRAB_KEYBOARD 32")]
    public const int XCB_UNGRAB_KEYBOARD = 32;

    [NativeTypeName("#define XCB_GRAB_KEY 33")]
    public const int XCB_GRAB_KEY = 33;

    [NativeTypeName("#define XCB_UNGRAB_KEY 34")]
    public const int XCB_UNGRAB_KEY = 34;

    [NativeTypeName("#define XCB_ALLOW_EVENTS 35")]
    public const int XCB_ALLOW_EVENTS = 35;

    [NativeTypeName("#define XCB_GRAB_SERVER 36")]
    public const int XCB_GRAB_SERVER = 36;

    [NativeTypeName("#define XCB_UNGRAB_SERVER 37")]
    public const int XCB_UNGRAB_SERVER = 37;

    [NativeTypeName("#define XCB_QUERY_POINTER 38")]
    public const int XCB_QUERY_POINTER = 38;

    [NativeTypeName("#define XCB_GET_MOTION_EVENTS 39")]
    public const int XCB_GET_MOTION_EVENTS = 39;

    [NativeTypeName("#define XCB_TRANSLATE_COORDINATES 40")]
    public const int XCB_TRANSLATE_COORDINATES = 40;

    [NativeTypeName("#define XCB_WARP_POINTER 41")]
    public const int XCB_WARP_POINTER = 41;

    [NativeTypeName("#define XCB_SET_INPUT_FOCUS 42")]
    public const int XCB_SET_INPUT_FOCUS = 42;

    [NativeTypeName("#define XCB_GET_INPUT_FOCUS 43")]
    public const int XCB_GET_INPUT_FOCUS = 43;

    [NativeTypeName("#define XCB_QUERY_KEYMAP 44")]
    public const int XCB_QUERY_KEYMAP = 44;

    [NativeTypeName("#define XCB_OPEN_FONT 45")]
    public const int XCB_OPEN_FONT = 45;

    [NativeTypeName("#define XCB_CLOSE_FONT 46")]
    public const int XCB_CLOSE_FONT = 46;

    [NativeTypeName("#define XCB_QUERY_FONT 47")]
    public const int XCB_QUERY_FONT = 47;

    [NativeTypeName("#define XCB_QUERY_TEXT_EXTENTS 48")]
    public const int XCB_QUERY_TEXT_EXTENTS = 48;

    [NativeTypeName("#define XCB_LIST_FONTS 49")]
    public const int XCB_LIST_FONTS = 49;

    [NativeTypeName("#define XCB_LIST_FONTS_WITH_INFO 50")]
    public const int XCB_LIST_FONTS_WITH_INFO = 50;

    [NativeTypeName("#define XCB_SET_FONT_PATH 51")]
    public const int XCB_SET_FONT_PATH = 51;

    [NativeTypeName("#define XCB_GET_FONT_PATH 52")]
    public const int XCB_GET_FONT_PATH = 52;

    [NativeTypeName("#define XCB_CREATE_PIXMAP 53")]
    public const int XCB_CREATE_PIXMAP = 53;

    [NativeTypeName("#define XCB_FREE_PIXMAP 54")]
    public const int XCB_FREE_PIXMAP = 54;

    [NativeTypeName("#define XCB_CREATE_GC 55")]
    public const int XCB_CREATE_GC = 55;

    [NativeTypeName("#define XCB_CHANGE_GC 56")]
    public const int XCB_CHANGE_GC = 56;

    [NativeTypeName("#define XCB_COPY_GC 57")]
    public const int XCB_COPY_GC = 57;

    [NativeTypeName("#define XCB_SET_DASHES 58")]
    public const int XCB_SET_DASHES = 58;

    [NativeTypeName("#define XCB_SET_CLIP_RECTANGLES 59")]
    public const int XCB_SET_CLIP_RECTANGLES = 59;

    [NativeTypeName("#define XCB_FREE_GC 60")]
    public const int XCB_FREE_GC = 60;

    [NativeTypeName("#define XCB_CLEAR_AREA 61")]
    public const int XCB_CLEAR_AREA = 61;

    [NativeTypeName("#define XCB_COPY_AREA 62")]
    public const int XCB_COPY_AREA = 62;

    [NativeTypeName("#define XCB_COPY_PLANE 63")]
    public const int XCB_COPY_PLANE = 63;

    [NativeTypeName("#define XCB_POLY_POINT 64")]
    public const int XCB_POLY_POINT = 64;

    [NativeTypeName("#define XCB_POLY_LINE 65")]
    public const int XCB_POLY_LINE = 65;

    [NativeTypeName("#define XCB_POLY_SEGMENT 66")]
    public const int XCB_POLY_SEGMENT = 66;

    [NativeTypeName("#define XCB_POLY_RECTANGLE 67")]
    public const int XCB_POLY_RECTANGLE = 67;

    [NativeTypeName("#define XCB_POLY_ARC 68")]
    public const int XCB_POLY_ARC = 68;

    [NativeTypeName("#define XCB_FILL_POLY 69")]
    public const int XCB_FILL_POLY = 69;

    [NativeTypeName("#define XCB_POLY_FILL_RECTANGLE 70")]
    public const int XCB_POLY_FILL_RECTANGLE = 70;

    [NativeTypeName("#define XCB_POLY_FILL_ARC 71")]
    public const int XCB_POLY_FILL_ARC = 71;

    [NativeTypeName("#define XCB_PUT_IMAGE 72")]
    public const int XCB_PUT_IMAGE = 72;

    [NativeTypeName("#define XCB_GET_IMAGE 73")]
    public const int XCB_GET_IMAGE = 73;

    [NativeTypeName("#define XCB_POLY_TEXT_8 74")]
    public const int XCB_POLY_TEXT_8 = 74;

    [NativeTypeName("#define XCB_POLY_TEXT_16 75")]
    public const int XCB_POLY_TEXT_16 = 75;

    [NativeTypeName("#define XCB_IMAGE_TEXT_8 76")]
    public const int XCB_IMAGE_TEXT_8 = 76;

    [NativeTypeName("#define XCB_IMAGE_TEXT_16 77")]
    public const int XCB_IMAGE_TEXT_16 = 77;

    [NativeTypeName("#define XCB_CREATE_COLORMAP 78")]
    public const int XCB_CREATE_COLORMAP = 78;

    [NativeTypeName("#define XCB_FREE_COLORMAP 79")]
    public const int XCB_FREE_COLORMAP = 79;

    [NativeTypeName("#define XCB_COPY_COLORMAP_AND_FREE 80")]
    public const int XCB_COPY_COLORMAP_AND_FREE = 80;

    [NativeTypeName("#define XCB_INSTALL_COLORMAP 81")]
    public const int XCB_INSTALL_COLORMAP = 81;

    [NativeTypeName("#define XCB_UNINSTALL_COLORMAP 82")]
    public const int XCB_UNINSTALL_COLORMAP = 82;

    [NativeTypeName("#define XCB_LIST_INSTALLED_COLORMAPS 83")]
    public const int XCB_LIST_INSTALLED_COLORMAPS = 83;

    [NativeTypeName("#define XCB_ALLOC_COLOR 84")]
    public const int XCB_ALLOC_COLOR = 84;

    [NativeTypeName("#define XCB_ALLOC_NAMED_COLOR 85")]
    public const int XCB_ALLOC_NAMED_COLOR = 85;

    [NativeTypeName("#define XCB_ALLOC_COLOR_CELLS 86")]
    public const int XCB_ALLOC_COLOR_CELLS = 86;

    [NativeTypeName("#define XCB_ALLOC_COLOR_PLANES 87")]
    public const int XCB_ALLOC_COLOR_PLANES = 87;

    [NativeTypeName("#define XCB_FREE_COLORS 88")]
    public const int XCB_FREE_COLORS = 88;

    [NativeTypeName("#define XCB_STORE_COLORS 89")]
    public const int XCB_STORE_COLORS = 89;

    [NativeTypeName("#define XCB_STORE_NAMED_COLOR 90")]
    public const int XCB_STORE_NAMED_COLOR = 90;

    [NativeTypeName("#define XCB_QUERY_COLORS 91")]
    public const int XCB_QUERY_COLORS = 91;

    [NativeTypeName("#define XCB_LOOKUP_COLOR 92")]
    public const int XCB_LOOKUP_COLOR = 92;

    [NativeTypeName("#define XCB_CREATE_CURSOR 93")]
    public const int XCB_CREATE_CURSOR = 93;

    [NativeTypeName("#define XCB_CREATE_GLYPH_CURSOR 94")]
    public const int XCB_CREATE_GLYPH_CURSOR = 94;

    [NativeTypeName("#define XCB_FREE_CURSOR 95")]
    public const int XCB_FREE_CURSOR = 95;

    [NativeTypeName("#define XCB_RECOLOR_CURSOR 96")]
    public const int XCB_RECOLOR_CURSOR = 96;

    [NativeTypeName("#define XCB_QUERY_BEST_SIZE 97")]
    public const int XCB_QUERY_BEST_SIZE = 97;

    [NativeTypeName("#define XCB_QUERY_EXTENSION 98")]
    public const int XCB_QUERY_EXTENSION = 98;

    [NativeTypeName("#define XCB_LIST_EXTENSIONS 99")]
    public const int XCB_LIST_EXTENSIONS = 99;

    [NativeTypeName("#define XCB_CHANGE_KEYBOARD_MAPPING 100")]
    public const int XCB_CHANGE_KEYBOARD_MAPPING = 100;

    [NativeTypeName("#define XCB_GET_KEYBOARD_MAPPING 101")]
    public const int XCB_GET_KEYBOARD_MAPPING = 101;

    [NativeTypeName("#define XCB_CHANGE_KEYBOARD_CONTROL 102")]
    public const int XCB_CHANGE_KEYBOARD_CONTROL = 102;

    [NativeTypeName("#define XCB_GET_KEYBOARD_CONTROL 103")]
    public const int XCB_GET_KEYBOARD_CONTROL = 103;

    [NativeTypeName("#define XCB_BELL 104")]
    public const int XCB_BELL = 104;

    [NativeTypeName("#define XCB_CHANGE_POINTER_CONTROL 105")]
    public const int XCB_CHANGE_POINTER_CONTROL = 105;

    [NativeTypeName("#define XCB_GET_POINTER_CONTROL 106")]
    public const int XCB_GET_POINTER_CONTROL = 106;

    [NativeTypeName("#define XCB_SET_SCREEN_SAVER 107")]
    public const int XCB_SET_SCREEN_SAVER = 107;

    [NativeTypeName("#define XCB_GET_SCREEN_SAVER 108")]
    public const int XCB_GET_SCREEN_SAVER = 108;

    [NativeTypeName("#define XCB_CHANGE_HOSTS 109")]
    public const int XCB_CHANGE_HOSTS = 109;

    [NativeTypeName("#define XCB_LIST_HOSTS 110")]
    public const int XCB_LIST_HOSTS = 110;

    [NativeTypeName("#define XCB_SET_ACCESS_CONTROL 111")]
    public const int XCB_SET_ACCESS_CONTROL = 111;

    [NativeTypeName("#define XCB_SET_CLOSE_DOWN_MODE 112")]
    public const int XCB_SET_CLOSE_DOWN_MODE = 112;

    [NativeTypeName("#define XCB_KILL_CLIENT 113")]
    public const int XCB_KILL_CLIENT = 113;

    [NativeTypeName("#define XCB_ROTATE_PROPERTIES 114")]
    public const int XCB_ROTATE_PROPERTIES = 114;

    [NativeTypeName("#define XCB_FORCE_SCREEN_SAVER 115")]
    public const int XCB_FORCE_SCREEN_SAVER = 115;

    [NativeTypeName("#define XCB_SET_POINTER_MAPPING 116")]
    public const int XCB_SET_POINTER_MAPPING = 116;

    [NativeTypeName("#define XCB_GET_POINTER_MAPPING 117")]
    public const int XCB_GET_POINTER_MAPPING = 117;

    [NativeTypeName("#define XCB_SET_MODIFIER_MAPPING 118")]
    public const int XCB_SET_MODIFIER_MAPPING = 118;

    [NativeTypeName("#define XCB_GET_MODIFIER_MAPPING 119")]
    public const int XCB_GET_MODIFIER_MAPPING = 119;

    [NativeTypeName("#define XCB_NO_OPERATION 127")]
    public const int XCB_NO_OPERATION = 127;

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_char2b_next(xcb_char2b_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_char2b_end(xcb_char2b_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_window_next(xcb_window_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_window_end(xcb_window_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_pixmap_next(xcb_pixmap_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_pixmap_end(xcb_pixmap_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_cursor_next(xcb_cursor_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_cursor_end(xcb_cursor_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_font_next(xcb_font_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_font_end(xcb_font_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_gcontext_next(xcb_gcontext_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_gcontext_end(xcb_gcontext_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_colormap_next(xcb_colormap_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_colormap_end(xcb_colormap_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_atom_next(xcb_atom_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_atom_end(xcb_atom_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_drawable_next(xcb_drawable_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_drawable_end(xcb_drawable_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_fontable_next(xcb_fontable_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_fontable_end(xcb_fontable_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_bool32_next(xcb_bool32_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_bool32_end(xcb_bool32_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_visualid_next(xcb_visualid_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_visualid_end(xcb_visualid_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_timestamp_next(xcb_timestamp_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_timestamp_end(xcb_timestamp_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_keysym_next(xcb_keysym_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_keysym_end(xcb_keysym_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_keycode_next(xcb_keycode_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_keycode_end(xcb_keycode_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_keycode32_next(xcb_keycode32_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_keycode32_end(xcb_keycode32_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_button_next(xcb_button_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_button_end(xcb_button_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_point_next(xcb_point_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_point_end(xcb_point_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_rectangle_next(xcb_rectangle_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_rectangle_end(xcb_rectangle_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_arc_next(xcb_arc_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_arc_end(xcb_arc_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_format_next(xcb_format_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_format_end(xcb_format_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_visualtype_next(xcb_visualtype_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_visualtype_end(xcb_visualtype_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_depth_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_visualtype_t* xcb_depth_visuals([NativeTypeName("const xcb_depth_t *")] xcb_depth_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_depth_visuals_length([NativeTypeName("const xcb_depth_t *")] xcb_depth_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_visualtype_iterator_t xcb_depth_visuals_iterator([NativeTypeName("const xcb_depth_t *")] xcb_depth_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_depth_next(xcb_depth_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_depth_end(xcb_depth_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_screen_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_screen_allowed_depths_length([NativeTypeName("const xcb_screen_t *")] xcb_screen_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_depth_iterator_t xcb_screen_allowed_depths_iterator([NativeTypeName("const xcb_screen_t *")] xcb_screen_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_screen_next(xcb_screen_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_screen_end(xcb_screen_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_request_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_setup_request_authorization_protocol_name([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_request_authorization_protocol_name_length([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_request_authorization_protocol_name_end([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_setup_request_authorization_protocol_data([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_request_authorization_protocol_data_length([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_request_authorization_protocol_data_end([NativeTypeName("const xcb_setup_request_t *")] xcb_setup_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_setup_request_next(xcb_setup_request_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_request_end(xcb_setup_request_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_failed_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_setup_failed_reason([NativeTypeName("const xcb_setup_failed_t *")] xcb_setup_failed_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_failed_reason_length([NativeTypeName("const xcb_setup_failed_t *")] xcb_setup_failed_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_failed_reason_end([NativeTypeName("const xcb_setup_failed_t *")] xcb_setup_failed_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_setup_failed_next(xcb_setup_failed_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_failed_end(xcb_setup_failed_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_authenticate_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_setup_authenticate_reason([NativeTypeName("const xcb_setup_authenticate_t *")] xcb_setup_authenticate_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_authenticate_reason_length([NativeTypeName("const xcb_setup_authenticate_t *")] xcb_setup_authenticate_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_authenticate_reason_end([NativeTypeName("const xcb_setup_authenticate_t *")] xcb_setup_authenticate_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_setup_authenticate_next(xcb_setup_authenticate_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_authenticate_end(xcb_setup_authenticate_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_setup_vendor([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_vendor_length([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_vendor_end([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_format_t* xcb_setup_pixmap_formats([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_pixmap_formats_length([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_format_iterator_t xcb_setup_pixmap_formats_iterator([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_setup_roots_length([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_screen_iterator_t xcb_setup_roots_iterator([NativeTypeName("const xcb_setup_t *")] xcb_setup_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_setup_next(xcb_setup_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_setup_end(xcb_setup_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_client_message_data_next(xcb_client_message_data_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_client_message_data_end(xcb_client_message_data_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_window_value_list_serialize(void** _buffer, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_window_value_list_t *")] xcb_create_window_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_window_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask, xcb_create_window_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_window_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_window_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_window_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_window_t")] uint wid, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint16_t")] ushort border_width, [NativeTypeName("uint16_t")] ushort _class, [NativeTypeName("xcb_visualid_t")] uint visual, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_window(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_window_t")] uint wid, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint16_t")] ushort border_width, [NativeTypeName("uint16_t")] ushort _class, [NativeTypeName("xcb_visualid_t")] uint visual, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_window_aux_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_window_t")] uint wid, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint16_t")] ushort border_width, [NativeTypeName("uint16_t")] ushort _class, [NativeTypeName("xcb_visualid_t")] uint visual, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_window_value_list_t *")] xcb_create_window_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_window_aux(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_window_t")] uint wid, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint16_t")] ushort border_width, [NativeTypeName("uint16_t")] ushort _class, [NativeTypeName("xcb_visualid_t")] uint visual, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_window_value_list_t *")] xcb_create_window_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_create_window_value_list([NativeTypeName("const xcb_create_window_request_t *")] xcb_create_window_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_window_attributes_value_list_serialize(void** _buffer, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_window_attributes_value_list_t *")] xcb_change_window_attributes_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_window_attributes_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask, xcb_change_window_attributes_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_window_attributes_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_window_attributes_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_window_attributes_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_window_attributes(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_window_attributes_aux_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_window_attributes_value_list_t *")] xcb_change_window_attributes_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_window_attributes_aux(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_window_attributes_value_list_t *")] xcb_change_window_attributes_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_change_window_attributes_value_list([NativeTypeName("const xcb_change_window_attributes_request_t *")] xcb_change_window_attributes_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_window_attributes_cookie_t xcb_get_window_attributes(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_window_attributes_cookie_t xcb_get_window_attributes_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_window_attributes_reply_t* xcb_get_window_attributes_reply(xcb_connection_t* c, xcb_get_window_attributes_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_destroy_window_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_destroy_window(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_destroy_subwindows_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_destroy_subwindows(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_save_set_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_save_set(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_reparent_window_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_reparent_window(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_window_t")] uint parent, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_map_window_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_map_window(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_map_subwindows_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_map_subwindows(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_unmap_window_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_unmap_window(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_unmap_subwindows_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_unmap_subwindows(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_configure_window_value_list_serialize(void** _buffer, [NativeTypeName("uint16_t")] ushort value_mask, [NativeTypeName("const xcb_configure_window_value_list_t *")] xcb_configure_window_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_configure_window_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint16_t")] ushort value_mask, xcb_configure_window_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_configure_window_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint16_t")] ushort value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_configure_window_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_configure_window_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_configure_window(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_configure_window_aux_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort value_mask, [NativeTypeName("const xcb_configure_window_value_list_t *")] xcb_configure_window_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_configure_window_aux(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort value_mask, [NativeTypeName("const xcb_configure_window_value_list_t *")] xcb_configure_window_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_configure_window_value_list([NativeTypeName("const xcb_configure_window_request_t *")] xcb_configure_window_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_circulate_window_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte direction, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_circulate_window(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte direction, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_geometry_cookie_t xcb_get_geometry(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_geometry_cookie_t xcb_get_geometry_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_geometry_reply_t* xcb_get_geometry_reply(xcb_connection_t* c, xcb_get_geometry_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_tree_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_tree_cookie_t xcb_query_tree(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_tree_cookie_t xcb_query_tree_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_window_t *")]
    public static extern uint* xcb_query_tree_children([NativeTypeName("const xcb_query_tree_reply_t *")] xcb_query_tree_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_tree_children_length([NativeTypeName("const xcb_query_tree_reply_t *")] xcb_query_tree_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_query_tree_children_end([NativeTypeName("const xcb_query_tree_reply_t *")] xcb_query_tree_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_tree_reply_t* xcb_query_tree_reply(xcb_connection_t* c, xcb_query_tree_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_intern_atom_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_intern_atom_cookie_t xcb_intern_atom(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte only_if_exists, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_intern_atom_cookie_t xcb_intern_atom_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte only_if_exists, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_intern_atom_reply_t* xcb_intern_atom_reply(xcb_connection_t* c, xcb_intern_atom_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_atom_name_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_atom_name_cookie_t xcb_get_atom_name(xcb_connection_t* c, [NativeTypeName("xcb_atom_t")] uint atom);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_atom_name_cookie_t xcb_get_atom_name_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_atom_t")] uint atom);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_get_atom_name_name([NativeTypeName("const xcb_get_atom_name_reply_t *")] xcb_get_atom_name_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_atom_name_name_length([NativeTypeName("const xcb_get_atom_name_reply_t *")] xcb_get_atom_name_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_atom_name_name_end([NativeTypeName("const xcb_get_atom_name_reply_t *")] xcb_get_atom_name_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_atom_name_reply_t* xcb_get_atom_name_reply(xcb_connection_t* c, xcb_get_atom_name_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_property_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_property_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_atom_t")] uint type, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("uint32_t")] uint data_len, [NativeTypeName("const void *")] void* data);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_property(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_atom_t")] uint type, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("uint32_t")] uint data_len, [NativeTypeName("const void *")] void* data);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_change_property_data([NativeTypeName("const xcb_change_property_request_t *")] xcb_change_property_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_property_data_length([NativeTypeName("const xcb_change_property_request_t *")] xcb_change_property_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_change_property_data_end([NativeTypeName("const xcb_change_property_request_t *")] xcb_change_property_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_delete_property_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_delete_property(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_property_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_property_cookie_t xcb_get_property(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte _delete, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_atom_t")] uint type, [NativeTypeName("uint32_t")] uint long_offset, [NativeTypeName("uint32_t")] uint long_length);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_property_cookie_t xcb_get_property_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte _delete, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_atom_t")] uint type, [NativeTypeName("uint32_t")] uint long_offset, [NativeTypeName("uint32_t")] uint long_length);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_get_property_value([NativeTypeName("const xcb_get_property_reply_t *")] xcb_get_property_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_property_value_length([NativeTypeName("const xcb_get_property_reply_t *")] xcb_get_property_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_property_value_end([NativeTypeName("const xcb_get_property_reply_t *")] xcb_get_property_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_property_reply_t* xcb_get_property_reply(xcb_connection_t* c, xcb_get_property_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_properties_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_properties_cookie_t xcb_list_properties(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_properties_cookie_t xcb_list_properties_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_atom_t *")]
    public static extern uint* xcb_list_properties_atoms([NativeTypeName("const xcb_list_properties_reply_t *")] xcb_list_properties_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_properties_atoms_length([NativeTypeName("const xcb_list_properties_reply_t *")] xcb_list_properties_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_list_properties_atoms_end([NativeTypeName("const xcb_list_properties_reply_t *")] xcb_list_properties_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_properties_reply_t* xcb_list_properties_reply(xcb_connection_t* c, xcb_list_properties_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_selection_owner_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint owner, [NativeTypeName("xcb_atom_t")] uint selection, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_selection_owner(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint owner, [NativeTypeName("xcb_atom_t")] uint selection, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_selection_owner_cookie_t xcb_get_selection_owner(xcb_connection_t* c, [NativeTypeName("xcb_atom_t")] uint selection);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_selection_owner_cookie_t xcb_get_selection_owner_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_atom_t")] uint selection);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_selection_owner_reply_t* xcb_get_selection_owner_reply(xcb_connection_t* c, xcb_get_selection_owner_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_convert_selection_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint requestor, [NativeTypeName("xcb_atom_t")] uint selection, [NativeTypeName("xcb_atom_t")] uint target, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_convert_selection(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint requestor, [NativeTypeName("xcb_atom_t")] uint selection, [NativeTypeName("xcb_atom_t")] uint target, [NativeTypeName("xcb_atom_t")] uint property, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_send_event_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte propagate, [NativeTypeName("xcb_window_t")] uint destination, [NativeTypeName("uint32_t")] uint event_mask, [NativeTypeName("const char *")] sbyte* @event);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_send_event(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte propagate, [NativeTypeName("xcb_window_t")] uint destination, [NativeTypeName("uint32_t")] uint event_mask, [NativeTypeName("const char *")] sbyte* @event);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_pointer_cookie_t xcb_grab_pointer(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort event_mask, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode, [NativeTypeName("xcb_window_t")] uint confine_to, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_pointer_cookie_t xcb_grab_pointer_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort event_mask, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode, [NativeTypeName("xcb_window_t")] uint confine_to, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_pointer_reply_t* xcb_grab_pointer_reply(xcb_connection_t* c, xcb_grab_pointer_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_pointer_checked(xcb_connection_t* c, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_pointer(xcb_connection_t* c, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_button_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort event_mask, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode, [NativeTypeName("xcb_window_t")] uint confine_to, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("uint8_t")] byte button, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_button(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort event_mask, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode, [NativeTypeName("xcb_window_t")] uint confine_to, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("uint8_t")] byte button, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_button_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte button, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_button(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte button, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_active_pointer_grab_checked(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("xcb_timestamp_t")] uint time, [NativeTypeName("uint16_t")] ushort event_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_active_pointer_grab(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("xcb_timestamp_t")] uint time, [NativeTypeName("uint16_t")] ushort event_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_keyboard_cookie_t xcb_grab_keyboard(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("xcb_timestamp_t")] uint time, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_keyboard_cookie_t xcb_grab_keyboard_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("xcb_timestamp_t")] uint time, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_grab_keyboard_reply_t* xcb_grab_keyboard_reply(xcb_connection_t* c, xcb_grab_keyboard_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_keyboard_checked(xcb_connection_t* c, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_keyboard(xcb_connection_t* c, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_key_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers, [NativeTypeName("xcb_keycode_t")] byte key, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_key(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte owner_events, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers, [NativeTypeName("xcb_keycode_t")] byte key, [NativeTypeName("uint8_t")] byte pointer_mode, [NativeTypeName("uint8_t")] byte keyboard_mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_key_checked(xcb_connection_t* c, [NativeTypeName("xcb_keycode_t")] byte key, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_key(xcb_connection_t* c, [NativeTypeName("xcb_keycode_t")] byte key, [NativeTypeName("xcb_window_t")] uint grab_window, [NativeTypeName("uint16_t")] ushort modifiers);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_allow_events_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_allow_events(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_server_checked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_grab_server(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_server_checked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_ungrab_server(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_pointer_cookie_t xcb_query_pointer(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_pointer_cookie_t xcb_query_pointer_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_pointer_reply_t* xcb_query_pointer_reply(xcb_connection_t* c, xcb_query_pointer_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_timecoord_next(xcb_timecoord_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_timecoord_end(xcb_timecoord_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_motion_events_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_motion_events_cookie_t xcb_get_motion_events(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_timestamp_t")] uint start, [NativeTypeName("xcb_timestamp_t")] uint stop);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_motion_events_cookie_t xcb_get_motion_events_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_timestamp_t")] uint start, [NativeTypeName("xcb_timestamp_t")] uint stop);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_timecoord_t* xcb_get_motion_events_events([NativeTypeName("const xcb_get_motion_events_reply_t *")] xcb_get_motion_events_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_motion_events_events_length([NativeTypeName("const xcb_get_motion_events_reply_t *")] xcb_get_motion_events_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_timecoord_iterator_t xcb_get_motion_events_events_iterator([NativeTypeName("const xcb_get_motion_events_reply_t *")] xcb_get_motion_events_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_motion_events_reply_t* xcb_get_motion_events_reply(xcb_connection_t* c, xcb_get_motion_events_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_translate_coordinates_cookie_t xcb_translate_coordinates(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint src_window, [NativeTypeName("xcb_window_t")] uint dst_window, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_translate_coordinates_cookie_t xcb_translate_coordinates_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint src_window, [NativeTypeName("xcb_window_t")] uint dst_window, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_translate_coordinates_reply_t* xcb_translate_coordinates_reply(xcb_connection_t* c, xcb_translate_coordinates_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_warp_pointer_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint src_window, [NativeTypeName("xcb_window_t")] uint dst_window, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("uint16_t")] ushort src_width, [NativeTypeName("uint16_t")] ushort src_height, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_warp_pointer(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint src_window, [NativeTypeName("xcb_window_t")] uint dst_window, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("uint16_t")] ushort src_width, [NativeTypeName("uint16_t")] ushort src_height, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_input_focus_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte revert_to, [NativeTypeName("xcb_window_t")] uint focus, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_input_focus(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte revert_to, [NativeTypeName("xcb_window_t")] uint focus, [NativeTypeName("xcb_timestamp_t")] uint time);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_input_focus_cookie_t xcb_get_input_focus(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_input_focus_cookie_t xcb_get_input_focus_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_input_focus_reply_t* xcb_get_input_focus_reply(xcb_connection_t* c, xcb_get_input_focus_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_keymap_cookie_t xcb_query_keymap(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_keymap_cookie_t xcb_query_keymap_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_keymap_reply_t* xcb_query_keymap_reply(xcb_connection_t* c, xcb_query_keymap_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_open_font_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_open_font_checked(xcb_connection_t* c, [NativeTypeName("xcb_font_t")] uint fid, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_open_font(xcb_connection_t* c, [NativeTypeName("xcb_font_t")] uint fid, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_open_font_name([NativeTypeName("const xcb_open_font_request_t *")] xcb_open_font_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_open_font_name_length([NativeTypeName("const xcb_open_font_request_t *")] xcb_open_font_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_open_font_name_end([NativeTypeName("const xcb_open_font_request_t *")] xcb_open_font_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_close_font_checked(xcb_connection_t* c, [NativeTypeName("xcb_font_t")] uint font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_close_font(xcb_connection_t* c, [NativeTypeName("xcb_font_t")] uint font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_fontprop_next(xcb_fontprop_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_fontprop_end(xcb_fontprop_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_charinfo_next(xcb_charinfo_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_charinfo_end(xcb_charinfo_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_font_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_font_cookie_t xcb_query_font(xcb_connection_t* c, [NativeTypeName("xcb_fontable_t")] uint font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_font_cookie_t xcb_query_font_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_fontable_t")] uint font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_fontprop_t* xcb_query_font_properties([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_font_properties_length([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_fontprop_iterator_t xcb_query_font_properties_iterator([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_charinfo_t* xcb_query_font_char_infos([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_font_char_infos_length([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_charinfo_iterator_t xcb_query_font_char_infos_iterator([NativeTypeName("const xcb_query_font_reply_t *")] xcb_query_font_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_font_reply_t* xcb_query_font_reply(xcb_connection_t* c, xcb_query_font_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_text_extents_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint string_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_text_extents_cookie_t xcb_query_text_extents(xcb_connection_t* c, [NativeTypeName("xcb_fontable_t")] uint font, [NativeTypeName("uint32_t")] uint string_len, [NativeTypeName("const xcb_char2b_t *")] xcb_char2b_t* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_text_extents_cookie_t xcb_query_text_extents_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_fontable_t")] uint font, [NativeTypeName("uint32_t")] uint string_len, [NativeTypeName("const xcb_char2b_t *")] xcb_char2b_t* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_text_extents_reply_t* xcb_query_text_extents_reply(xcb_connection_t* c, xcb_query_text_extents_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_str_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_str_name([NativeTypeName("const xcb_str_t *")] xcb_str_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_str_name_length([NativeTypeName("const xcb_str_t *")] xcb_str_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_str_name_end([NativeTypeName("const xcb_str_t *")] xcb_str_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_str_next(xcb_str_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_str_end(xcb_str_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_fonts_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_cookie_t xcb_list_fonts(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort max_names, [NativeTypeName("uint16_t")] ushort pattern_len, [NativeTypeName("const char *")] sbyte* pattern);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_cookie_t xcb_list_fonts_unchecked(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort max_names, [NativeTypeName("uint16_t")] ushort pattern_len, [NativeTypeName("const char *")] sbyte* pattern);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_fonts_names_length([NativeTypeName("const xcb_list_fonts_reply_t *")] xcb_list_fonts_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_str_iterator_t xcb_list_fonts_names_iterator([NativeTypeName("const xcb_list_fonts_reply_t *")] xcb_list_fonts_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_reply_t* xcb_list_fonts_reply(xcb_connection_t* c, xcb_list_fonts_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_fonts_with_info_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_with_info_cookie_t xcb_list_fonts_with_info(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort max_names, [NativeTypeName("uint16_t")] ushort pattern_len, [NativeTypeName("const char *")] sbyte* pattern);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_with_info_cookie_t xcb_list_fonts_with_info_unchecked(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort max_names, [NativeTypeName("uint16_t")] ushort pattern_len, [NativeTypeName("const char *")] sbyte* pattern);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_fontprop_t* xcb_list_fonts_with_info_properties([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_fonts_with_info_properties_length([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_fontprop_iterator_t xcb_list_fonts_with_info_properties_iterator([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_list_fonts_with_info_name([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_fonts_with_info_name_length([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_list_fonts_with_info_name_end([NativeTypeName("const xcb_list_fonts_with_info_reply_t *")] xcb_list_fonts_with_info_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_fonts_with_info_reply_t* xcb_list_fonts_with_info_reply(xcb_connection_t* c, xcb_list_fonts_with_info_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_font_path_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_font_path_checked(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort font_qty, [NativeTypeName("const xcb_str_t *")] xcb_str_t* font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_font_path(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort font_qty, [NativeTypeName("const xcb_str_t *")] xcb_str_t* font);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_font_path_font_length([NativeTypeName("const xcb_set_font_path_request_t *")] xcb_set_font_path_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_str_iterator_t xcb_set_font_path_font_iterator([NativeTypeName("const xcb_set_font_path_request_t *")] xcb_set_font_path_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_font_path_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_font_path_cookie_t xcb_get_font_path(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_font_path_cookie_t xcb_get_font_path_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_font_path_path_length([NativeTypeName("const xcb_get_font_path_reply_t *")] xcb_get_font_path_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_str_iterator_t xcb_get_font_path_path_iterator([NativeTypeName("const xcb_get_font_path_reply_t *")] xcb_get_font_path_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_font_path_reply_t* xcb_get_font_path_reply(xcb_connection_t* c, xcb_get_font_path_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_pixmap_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_pixmap_t")] uint pid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_pixmap(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("xcb_pixmap_t")] uint pid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_pixmap_checked(xcb_connection_t* c, [NativeTypeName("xcb_pixmap_t")] uint pixmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_pixmap(xcb_connection_t* c, [NativeTypeName("xcb_pixmap_t")] uint pixmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_gc_value_list_serialize(void** _buffer, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_gc_value_list_t *")] xcb_create_gc_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_gc_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask, xcb_create_gc_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_gc_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_create_gc_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_gc_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint cid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_gc(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint cid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_gc_aux_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint cid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_gc_value_list_t *")] xcb_create_gc_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_gc_aux(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint cid, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_create_gc_value_list_t *")] xcb_create_gc_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_create_gc_value_list([NativeTypeName("const xcb_create_gc_request_t *")] xcb_create_gc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_gc_value_list_serialize(void** _buffer, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_gc_value_list_t *")] xcb_change_gc_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_gc_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask, xcb_change_gc_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_gc_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_gc_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_gc_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_gc(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_gc_aux_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_gc_value_list_t *")] xcb_change_gc_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_gc_aux(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_gc_value_list_t *")] xcb_change_gc_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_change_gc_value_list([NativeTypeName("const xcb_change_gc_request_t *")] xcb_change_gc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_gc_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint src_gc, [NativeTypeName("xcb_gcontext_t")] uint dst_gc, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_gc(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint src_gc, [NativeTypeName("xcb_gcontext_t")] uint dst_gc, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_dashes_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_dashes_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint16_t")] ushort dash_offset, [NativeTypeName("uint16_t")] ushort dashes_len, [NativeTypeName("const uint8_t *")] byte* dashes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_dashes(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint16_t")] ushort dash_offset, [NativeTypeName("uint16_t")] ushort dashes_len, [NativeTypeName("const uint8_t *")] byte* dashes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_set_dashes_dashes([NativeTypeName("const xcb_set_dashes_request_t *")] xcb_set_dashes_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_dashes_dashes_length([NativeTypeName("const xcb_set_dashes_request_t *")] xcb_set_dashes_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_set_dashes_dashes_end([NativeTypeName("const xcb_set_dashes_request_t *")] xcb_set_dashes_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_clip_rectangles_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint rectangles_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_clip_rectangles_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte ordering, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short clip_x_origin, [NativeTypeName("int16_t")] short clip_y_origin, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_clip_rectangles(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte ordering, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short clip_x_origin, [NativeTypeName("int16_t")] short clip_y_origin, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_t* xcb_set_clip_rectangles_rectangles([NativeTypeName("const xcb_set_clip_rectangles_request_t *")] xcb_set_clip_rectangles_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_clip_rectangles_rectangles_length([NativeTypeName("const xcb_set_clip_rectangles_request_t *")] xcb_set_clip_rectangles_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_iterator_t xcb_set_clip_rectangles_rectangles_iterator([NativeTypeName("const xcb_set_clip_rectangles_request_t *")] xcb_set_clip_rectangles_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_gc_checked(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_gc(xcb_connection_t* c, [NativeTypeName("xcb_gcontext_t")] uint gc);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_clear_area_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte exposures, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_clear_area(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte exposures, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_area_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint src_drawable, [NativeTypeName("xcb_drawable_t")] uint dst_drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_area(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint src_drawable, [NativeTypeName("xcb_drawable_t")] uint dst_drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_plane_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint src_drawable, [NativeTypeName("xcb_drawable_t")] uint dst_drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint32_t")] uint bit_plane);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_plane(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint src_drawable, [NativeTypeName("xcb_drawable_t")] uint dst_drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short src_x, [NativeTypeName("int16_t")] short src_y, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint32_t")] uint bit_plane);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_point_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint points_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_point_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_point(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_t* xcb_poly_point_points([NativeTypeName("const xcb_poly_point_request_t *")] xcb_poly_point_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_point_points_length([NativeTypeName("const xcb_poly_point_request_t *")] xcb_poly_point_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_iterator_t xcb_poly_point_points_iterator([NativeTypeName("const xcb_poly_point_request_t *")] xcb_poly_point_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_line_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint points_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_line_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_line(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_t* xcb_poly_line_points([NativeTypeName("const xcb_poly_line_request_t *")] xcb_poly_line_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_line_points_length([NativeTypeName("const xcb_poly_line_request_t *")] xcb_poly_line_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_iterator_t xcb_poly_line_points_iterator([NativeTypeName("const xcb_poly_line_request_t *")] xcb_poly_line_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_segment_next(xcb_segment_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_segment_end(xcb_segment_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_segment_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint segments_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_segment_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint segments_len, [NativeTypeName("const xcb_segment_t *")] xcb_segment_t* segments);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_segment(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint segments_len, [NativeTypeName("const xcb_segment_t *")] xcb_segment_t* segments);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_segment_t* xcb_poly_segment_segments([NativeTypeName("const xcb_poly_segment_request_t *")] xcb_poly_segment_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_segment_segments_length([NativeTypeName("const xcb_poly_segment_request_t *")] xcb_poly_segment_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_segment_iterator_t xcb_poly_segment_segments_iterator([NativeTypeName("const xcb_poly_segment_request_t *")] xcb_poly_segment_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_rectangle_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint rectangles_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_rectangle_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_rectangle(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_t* xcb_poly_rectangle_rectangles([NativeTypeName("const xcb_poly_rectangle_request_t *")] xcb_poly_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_rectangle_rectangles_length([NativeTypeName("const xcb_poly_rectangle_request_t *")] xcb_poly_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_iterator_t xcb_poly_rectangle_rectangles_iterator([NativeTypeName("const xcb_poly_rectangle_request_t *")] xcb_poly_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_arc_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint arcs_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_arc_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint arcs_len, [NativeTypeName("const xcb_arc_t *")] xcb_arc_t* arcs);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_arc(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint arcs_len, [NativeTypeName("const xcb_arc_t *")] xcb_arc_t* arcs);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_arc_t* xcb_poly_arc_arcs([NativeTypeName("const xcb_poly_arc_request_t *")] xcb_poly_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_arc_arcs_length([NativeTypeName("const xcb_poly_arc_request_t *")] xcb_poly_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_arc_iterator_t xcb_poly_arc_arcs_iterator([NativeTypeName("const xcb_poly_arc_request_t *")] xcb_poly_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_fill_poly_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint points_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_fill_poly_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint8_t")] byte shape, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_fill_poly(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint8_t")] byte shape, [NativeTypeName("uint8_t")] byte coordinate_mode, [NativeTypeName("uint32_t")] uint points_len, [NativeTypeName("const xcb_point_t *")] xcb_point_t* points);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_t* xcb_fill_poly_points([NativeTypeName("const xcb_fill_poly_request_t *")] xcb_fill_poly_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_fill_poly_points_length([NativeTypeName("const xcb_fill_poly_request_t *")] xcb_fill_poly_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_point_iterator_t xcb_fill_poly_points_iterator([NativeTypeName("const xcb_fill_poly_request_t *")] xcb_fill_poly_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_fill_rectangle_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint rectangles_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_fill_rectangle_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_fill_rectangle(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint rectangles_len, [NativeTypeName("const xcb_rectangle_t *")] xcb_rectangle_t* rectangles);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_t* xcb_poly_fill_rectangle_rectangles([NativeTypeName("const xcb_poly_fill_rectangle_request_t *")] xcb_poly_fill_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_fill_rectangle_rectangles_length([NativeTypeName("const xcb_poly_fill_rectangle_request_t *")] xcb_poly_fill_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rectangle_iterator_t xcb_poly_fill_rectangle_rectangles_iterator([NativeTypeName("const xcb_poly_fill_rectangle_request_t *")] xcb_poly_fill_rectangle_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_fill_arc_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint arcs_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_fill_arc_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint arcs_len, [NativeTypeName("const xcb_arc_t *")] xcb_arc_t* arcs);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_fill_arc(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint32_t")] uint arcs_len, [NativeTypeName("const xcb_arc_t *")] xcb_arc_t* arcs);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_arc_t* xcb_poly_fill_arc_arcs([NativeTypeName("const xcb_poly_fill_arc_request_t *")] xcb_poly_fill_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_fill_arc_arcs_length([NativeTypeName("const xcb_poly_fill_arc_request_t *")] xcb_poly_fill_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_arc_iterator_t xcb_poly_fill_arc_arcs_iterator([NativeTypeName("const xcb_poly_fill_arc_request_t *")] xcb_poly_fill_arc_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_put_image_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint data_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_put_image_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint8_t")] byte left_pad, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("uint32_t")] uint data_len, [NativeTypeName("const uint8_t *")] byte* data);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_put_image(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("int16_t")] short dst_x, [NativeTypeName("int16_t")] short dst_y, [NativeTypeName("uint8_t")] byte left_pad, [NativeTypeName("uint8_t")] byte depth, [NativeTypeName("uint32_t")] uint data_len, [NativeTypeName("const uint8_t *")] byte* data);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_put_image_data([NativeTypeName("const xcb_put_image_request_t *")] xcb_put_image_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_put_image_data_length([NativeTypeName("const xcb_put_image_request_t *")] xcb_put_image_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_put_image_data_end([NativeTypeName("const xcb_put_image_request_t *")] xcb_put_image_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_image_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_image_cookie_t xcb_get_image(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint32_t")] uint plane_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_image_cookie_t xcb_get_image_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte format, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height, [NativeTypeName("uint32_t")] uint plane_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_get_image_data([NativeTypeName("const xcb_get_image_reply_t *")] xcb_get_image_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_image_data_length([NativeTypeName("const xcb_get_image_reply_t *")] xcb_get_image_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_image_data_end([NativeTypeName("const xcb_get_image_reply_t *")] xcb_get_image_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_image_reply_t* xcb_get_image_reply(xcb_connection_t* c, xcb_get_image_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_text_8_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint items_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_text_8_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const uint8_t *")] byte* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_text_8(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const uint8_t *")] byte* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_poly_text_8_items([NativeTypeName("const xcb_poly_text_8_request_t *")] xcb_poly_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_text_8_items_length([NativeTypeName("const xcb_poly_text_8_request_t *")] xcb_poly_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_poly_text_8_items_end([NativeTypeName("const xcb_poly_text_8_request_t *")] xcb_poly_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_text_16_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint items_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_text_16_checked(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const uint8_t *")] byte* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_poly_text_16(xcb_connection_t* c, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const uint8_t *")] byte* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_poly_text_16_items([NativeTypeName("const xcb_poly_text_16_request_t *")] xcb_poly_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_poly_text_16_items_length([NativeTypeName("const xcb_poly_text_16_request_t *")] xcb_poly_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_poly_text_16_items_end([NativeTypeName("const xcb_poly_text_16_request_t *")] xcb_poly_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_image_text_8_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_image_text_8_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte string_len, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("const char *")] sbyte* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_image_text_8(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte string_len, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("const char *")] sbyte* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_image_text_8_string([NativeTypeName("const xcb_image_text_8_request_t *")] xcb_image_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_image_text_8_string_length([NativeTypeName("const xcb_image_text_8_request_t *")] xcb_image_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_image_text_8_string_end([NativeTypeName("const xcb_image_text_8_request_t *")] xcb_image_text_8_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_image_text_16_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_image_text_16_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte string_len, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("const xcb_char2b_t *")] xcb_char2b_t* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_image_text_16(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte string_len, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("xcb_gcontext_t")] uint gc, [NativeTypeName("int16_t")] short x, [NativeTypeName("int16_t")] short y, [NativeTypeName("const xcb_char2b_t *")] xcb_char2b_t* @string);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_char2b_t* xcb_image_text_16_string([NativeTypeName("const xcb_image_text_16_request_t *")] xcb_image_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_image_text_16_string_length([NativeTypeName("const xcb_image_text_16_request_t *")] xcb_image_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_char2b_iterator_t xcb_image_text_16_string_iterator([NativeTypeName("const xcb_image_text_16_request_t *")] xcb_image_text_16_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_colormap_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte alloc, [NativeTypeName("xcb_colormap_t")] uint mid, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_visualid_t")] uint visual);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_colormap(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte alloc, [NativeTypeName("xcb_colormap_t")] uint mid, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("xcb_visualid_t")] uint visual);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_colormap_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_colormap(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_colormap_and_free_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint mid, [NativeTypeName("xcb_colormap_t")] uint src_cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_copy_colormap_and_free(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint mid, [NativeTypeName("xcb_colormap_t")] uint src_cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_install_colormap_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_install_colormap(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_uninstall_colormap_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_uninstall_colormap(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_installed_colormaps_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_installed_colormaps_cookie_t xcb_list_installed_colormaps(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_installed_colormaps_cookie_t xcb_list_installed_colormaps_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_colormap_t *")]
    public static extern uint* xcb_list_installed_colormaps_cmaps([NativeTypeName("const xcb_list_installed_colormaps_reply_t *")] xcb_list_installed_colormaps_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_installed_colormaps_cmaps_length([NativeTypeName("const xcb_list_installed_colormaps_reply_t *")] xcb_list_installed_colormaps_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_list_installed_colormaps_cmaps_end([NativeTypeName("const xcb_list_installed_colormaps_reply_t *")] xcb_list_installed_colormaps_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_installed_colormaps_reply_t* xcb_list_installed_colormaps_reply(xcb_connection_t* c, xcb_list_installed_colormaps_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_cookie_t xcb_alloc_color(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort red, [NativeTypeName("uint16_t")] ushort green, [NativeTypeName("uint16_t")] ushort blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_cookie_t xcb_alloc_color_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort red, [NativeTypeName("uint16_t")] ushort green, [NativeTypeName("uint16_t")] ushort blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_reply_t* xcb_alloc_color_reply(xcb_connection_t* c, xcb_alloc_color_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_named_color_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_named_color_cookie_t xcb_alloc_named_color(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_named_color_cookie_t xcb_alloc_named_color_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_named_color_reply_t* xcb_alloc_named_color_reply(xcb_connection_t* c, xcb_alloc_named_color_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_color_cells_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_cells_cookie_t xcb_alloc_color_cells(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte contiguous, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort colors, [NativeTypeName("uint16_t")] ushort planes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_cells_cookie_t xcb_alloc_color_cells_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte contiguous, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort colors, [NativeTypeName("uint16_t")] ushort planes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t *")]
    public static extern uint* xcb_alloc_color_cells_pixels([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_color_cells_pixels_length([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_alloc_color_cells_pixels_end([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t *")]
    public static extern uint* xcb_alloc_color_cells_masks([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_color_cells_masks_length([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_alloc_color_cells_masks_end([NativeTypeName("const xcb_alloc_color_cells_reply_t *")] xcb_alloc_color_cells_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_cells_reply_t* xcb_alloc_color_cells_reply(xcb_connection_t* c, xcb_alloc_color_cells_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_color_planes_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_planes_cookie_t xcb_alloc_color_planes(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte contiguous, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort colors, [NativeTypeName("uint16_t")] ushort reds, [NativeTypeName("uint16_t")] ushort greens, [NativeTypeName("uint16_t")] ushort blues);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_planes_cookie_t xcb_alloc_color_planes_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte contiguous, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort colors, [NativeTypeName("uint16_t")] ushort reds, [NativeTypeName("uint16_t")] ushort greens, [NativeTypeName("uint16_t")] ushort blues);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t *")]
    public static extern uint* xcb_alloc_color_planes_pixels([NativeTypeName("const xcb_alloc_color_planes_reply_t *")] xcb_alloc_color_planes_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_alloc_color_planes_pixels_length([NativeTypeName("const xcb_alloc_color_planes_reply_t *")] xcb_alloc_color_planes_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_alloc_color_planes_pixels_end([NativeTypeName("const xcb_alloc_color_planes_reply_t *")] xcb_alloc_color_planes_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_alloc_color_planes_reply_t* xcb_alloc_color_planes_reply(xcb_connection_t* c, xcb_alloc_color_planes_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_free_colors_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint pixels_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_colors_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint plane_mask, [NativeTypeName("uint32_t")] uint pixels_len, [NativeTypeName("const uint32_t *")] uint* pixels);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_colors(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint plane_mask, [NativeTypeName("uint32_t")] uint pixels_len, [NativeTypeName("const uint32_t *")] uint* pixels);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t *")]
    public static extern uint* xcb_free_colors_pixels([NativeTypeName("const xcb_free_colors_request_t *")] xcb_free_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_free_colors_pixels_length([NativeTypeName("const xcb_free_colors_request_t *")] xcb_free_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_free_colors_pixels_end([NativeTypeName("const xcb_free_colors_request_t *")] xcb_free_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_coloritem_next(xcb_coloritem_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_coloritem_end(xcb_coloritem_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_store_colors_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint items_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_store_colors_checked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const xcb_coloritem_t *")] xcb_coloritem_t* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_store_colors(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint items_len, [NativeTypeName("const xcb_coloritem_t *")] xcb_coloritem_t* items);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_coloritem_t* xcb_store_colors_items([NativeTypeName("const xcb_store_colors_request_t *")] xcb_store_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_store_colors_items_length([NativeTypeName("const xcb_store_colors_request_t *")] xcb_store_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_coloritem_iterator_t xcb_store_colors_items_iterator([NativeTypeName("const xcb_store_colors_request_t *")] xcb_store_colors_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_store_named_color_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_store_named_color_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte flags, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint pixel, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_store_named_color(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte flags, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint pixel, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* xcb_store_named_color_name([NativeTypeName("const xcb_store_named_color_request_t *")] xcb_store_named_color_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_store_named_color_name_length([NativeTypeName("const xcb_store_named_color_request_t *")] xcb_store_named_color_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_store_named_color_name_end([NativeTypeName("const xcb_store_named_color_request_t *")] xcb_store_named_color_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_rgb_next(xcb_rgb_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_rgb_end(xcb_rgb_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_colors_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint pixels_len);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_colors_cookie_t xcb_query_colors(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint pixels_len, [NativeTypeName("const uint32_t *")] uint* pixels);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_colors_cookie_t xcb_query_colors_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint32_t")] uint pixels_len, [NativeTypeName("const uint32_t *")] uint* pixels);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rgb_t* xcb_query_colors_colors([NativeTypeName("const xcb_query_colors_reply_t *")] xcb_query_colors_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_colors_colors_length([NativeTypeName("const xcb_query_colors_reply_t *")] xcb_query_colors_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_rgb_iterator_t xcb_query_colors_colors_iterator([NativeTypeName("const xcb_query_colors_reply_t *")] xcb_query_colors_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_colors_reply_t* xcb_query_colors_reply(xcb_connection_t* c, xcb_query_colors_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_lookup_color_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_lookup_color_cookie_t xcb_lookup_color(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_lookup_color_cookie_t xcb_lookup_color_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_colormap_t")] uint cmap, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_lookup_color_reply_t* xcb_lookup_color_reply(xcb_connection_t* c, xcb_lookup_color_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_cursor_checked(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cid, [NativeTypeName("xcb_pixmap_t")] uint source, [NativeTypeName("xcb_pixmap_t")] uint mask, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue, [NativeTypeName("uint16_t")] ushort x, [NativeTypeName("uint16_t")] ushort y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_cursor(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cid, [NativeTypeName("xcb_pixmap_t")] uint source, [NativeTypeName("xcb_pixmap_t")] uint mask, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue, [NativeTypeName("uint16_t")] ushort x, [NativeTypeName("uint16_t")] ushort y);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_glyph_cursor_checked(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cid, [NativeTypeName("xcb_font_t")] uint source_font, [NativeTypeName("xcb_font_t")] uint mask_font, [NativeTypeName("uint16_t")] ushort source_char, [NativeTypeName("uint16_t")] ushort mask_char, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_create_glyph_cursor(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cid, [NativeTypeName("xcb_font_t")] uint source_font, [NativeTypeName("xcb_font_t")] uint mask_font, [NativeTypeName("uint16_t")] ushort source_char, [NativeTypeName("uint16_t")] ushort mask_char, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_cursor_checked(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_free_cursor(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_recolor_cursor_checked(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_recolor_cursor(xcb_connection_t* c, [NativeTypeName("xcb_cursor_t")] uint cursor, [NativeTypeName("uint16_t")] ushort fore_red, [NativeTypeName("uint16_t")] ushort fore_green, [NativeTypeName("uint16_t")] ushort fore_blue, [NativeTypeName("uint16_t")] ushort back_red, [NativeTypeName("uint16_t")] ushort back_green, [NativeTypeName("uint16_t")] ushort back_blue);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_best_size_cookie_t xcb_query_best_size(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte _class, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_best_size_cookie_t xcb_query_best_size_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte _class, [NativeTypeName("xcb_drawable_t")] uint drawable, [NativeTypeName("uint16_t")] ushort width, [NativeTypeName("uint16_t")] ushort height);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_best_size_reply_t* xcb_query_best_size_reply(xcb_connection_t* c, xcb_query_best_size_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_query_extension_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_extension_cookie_t xcb_query_extension(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_extension_cookie_t xcb_query_extension_unchecked(xcb_connection_t* c, [NativeTypeName("uint16_t")] ushort name_len, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_query_extension_reply_t* xcb_query_extension_reply(xcb_connection_t* c, xcb_query_extension_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_extensions_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_extensions_cookie_t xcb_list_extensions(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_extensions_cookie_t xcb_list_extensions_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_extensions_names_length([NativeTypeName("const xcb_list_extensions_reply_t *")] xcb_list_extensions_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_str_iterator_t xcb_list_extensions_names_iterator([NativeTypeName("const xcb_list_extensions_reply_t *")] xcb_list_extensions_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_extensions_reply_t* xcb_list_extensions_reply(xcb_connection_t* c, xcb_list_extensions_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_mapping_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte keycode_count, [NativeTypeName("xcb_keycode_t")] byte first_keycode, [NativeTypeName("uint8_t")] byte keysyms_per_keycode, [NativeTypeName("const xcb_keysym_t *")] uint* keysyms);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_mapping(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte keycode_count, [NativeTypeName("xcb_keycode_t")] byte first_keycode, [NativeTypeName("uint8_t")] byte keysyms_per_keycode, [NativeTypeName("const xcb_keysym_t *")] uint* keysyms);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_keysym_t *")]
    public static extern uint* xcb_change_keyboard_mapping_keysyms([NativeTypeName("const xcb_change_keyboard_mapping_request_t *")] xcb_change_keyboard_mapping_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_mapping_keysyms_length([NativeTypeName("const xcb_change_keyboard_mapping_request_t *")] xcb_change_keyboard_mapping_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_change_keyboard_mapping_keysyms_end([NativeTypeName("const xcb_change_keyboard_mapping_request_t *")] xcb_change_keyboard_mapping_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_keyboard_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_mapping_cookie_t xcb_get_keyboard_mapping(xcb_connection_t* c, [NativeTypeName("xcb_keycode_t")] byte first_keycode, [NativeTypeName("uint8_t")] byte count);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_mapping_cookie_t xcb_get_keyboard_mapping_unchecked(xcb_connection_t* c, [NativeTypeName("xcb_keycode_t")] byte first_keycode, [NativeTypeName("uint8_t")] byte count);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_keysym_t *")]
    public static extern uint* xcb_get_keyboard_mapping_keysyms([NativeTypeName("const xcb_get_keyboard_mapping_reply_t *")] xcb_get_keyboard_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_keyboard_mapping_keysyms_length([NativeTypeName("const xcb_get_keyboard_mapping_reply_t *")] xcb_get_keyboard_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_keyboard_mapping_keysyms_end([NativeTypeName("const xcb_get_keyboard_mapping_reply_t *")] xcb_get_keyboard_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_mapping_reply_t* xcb_get_keyboard_mapping_reply(xcb_connection_t* c, xcb_get_keyboard_mapping_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_control_value_list_serialize(void** _buffer, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_keyboard_control_value_list_t *")] xcb_change_keyboard_control_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_control_value_list_unpack([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask, xcb_change_keyboard_control_value_list_t* _aux);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_control_value_list_sizeof([NativeTypeName("const void *")] void* _buffer, [NativeTypeName("uint32_t")] uint value_mask);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_keyboard_control_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_control_checked(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_control(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const void *")] void* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_control_aux_checked(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_keyboard_control_value_list_t *")] xcb_change_keyboard_control_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_keyboard_control_aux(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint value_mask, [NativeTypeName("const xcb_change_keyboard_control_value_list_t *")] xcb_change_keyboard_control_value_list_t* value_list);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* xcb_change_keyboard_control_value_list([NativeTypeName("const xcb_change_keyboard_control_request_t *")] xcb_change_keyboard_control_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_control_cookie_t xcb_get_keyboard_control(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_control_cookie_t xcb_get_keyboard_control_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_keyboard_control_reply_t* xcb_get_keyboard_control_reply(xcb_connection_t* c, xcb_get_keyboard_control_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_bell_checked(xcb_connection_t* c, [NativeTypeName("int8_t")] sbyte percent);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_bell(xcb_connection_t* c, [NativeTypeName("int8_t")] sbyte percent);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_pointer_control_checked(xcb_connection_t* c, [NativeTypeName("int16_t")] short acceleration_numerator, [NativeTypeName("int16_t")] short acceleration_denominator, [NativeTypeName("int16_t")] short threshold, [NativeTypeName("uint8_t")] byte do_acceleration, [NativeTypeName("uint8_t")] byte do_threshold);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_pointer_control(xcb_connection_t* c, [NativeTypeName("int16_t")] short acceleration_numerator, [NativeTypeName("int16_t")] short acceleration_denominator, [NativeTypeName("int16_t")] short threshold, [NativeTypeName("uint8_t")] byte do_acceleration, [NativeTypeName("uint8_t")] byte do_threshold);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_control_cookie_t xcb_get_pointer_control(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_control_cookie_t xcb_get_pointer_control_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_control_reply_t* xcb_get_pointer_control_reply(xcb_connection_t* c, xcb_get_pointer_control_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_screen_saver_checked(xcb_connection_t* c, [NativeTypeName("int16_t")] short timeout, [NativeTypeName("int16_t")] short interval, [NativeTypeName("uint8_t")] byte prefer_blanking, [NativeTypeName("uint8_t")] byte allow_exposures);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_screen_saver(xcb_connection_t* c, [NativeTypeName("int16_t")] short timeout, [NativeTypeName("int16_t")] short interval, [NativeTypeName("uint8_t")] byte prefer_blanking, [NativeTypeName("uint8_t")] byte allow_exposures);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_screen_saver_cookie_t xcb_get_screen_saver(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_screen_saver_cookie_t xcb_get_screen_saver_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_screen_saver_reply_t* xcb_get_screen_saver_reply(xcb_connection_t* c, xcb_get_screen_saver_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_hosts_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_hosts_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("uint8_t")] byte family, [NativeTypeName("uint16_t")] ushort address_len, [NativeTypeName("const uint8_t *")] byte* address);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_change_hosts(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode, [NativeTypeName("uint8_t")] byte family, [NativeTypeName("uint16_t")] ushort address_len, [NativeTypeName("const uint8_t *")] byte* address);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_change_hosts_address([NativeTypeName("const xcb_change_hosts_request_t *")] xcb_change_hosts_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_change_hosts_address_length([NativeTypeName("const xcb_change_hosts_request_t *")] xcb_change_hosts_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_change_hosts_address_end([NativeTypeName("const xcb_change_hosts_request_t *")] xcb_change_hosts_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_host_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_host_address([NativeTypeName("const xcb_host_t *")] xcb_host_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_host_address_length([NativeTypeName("const xcb_host_t *")] xcb_host_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_host_address_end([NativeTypeName("const xcb_host_t *")] xcb_host_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void xcb_host_next(xcb_host_iterator_t* i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_host_end(xcb_host_iterator_t i);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_hosts_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_hosts_cookie_t xcb_list_hosts(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_hosts_cookie_t xcb_list_hosts_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_list_hosts_hosts_length([NativeTypeName("const xcb_list_hosts_reply_t *")] xcb_list_hosts_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_host_iterator_t xcb_list_hosts_hosts_iterator([NativeTypeName("const xcb_list_hosts_reply_t *")] xcb_list_hosts_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_list_hosts_reply_t* xcb_list_hosts_reply(xcb_connection_t* c, xcb_list_hosts_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_access_control_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_access_control(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_close_down_mode_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_set_close_down_mode(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_kill_client_checked(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint resource);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_kill_client(xcb_connection_t* c, [NativeTypeName("uint32_t")] uint resource);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_rotate_properties_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_rotate_properties_checked(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort atoms_len, [NativeTypeName("int16_t")] short delta, [NativeTypeName("const xcb_atom_t *")] uint* atoms);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_rotate_properties(xcb_connection_t* c, [NativeTypeName("xcb_window_t")] uint window, [NativeTypeName("uint16_t")] ushort atoms_len, [NativeTypeName("int16_t")] short delta, [NativeTypeName("const xcb_atom_t *")] uint* atoms);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_atom_t *")]
    public static extern uint* xcb_rotate_properties_atoms([NativeTypeName("const xcb_rotate_properties_request_t *")] xcb_rotate_properties_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_rotate_properties_atoms_length([NativeTypeName("const xcb_rotate_properties_request_t *")] xcb_rotate_properties_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_rotate_properties_atoms_end([NativeTypeName("const xcb_rotate_properties_request_t *")] xcb_rotate_properties_request_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_force_screen_saver_checked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_force_screen_saver(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte mode);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_pointer_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_pointer_mapping_cookie_t xcb_set_pointer_mapping(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte map_len, [NativeTypeName("const uint8_t *")] byte* map);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_pointer_mapping_cookie_t xcb_set_pointer_mapping_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte map_len, [NativeTypeName("const uint8_t *")] byte* map);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_pointer_mapping_reply_t* xcb_set_pointer_mapping_reply(xcb_connection_t* c, xcb_set_pointer_mapping_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_pointer_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_mapping_cookie_t xcb_get_pointer_mapping(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_mapping_cookie_t xcb_get_pointer_mapping_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t *")]
    public static extern byte* xcb_get_pointer_mapping_map([NativeTypeName("const xcb_get_pointer_mapping_reply_t *")] xcb_get_pointer_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_pointer_mapping_map_length([NativeTypeName("const xcb_get_pointer_mapping_reply_t *")] xcb_get_pointer_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_pointer_mapping_map_end([NativeTypeName("const xcb_get_pointer_mapping_reply_t *")] xcb_get_pointer_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_pointer_mapping_reply_t* xcb_get_pointer_mapping_reply(xcb_connection_t* c, xcb_get_pointer_mapping_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_set_modifier_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_modifier_mapping_cookie_t xcb_set_modifier_mapping(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte keycodes_per_modifier, [NativeTypeName("const xcb_keycode_t *")] byte* keycodes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_modifier_mapping_cookie_t xcb_set_modifier_mapping_unchecked(xcb_connection_t* c, [NativeTypeName("uint8_t")] byte keycodes_per_modifier, [NativeTypeName("const xcb_keycode_t *")] byte* keycodes);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_set_modifier_mapping_reply_t* xcb_set_modifier_mapping_reply(xcb_connection_t* c, xcb_set_modifier_mapping_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_modifier_mapping_sizeof([NativeTypeName("const void *")] void* _buffer);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_modifier_mapping_cookie_t xcb_get_modifier_mapping(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_modifier_mapping_cookie_t xcb_get_modifier_mapping_unchecked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("xcb_keycode_t *")]
    public static extern byte* xcb_get_modifier_mapping_keycodes([NativeTypeName("const xcb_get_modifier_mapping_reply_t *")] xcb_get_modifier_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int xcb_get_modifier_mapping_keycodes_length([NativeTypeName("const xcb_get_modifier_mapping_reply_t *")] xcb_get_modifier_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_generic_iterator_t xcb_get_modifier_mapping_keycodes_end([NativeTypeName("const xcb_get_modifier_mapping_reply_t *")] xcb_get_modifier_mapping_reply_t* R);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_get_modifier_mapping_reply_t* xcb_get_modifier_mapping_reply(xcb_connection_t* c, xcb_get_modifier_mapping_cookie_t cookie, xcb_generic_error_t** e);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_no_operation_checked(xcb_connection_t* c);

    [DllImport("xcb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern xcb_void_cookie_t xcb_no_operation(xcb_connection_t* c);
}
