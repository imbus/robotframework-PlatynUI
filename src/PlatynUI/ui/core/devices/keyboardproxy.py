# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import enum
from abc import ABCMeta
from typing import Any, Iterable, Optional, Union, cast

from typing_extensions import Self

from ....core import Adapter
from ...core.ui_technology import UiTechnology
from .keyboarddevice import KeyboardDevice

__all__ = ["AdapterKeyboardProxy", "KeyboardProxy"]


class KeyboardProxy(metaclass=ABCMeta):
    def __init__(self, keyboard_device: KeyboardDevice):
        self.keyboard_device = keyboard_device

    def __enter__(self) -> "Self":
        return self

    def __exit__(self, exc_type: Any, exc_val: Any, exc_tb: Any) -> None:
        pass

    class Action(enum.Enum):
        TYPE = 1
        PRESS = 2
        RELEASE = 3

    def before_action(self, action: Action) -> None:
        pass

    def after_action(self, action: Action) -> None:
        pass

    def type_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.before_action(KeyboardProxy.Action.TYPE)

        self.keyboard_device.type_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.TYPE)

    def type_keys_without_context(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.keyboard_device.type_keys(*keys, delay=delay)

    def press_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.before_action(KeyboardProxy.Action.PRESS)

        self.keyboard_device.press_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.PRESS)

    def release_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.before_action(KeyboardProxy.Action.RELEASE)

        self.keyboard_device.release_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.RELEASE)

    def escape_text(self, value: str) -> str:
        return self.keyboard_device.escape_text(value)


class AdapterKeyboardProxy(KeyboardProxy):
    def __init__(self, adapter: Adapter, keyboard_device: KeyboardDevice = None):
        super().__init__(keyboard_device or cast(UiTechnology, adapter.technology).keyboard_device)
        self._adapter = adapter

    def before_action(self, action: KeyboardProxy.Action) -> None:
        self.keyboard_device.add_context(self._adapter)

    def after_action(self, action: KeyboardProxy.Action) -> None:
        self.keyboard_device.remove_context(self._adapter)
