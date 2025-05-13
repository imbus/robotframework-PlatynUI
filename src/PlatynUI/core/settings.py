# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from dataclasses import dataclass
from typing import Any, Literal, Optional, TypeVar

__all__ = ["Settings"]

T = TypeVar("T")


_current: Optional["Settings"] = None


@dataclass
class Settings:
    wait_for_timeout: float = 1.0
    wait_for_delay: float = 0.1

    ensure_timeout: float = 15
    ensure_delay: float = 0.1
    exists_timeout: float = 1.0

    window_close_timeout: float = 1.0

    input_after_input_delay: float = 0.001

    mouse_before_next_click_delay_multiplicator: float = 1.5

    mouse_after_click_delay: float = 0.010
    mouse_multi_click_delay_multiplicator: float = 0.5
    mouse_press_release_delay: float = 0.010
    mouse_after_move_delay: float = 0.010
    mouse_move_delay: float = 0.001
    mouse_move_time: float = 0.2

    keyboard_after_press_key_delay: float = 0.01
    keyboard_after_release_key_delay: float = 0.01
    keyboard_after_press_release_delay: float = 0.05

    display_screenshot_format: str = "png"
    display_screenshot_quality: int = -1
    display_screenshot_basename: str = "screenshot"

    element_highlight_time: float = 2
    element_highlight_ensure_timeout: float = 2

    @staticmethod
    def current() -> "Settings":
        global _current
        if _current is None:
            _current = Settings()
        return _current

    def __enter__(self) -> "Settings":
        global _current
        self._old = _current
        _current = self
        return self

    def __exit__(self, exc_type: Any, exc_val: Any, exc_tb: Any) -> Literal[False]:
        global _current
        _current = self._old
        del self._old
        return False
