# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import TYPE_CHECKING, Any, Dict, List, Optional, Set, cast

from PlatynUI.core import Adapter
from PlatynUI.core.adapterproxy import AdapterProxyFactory
from PlatynUI.core.strategies import Properties
from PlatynUI.core.types import Point, Rect
from PlatynUI.Extension.Win32.UiAutomation.core.technology import UiaTechnology
from PlatynUI.ui.strategies import Element

from .loader import DotNetInterface
from .uiabase import UiaBase

# pyright: reportMissingModuleSource=false

if TYPE_CHECKING:
    from PlatynUI.Extension.Win32.UiAutomation.Client import IUIAutomationElement


class UiaElement(Element, UiaBase):
    @property
    def is_readonly(self) -> bool:
        return DotNetInterface.adapter().IsReadOnly(self.element)

    @property
    def is_enabled(self) -> bool:
        return self.element.CurrentIsEnabled != 0

    @property
    def is_visible(self) -> bool:
        return self.element.CurrentIsOffscreen == 0

    @property
    def is_in_view(self) -> bool:
        if not self.valid:
            return False

        br = self.bounding_rectangle
        vr = self.visible_rectangle

        # check if the element is 1/3 visible
        if vr.width < br.width / 3 or vr.height < br.height / 3:
            return False

        top_level_parent = self.top_level_parent
        if top_level_parent is None or not top_level_parent.supports_strategy(Element):
            # TODO: check screen bounds
            return True

        center = vr.center
        return bool(center and top_level_parent.get_strategy(Element).bounding_rectangle.contains(center))

    @property
    def toplevel_parent_is_active(self) -> bool:
        raise NotImplementedError("toplevel_parent_is_active")

    @property
    def bounding_rectangle(self) -> Rect:
        r = self.element.CurrentBoundingRectangle
        return Rect(r.left, r.top, r.right - r.left, r.bottom - r.top)

    @property
    def visible_rectangle(self) -> Rect:
        result = self.bounding_rectangle

        top_level_parent = self.top_level_parent
        if top_level_parent is None or not top_level_parent.supports_strategy(Element):
            return result

        return top_level_parent.get_strategy(Element).bounding_rectangle.intersected(result)

    @property
    def default_click_position(self) -> Point:
        p = DotNetInterface.adapter().GetClickablePoint(self.element)
        if p is None:
            return Point()

        return Point(p.X, p.Y)

    @property
    def top_level_parent(self) -> Optional[Adapter]:
        result = DotNetInterface.adapter().GetTopLevelParent(self.element)

        if result is None:
            return None

        return AdapterProxyFactory.find_proxy_for(UiaAdapter(result, cast(UiaTechnology, self.technology)))

    def try_ensure_visible(self) -> bool:
        # TODO: Implement this if parent toplevel is out of view
        return self.element.CurrentIsOffscreen == 0

    def try_ensure_application_is_ready(self) -> bool:
        # TODO: raise NotImplementedError("try_ensure_application_is_ready")
        return True

    def try_ensure_toplevel_parent_is_active(self) -> bool:
        return DotNetInterface.adapter().TryEnsureTopLevelParentIsActive(self.element)

    def try_bring_into_view(self) -> bool:
        # TODO: Implement this if element toplevel is out of view and can be scrolled
        return True


class UiaProperties(Properties, UiaBase):
    def get_property_names(self) -> List[str]:
        return list(DotNetInterface.adapter().GetSupportedPropertyNames(self.element))

    def get_property_value(self, name: str) -> Any:
        return DotNetInterface.adapter().GetPropertyValue(self.element, name)


class UiaAdapter(Adapter, UiaProperties, UiaElement):
    def __init__(self, element: "IUIAutomationElement", technology: UiaTechnology) -> None:
        super().__init__()
        self._element: Optional["IUIAutomationElement"] = element
        self._technology = technology
        self._strategies_cache: Dict[str, Any] = {}

    @property
    def element(self) -> "IUIAutomationElement":
        if not self.valid or self._element is None:
            raise RuntimeError("element is not valid")

        return self._element

    @property
    def valid(self) -> bool:
        if self._element is None:
            return False

        v = False
        try:
            v = self._element.CurrentProcessId != 0
        except BaseException:
            v = False

        if not v:
            self.invalidate()

        return self._element is not None

    def invalidate(self) -> None:
        self._element = None

    @property
    def id(self) -> str:
        raise NotImplementedError("id")

    @property
    def name(self) -> str:
        return self.element.CurrentName

    @property
    def class_name(self) -> str:
        return self.element.CurrentClassName

    @property
    def tag_name(self) -> str:
        return self.role

    @property
    def role(self) -> str:
        return DotNetInterface.adapter().GetRole(self.element)

    @property
    def supported_roles(self) -> Set[str]:
        return {self.role, "Control", "Element", "Adapter"}

    @property
    def type(self) -> str:
        return "element" if self.valid else ""

    @property
    def supported_types(self) -> Set[str]:
        return {"element", "adapter"} if self.valid else set()

    @property
    def framework_id(self) -> str:
        return self.element.CurrentFrameworkId

    @property
    def runtime_id(self) -> str:
        return DotNetInterface.adapter().GetRuntimeId(self.element)

    @property
    def technology(self) -> UiaTechnology:
        return self._technology

    @property
    def parent(self) -> Optional["Adapter"]:
        raise NotImplementedError("parent")

    @property
    def children(self) -> List["Adapter"]:
        raise NotImplementedError("children")
