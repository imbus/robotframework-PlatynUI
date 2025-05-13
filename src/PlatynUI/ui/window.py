# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from ..core import Settings, context
from ..core.predicate import predicate
from ..core.types import Point, Size
from . import strategies
from .control import Control

__all__ = ["Frame", "Window"]


@context
class Window(Control):
    @property
    def has_focus(self) -> bool:
        return self.is_active

    @predicate("control {0} has focus")
    def _control_has_focus(self) -> bool:
        return self._window_is_active()

    def focus(self) -> bool:
        return self.activate()

    @predicate("parent window of element {0} is active")
    def _toplevel_parent_is_active(self) -> bool:
        if not self.is_active:
            self.activate()
            return False
        return True

    @predicate("window {0} is not minimized")
    def _window_is_not_minimized(self) -> bool:
        self.ensure_that(self._application_is_ready)

        if self.adapter.get_strategy(strategies.HasCanMinimize).is_minimized:
            self.adapter.get_strategy(strategies.Restorable).restore()
            return False

        return not self.adapter.get_strategy(strategies.HasCanMinimize).is_minimized

    @predicate("window {0} is active")
    def _window_is_active(self) -> bool:
        self.ensure_that(self._element_is_visible, self._window_is_not_minimized)

        if self.adapter.get_strategy(strategies.HasIsActive).is_active:
            return True

        self.adapter.get_strategy(strategies.Activatable).activate()

        return self.adapter.get_strategy(strategies.HasIsActive).is_active

    @property
    def is_active(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasIsActive).is_active

    def activate(self) -> bool:
        result = self.ensure_that(self._window_is_active)

        self.ensure_that(self._application_is_ready, raise_exception=False)

        return result

    @property
    def can_minimize(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasCanMinimize).can_minimize

    @predicate("window {0} is minimizeable")
    def _window_is_minimizeable(self) -> bool:
        return self.can_minimize

    @property
    def is_minimized(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasCanMinimize).is_minimized

    def minimize(self) -> None:
        if self.is_minimized:
            return

        self.ensure_that(self._window_is_minimizeable, self._window_is_active)

        self.adapter.get_strategy(strategies.Minimizable).minimize()

        self.ensure_that(self._application_is_ready, raise_exception=False)

    @property
    def can_maximize(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasCanMaximize).can_maximize

    @predicate("window {0} is maximizeable")
    def _window_is_maximizeable(self) -> bool:
        return self.can_maximize

    @property
    def is_maximized(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasCanMaximize).is_maximized

    def maximize(self) -> None:
        if self.is_maximized:
            return

        self.ensure_that(self._window_is_maximizeable, self._window_is_active)

        self.adapter.get_strategy(strategies.Maximizable).maximize()

        self.ensure_that(self._application_is_ready, raise_exception=False)

    @property
    def can_close(self) -> bool:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.HasCanClose).can_close

    def close(self) -> None:
        self.ensure_that(self._window_is_active)

        self.adapter.get_strategy(strategies.Closeable).close()

        self.ensure_that(
            self._application_is_ready, timeout=Settings.current().window_close_timeout, raise_exception=False
        )

    def restore(self) -> None:
        if self.is_maximized or self.is_minimized:
            self.ensure_that(
                self._application_is_ready,
                self._element_is_visible,
                self._window_is_active if self.is_maximized or self.is_minimized else None,
            )

            self.adapter.get_strategy(strategies.Restorable).restore()

            self.ensure_that(self._application_is_ready, raise_exception=False)

    @property
    def title(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Title).title

    @property
    def text(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Text).text

    def move(self, x: Optional[float] = None, y: Optional[float] = None, pos: Point = None) -> None:
        if pos is None and (x is None or y is None):
            return

        if pos is None:
            pos = Point(x, y)

        if not pos.is_valid:
            return

        self.ensure_that(self._application_is_ready, self._element_is_visible, self._window_is_active)

        self.adapter.get_strategy(strategies.Movable).move(pos)

        self.ensure_that(self._application_is_ready, raise_exception=False)

    def resize(self, width: Optional[float] = None, height: Optional[float] = None, size: Size = None) -> None:
        if size is None and (width is None or height is None):
            return

        if size is None:
            size = Size(width, height)

        if not size.is_valid:
            return

        self.ensure_that(self._application_is_ready, self._element_is_visible, self._window_is_active)

        self.adapter.get_strategy(strategies.Resizable).resize(size)

        self.ensure_that(self._application_is_ready, raise_exception=False)


@context
class Frame(Window):
    pass
