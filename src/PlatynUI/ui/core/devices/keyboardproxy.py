import enum
from abc import ABCMeta
from typing import Any, Iterable, Union, cast

from ....core import Adapter
from ...core.ui_technology import UiTechnology
from .keyboarddevice import KeyboardDevice

__all__ = ["KeyboardProxy", "AdapterKeyboardProxy"]


class KeyboardProxy(metaclass=ABCMeta):
    def __init__(self, keyboard_device: KeyboardDevice):
        self.keyboard_device = keyboard_device

    def __enter__(self):
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):
        pass

    class Action(enum.Enum):
        TYPE = 1
        PRESS = 2
        RELEASE = 3

    def before_action(self, action: Action):
        pass

    def after_action(self, action: Action):
        pass

    def type_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: float = None):
        self.before_action(KeyboardProxy.Action.TYPE)

        self.keyboard_device.type_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.TYPE)

    def press_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: float = None):
        self.before_action(KeyboardProxy.Action.PRESS)

        self.keyboard_device.press_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.PRESS)

    def release_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: float = None):
        self.before_action(KeyboardProxy.Action.RELEASE)

        self.keyboard_device.release_keys(*keys, delay=delay)

        self.after_action(KeyboardProxy.Action.RELEASE)

    def escape_text(self, value: str) -> str:
        return self.keyboard_device.escape_text(value)


class AdapterKeyboardProxy(KeyboardProxy):
    def __init__(self, adapter: Adapter, keyboard_device: KeyboardDevice = None):
        super().__init__(keyboard_device or cast(UiTechnology, adapter.technology).keyboard_device)
        self._adapter = adapter

    def before_action(self, action: KeyboardProxy.Action):
        self.keyboard_device.add_context(self._adapter)

    def after_action(self, action: KeyboardProxy.Action):
        self.keyboard_device.remove_context(self._adapter)
