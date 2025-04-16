# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import typing

from ..core import LocatorScope, context
from . import strategies
from .control import Control
from .item import Item
from .orientation import Orientation

__all__ = ["Header", "HeaderItem", "Orientation"]


@context
class HeaderItem(Item):
    pass


@context
class Header(Control):
    @property
    def orientation(self) -> Orientation:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasOrientation).orientation

    def get_items(self, *args, **kwargs) -> typing.List[HeaderItem]:
        return self.get_all(HeaderItem, scope=LocatorScope.Children, *args, **kwargs)

    def iter_items(self, *args, **kwargs) -> typing.Iterator[HeaderItem]:
        return self.iter_all(HeaderItem, scope=LocatorScope.Children, *args, **kwargs)

    def get_item(self, *args, **kwargs) -> HeaderItem:
        return self.get(HeaderItem, scope=LocatorScope.Children, *args, **kwargs)
