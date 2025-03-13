# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import abc
import logging
from typing import Any, Iterable, Iterator, List, Optional, Union

from ....core.exceptions import PlatyUiError
from ....core.settings import Settings
from .basekeyboarddevice import BaseKeyboardDevice, BaseKeyCode, InputType, Key
from .inputdevice import InputDevice

__all__ = [
    "DefaultKeyboardDevice",
    "InvalidKeyCodeError",
    "InvalidKeyError",
    "InvalidKeySequenceError",
    "KeyboardDevice",
]

logger = logging.getLogger(__name__)


class KeyboardDevice(InputDevice):
    __after_press_release_delay: Optional[float] = None
    __after_release_key_delay: Optional[float] = None
    __after_press_key_delay: Optional[float] = None

    @property
    def after_press_key_delay(self) -> float:
        if self.__after_press_key_delay is None:
            return Settings.current().keyboard_after_press_key_delay
        return self.__after_press_key_delay

    @after_press_key_delay.setter
    def after_press_key_delay(self, value: float) -> None:
        self.__after_press_key_delay = value

    @property
    def after_release_key_delay(self) -> float:
        if self.__after_release_key_delay is None:
            return Settings.current().keyboard_after_release_key_delay
        return self.__after_release_key_delay

    @after_release_key_delay.setter
    def after_release_key_delay(self, value: float) -> None:
        self.__after_release_key_delay = value

    @property
    def after_press_release_delay(self) -> float:
        if self.__after_press_release_delay is None:
            return Settings.current().keyboard_after_press_release_delay
        return self.__after_press_release_delay

    @after_press_release_delay.setter
    def after_press_release_delay(self, value: float) -> None:
        self.__after_press_release_delay = value

    def add_context(self, context: Any) -> None:
        pass

    def remove_context(self, context: Any) -> None:
        pass

    @abc.abstractmethod
    def escape_text(self, value: str) -> str:
        pass

    @abc.abstractmethod
    def type_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None: ...

    @abc.abstractmethod
    def press_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None: ...

    @abc.abstractmethod
    def release_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None: ...


class KeyEvent:
    def __init__(self, key_code: BaseKeyCode, press: bool):
        self.key_code = key_code
        self.press = press

    def __repr__(self) -> str:
        return "KeyEvent(key=%s, press=%s)" % (repr(self.key_code), repr(self.press))

    def __str__(self) -> str:
        return self.__repr__()


class KeyboardSendError(PlatyUiError):
    pass


class InvalidKeyError(PlatyUiError):
    pass


class InvalidKeySequenceError(InvalidKeyError):
    pass


class InvalidKeyCodeError(InvalidKeyError):
    pass


class KeyConverter:
    def __init__(
        self,
        base_keyboard_device: BaseKeyboardDevice,
        *keys: Union[str, Any, Iterable[Any]],
        down: bool = True,
        up: bool = True,
    ):
        self.base_keyboard_device = base_keyboard_device
        self.input = keys
        self._current_index = 0
        self._down = down
        self._up = up

    def __next_key(self, keys: str) -> Iterator[str]:
        for i in keys:
            yield i
            self._current_index += 1

    def convert(self) -> Iterator[KeyEvent]:
        for i in self.input:
            for j in self.__convert_single(i):
                yield j

    def __key_to_keycode(self, key: Key) -> BaseKeyCode:
        result = self.base_keyboard_device.key_to_keycode(key)

        if not result.valid:
            raise InvalidKeyCodeError(
                "can't get a valid keycode from device for key '%s'%s"
                % (key, "" if result.error_text is None else ": %s" % result.error_text)
            )
        return result

    def __convert_single(self, keys: Union[str, Any, Iterable[Any]]) -> Iterable[KeyEvent]:
        if isinstance(keys, str):
            g = self.__next_key(keys)
            for i in g:
                if i == "<":
                    current = ""
                    first = 0
                    escape = False
                    sequence = []
                    need_end = True
                    for j in g:
                        first += 1
                        if j == "<":
                            if first == 1:
                                if self._down:
                                    yield KeyEvent(self.__key_to_keycode(j), press=True)
                                if self._up:
                                    yield KeyEvent(self.__key_to_keycode(j), press=False)
                                escape = True
                                break
                            else:
                                raise InvalidKeySequenceError(f"Invalid key sequence at index {self._current_index}")

                        if str.isspace(j):
                            continue
                        elif j == "+":
                            sequence.append(current)
                            current = ""
                        elif j == ">":
                            need_end = False
                            break
                        else:
                            current += j
                    if need_end:
                        raise InvalidKeySequenceError(
                            f"Invalid key sequence at index {self._current_index}: > is missing"
                        )

                    if not escape:
                        if current:
                            sequence.append(current)

                        if len(sequence) == 0:
                            raise InvalidKeySequenceError(f"Empty key sequence at index {self._current_index}")

                        if self._down:
                            for k in [KeyEvent(self.__key_to_keycode(k), press=True) for k in sequence]:
                                yield k
                        if self._up:
                            for k in [
                                KeyEvent(self.__key_to_keycode(k), press=False)
                                for k in (reversed(sequence) if self._down else sequence)
                            ]:
                                yield k
                    continue

                if self._down:
                    yield KeyEvent(self.__key_to_keycode(i), press=True)
                if self._up:
                    yield KeyEvent(self.__key_to_keycode(i), press=False)
        elif isinstance(keys, Iterable):
            if self._down:
                for i in keys:
                    yield KeyEvent(self.__key_to_keycode(i), press=True)
            if self._up:
                for i in list(keys)[::-1] if self._down else keys:
                    yield KeyEvent(self.__key_to_keycode(i), press=False)
        else:
            if self._down:
                yield KeyEvent(self.__key_to_keycode(keys), press=True)
            if self._up:
                yield KeyEvent(self.__key_to_keycode(keys), press=False)


class DefaultKeyboardDevice(KeyboardDevice):
    __base_keyboard_device = None  # type: BaseKeyboardDevice

    def __init__(self, base_keyboard_device: BaseKeyboardDevice):
        self.__base_keyboard_device = base_keyboard_device

        self._pressed_keys: List[BaseKeyCode] = []

    def escape_text(self, value: str) -> str:
        return str.replace(value, "<", "<<")

    @property
    def base_keyboard_device(self) -> BaseKeyboardDevice:
        return self.__base_keyboard_device

    def __del__(self) -> None:
        if len(self._pressed_keys) > 0:
            logger.warning("there are pressed keys (%s), try to send key release", self._pressed_keys, exc_info=True)
            for k in self._pressed_keys:
                self.base_keyboard_device.send_keycode(k, False)

    def __send_key_event(self, key_event: KeyEvent, delay: Optional[float]) -> None:
        if key_event.key_code.valid:
            if not self.base_keyboard_device.send_keycode(key_event.key_code, key_event.press):
                raise KeyboardSendError(
                    "can't send {} {}".format("press" if key_event.press else "release", key_event.key_code)
                )
        else:
            logger.warning(
                "invalid keycode %s, don't send it to device%s",
                key_event.key_code,
                "" if key_event.key_code.error_text is None else ": %s" % key_event.key_code.error_text,
            )

        if key_event.press:
            self.base_keyboard_device.delay(self.after_press_key_delay)
            self._pressed_keys.insert(0, key_event.key_code)
        else:
            self.base_keyboard_device.delay(delay if delay is not None else self.after_release_key_delay)
            if key_event.key_code in self._pressed_keys:
                self._pressed_keys.remove(key_event.key_code)

    def type_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.base_keyboard_device.start_input(InputType.TYPE)
        try:
            for c in KeyConverter(self.base_keyboard_device, *keys, down=True, up=True).convert():
                self.__send_key_event(c, delay)

            self.delay(self.after_press_release_delay)
            self.delay(self.after_input_delay)
        finally:
            self.base_keyboard_device.end_input()

    def press_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.base_keyboard_device.start_input(InputType.PRESS)
        try:
            for c in KeyConverter(self.base_keyboard_device, *keys, down=True, up=False).convert():
                self.__send_key_event(c, delay)

            self.delay(self.after_press_release_delay)
            self.delay(self.after_input_delay)
        finally:
            self.base_keyboard_device.end_input()

    def release_keys(self, *keys: Union[str, Any, Iterable[Any]], delay: Optional[float] = None) -> None:
        self.base_keyboard_device.start_input(InputType.RELEASE)
        try:
            for c in KeyConverter(self.base_keyboard_device, *keys, down=False, up=True).convert():
                self.__send_key_event(c, delay)

            self.delay(self.after_press_release_delay)
            self.delay(self.after_input_delay)
        finally:
            self.base_keyboard_device.end_input()
