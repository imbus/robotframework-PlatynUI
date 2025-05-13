# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from robotlibcore import keyword

from ..ui.strategies import EditableText, Text
from .assertable import assertable
from .types import ElementDescriptor


class TextKeywords:
    @keyword
    def set_text(self, descriptor: ElementDescriptor[EditableText], text: str) -> None:
        descriptor().set_text(text)

    @keyword
    @assertable
    def get_text(self, descriptor: ElementDescriptor[Text]) -> str:
        result = descriptor().text
        return result
