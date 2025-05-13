# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional, cast

from ...core import Adapter, AdapterProxy, Point, Rect, Size, adapter_proxy_for, wait_for
from .. import strategies
from ..core import UiTechnology
from ..core.devices import AdapterKeyboardProxy, AdapterMouseProxy


@adapter_proxy_for(role="Element")
class ElementProxy(AdapterProxy, strategies.Element):
    @property
    def bounding_rectangle(self) -> Rect:
        return self.adapter.get_strategy(strategies.Element).bounding_rectangle

    @property
    def visible_rectangle(self) -> Rect:
        return self.adapter.get_strategy(strategies.Element).visible_rectangle

    @property
    def default_click_position(self) -> Point:
        return self.adapter.get_strategy(strategies.Element).default_click_position

    def try_ensure_toplevel_parent_is_active(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).try_ensure_toplevel_parent_is_active()

    @property
    def is_enabled(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).is_enabled

    def try_ensure_visible(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).try_ensure_visible()

    def try_bring_into_view(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).try_bring_into_view()

    def try_ensure_application_is_ready(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).try_ensure_application_is_ready()

    @property
    def is_visible(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).is_visible

    @property
    def is_in_view(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).is_in_view

    @property
    def toplevel_parent_is_active(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).toplevel_parent_is_active

    @property
    def is_readonly(self) -> bool:
        return self.adapter.get_strategy(strategies.Element).is_readonly

    @property
    def top_level_parent(self) -> Optional[Adapter]:
        return self.adapter.get_strategy(strategies.Element).top_level_parent


@adapter_proxy_for(role="Control")
class ControlProxy(ElementProxy, strategies.Control):
    @property
    def has_focus(self) -> bool:
        return self.adapter.get_strategy(strategies.Control).has_focus

    def try_ensure_focused(self) -> bool:
        return self.adapter.get_strategy(strategies.Control).try_ensure_focused()


@adapter_proxy_for(role="Text")
class TextProxy(ControlProxy, strategies.Text, strategies.Clearable, strategies.EditableText, strategies.HasMultiLine):
    @property
    def text(self) -> str:
        return self.adapter.get_strategy(strategies.Text).text

    @property
    def is_multi_line(self) -> bool:
        return (
            self.adapter.supports_strategy(strategies.HasMultiLine)
            and self.adapter.get_strategy(strategies.HasMultiLine).is_multi_line
        )

    def set_text(self, value: str) -> None:
        self.clear()
        kbd = AdapterKeyboardProxy(self.adapter)
        kbd.type_keys(kbd.escape_text(value))

    def clear(self) -> None:
        AdapterKeyboardProxy(self.adapter).type_keys(
            "<Control+Home><Control+Shift+End><Delete>" if self.is_multi_line else "<Home><Shift+End><Delete>"
        )


@adapter_proxy_for(role="Edit")
class EditProxy(TextProxy):
    pass


@adapter_proxy_for(role="Button")
class ButtonProxy(ControlProxy, strategies.Activatable):
    def activate(self) -> None:
        AdapterMouseProxy(self.adapter).click()


@adapter_proxy_for(role="PushButton")
class PushButtonProxy(ControlProxy, strategies.Activatable):
    def activate(self) -> None:
        AdapterMouseProxy(self.adapter).click()


@adapter_proxy_for(role="MenuItem")
class MenuItemProxy(ControlProxy, strategies.Activatable):
    def activate(self) -> None:
        with AdapterMouseProxy(self.adapter) as proxy:
            proxy.move_to(x=0, y=0)
            proxy.click()


@adapter_proxy_for(role="CheckBox")
class CheckBoxProxy(ControlProxy, strategies.HasToggleState, strategies.Toggleable):
    @property
    def state(self) -> strategies.ToggleState:
        return self.adapter.get_strategy(strategies.HasToggleState).state

    def toggle(self) -> None:
        AdapterMouseProxy(self.adapter).click()


@adapter_proxy_for(role="Item")
class ItemProxy(
    ElementProxy,
    strategies.Selectable,
    strategies.HasEditor,
    strategies.Clearable,
    strategies.EditableText,
    strategies.Activatable,
    strategies.Deactivatable,
    strategies.HasIsActive,
):
    def activate(self) -> None:
        AdapterMouseProxy(self.adapter).click()
        wait_for(lambda: self.is_active)

    def deactivate(self) -> None:
        AdapterMouseProxy(self.adapter).click()
        wait_for(lambda: not self.is_active)

    @property
    def is_active(self) -> bool:
        return self.adapter.get_strategy(strategies.HasIsActive).is_active

    def cancel(self) -> None:
        AdapterKeyboardProxy(self.adapter).type_keys("<Escape>")

    def accept(self) -> None:
        AdapterKeyboardProxy(self.adapter).type_keys("<Enter>")

    def open_editor(self) -> None:
        AdapterMouseProxy(self.adapter).click(times=2)
        # cast(UiTechnology, self.adapter.technology).keyboard_device.type_keys("<F2>")

    def clear(self) -> None:
        AdapterKeyboardProxy(self.adapter).type_keys("<Home><Shift+End><Delete>")

    def set_text(self, value: str) -> None:
        self.clear()
        with AdapterKeyboardProxy(self.adapter) as kbd:
            kbd.type_keys(kbd.escape_text(value))

    @property
    def is_selectable(self) -> bool:
        return self.adapter.get_strategy(strategies.HasSelected).is_selectable

    @property
    def is_selected(self) -> bool:
        return self.adapter.get_strategy(strategies.HasSelected).is_selected

    def select(self) -> None:
        AdapterMouseProxy(self.adapter).click()
        wait_for(lambda: self.is_selected)


@adapter_proxy_for(role="TabItem")
class TabItemProxy(ItemProxy):
    pass


@adapter_proxy_for(role="ListItem")
class ListItemProxy(ItemProxy):
    pass


@adapter_proxy_for(role="Cell")
class CellProxy(ItemProxy):
    pass


@adapter_proxy_for(role="TreeItem")
class TreeItemProxy(ItemProxy, strategies.Expandable):
    @property
    def is_expanded(self) -> bool:
        return self.adapter.get_strategy(strategies.HasExpanded).is_expanded

    @property
    def can_expand(self) -> bool:
        return self.adapter.get_strategy(strategies.HasExpanded).can_expand

    def expand(self) -> None:
        AdapterMouseProxy(self.adapter).click()
        wait_for(lambda: self.is_expanded)

    def collapse(self) -> None:
        AdapterMouseProxy(self.adapter).click()
        wait_for(lambda: not self.is_expanded)


@adapter_proxy_for(role="ComboBox")
class ComboBoxProxy(ControlProxy, strategies.Expandable, strategies.EditableText):
    @property
    def is_expanded(self) -> bool:
        return self.adapter.get_strategy(strategies.HasExpanded).is_expanded

    @property
    def can_expand(self) -> bool:
        return self.adapter.get_strategy(strategies.HasExpanded).can_expand

    def expand(self) -> None:
        if self.is_expanded:
            return

        for _ in range(2):
            AdapterMouseProxy(self.adapter).click()

            if wait_for(lambda: self.is_expanded):
                break

    def collapse(self) -> None:
        if not self.is_expanded:
            return
        for _ in range(2):
            AdapterMouseProxy(self.adapter).click()

            if wait_for(lambda: not self.is_expanded):
                break

    @property
    def text(self) -> str:
        return self.adapter.get_strategy(strategies.Text).text

    def set_text(self, value: str) -> None:
        self.clear()
        with AdapterKeyboardProxy(self.adapter) as kbd:
            kbd.type_keys(kbd.escape_text(value))

    def clear(self) -> None:
        AdapterKeyboardProxy(self.adapter).type_keys("<Home><Shift+End><Delete>")


@adapter_proxy_for(role="Window")
class WindowProxy(
    ControlProxy,
    strategies.Activatable,
    strategies.HasIsActive,
    strategies.HasCanMaximize,
    strategies.HasCanMinimize,
    strategies.Restorable,
    strategies.Maximizable,
    strategies.Minimizable,
    strategies.Closeable,
    strategies.Movable,
    strategies.Resizable,
):
    @property
    def can_maximize(self) -> bool:
        return self.adapter.get_strategy(strategies.HasCanMaximize).can_maximize

    @property
    def is_maximized(self) -> bool:
        return self.adapter.get_strategy(strategies.HasCanMaximize).is_maximized

    @property
    def can_minimize(self) -> bool:
        return self.adapter.get_strategy(strategies.HasCanMinimize).can_minimize

    @property
    def is_minimized(self) -> bool:
        return self.adapter.get_strategy(strategies.HasCanMinimize).is_minimized

    def maximize(self) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.maximize_window(
                native_window.native_window_handle
            )
        else:
            self.adapter.get_strategy(strategies.Maximizable).maximize()

        wait_for(lambda: self.is_maximized)

    def minimize(self) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.minimize_window(
                native_window.native_window_handle
            )
        else:
            self.adapter.get_strategy(strategies.Minimizable).minimize()

        wait_for(lambda: self.is_minimized)

    def restore(self) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.restore_window(
                native_window.native_window_handle
            )
        else:
            self.adapter.get_strategy(strategies.Restorable).restore()

        wait_for(lambda: not (self.is_maximized or self.is_maximized))

    def activate(self) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.activate_window(
                native_window.native_window_handle
            )
        else:
            self.adapter.get_strategy(strategies.Activatable).activate()

        wait_for(lambda: self.is_active)

    @property
    def is_active(self) -> bool:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            return (
                cast(UiTechnology, self.adapter.technology).window_manager.active_window
                == native_window.native_window_handle
            )

        return self.adapter.get_strategy(strategies.HasIsActive).is_active

    def close(self) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.close_window(native_window.native_window_handle)
        else:
            self.adapter.get_strategy(strategies.Closeable).close()

    def move(self, pos: Point) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.move_window(
                native_window.native_window_handle, pos
            )
        else:
            self.adapter.get_strategy(strategies.Movable).move(pos)

    def resize(self, size: Size) -> None:
        native_window = self.adapter.get_strategy(strategies.HasNativeWindowHandle, False)
        if (
            native_window is not None
            and len(native_window.native_window_handle) > 0
            and cast(UiTechnology, self.adapter.technology).window_manager.window_manager_present
        ):
            cast(UiTechnology, self.adapter.technology).window_manager.resize_window(
                native_window.native_window_handle, size
            )
        else:
            self.adapter.get_strategy(strategies.Resizable).resize(size)


@adapter_proxy_for(role="Frame")
class FrameProxy(WindowProxy):
    pass
