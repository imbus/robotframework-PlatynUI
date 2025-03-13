# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from robotlibcore import keyword

from ..ui.strategies import HasKeyboard
from .types import ElementDescriptor


class Keyboard:
    @keyword
    def type_keys(self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None) -> None:
        descriptor().keyboard.type_keys(*keys, delay=delay)

    @keyword
    def press_keys(self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None) -> None:
        descriptor().keyboard.press_keys(*keys, delay=delay)

    @keyword
    def release_keys(
        self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None
    ) -> None:
        descriptor().keyboard.release_keys(*keys, delay=delay)
