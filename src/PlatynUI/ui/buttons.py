# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from abc import abstractmethod

from ..core import context
from . import strategies
from .control import Control
from .togglestate import ToggleState

__all__ = ["AbstractButton", "Button", "CheckBox", "Link", "PushButton", "RadioButton", "ToggleState"]


class AbstractButton(Control, strategies.Text, strategies.Activatable):
    @property
    def text(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Text).text

    @abstractmethod
    def activate(self) -> None: ...


@context
class Button(AbstractButton):
    def activate(self) -> None:
        self.ensure_that(self._toplevel_parent_is_active, self._element_is_in_view, self._element_is_enabled)

        self.adapter.get_strategy(strategies.Activatable).activate()

        self.ensure_that(self._application_is_ready, raise_exception=False)


@context
class PushButton(Button):
    pass


@context
class Link(Button):
    pass


@context
class CheckBox(AbstractButton, strategies.HasToggleState, strategies.Toggleable):
    @property
    def state(self) -> ToggleState:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasToggleState).state

    @state.setter
    def state(self, value: ToggleState) -> None:
        self.set_state(value)

    def set_state(self, state: ToggleState) -> None:
        for _ in ToggleState:
            if self.state == state:
                break
            self.toggle()

    def activate(self) -> None:
        self.set_state(ToggleState.Checked)

    def check(self) -> None:
        self.set_state(ToggleState.Checked)

    @property
    def is_checked(self) -> bool:
        return self.state == ToggleState.Checked

    @property
    def is_unchecked(self) -> bool:
        return self.state == ToggleState.Unchecked

    def uncheck(self) -> None:
        self.set_state(ToggleState.Unchecked)

    def toggle(self) -> None:
        self.ensure_that(
            self._toplevel_parent_is_active,
            self._element_is_in_view,
            self._element_is_enabled,
            self._element_is_not_readonly,
        )

        self.adapter.get_strategy(strategies.Toggleable).toggle()

        self.ensure_that(self._application_is_ready, raise_exception=False)


@context
class RadioButton(Button):
    pass
