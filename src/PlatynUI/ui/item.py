# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from ..core import context
from ..core.predicate import predicate
from . import strategies
from .element import Element

__all__ = ["Item"]


@context
class Item(Element):
    default_prefix = "item"

    @predicate("item {0} is editable")
    def _item_is_editable(self):
        return not self.is_readonly

    @property
    def text(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Text).text

    def activate(self):
        if not self.is_active:
            self.ensure_that(self._element_is_enabled, self._element_is_in_view)

            self.adapter.get_strategy(strategies.Activatable).activate()

            self.ensure_that(self._application_is_ready, raise_exception=False)

    def deactivate(self):
        if self.is_active:
            self.ensure_that(self._element_is_enabled, self._element_is_in_view)

            self.adapter.get_strategy(strategies.Deactivatable).deactivate()

            self.ensure_that(self._application_is_ready, raise_exception=False)

    @property
    def is_active(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasIsActive).is_active

    def _item_is_active(self) -> bool:
        if not self.is_active:
            self.activate()

            return False
        return self.is_active

    @text.setter
    def text(self, value: str):
        self.set_text(value)

    def set_text(self, value):
        self.ensure_that(self._item_is_editable, self._item_is_active)

        self.adapter.get_strategy(strategies.HasEditor).open_editor()
        self.adapter.get_strategy(strategies.EditableText).set_text(value)
        self.adapter.get_strategy(strategies.HasEditor).accept()

        self.ensure_that(self._application_is_ready, raise_exception=False)

    def clear(self):
        self.ensure_that(self._item_is_editable)

        self.adapter.get_strategy(strategies.HasEditor).open_editor()
        self.adapter.get_strategy(strategies.Clearable).clear()
        self.adapter.get_strategy(strategies.HasEditor).accept()

        self.ensure_that(self._application_is_ready, raise_exception=False)

    @property
    def is_selectable(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasSelected).is_selectable

    @predicate("item {0} is selectable")
    def _item_is_selectable(self):
        return self.is_selectable

    @property
    def is_selected(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasSelected).is_selected

    def select(self):
        self.ensure_that(
            self._toplevel_parent_is_active,
            self._element_is_in_view,
            self._element_is_enabled,
            self._item_is_selectable,
        )

        if not self.is_selected:
            self.adapter.get_strategy(strategies.Selectable).select()

            self.ensure_that(self._application_is_ready, raise_exception=False)

        self.ensure_that(self._application_is_ready, raise_exception=False)
