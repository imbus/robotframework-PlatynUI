# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from ..core import ContextBase, context
from . import strategies
from .control import Control
from .desktopbase import DesktopBase
from .window import Window

__all__ = ["Menu", "MenuBar", "MenuItem"]


@context
class Menu(Control):
    pass


@context
class MenuBar(Control):
    pass


@context
class MenuItem(Control):
    def activate(self) -> None:
        items = []
        p: Optional[ContextBase] = self
        while p is not None and not isinstance(p, (Window, DesktopBase)):
            if isinstance(p, MenuItem):
                items.append(p)

            p = p.parent

        for i in reversed(items[1:]):
            if not self.toplevel_parent_is_active:
                i.activate()

        self.ensure_that(self._toplevel_parent_is_active, self._element_is_in_view)
        try:
            self.adapter.get_strategy(strategies.Activatable).activate()
        finally:
            self.ensure_that(self._application_is_ready, raise_exception=False)
