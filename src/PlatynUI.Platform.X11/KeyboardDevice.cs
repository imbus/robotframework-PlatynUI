using System.ComponentModel.Composition;
using PlatynUI.Platform.X11.Interop.XCB;
using PlatynUI.Runtime.Core;
using static PlatynUI.Platform.X11.Interop.XCB.XCB;

namespace PlatynUI.Platform.X11
{
    public class X11Key(string name, uint keysym, bool extended = false)
    {
        public string Name { get; } = name;
        public uint Keysym { get; } = keysym;
        public bool Extended { get; } = extended;

        // Dictionary to store all key mappings
        public static readonly Dictionary<string, X11Key> Keys = [];

        // Special Keys
        public static readonly X11Key None = new("None", 0x0);
        public static readonly X11Key Cancel = new("Cancel", 0xff69);
        public static readonly X11Key Back = new("BackSpace", 0xff08);
        public static readonly X11Key Tab = new("Tab", 0xff09);
        public static readonly X11Key Clear = new("Clear", 0xff0b);
        public static readonly X11Key Return = new("Return", 0xff0d);
        public static readonly X11Key Enter = Return;
        public static readonly X11Key Shift = new("Shift_L", 0xffe1);
        public static readonly X11Key Lshift = new("Shift_L", 0xffe1);
        public static readonly X11Key Rshift = new("Shift_R", 0xffe2);
        public static readonly X11Key Control = new("Control_L", 0xffe3);
        public static readonly X11Key Lcontrol = new("Control_L", 0xffe3);
        public static readonly X11Key Rcontrol = new("Control_R", 0xffe4);
        public static readonly X11Key Alt = new("Alt_L", 0xffe9);
        public static readonly X11Key Lmenu = new("Alt_L", 0xffe9);
        public static readonly X11Key Rmenu = new("Alt_R", 0xffea);
        public static readonly X11Key Pause = new("Pause", 0xff13);
        public static readonly X11Key Capital = new("Caps_Lock", 0xffe5);
        public static readonly X11Key CapsLock = Capital;
        public static readonly X11Key Escape = new("Escape", 0xff1b);
        public static readonly X11Key Space = new("space", 0x0020);
        public static readonly X11Key Prior = new("Page_Up", 0xff55);
        public static readonly X11Key PageUp = Prior;
        public static readonly X11Key Next = new("Page_Down", 0xff56);
        public static readonly X11Key PageDown = Next;
        public static readonly X11Key End = new("End", 0xff57);
        public static readonly X11Key Home = new("Home", 0xff50);
        public static readonly X11Key Left = new("Left", 0xff51);
        public static readonly X11Key Up = new("Up", 0xff52);
        public static readonly X11Key Right = new("Right", 0xff53);
        public static readonly X11Key Down = new("Down", 0xff54);
        public static readonly X11Key Select = new("Select", 0xff60);
        public static readonly X11Key Print = new("Print", 0xff61);
        public static readonly X11Key Execute = new("Execute", 0xff62);
        public static readonly X11Key Snapshot = new("Print", 0xff61);
        public static readonly X11Key PrintScreen = Snapshot;
        public static readonly X11Key Insert = new("Insert", 0xff63);
        public static readonly X11Key Delete = new("Delete", 0xffff);
        public static readonly X11Key Help = new("Help", 0xff6a);

        // Number keys (0-9)
        public static readonly X11Key D0 = new("0", 0x0030);
        public static readonly X11Key D1 = new("1", 0x0031);
        public static readonly X11Key D2 = new("2", 0x0032);
        public static readonly X11Key D3 = new("3", 0x0033);
        public static readonly X11Key D4 = new("4", 0x0034);
        public static readonly X11Key D5 = new("5", 0x0035);
        public static readonly X11Key D6 = new("6", 0x0036);
        public static readonly X11Key D7 = new("7", 0x0037);
        public static readonly X11Key D8 = new("8", 0x0038);
        public static readonly X11Key D9 = new("9", 0x0039);

        // Letter keys (A-Z)
        public static readonly X11Key A = new("a", 0x0061);
        public static readonly X11Key B = new("b", 0x0062);
        public static readonly X11Key C = new("c", 0x0063);
        public static readonly X11Key D = new("d", 0x0064);
        public static readonly X11Key E = new("e", 0x0065);
        public static readonly X11Key F = new("f", 0x0066);
        public static readonly X11Key G = new("g", 0x0067);
        public static readonly X11Key H = new("h", 0x0068);
        public static readonly X11Key I = new("i", 0x0069);
        public static readonly X11Key J = new("j", 0x006a);
        public static readonly X11Key K = new("k", 0x006b);
        public static readonly X11Key L = new("l", 0x006c);
        public static readonly X11Key M = new("m", 0x006d);
        public static readonly X11Key N = new("n", 0x006e);
        public static readonly X11Key O = new("o", 0x006f);
        public static readonly X11Key P = new("p", 0x0070);
        public static readonly X11Key Q = new("q", 0x0071);
        public static readonly X11Key R = new("r", 0x0072);
        public static readonly X11Key S = new("s", 0x0073);
        public static readonly X11Key T = new("t", 0x0074);
        public static readonly X11Key U = new("u", 0x0075);
        public static readonly X11Key V = new("v", 0x0076);
        public static readonly X11Key W = new("w", 0x0077);
        public static readonly X11Key X = new("x", 0x0078);
        public static readonly X11Key Y = new("y", 0x0079);
        public static readonly X11Key Z = new("z", 0x007a);

        // Function keys
        public static readonly X11Key F1 = new("F1", 0xffbe);
        public static readonly X11Key F2 = new("F2", 0xffbf);
        public static readonly X11Key F3 = new("F3", 0xffc0);
        public static readonly X11Key F4 = new("F4", 0xffc1);
        public static readonly X11Key F5 = new("F5", 0xffc2);
        public static readonly X11Key F6 = new("F6", 0xffc3);
        public static readonly X11Key F7 = new("F7", 0xffc4);
        public static readonly X11Key F8 = new("F8", 0xffc5);
        public static readonly X11Key F9 = new("F9", 0xffc6);
        public static readonly X11Key F10 = new("F10", 0xffc7);
        public static readonly X11Key F11 = new("F11", 0xffc8);
        public static readonly X11Key F12 = new("F12", 0xffc9);

        // Numpad keys
        public static readonly X11Key NumLock = new("Num_Lock", 0xff7f);
        public static readonly X11Key Numpad0 = new("KP_0", 0xffb0);
        public static readonly X11Key Numpad1 = new("KP_1", 0xffb1);
        public static readonly X11Key Numpad2 = new("KP_2", 0xffb2);
        public static readonly X11Key Numpad3 = new("KP_3", 0xffb3);
        public static readonly X11Key Numpad4 = new("KP_4", 0xffb4);
        public static readonly X11Key Numpad5 = new("KP_5", 0xffb5);
        public static readonly X11Key Numpad6 = new("KP_6", 0xffb6);
        public static readonly X11Key Numpad7 = new("KP_7", 0xffb7);
        public static readonly X11Key Numpad8 = new("KP_8", 0xffb8);
        public static readonly X11Key Numpad9 = new("KP_9", 0xffb9);
        public static readonly X11Key Multiply = new("KP_Multiply", 0xffaa);
        public static readonly X11Key Add = new("KP_Add", 0xffab);
        public static readonly X11Key Separator = new("KP_Separator", 0xffac);
        public static readonly X11Key Subtract = new("KP_Subtract", 0xffad);
        public static readonly X11Key Decimal = new("KP_Decimal", 0xffae);
        public static readonly X11Key Divide = new("KP_Divide", 0xffaf);

        // Additional keys
        public static readonly X11Key Super = new("Super_L", 0xffeb);
        public static readonly X11Key LSuper = new("Super_L", 0xffeb);
        public static readonly X11Key RSuper = new("Super_R", 0xffec);
        public static readonly X11Key Meta = Super;
        public static readonly X11Key Win = Super;

        // Common symbol keys
        public static readonly X11Key Grave = new("grave", 0x0060); // `
        public static readonly X11Key Minus = new("minus", 0x002d); // -
        public static readonly X11Key Equal = new("equal", 0x003d); // =
        public static readonly X11Key BracketLeft = new("bracketleft", 0x005b); // [
        public static readonly X11Key BracketRight = new("bracketright", 0x005d); // ]
        public static readonly X11Key Backslash = new("backslash", 0x005c); // \
        public static readonly X11Key Semicolon = new("semicolon", 0x003b); // ;
        public static readonly X11Key Apostrophe = new("apostrophe", 0x0027); // '
        public static readonly X11Key Comma = new("comma", 0x002c); // ,
        public static readonly X11Key Period = new("period", 0x002e); // .
        public static readonly X11Key Slash = new("slash", 0x002f); // /

        static X11Key()
        {
            // Add all keys to the dictionary
            foreach (var field in typeof(X11Key).GetFields())
            {
                if (field.IsStatic && field.FieldType == typeof(X11Key))
                {
                    var value = field.GetValue(null);
                    if (value is X11Key key && !Keys.ContainsKey(key.Name))
                    {
                        Keys[key.Name] = key;

                        // Also add with field name as key for compatibility
                        string fieldName = field.Name;
                        if (!Keys.ContainsKey(fieldName))
                            Keys[fieldName] = key;
                    }
                }
            }
        }
    }

    [Export(typeof(IKeyboardDevice))]
    [method: ImportingConstructor]
    public partial class KeyboardDevice() : IKeyboardDevice
    {
        private const byte XCB_KEY_PRESS = 2;
        private const byte XCB_KEY_RELEASE = 3;
        private const uint XCB_CURRENT_TIME = 0;
        private const uint XCB_NONE = 0;
        private const uint XKB_KEY_NoSymbol = 0;

        // Common modifier masks
        private const uint XCB_MOD_MASK_SHIFT = 0x01;
        private const uint XCB_MOD_MASK_CONTROL = 0x04;
        private const uint XCB_MOD_MASK_MOD1 = 0x08; // Alt
        private const uint XCB_MOD_MASK_MOD4 = 0x40; // Super/Windows key

        public static XCBConnection Connection => XCBConnection.Default;

        // Similar to Windows VkKeyScan, but for X11
        private static KeyCodeInfo ToKeysymWithModifiers(char c)
        {
            uint modifiers = 0;
            uint keysym;

            // Handle special cases for common characters
            if (c >= 'A' && c <= 'Z')
            {
                // Capital letters need shift
                modifiers |= XCB_MOD_MASK_SHIFT;
                keysym = (uint)c;
            }
            else if (c == '\n')
            {
                keysym = 0xff0d; // Return key
            }
            else if (c <= 0x7F)
            {
                // ASCII characters map directly
                keysym = (uint)c;
            }
            else
            {
                // Unicode characters
                keysym = (uint)c | 0x01000000;
            }

            return new KeyCodeInfo(keysym, modifiers);
        }

        public Keycode KeyToKeyCode(object? key)
        {
            if (key == null)
                return new Keycode(key, null, false, "Key is null");

            switch (key)
            {
                case string keystr:
                {
                    if (X11Key.Keys.TryGetValue(keystr, out var x11Key))
                        return new Keycode(key, x11Key, true, null);

                    if (keystr.Length == 1)
                    {
                        return new Keycode(key, ToKeysymWithModifiers(keystr[0]), true, null);
                    }

                    return new Keycode(key, key, true, null);
                }

                case char keychar:
                    return new Keycode(key, ToKeysymWithModifiers(keychar), true, null);
            }

            return new Keycode(key, null, false, "Unknown key");
        }

        public unsafe bool SendKeyCode(object? keyCode, bool pressed)
        {
            if (keyCode == null)
                return false;

            if (keyCode is X11Key x11Key)
            {
                // Send key directly using its keysym
                return SendKeysym(x11Key.Keysym, pressed);
            }
            else if (keyCode is KeyCodeInfo keyInfo)
            {
                // Handle modifiers if needed
                bool success = true;

                if (pressed)
                {
                    // Apply modifiers before the key
                    if ((keyInfo.Modifiers & XCB_MOD_MASK_SHIFT) != 0)
                        success &= SendKeysym(X11Key.Shift.Keysym, true);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_CONTROL) != 0)
                        success &= SendKeysym(X11Key.Control.Keysym, true);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD1) != 0)
                        success &= SendKeysym(X11Key.Alt.Keysym, true);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD4) != 0)
                        success &= SendKeysym(X11Key.Super.Keysym, true);
                }

                // Send the actual key
                success &= SendKeysym(keyInfo.Keysym, pressed);

                // If releasing the key, release modifiers afterwards in reverse order
                if (!pressed)
                {
                    if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD4) != 0)
                        success &= SendKeysym(X11Key.Super.Keysym, false);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD1) != 0)
                        success &= SendKeysym(X11Key.Alt.Keysym, false);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_CONTROL) != 0)
                        success &= SendKeysym(X11Key.Control.Keysym, false);

                    if ((keyInfo.Modifiers & XCB_MOD_MASK_SHIFT) != 0)
                        success &= SendKeysym(X11Key.Shift.Keysym, false);
                }

                return success;
            }
            else if (keyCode is string keystr)
            {
                // Handle string input similar to Windows implementation
                var chars = keystr.ToCharArray();
                if (!pressed)
                {
                    Array.Reverse(chars);
                }

                bool success = true;
                foreach (char c in chars)
                {
                    // Convert character to keysym
                    uint keysym = c <= 0x7F ? (uint)c : ((uint)c | 0x01000000);
                    success &= SendKeysym(keysym, pressed);
                }

                return success;
            }

            return false;
        }

        private unsafe bool SendKeysym(uint keysym, bool pressed)
        {
            // Get the keycode from keysym
            byte? keycode = KeysymToKeycode(keysym);
            if (!keycode.HasValue)
                return false;

            // Get the root window
            uint rootWindow = GetRootWindow();

            // Send the key event
            var cookie = xcb_test_fake_input_checked(
                Connection,
                pressed ? XCB_KEY_PRESS : XCB_KEY_RELEASE,
                keycode.Value,
                XCB_CURRENT_TIME,
                rootWindow,
                0,
                0,
                0
            );

            // Check for errors
            var error = xcb_request_check(Connection, cookie);
            _ = xcb_flush(Connection);

            // Return true only if error is null (IntPtr.Zero)
            return (IntPtr)error == IntPtr.Zero;
        }

        private unsafe byte? KeysymToKeycode(uint keysym)
        {
            // Implementation depends on whether you have the managed xcb_key_symbols_get_keycode
            // Using a basic implementation with common mappings

            // Get setup to determine keycode range
            var setup = xcb_get_setup(Connection);
            if (setup == null)
                return null;

            // Get min and max keycodes
            byte min_keycode = setup->min_keycode;
            byte max_keycode = setup->max_keycode;

            // Request keyboard mapping
            var cookie = xcb_get_keyboard_mapping_unchecked(
                Connection,
                min_keycode,
                (byte)(max_keycode - min_keycode + 1)
            );

            xcb_generic_error_t* error_ptr = null;
            var reply = xcb_get_keyboard_mapping_reply(Connection, cookie, &error_ptr);
            if (reply == null)
                return FallbackKeysymToKeycode(keysym);

            try
            {
                // Extract keysyms_per_keycode
                byte keysyms_per_keycode = *((byte*)reply + 1);

                // Get keysyms array
                uint* keysyms = (uint*)xcb_get_keyboard_mapping_keysyms((xcb_get_keyboard_mapping_reply_t*)reply);
                if (keysyms == null)
                    return FallbackKeysymToKeycode(keysym);

                // Search for the keysym in the mapping
                for (byte keycode = min_keycode; keycode <= max_keycode; keycode++)
                {
                    int offset = (keycode - min_keycode) * keysyms_per_keycode;

                    for (int i = 0; i < keysyms_per_keycode; i++)
                    {
                        if (keysyms[offset + i] == keysym)
                        {
                            return keycode;
                        }
                    }
                }

                // Not found, use fallback
                return FallbackKeysymToKeycode(keysym);
            }
            finally
            {
                // Get the raw connection handle and free reply
                IntPtr rawConnection = (nint)Connection.Connection;
                free(reply);
            }
        }

        private static byte? FallbackKeysymToKeycode(uint keysym)
        {
            // Common keycodes for common keys (when direct mapping fails)
            return keysym switch
            {
                // Function keys
                0xffbe => 67, // F1
                0xffbf => 68, // F2
                0xffc0 => 69, // F3
                0xffc1 => 70, // F4
                0xffc2 => 71, // F5
                0xffc3 => 72, // F6
                0xffc4 => 73, // F7
                0xffc5 => 74, // F8
                0xffc6 => 75, // F9
                0xffc7 => 76, // F10
                0xffc8 => 95, // F11
                0xffc9 => 96, // F12

                // Navigation keys
                0xff50 => 110, // Home
                0xff51 => 113, // Left
                0xff52 => 111, // Up
                0xff53 => 114, // Right
                0xff54 => 116, // Down
                0xff55 => 112, // Page_Up
                0xff56 => 117, // Page_Down
                0xff57 => 115, // End

                // Control keys
                0xff0d => 36, // Return
                0xff1b => 9, // Escape
                0xffff => 119, // Delete
                0xff08 => 22, // BackSpace
                0xff09 => 23, // Tab
                0x0020 => 65, // space

                // Modifiers
                0xffe1 => 50, // Shift_L
                0xffe2 => 62, // Shift_R
                0xffe3 => 37, // Control_L
                0xffe4 => 105, // Control_R
                0xffe9 => 64, // Alt_L
                0xffea => 108, // Alt_R
                0xffeb => 133, // Super_L
                0xffec => 134, // Super_R

                // ASCII characters (a-z)
                0x0061 => 38, // a
                0x0062 => 56, // b
                0x0063 => 54, // c
                0x0064 => 40, // d
                0x0065 => 26, // e
                0x0066 => 41, // f
                0x0067 => 42, // g
                0x0068 => 43, // h
                0x0069 => 31, // i
                0x006a => 44, // j
                0x006b => 45, // k
                0x006c => 46, // l
                0x006d => 58, // m
                0x006e => 57, // n
                0x006f => 32, // o
                0x0070 => 33, // p
                0x0071 => 24, // q
                0x0072 => 27, // r
                0x0073 => 39, // s
                0x0074 => 28, // t
                0x0075 => 30, // u
                0x0076 => 55, // v
                0x0077 => 25, // w
                0x0078 => 53, // x
                0x0079 => 29, // y
                0x007a => 52, // z

                // Numbers
                0x0030 => 19, // 0
                0x0031 => 10, // 1
                0x0032 => 11, // 2
                0x0033 => 12, // 3
                0x0034 => 13, // 4
                0x0035 => 14, // 5
                0x0036 => 15, // 6
                0x0037 => 16, // 7
                0x0038 => 17, // 8
                0x0039 => 18, // 9

                _ => null, // No match found
            };
        }

        private unsafe uint GetRootWindow()
        {
            // Get the setup information for the X server
            var setup = xcb_get_setup(Connection);
            if (setup == null)
            {
                throw new InvalidOperationException("Failed to get X server setup information.");
            }

            // Get the iterator for the screen roots
            var iter = xcb_setup_roots_iterator(setup);

            // Return the root window of the first screen
            return iter.data->root;
        }
    }

    // Additional helper class to store keycode information with modifiers
    public class KeyCodeInfo(uint keysym, uint modifiers = 0)
    {
        public uint Keysym { get; } = keysym;
        public uint Modifiers { get; } = modifiers;
    }
}
