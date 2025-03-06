# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import time
from abc import abstractmethod
from enum import Enum
from typing import Any, Optional, Union

from ....core.strategybase import StrategyBase

__all__ = ["BaseKeyCode", "BaseKeyboardDevice", "InputType"]

Key = Union[str, Any]


class BaseKeyCode:
    def __init__(self, key: Any, code: Any, valid: bool, error_text: Optional[str] = None):
        self.key = key
        self.code = code
        self.valid = valid
        self.error_text = error_text

    def __repr__(self) -> str:
        return (
            f"{self.__class__.__name__}(key={self.key!r}, "
            f"code={self.code!r}, "
            f"valid={self.valid!r}, "
            f"error_text={self.error_text!r})"
        )

    def __str__(self) -> str:
        return repr(self)

    def __eq__(self, value: object) -> bool:
        if isinstance(value, BaseKeyCode):
            return bool(self.key == value.key and self.code == value.code)
        return False

    def __hash__(self) -> int:
        return hash((self.key, self.code))


class InputType(Enum):
    TYPE = 1
    PRESS = 2
    RELEASE = 3


class BaseKeyboardDevice(StrategyBase):
    strategy_name = "org.platynui.devices.Keyboard"

    def start_input(self, input_type: InputType) -> None:
        pass

    def end_input(self) -> None:
        pass

    def delay(self, seconds: float) -> None:
        time.sleep(seconds)

    @abstractmethod
    def key_to_keycode(self, key: Key) -> BaseKeyCode:
        pass

    @abstractmethod
    def send_keycode(self, keycode: BaseKeyCode, pressed: bool) -> bool:
        pass
