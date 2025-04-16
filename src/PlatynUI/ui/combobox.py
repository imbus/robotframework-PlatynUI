# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import typing

from ..core import LocatorScope, context
from ..core.predicate import predicate
from . import strategies
from .control import Control
from .lists import ListItem

__all__ = ["ComboBox"]


@context
class ComboBox(Control):
    @property
    def can_expand(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasExpanded).can_expand

    @property
    def is_expanded(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasExpanded).is_expanded

    @predicate("combobox {0} is expanded")
    def _combobox_is_expanded(self) -> bool:
        if not self.is_expanded:
            self.expand()
            return False

        return self.is_expanded

    @predicate("combobox {0} is collapsed")
    def _combobox_is_collapsed(self) -> bool:
        if self.is_expanded:
            self.collapse()
            return False

        return not self.is_expanded

    @predicate("combobox {0} can expand")
    def _combobox_can_expand(self) -> bool:
        return self.can_expand

    def expand(self) -> bool:
        if self.is_expanded:
            return False

        self.ensure_that(
            self._combobox_can_expand,
            self._toplevel_parent_is_active,
            self._element_is_in_view,
            self._control_has_focus,
        )

        self.adapter.get_strategy(strategies.Expandable).expand()

        self.ensure_that(self._application_is_ready, raise_exception=False)

        return True

    def collapse(self) -> bool:
        if not self.is_expanded:
            return False

        self.ensure_that(
            self._combobox_can_expand,
            self._toplevel_parent_is_active,
            self._element_is_in_view,
            self._control_has_focus,
        )

        self.adapter.get_strategy(strategies.Expandable).collapse()

        self.ensure_that(self._application_is_ready, raise_exception=False)

        return True

    def get_items(self, *args, **kwargs) -> typing.List[ListItem]:
        expanded = self.expand()
        try:
            self.ensure_that(self._combobox_is_expanded)
            return self.get_all(ListItem, scope=LocatorScope.Descendants, *args, **kwargs)
        finally:
            if expanded:
                self.collapse()

    def iter_items(self, *args, **kwargs) -> typing.Iterator[ListItem]:
        expanded = self.expand()
        try:
            self.ensure_that(self._combobox_is_expanded)
            return self.iter_all(ListItem, scope=LocatorScope.Descendants, *args, **kwargs)
        finally:
            if expanded:
                self.collapse()

    def get_item(self, *args, **kwargs) -> ListItem:
        expanded = self.expand()
        try:
            self.ensure_that(self._combobox_is_expanded)
            return self.get(ListItem, scope=LocatorScope.Descendants, *args, **kwargs)
        finally:
            if expanded:
                self.collapse()

    def select(self, *args, **kwargs) -> ListItem:
        expanded = self.expand()
        try:
            self.ensure_that(self._combobox_is_expanded)
            result = self.get_item(*args, **kwargs)
            result.select(False)
            return result
        finally:
            if expanded:
                self.collapse()

    @property
    def selected(self) -> typing.Optional[ListItem]:
        return self.get_item(IsSelected=True)

    @property
    def text(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Text).text

    @text.setter
    def text(self, value: str):
        self.set_text(value)

    @property
    def is_editable(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Editable).is_editable

    @predicate("combobox {0} is editable")
    def _combobox_is_editable(self) -> bool:
        return self.is_editable

    def set_text(self, value):
        self.ensure_that(
            self._toplevel_parent_is_active,
            self._element_is_in_view,
            self._element_is_enabled,
            self._element_is_not_readonly,
            self._combobox_is_editable,
            self._control_has_focus,
        )
        try:
            self.adapter.get_strategy(strategies.EditableText).set_text(value)
        finally:
            self.ensure_that(self._application_is_ready, raise_exception=False)
