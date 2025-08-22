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

    def _is_valid_control_sequence(self, content: str) -> bool:
        """Check if content between < and > represents a valid control key sequence."""
        if not content:
            return False

        # Split by + to get individual keys
        keys = [k.strip() for k in content.split("+") if k.strip()]
        if not keys:
            return False

        # Try to validate each key by attempting to convert it
        for key in keys:
            try:
                result = self.base_keyboard_device.key_to_keycode(key)
                if not result.valid:
                    return False
                # Check if this is actually a control key (not just a character string)
                # Control keys return KeyCodeInfo, character strings return str
                if hasattr(result, "code") and isinstance(result.code, str):
                    # This is a character string, not a control key
                    return False
            except Exception:
                return False

        return True

    def __convert_single(self, keys: Union[str, Any, Iterable[Any]]) -> Iterable[KeyEvent]:
        if isinstance(keys, str):
            g = self.__next_key(keys)
            for i in g:
                if i == "<":
                    # Collect everything until > (or end of string)
                    content_chars = []
                    found_end = False
                    first_char = True

                    for j in g:
                        if j == "<" and first_char:
                            # This is the << escape sequence
                            if self._down:
                                yield KeyEvent(self.__key_to_keycode("<"), press=True)
                            if self._up:
                                yield KeyEvent(self.__key_to_keycode("<"), press=False)
                            found_end = True
                            break
                        if j == ">":
                            found_end = True
                            break
                        content_chars.append(j)
                        first_char = False

                    if not found_end:
                        # No closing >, treat < as literal
                        if self._down:
                            yield KeyEvent(self.__key_to_keycode("<"), press=True)
                        if self._up:
                            yield KeyEvent(self.__key_to_keycode("<"), press=False)
                        # Re-process the collected characters
                        for char in content_chars:
                            if self._down:
                                yield KeyEvent(self.__key_to_keycode(char), press=True)
                            if self._up:
                                yield KeyEvent(self.__key_to_keycode(char), press=False)
                        continue

                    content = "".join(content_chars)

                    # Check if it's a valid control sequence
                    if self._is_valid_control_sequence(content):
                        # Parse as control sequence - strip the < and >
                        current = ""
                        sequence = []

                        for char in content:
                            if char == "+":
                                if current.strip():
                                    sequence.append(current.strip())
                                current = ""
                            elif not str.isspace(char):
                                current += char

                        if current.strip():
                            sequence.append(current.strip())

                        if len(sequence) == 0:
                            raise InvalidKeySequenceError(f"Empty key sequence at index {self._current_index}")

                        # Generate key events for the control sequence
                        if self._down:
                            for k in [KeyEvent(self.__key_to_keycode(k), press=True) for k in sequence]:
                                yield k
                        if self._up:
                            for k in [
                                KeyEvent(self.__key_to_keycode(k), press=False)
                                for k in (reversed(sequence) if self._down else sequence)
                            ]:
                                yield k
                    else:
                        # Not a valid control sequence, treat the entire <...> as literal characters
                        # Output: < + content + >
                        if self._down:
                            yield KeyEvent(self.__key_to_keycode("<"), press=True)
                        if self._up:
                            yield KeyEvent(self.__key_to_keycode("<"), press=False)

                        for char in content:
                            if self._down:
                                yield KeyEvent(self.__key_to_keycode(char), press=True)
                            if self._up:
                                yield KeyEvent(self.__key_to_keycode(char), press=False)

                        if self._down:
                            yield KeyEvent(self.__key_to_keycode(">"), press=True)
                        if self._up:
                            yield KeyEvent(self.__key_to_keycode(">"), press=False)

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
