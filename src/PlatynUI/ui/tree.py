# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import typing

from ..core import LocatorScope, context
from ..core.strategies import Properties
from . import Control, Item, strategies

__all__ = ["Tree", "TreeItem"]


@context
class TreeItem(Item):
    @property
    def item_count(self) -> int:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ItemCount")

    @property
    def column_count(self) -> int:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ColumnCount")

    @property
    def can_expand(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasExpanded).can_expand

    @property
    def is_expanded(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasExpanded).is_expanded

    def expand(self) -> bool:
        if not self.can_expand:
            return False

        if self.is_expanded:
            return True

        self.ensure_that(self._toplevel_parent_is_active, self._element_is_in_view)

        self.adapter.get_strategy(strategies.Expandable).expand()

        self.ensure_that(self._application_is_ready, raise_exception=False)

        return True

    def collapse(self) -> bool:
        if not self.can_expand:
            return False

        if not self.is_expanded:
            return True

        self.ensure_that(self._toplevel_parent_is_active, self._element_is_in_view)

        self.adapter.get_strategy(strategies.Expandable).collapse()

        self.ensure_that(self._application_is_ready, raise_exception=False)

        return True

    def get_items(self, *args: typing.Any, **kwargs: typing.Any) -> typing.List["TreeItem"]:
        return self.get_all(TreeItem, scope=LocatorScope.Children, *args, **kwargs)

    def iter_items(self, *args: typing.Any, **kwargs: typing.Any) -> typing.Iterator["TreeItem"]:
        return self.iter_all(TreeItem, scope=LocatorScope.Children, *args, **kwargs)

    def get_item(self, *args: typing.Any, **kwargs: typing.Any) -> "TreeItem":
        return self.get(TreeItem, scope=LocatorScope.Children, *args, **kwargs)


@context
class Tree(Control):
    @property
    def item_count(self):
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ItemCount")

    @property
    def column_count(self):
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ColumnCount")

    def get_items(self, *args, **kwargs) -> typing.List[TreeItem]:
        return self.get_all(TreeItem, scope=LocatorScope.Children, *args, **kwargs)

    def iter_items(self, *args, **kwargs) -> typing.Iterator[TreeItem]:
        return self.iter_all(TreeItem, scope=LocatorScope.Children, *args, **kwargs)

    def get_item(self, *args, **kwargs) -> TreeItem:
        return self.get(TreeItem, scope=LocatorScope.Children, *args, **kwargs)
