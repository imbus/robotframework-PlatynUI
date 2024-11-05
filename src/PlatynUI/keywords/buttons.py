# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from robotlibcore import keyword

from ..ui import Element
from ..ui.strategies import Activatable

__all__ = ["Buttons"]


class Buttons:
    @keyword
    def activate(self, element: Activatable) -> None:
        element.activate()

    @keyword
    def click(self, element: Element) -> None:
        element.mouse.click()

    @keyword
    def doubleclick(self, element: Element) -> None:
        element.mouse.double_click()
