# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import pytest

from PlatynUI.core.contextbase import ContextFactory
from PlatynUI.ui import Button, Desktop, Element, Locator, Pane, Window, locator

# mypy: disable-error-code="empty-body"

pytestmark = pytest.mark.wip

@locator(ClassName="Shell_TrayWnd", role="Pane")
class TaskBar(Window):
    @property
    @locator(name="Start")
    def start(self) -> Button: ...

def test_first() -> None:
    e = Element(Locator(path="/."))

    assert e.exists(5, raise_exception=True) is True


def test_second() -> None:
    e = Pane(Locator(name="Taskleiste"))
    assert e.exists(5, raise_exception=True) is True


def test_third() -> None:
    context: Element = ContextFactory.create_context(Locator(path="/."))
    assert isinstance(context, Desktop)


def test_fourth() -> None:
    assert Desktop().exists()

    win = TaskBar()
    assert win.start.exists()


def test_fifth() -> None:
    context: Element = ContextFactory.create_context(Locator(path="/Pane[@Name='Taskleiste']//Button[@Name='Start']"))

    assert isinstance(context, Button)

    desktop = Desktop()

    for i in range(4):
        context.activate()
        desktop.mouse.move_to(desktop.bounding_rectangle.CENTER)


def test_sixth() -> None:
    context: Element = ContextFactory.create_context(
        Locator(path="/Pane[@Name='Taskleiste']//Button[@Name='Start' and @Role='Button']"), Button
    )
    props = context.get_properties()

    assert "RuntimeId" in props
    assert "BoundingRectangle" in props
    assert "Name" in props
    assert "AutomationId" in props
    assert "ControlType" in props
    assert "Role" in props

    assert context.get_property_value("Role") == "Button"
    assert context.get_property_value("Name") == "Start"
    assert context.get_property_value("AutomationId") == "StartButton"


@locator(name="Rechner")
class Calculator(Window):
    @property
    @locator(AutomationId="num0Button")
    def n0(self) -> Button: ...
    @property
    @locator(AutomationId="num1Button")
    def n1(self) -> Button: ...
    @property
    @locator(AutomationId="num2Button")
    def n2(self) -> Button: ...
    @property
    @locator(AutomationId="num3Button")
    def n3(self) -> Button: ...
    @property
    @locator(AutomationId="num4Button")
    def n4(self) -> Button: ...
    @property
    @locator(AutomationId="num5Button")
    def n5(self) -> Button: ...
    @property
    @locator(AutomationId="num6Button")
    def n6(self) -> Button: ...
    @property
    @locator(AutomationId="num7Button")
    def n7(self) -> Button: ...
    @property
    @locator(AutomationId="num8Button")
    def n8(self) -> Button: ...
    @property
    @locator(AutomationId="num9Button")
    def n9(self) -> Button: ...


def test_seventh() -> None:
    calc = Calculator()
    desktop = Desktop()

    calc.activate()
    desktop.mouse.move_to(x=calc.n1.bounding_rectangle.left, y=calc.n1.bounding_rectangle.top)
    desktop.mouse.move_to(x=calc.n2.bounding_rectangle.left, y=calc.n2.bounding_rectangle.top)
    desktop.mouse.move_to(x=calc.n3.bounding_rectangle.left, y=calc.n3.bounding_rectangle.top)

    desktop.mouse.move_to(x=calc.n1.bounding_rectangle.left, y=calc.n1.bounding_rectangle.top)
    desktop.mouse.move_to(x=calc.n2.bounding_rectangle.left, y=calc.n2.bounding_rectangle.top)
    desktop.mouse.move_to(x=calc.n3.bounding_rectangle.left, y=calc.n3.bounding_rectangle.top)

    calc.n1.mouse.move_to()
    calc.n2.mouse.move_to()
    calc.n3.mouse.move_to()
    calc.n4.mouse.move_to()
    calc.n1.activate()
    calc.n2.activate()
    calc.n3.activate()
    calc.n4.activate()
    calc.n5.activate()
    calc.n6.activate()
    calc.n7.activate()
    calc.n8.activate()
    calc.n9.activate()
    calc.n0.activate()

    calc.n1.keyboard.type_keys("1234")


def test_eight() -> None:
    Window(Locator(path='Window[contains(@Name, "Rechner")]')).exists(5, raise_exception=True)


if __name__ == "__main__":
    # test_first()
    # test_second()
    # test_third()
    # test_fourth()
    # test_fifth()
    # test_sixth()
    # test_seventh()
    test_eight()
