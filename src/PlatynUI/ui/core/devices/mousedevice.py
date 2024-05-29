import abc
import time
from typing import Optional

from ....core.settings import Settings
from ....core.types import Point, Rect
from .basemousedevice import BaseMouseDevice
from .inputdevice import InputDevice
from .mousebutton import MouseButton

__all__ = ["MouseDevice", "DefaultMouseDevice"]


class MouseDevice(InputDevice):
    __double_click_time: float = 0.4  # type: float
    __multi_click_delay = None  # type: float
    __press_release_delay = None  # type: float
    __after_move_delay = None  # type: float
    __move_delay = None  # type: float
    __move_time = None  # type: float
    __after_click_delay = None  # type: float
    __before_next_click_delay = None  # type: float

    default_button = MouseButton.left

    @property
    def double_click_time(self) -> float:
        return self.__double_click_time

    @double_click_time.setter
    def double_click_time(self, value: float) -> None:
        self.__double_click_time = value

    @property
    def before_next_click_delay(self) -> float:
        if self.__before_next_click_delay is None:
            return self.double_click_time * Settings.current().mouse_before_next_click_delay_multiplicator
        return self.__before_next_click_delay

    @before_next_click_delay.setter
    def before_next_click_delay(self, value: float):
        self.__before_next_click_delay = value

    @property
    def after_click_delay(self) -> float:
        if self.__after_click_delay is None:
            return Settings.current().mouse_after_click_delay
        return self.__after_click_delay

    @after_click_delay.setter
    def after_click_delay(self, value: float):
        self.__after_click_delay = value

    @property
    def multi_click_delay(self) -> float:
        if self.__multi_click_delay is None:
            return self.double_click_time * Settings.current().mouse_multi_click_delay_multiplicator
        return self.__multi_click_delay

    @multi_click_delay.setter
    def multi_click_delay(self, value: float):
        self.__multi_click_delay = value

    @property
    def press_release_delay(self) -> float:
        if self.__press_release_delay is None:
            return Settings.current().mouse_press_release_delay
        return self.__press_release_delay

    @press_release_delay.setter
    def press_release_delay(self, value: float):
        self.__press_release_delay = value

    @property
    def after_move_delay(self) -> float:
        if self.__after_move_delay is None:
            return Settings.current().mouse_after_move_delay
        return self.__after_move_delay

    @after_move_delay.setter
    def after_move_delay(self, value: float):
        self.__move_time = value

    @property
    def move_time(self) -> float:
        if self.__move_time is None:
            return Settings.current().mouse_move_time
        return self.__move_time

    @move_time.setter
    def move_time(self, value: float):
        self.__move_time = value

    @property
    def move_delay(self) -> float:
        if self.__move_delay is None:
            return Settings.current().mouse_move_delay
        return self.__move_delay

    @move_delay.setter
    def move_delay(self, value: float):
        self.__move_delay = value

    def add_context(self, context):
        pass

    def remove_context(self, context):
        pass

    @abc.abstractmethod
    def get_position(self) -> Point:
        pass

    @abc.abstractmethod
    def move_to(
        self, pos: Point = None, x: float = None, y: float = None, raise_exception: bool = True
    ) -> Optional[Point]:
        pass

    @abc.abstractmethod
    def press(self, pos: Point = None, x: float = None, y: float = None, button: MouseButton = None) -> Optional[Point]:
        pass

    @abc.abstractmethod
    def release(
        self, pos: Point = None, x: float = None, y: float = None, button: MouseButton = None
    ) -> Optional[Point]:
        pass

    @abc.abstractmethod
    def click(
        self, pos: Point = Point(), x: float = None, y: float = None, button: MouseButton = None, times: int = 1
    ) -> Optional[Point]:
        pass


class DefaultMouseDevice(MouseDevice):
    __base_mouse_device = None  # type: BaseMouseDevice

    def __init__(self, base_mouse_device: BaseMouseDevice):
        self.__base_mouse_device = base_mouse_device
        self.double_click_time = self.__base_mouse_device.double_click_time

    @property
    def base_mouse_device(self) -> BaseMouseDevice:
        return self.__base_mouse_device

    def get_position(self) -> Point:
        return self.__base_mouse_device.get_position()

    def _generate_move_events(self, start_pos, end_pos):
        end_time = time.time() + self.move_time

        while True:
            current_time = end_time - time.time()
            if current_time > 0:
                step = current_time / self.move_time

                yield Point((1 - step) * end_pos.x + step * start_pos.x, (1 - step) * end_pos.y + step * start_pos.y)
            else:
                break

    def move_to(
        self, pos: Point = None, x: float = None, y: float = None, raise_exception: bool = True
    ) -> Optional[Point]:
        if pos is None:
            pos = Point()

        start_pos = self.get_position()
        if not pos:
            pos = start_pos.clone()

        if x is not None:
            pos.x = x
        if y is not None:
            pos.y = y

        if pos and pos != start_pos:
            if self.move_time > 0:
                for p in self._generate_move_events(start_pos, pos):
                    self.__base_mouse_device.move_to(p)
                    self.delay(self.move_delay)

            self.__base_mouse_device.move_to(pos)

            self.delay(self.after_move_delay)
            self.delay(self.after_input_delay)
        else:
            if not pos and raise_exception:
                raise Exception("invalid position %s" % pos)

        return pos

    def press(self, pos: Point = None, x: float = None, y: float = None, button: MouseButton = None) -> Optional[Point]:
        if button is None:
            button = self.default_button

        result = self.move_to(pos=pos, x=x, y=y, raise_exception=False)

        self.__base_mouse_device.press(button)

        self.delay(self.press_release_delay)
        self.delay(self.after_input_delay)

        return result

    def release(
        self, pos: Point = None, x: float = None, y: float = None, button: MouseButton = None
    ) -> Optional[Point]:
        if button is None:
            button = self.default_button

        result = self.move_to(pos=pos, x=x, y=y, raise_exception=False)

        self.__base_mouse_device.release(button)

        self.delay(self.press_release_delay)
        self.delay(self.after_input_delay)

        return result

    _last_click_time = 0
    _last_click_pos = Point()
    _last_click_rect = Rect()

    def _calc_last_click_rect(self, p: Point):
        size = self.__base_mouse_device.double_click_size

        return Rect(p.x - size.width / 2, p.y - size.height / 2, size.width, size.height)

    def click(
        self, pos: Point = Point(), x: float = None, y: float = None, button: MouseButton = None, times: int = 1
    ) -> Optional[Point]:
        last_click_time_span = time.time() - self._last_click_time

        if button is None:
            button = self.default_button

        result = self.move_to(pos=pos, x=x, y=y, raise_exception=False)

        if self._last_click_rect.contains(result) and last_click_time_span < self.before_next_click_delay:
            self.delay(self.before_next_click_delay - last_click_time_span)

        for i in range(times):
            if i > 0:
                self.delay(self.multi_click_delay)

            self.__base_mouse_device.press(button)
            self.delay(self.press_release_delay)
            self.__base_mouse_device.release(button)

        self._last_click_time = time.time()
        self._last_click_pos = result
        self._last_click_rect = self._calc_last_click_rect(result)

        self.delay(self.after_click_delay)
        self.delay(self.after_input_delay)

        return result
