# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import typing

from ..core import LocatorScope, context
from . import strategies
from .control import Control
from .item import Item

__all__ = ["List", "ListItem"]


@context
class ListItem(Item):
    def select(self, check_selected: bool = True):
        self.ensure_that(self._toplevel_parent_is_active, self._element_is_in_view, self._element_is_enabled)

        if not check_selected or not self.is_selected:
            self.adapter.get_strategy(strategies.Selectable).select()

            self.ensure_that(self._application_is_ready, raise_exception=False)


@context
class List(Control):
    def get_items(self, *args, **kwargs) -> typing.List[ListItem]:
        return self.get_all(ListItem, scope=LocatorScope.Children, *args, **kwargs)

    def iter_items(self, *args, **kwargs) -> typing.Iterator[ListItem]:
        return self.iter_all(ListItem, scope=LocatorScope.Children, *args, **kwargs)

    def get_item(self, *args, **kwargs) -> ListItem:
        return self.get(ListItem, scope=LocatorScope.Children, *args, **kwargs)

    def select(self, *args, **kwargs) -> ListItem:
        result = self.get_item(*args, **kwargs)
        result.select(False)
        return result
