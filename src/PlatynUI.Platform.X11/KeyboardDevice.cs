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

        // Single letter keys should not be predefined - they should go through ToKeysymWithModifiers
        // to handle uppercase/lowercase conversion and shift modifiers properly
        // Removed A-Z key definitions to fix uppercase typing issue

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
            // Populate the Keys dictionary with all static key fields
            var keyFields = typeof(X11Key).GetFields(System.Reflection.BindingFlags.Public |
                                                    System.Reflection.BindingFlags.Static)
                                        .Where(f => f.FieldType == typeof(X11Key));

            foreach (var field in keyFields)
            {
                object? value = field.GetValue(null);
                if (value is X11Key key && !Keys.ContainsKey(key.Name))
                {
                    Keys[key.Name] = key;
                    // Also add field name as an alias if different from key name
                    if (!Keys.ContainsKey(field.Name) && field.Name != key.Name)
                    {
                        Keys[field.Name] = key;
                    }
                    
                    // Add common aliases for compatibility
                    if (field.Name == "Control") Keys["Ctrl"] = key;
                    if (field.Name == "Return") Keys["Enter"] = key;
                    if (field.Name == "Back") Keys["Backspace"] = key;
                    if (field.Name == "Prior") Keys["PageUp"] = key;
                    if (field.Name == "Next") Keys["PageDown"] = key;
                }
            }

            System.Diagnostics.Debug.WriteLine($"Populated {Keys.Count} keys in X11Key.Keys dictionary");
        }

        [Export(typeof(IKeyboardDevice))]
        [method: ImportingConstructor]
        public partial class KeyboardDevice() : IKeyboardDevice
        {
            private static readonly bool _debugLogged = LogConstruction();
            
            private static bool LogConstruction()
            {
                try { System.IO.File.AppendAllText("/tmp/x11_keyboard_constructed.log", "X11 KeyboardDevice class loaded\n"); } catch { }
                return true;
            }
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
            private unsafe KeyCodeInfo? GetKeycodeWithModifiers(char c)
            {
                // Try to find the character in XCB's keyboard mapping
                uint targetKeysym = (uint)c;
                
                // Get keyboard mapping from XCB
                var setup = xcb_get_setup(Connection);
                if (setup == null) 
                {
                    return null;
                }

                byte min_keycode = setup->min_keycode;
                byte max_keycode = setup->max_keycode;

                var cookie = xcb_get_keyboard_mapping_unchecked(
                    Connection,
                    min_keycode,
                    (byte)(max_keycode - min_keycode + 1)
                );

                xcb_generic_error_t* error_ptr = null;
                var reply = xcb_get_keyboard_mapping_reply(Connection, cookie, &error_ptr);
                if (reply == null || error_ptr != null) return null;

                byte keysyms_per_keycode = reply->keysyms_per_keycode;
                if (keysyms_per_keycode == 0) return null;

                uint* keysyms = (uint*)xcb_get_keyboard_mapping_keysyms(reply);
                if (keysyms == null) return null;

                // Search for the character in different modifier levels
                for (byte keycode = min_keycode; keycode <= max_keycode; keycode++)
                {
                    int offset = (keycode - min_keycode) * keysyms_per_keycode;

                    // Check each modifier level (0=none, 1=shift, 2=mode_switch, 3=shift+mode_switch, etc.)
                    for (int level = 0; level < keysyms_per_keycode; level++)
                    {
                        if (keysyms[offset + level] == targetKeysym)
                        {
                            // Determine modifiers based on the level where we found the keysym
                            uint modifiers = 0;
                            
                            // Standard XKB level mappings:
                            // Level 0: Base
                            // Level 1: Shift
                            // Level 2: AltGr (Mode_switch)
                            // Level 3: Shift + AltGr
                            // Level 4: Caps Lock + AltGr (or other combinations)
                            // Level 5: Caps Lock + Shift + AltGr
                            // etc.
                            
                            if (level == 1 || level == 3 || level == 5) modifiers |= XCB_MOD_MASK_SHIFT;
                            if (level == 2 || level == 3 || level == 4 || level == 5) modifiers |= XCB_MOD_MASK_MOD1; // AltGr
                            
                            // For characters that need modifiers, return the base keysym with modifiers
                            uint baseKeysym = keysyms[offset]; // Level 0 is the base character
                            
                            System.Diagnostics.Debug.WriteLine($"XCB: Found '{c}' at keycode={keycode}, level={level}, base_keysym=0x{baseKeysym:X8}, target_keysym=0x{targetKeysym:X8}, modifiers=0x{modifiers:X8}");
                            
                            free(reply);
                            return new KeyCodeInfo(baseKeysym, modifiers);
                        }
                    }
                }

                free(reply);
                return null;
            }

            private KeyCodeInfo ToKeysymWithModifiers(char c)
            {
                // First, try the dynamic XCB approach
                var dynamicResult = GetKeycodeWithModifiers(c);
                if (dynamicResult != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Dynamic XCB: Found character '{c}' with keysym 0x{dynamicResult.Keysym:X8}, modifiers 0x{dynamicResult.Modifiers:X8}");
                    return dynamicResult;
                }

                System.Diagnostics.Debug.WriteLine($"Dynamic XCB: Character '{c}' not found, using fallback static approach");

                // Fallback to minimal static mappings for known cases
                uint modifiers = 0;
                uint keysym;

                // Handle special cases for common characters
                if (c >= 'A' && c <= 'Z')
                {
                    // Use shift + lowercase approach - this is the standard way
                    modifiers |= XCB_MOD_MASK_SHIFT;
                    keysym = (uint)(c + 32); // Convert to lowercase keysym (a-z)
                }
                else if (c == '\n')
                {
                    keysym = X11Key.Return.Keysym;
                }
                else if (c == '\t')
                {
                    keysym = X11Key.Tab.Keysym;
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
                    System.Diagnostics.Debug.WriteLine($"Unicode character: Using keysym 0x{keysym:X8}");
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
                            // Skip predefined key lookup for German layout special characters
                            if (keystr.Length == 1 && "!@#$%^&*()_+={}[]|:\">?~\\".Contains(keystr[0]))
                            {
                                return new Keycode(key, ToKeysymWithModifiers(keystr[0]), true, null);
                            }
                            
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
                    return SendKeysym(x11Key.Keysym, pressed);
                }
                else if (keyCode is KeyCodeInfo keyInfo)
                {
                    bool success = true;

                    if (pressed)
                    {
                        // Apply modifiers first
                        if ((keyInfo.Modifiers & XCB_MOD_MASK_SHIFT) != 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Pressing SHIFT modifier");
                            success &= SendKeysym(X11Key.Shift.Keysym, true);
                            System.Threading.Thread.Sleep(10); // Small delay
                        }
                        if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD1) != 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Pressing AltGr modifier");
                            success &= SendKeysym(X11Key.Rmenu.Keysym, true); // AltGr is Right Alt
                            System.Threading.Thread.Sleep(10); // Small delay
                        }

                        // Then press the key
                        success &= SendKeysym(keyInfo.Keysym, true);
                    }
                    else
                    {
                        // Release the key first
                        success &= SendKeysym(keyInfo.Keysym, false);

                        // Then release modifiers
                        if ((keyInfo.Modifiers & XCB_MOD_MASK_MOD1) != 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Releasing AltGr modifier");
                            success &= SendKeysym(X11Key.Rmenu.Keysym, false); // AltGr is Right Alt
                            System.Threading.Thread.Sleep(10); // Small delay
                        }
                        if ((keyInfo.Modifiers & XCB_MOD_MASK_SHIFT) != 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Releasing SHIFT modifier");
                            success &= SendKeysym(X11Key.Shift.Keysym, false);
                            System.Threading.Thread.Sleep(10); // Small delay
                        }
                    }

                    return success;
                }
                else if (keyCode is string keystr)
                {
                    System.Diagnostics.Debug.WriteLine($"Processing string: '{keystr}'");
                    System.Console.WriteLine($"DEBUG: Processing string: '{keystr}'");

                    // For single character strings, handle them directly
                    if (keystr.Length == 1)
                    {
                        System.Console.WriteLine($"DEBUG: Single character string '{keystr}' - calling ToKeysymWithModifiers");
                        var charKeyInfo = ToKeysymWithModifiers(keystr[0]);
                        System.Console.WriteLine($"DEBUG: ToKeysymWithModifiers returned - keysym=0x{charKeyInfo.Keysym:X8}, modifiers=0x{charKeyInfo.Modifiers:X8}");
                        return SendKeyCode(charKeyInfo, pressed);
                    }

                    // For multi-character strings, check if it's a known special key
                    if (X11Key.Keys.TryGetValue(keystr, out var specialX11Key))
                    {
                        System.Diagnostics.Debug.WriteLine($"Found special key: '{keystr}'");
                        return SendKeysym(specialX11Key.Keysym, pressed);
                    }

                    // For unknown multi-character strings, process each character
                    if (pressed) // Only process on press
                    {
                        return ProcessCharacterSequence(keystr);
                    }
                    return true; // Return success for release
                }
                else if (keyCode is char c)
                {
                    // Single character handling
                    var charKeyInfo = ToKeysymWithModifiers(c);
                    return SendKeyCode(charKeyInfo, pressed);
                }

                System.Diagnostics.Debug.WriteLine($"Unsupported key type: {keyCode.GetType().Name}");
                return false;
            }

            private bool ProcessCharacterSequence(string text)
            {
                System.Diagnostics.Debug.WriteLine($"ProcessCharacterSequence: '{text}'");
                bool success = true;

                foreach (char c in text)
                {
                    var charKeyInfo = ToKeysymWithModifiers(c);

                    try
                    {
                        // Press and release each character completely before moving to next
                        System.Diagnostics.Debug.WriteLine($"Processing character '{c}' with keysym 0x{charKeyInfo.Keysym:X8}, modifiers 0x{charKeyInfo.Modifiers:X8}");

                        // Experimental: Try using hardcoded common keycodes instead of KeysymToKeycode lookup
                        if ((charKeyInfo.Modifiers & XCB_MOD_MASK_SHIFT) != 0 && c >= 'A' && c <= 'Z')
                        {
                            System.Console.WriteLine($"DEBUG: Hardcoded keycode approach for '{c}' (modifier check passed)");
                            
                            // Common keycode mappings on most Linux systems:
                            // Shift keys are usually keycode 50 (left shift) or 62 (right shift)
                            // Letter keys: a=38, b=56, c=54, d=40, e=26, f=41, g=42, h=43, i=31, etc.
                            
                            byte shiftKeycode = 50; // Left shift keycode (common on most systems)
                            byte charKeycode = 0;
                            
                            // Map uppercase letters to their corresponding lowercase keycodes
                            // This is a rough approximation - real systems vary
                            switch (c)
                            {
                                case 'A': charKeycode = 38; break; // 'a' key
                                case 'H': charKeycode = 43; break; // 'h' key
                                case 'E': charKeycode = 26; break; // 'e' key
                                case 'L': charKeycode = 46; break; // 'l' key
                                case 'O': charKeycode = 32; break; // 'o' key
                                default: 
                                    // Fallback to keycode lookup
                                    byte? fallback = KeysymToKeycode(charKeyInfo.Keysym);
                                    if (fallback.HasValue)
                                        charKeycode = fallback.Value;
                                    else
                                    {
                                        System.Console.WriteLine($"DEBUG: No hardcoded keycode for '{c}', skipping");
                                        success = false;
                                        continue;
                                    }
                                    break;
                            }
                            
                            System.Console.WriteLine($"DEBUG: Using hardcoded keycodes - Shift: {shiftKeycode}, Char '{(char)charKeyInfo.Keysym}': {charKeycode}");
                            
                            unsafe 
                            {
                                // Try sending to the focused window instead of root window
                                // Get the currently focused window
                                var reply = xcb_get_input_focus_reply(Connection, xcb_get_input_focus(Connection), null);
                                uint focusedWindow = reply != null ? reply->focus : 0;
                                
                                if (focusedWindow == 0)
                                    focusedWindow = GetRootWindow(); // Fallback to root
                                
                                System.Console.WriteLine($"DEBUG: Sending to focused window: 0x{focusedWindow:X8}");
                                
                                // Press shift
                                _ = xcb_test_fake_input_checked(Connection, XCB_KEY_PRESS, shiftKeycode, XCB_CURRENT_TIME, focusedWindow, 0, 0, 0);
                                _ = xcb_flush(Connection);
                                System.Threading.Thread.Sleep(30);
                                
                                // Press character
                                _ = xcb_test_fake_input_checked(Connection, XCB_KEY_PRESS, charKeycode, XCB_CURRENT_TIME, focusedWindow, 0, 0, 0);
                                _ = xcb_flush(Connection);
                                System.Threading.Thread.Sleep(15);
                                
                                // Release character
                                _ = xcb_test_fake_input_checked(Connection, XCB_KEY_RELEASE, charKeycode, XCB_CURRENT_TIME, focusedWindow, 0, 0, 0);
                                _ = xcb_flush(Connection);
                                System.Threading.Thread.Sleep(15);
                                
                                // Release shift
                                _ = xcb_test_fake_input_checked(Connection, XCB_KEY_RELEASE, shiftKeycode, XCB_CURRENT_TIME, focusedWindow, 0, 0, 0);
                                _ = xcb_flush(Connection);
                                System.Threading.Thread.Sleep(30);
                            }
                            
                            System.Console.WriteLine($"DEBUG: Completed hardcoded keycode sequence for '{c}'");
                        }
                        else
                        {
                            // If there are modifiers, use SendKeyCode to handle them properly
                            if (charKeyInfo.Modifiers != 0)
                            {
                                success &= SendKeyCode(charKeyInfo, true);
                                System.Threading.Thread.Sleep(10);
                                success &= SendKeyCode(charKeyInfo, false);
                            }
                            else
                            {
                                success &= SendKeysym(charKeyInfo.Keysym, true);
                                System.Threading.Thread.Sleep(10);
                                success &= SendKeysym(charKeyInfo.Keysym, false);
                            }
                            System.Threading.Thread.Sleep(10);
                        }

                        // Force a flush to ensure events are processed
                        unsafe { _ = xcb_flush(Connection); }
                        System.Threading.Thread.Sleep(15); // Delay between characters - increased for reliability

                        if (!success)
                        {
                            System.Diagnostics.Debug.WriteLine($"Failed to send character '{c}'");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing character '{c}': {ex.Message}");
                        success = false;
                        break;
                    }
                }

                return success;
            }

            private unsafe bool SendKeysym(uint keysym, bool pressed)
            {
                try
                {
                    // Get the keycode from keysym
                    byte? keycode = KeysymToKeycode(keysym);
                    if (!keycode.HasValue)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to get keycode for keysym: 0x{keysym:X8}");
                        System.Console.WriteLine($"DEBUG: Failed to get keycode for keysym: 0x{keysym:X8}");
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine($"Sending key event: keysym=0x{keysym:X8}, keycode={keycode.Value}, pressed={pressed}");
                    System.Console.WriteLine($"DEBUG: Sending key event: keysym=0x{keysym:X8} ('{(char)keysym}'), keycode={keycode.Value}, pressed={pressed}");

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

                    // Force a flush to ensure the event is sent immediately
                    unsafe { _ = xcb_flush(Connection); }

                    // Check for errors
                    var error = xcb_request_check(Connection, cookie);
                    bool success = (IntPtr)error == IntPtr.Zero;

                    if (!success)
                    {
                        System.Diagnostics.Debug.WriteLine($"XCB error sending key event. Error code: {error->error_code}");
                    }

                    return success;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception in SendKeysym: {ex.Message}");
                    return false;
                }
            }

            private unsafe byte? KeysymToKeycode(uint keysym)
            {
                try
                {
                    // Protection against potential infinite loops
                    if (keysym == 0)
                        return null;

                    // Try the fallback first for common keys - faster and more reliable
                    var fallbackCode = FallbackKeysymToKeycode(keysym);
                    if (fallbackCode.HasValue)
                        return fallbackCode;

                    // Get setup to determine keycode range
                    var setup = xcb_get_setup(Connection);
                    if (setup == null)
                        return null;

                    // Get min and max keycodes
                    byte min_keycode = setup->min_keycode;
                    byte max_keycode = setup->max_keycode;

                    // Request keyboard mapping with timeout protection
                    var cookie = xcb_get_keyboard_mapping_unchecked(
                        Connection,
                        min_keycode,
                        (byte)(max_keycode - min_keycode + 1)
                    );

                    xcb_generic_error_t* error_ptr = null;
                    var reply = xcb_get_keyboard_mapping_reply(Connection, cookie, &error_ptr);
                    if (reply == null || error_ptr != null)
                        return null;

                    // Extract keysyms_per_keycode
                    byte keysyms_per_keycode = reply->keysyms_per_keycode;
                    if (keysyms_per_keycode == 0)
                        return null;

                    // Get keysyms array
                    uint* keysyms = (uint*)xcb_get_keyboard_mapping_keysyms(reply);
                    if (keysyms == null)
                        return null;

                    // Search for the keysym in the mapping (with bound checking)
                    for (byte keycode = min_keycode; keycode <= max_keycode; keycode++)
                    {
                        int offset = (keycode - min_keycode) * keysyms_per_keycode;

                        for (int i = 0; i < keysyms_per_keycode; i++)
                        {
                            if (keysyms[offset + i] == keysym)
                            {
                                free(reply);
                                return keycode;
                            }
                        }
                    }

                    // Free the reply before returning
                    free(reply);
                    return null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in KeysymToKeycode: {ex.Message}");
                    return FallbackKeysymToKeycode(keysym);
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

                    // Special characters
                    0x003d => 19, // = (equals on keycode 19 with Shift)

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
}
