# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from PlatynUI.core.types import Point, Size
from PlatynUI.ui.core.devices.basemousedevice import BaseMouseDevice
from PlatynUI.ui.core.devices.mousebutton import MouseButton

from .dotnet_interface import DotNetInterface


class MouseDeviceImpl(BaseMouseDevice):
    @property
    def double_click_time(self) -> float:
        return DotNetInterface.mouse_device().GetDoubleClickTime() * 0.001  # ms to s

    @property
    def double_click_size(self) -> Size:
        r = DotNetInterface.mouse_device().GetDoubleClickSize()
        return Size(r.Width, r.Height)

    def get_position(self) -> Point:
        r = DotNetInterface.mouse_device().GetPosition()
        return Point(r.X, r.Y)

    def move_to(self, pos: Point) -> None:
        DotNetInterface.mouse_device().Move(pos.x, pos.y)

    def press(self, button: MouseButton) -> None:
        DotNetInterface.mouse_device().Press(int(button))

    def release(self, button: MouseButton) -> None:
        DotNetInterface.mouse_device().Release(int(button))
