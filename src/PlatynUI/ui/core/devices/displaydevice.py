# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import datetime
import os
from abc import ABCMeta, abstractmethod
from typing import Optional

from ....core.settings import Settings
from ....core.types import Rect
from .basedisplaydevice import BaseDisplayDevice

__all__ = ["DefaultDisplayDevice", "DisplayDevice"]


class DisplayDevice(metaclass=ABCMeta):
    @property
    @abstractmethod
    def name(self) -> str:
        pass

    @property
    @abstractmethod
    def bounding_rectangle(self) -> Rect:
        pass

    @abstractmethod
    def highlight_rect(self, rect: Rect, time: float) -> None:
        pass

    @abstractmethod
    def get_screenshot(self, rect: Rect, format: Optional[str] = None, quality: Optional[int] = None) -> bytearray:
        pass

    @abstractmethod
    def save_screenshot(
        self,
        filename: Optional[str] = None,
        rect: Rect = None,
        format: Optional[str] = None,
        quality: Optional[int] = None,
    ) -> str:
        pass


class DefaultDisplayDevice(DisplayDevice):
    # TODO implement something that gets the single display sizes and orientations to check if the mouse pointer movements are in range

    def __init__(self, base_display_device: BaseDisplayDevice):
        self.__base_display_device = base_display_device

    @property
    def name(self) -> str:
        return self.__base_display_device.name

    @property
    def bounding_rectangle(self) -> Rect:
        return self.__base_display_device.bounding_rectangle

    def highlight_rect(self, rect: Rect, time: float) -> None:
        self.__base_display_device.highlight_rect(rect, time)

    @staticmethod
    def __timestamp() -> str:
        return "_" + datetime.datetime.now().isoformat()  # noqa: DTZ005

    def get_screenshot(self, rect: Rect, format: Optional[str] = None, quality: Optional[int] = None) -> bytearray:
        return self.__base_display_device.get_screenshot(
            rect,
            format if format is not None else Settings.current().display_screenshot_format,
            quality if quality is not None else Settings.current().display_screenshot_quality,
        )

    def save_screenshot(
        self,
        filename: Optional[str] = None,
        rect: Optional[Rect] = None,
        format: Optional[str] = None,
        quality: Optional[int] = None,
    ) -> str:
        directory = "."
        if filename is not None and os.path.isdir(filename):
            directory = filename
            filename = None
        if filename is None:
            while True:
                filename = os.path.join(
                    directory,
                    Settings.current().display_screenshot_basename
                    + self.__timestamp()
                    + "."
                    + Settings.current().display_screenshot_format,
                )

                if os.path.exists(filename):
                    continue
                break

        if format is None:
            filename, file_extension = os.path.splitext(filename)
            if file_extension is not None and file_extension != "":
                format = file_extension[1:]
                if format is None or format == "":
                    format = None
            if format is None or format == "":
                format = Settings.current().display_screenshot_format

            filename = "%s.%s" % (filename, format)

        if rect is None:
            rect = Rect()

        with open(filename, "wb") as f:
            f.write(self.get_screenshot(rect, format, quality))

        return filename
