# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import logging
from pathlib import Path
from typing import TYPE_CHECKING, Optional, cast

__all__ = ["DotNetInterface"]

logger = logging.getLogger(__name__)

# pyright: reportMissingModuleSource=false

if TYPE_CHECKING:
    from PlatynUI.Runtime import (
        # Adapter,
        DisplayDevice,
        Finder,
        KeyboardDevice,
        MouseDevice,
        # Patterns,
    )


class DotNetInterface:
    # _adapter: Optional["Adapter"] = None
    _finder: Optional["Finder"] = None
    _mouse_device: Optional["MouseDevice"] = None
    _keyboard_device: Optional["KeyboardDevice"] = None
    _display_device: Optional["DisplayDevice"] = None
    # _patterns: Optional["Patterns"] = None

    __loaded = False

    @classmethod
    def _ensure_loaded(cls) -> None:
        if cls.__loaded:
            return

        cls.__loaded = True

        from pythonnet import get_runtime_info, load

        load("coreclr")

        import clr

        rt_kind = get_runtime_info().kind
        logger.debug(f"Runtime kind: {rt_kind}")  # noqa: G004

        if rt_kind == "CoreCLR":
            kind = "coreclr"
        else:
            logger.error(f"Unsupported runtime for PlatynUI: {get_runtime_info().kind}")  # noqa: G004
            raise RuntimeError(f"Unsupported runtime for PlatynUI: {get_runtime_info().kind}")

        logger.debug(f"Runtime kind: {rt_kind}")  # noqa: G004

        assembly_name = "PlatynUI.Runtime.dll"
        debug_assembly_path = Path(__file__).parent / f"../../../../artifacts/bin/PlatynUI.Spy/debug/{assembly_name}"
        runtime_assembly_path = Path(__file__).parent / f"{kind}/{assembly_name}"

        if debug_assembly_path.exists():
            logger.debug(f"Loading PlatynUI from {debug_assembly_path}")  # noqa: G004
            clr.AddReference(str(debug_assembly_path)[:-4])
        elif runtime_assembly_path.exists():
            logger.debug(f"Loading PlatynUI from {runtime_assembly_path}")  # noqa: G004
            clr.AddReference(str(debug_assembly_path)[:-4])
        else:
            logger.error(
                "Load PlatynUI failed: Could not find the assembly "
                "at %(debug_assembly_path) or %(runtime_assembly_path)",
                extra={"debug_assembly_path": debug_assembly_path, "runtime_assembly_path": runtime_assembly_path},
            )
            raise RuntimeError(
                f"Load PlatynUI failed: Could not find the assembly at {debug_assembly_path} or {runtime_assembly_path}"
            )

    # @classmethod
    # def adapter(cls) -> "Adapter":
    #     if cls._adapter is None:
    #         cls._ensure_loaded()

    #         from PlatynUI.Extension.Win32.UiAutomation import Adapter

    #         cls._adapter = cast("Adapter", Adapter)

    #     return cls._adapter

    @classmethod
    def finder(cls) -> "Finder":
        if cls._finder is None:
            cls._ensure_loaded()

            from PlatynUI.Runtime import Finder

            cls._finder = cast("Finder", Finder)

        return cls._finder

    @classmethod
    def mouse_device(cls) -> "MouseDevice":
        if cls._mouse_device is None:
            cls._ensure_loaded()

            from PlatynUI.Runtime import MouseDevice

            cls._mouse_device = cast("MouseDevice", MouseDevice)

        return cls._mouse_device

    @classmethod
    def keyboard_device(cls) -> "KeyboardDevice":
        if cls._keyboard_device is None:
            cls._ensure_loaded()

            from PlatynUI.Runtime import KeyboardDevice

            cls._keyboard_device = cast("KeyboardDevice", KeyboardDevice)

        return cls._keyboard_device

    @classmethod
    def display_device(cls) -> "DisplayDevice":
        if cls._display_device is None:
            cls._ensure_loaded()

            from PlatynUI.Runtime import DisplayDevice

            cls._display_device = cast("DisplayDevice", DisplayDevice)

        return cls._display_device

    # @classmethod
    # def patterns(cls) -> "Patterns":
    #     if cls._patterns is None:
    #         cls._ensure_loaded()

    #         from PlatynUI.Extension.Win32.UiAutomation import Patterns

    #         cls._patterns = cast("Patterns", Patterns)

    #     return cls._patterns
