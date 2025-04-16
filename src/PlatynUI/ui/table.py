# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import typing

from ..core import LocatorScope, context
from ..core.strategies import *
from . import Control, Item

__all__ = ["Cell", "Row", "Table"]


@context
class Cell(Item):
    pass


@context
class Row(Item):
    @property
    def column_count(self):
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ColumnCount")

    def get_cells(self, *args, **kwargs) -> typing.List[Cell]:
        return self.get_all(Cell, scope=LocatorScope.Children, *args, **kwargs)

    def iter_cells(self, *args, **kwargs) -> typing.Iterator[Cell]:
        return self.iter_all(Cell, scope=LocatorScope.Children, *args, **kwargs)

    def get_cell(self, *args, **kwargs) -> Cell:
        return self.get(Cell, scope=LocatorScope.Children, *args, **kwargs)


@context
class Table(Control):
    @property
    def row_count(self):
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("RowCount")

    @property
    def column_count(self):
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(Properties).get_property_value("ColumnCount")

    def get_rows(self, *args, **kwargs) -> typing.List[Row]:
        return self.get_all(Row, scope=LocatorScope.Children, *args, **kwargs)

    def iter_rows(self, *args, **kwargs) -> typing.Iterator[Row]:
        return self.iter_all(Row, scope=LocatorScope.Children, *args, **kwargs)

    def get_row(self, *args, **kwargs) -> Row:
        return self.get(Row, scope=LocatorScope.Children, *args, **kwargs)
