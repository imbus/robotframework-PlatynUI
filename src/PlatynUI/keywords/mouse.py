# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from robotlibcore import keyword

from ..ui.core.devices.mousebutton import MouseButton
from ..ui.strategies import HasMouse
from .types import ElementDescriptor


class Mouse:
    @keyword
    def mouse_click(
        self,
        descriptor: ElementDescriptor[HasMouse],
        x: Optional[float] = None,
        y: Optional[float] = None,
        button: Optional[MouseButton] = None,
        times: int = 1,
    ) -> None:
        descriptor().mouse.click(x=x, y=y, button=button, times=times)

    @keyword
    def mouse_double_click(
        self,
        descriptor: ElementDescriptor[HasMouse],
        x: Optional[float] = None,
        y: Optional[float] = None,
        button: Optional[MouseButton] = None,
    ) -> None:
        descriptor().mouse.double_click(x=x, y=y, button=button)

    @keyword
    def mouse_move_to(
        self,
        descriptor: ElementDescriptor[HasMouse],
        x: Optional[float] = None,
        y: Optional[float] = None,
    ) -> None:
        descriptor().mouse.move_to(x=x, y=y)
