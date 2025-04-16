# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import pytest

from PlatynUI.ui.runtime.dotnet_interface import DotNetInterface

pytestmark = pytest.mark.wip

def test_finder_should_find_desktop() -> None:
    result = DotNetInterface.finder().FindSingleNode(None, "/.", False)
    assert result is not None


def test_finder_should_find_one_node() -> None:
    result = DotNetInterface.finder().FindSingleNode(None, "/*", False)
    assert result is not None


def test_mouse_device_should_work() -> None:
    DotNetInterface.mouse_device().Move(1, 1)
    pos = DotNetInterface.mouse_device().GetPosition()
    assert pos.X != 0
    assert pos.Y != 0


def test_keyboard_device_should_work() -> None:
    result = DotNetInterface.keyboard_device().KeyToKeyCode("A")
    assert result.Valid


def test_display_device_should_work() -> None:
    result = DotNetInterface.display_device().GetBoundingRectangle()
    assert result.Width != 0
    assert result.Height != 0
