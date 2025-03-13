# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0


from robotlibcore import keyword

from ..ui.strategies import Activatable, Deactivatable, HasIsActive
from .assertable import assertable
from .types import ElementDescriptor

__all__ = ["ActivatableKeywords"]


class ActivatableKeywords:
    @keyword
    def activate(self, descriptor: ElementDescriptor[Activatable]) -> None:
        descriptor().activate()

    @keyword
    def deactivate(self, descriptor: ElementDescriptor[Deactivatable]) -> None:
        descriptor().deactivate()

    @keyword
    @assertable
    def is_active(self, descriptor: ElementDescriptor[HasIsActive]) -> bool:
        return descriptor().is_active
