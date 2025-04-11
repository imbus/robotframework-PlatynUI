// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

using System.Runtime.InteropServices;

namespace PlatynUI.Platform.MacOS.SwiftInterop
{
    internal static partial class Interop
    {
        private const string LibraryName = "PlatynUI.Platform.MacOS.Interop";

        static Interop()
        {
            NativeLibrary.SetDllImportResolver(
                typeof(Interop).Assembly,
                (libraryName, assembly, searchPath) =>
                {
                    if (libraryName == LibraryName && RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        string customPath = $"runtimes/osx/native/lib{LibraryName}.dylib";
                        if (NativeLibrary.TryLoad(customPath, out IntPtr handle))
                        {
                            return handle;
                        }
                    }
                    return IntPtr.Zero;
                }
            );
        }

        [LibraryImport(LibraryName)]
        public static partial void GetBoundingRectangle(
            out double x,
            out double y,
            out double width,
            out double height
        );
    }
}
