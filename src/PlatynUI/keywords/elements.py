# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import List

from robotlibcore import keyword

from ..core.contextbase import ContextBase
from .types import ElementDescriptor


class Elements:
    @keyword
    def get_children(self, descriptor: ElementDescriptor) -> List[ContextBase]:
        return descriptor.context().get_children(None)
