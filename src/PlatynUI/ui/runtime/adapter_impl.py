# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

# pyright: reportMissingModuleSource=false

from typing import TYPE_CHECKING, Any, Dict, List, Optional, Set, Tuple, Type, cast, overload

from PlatynUI.core import Adapter
from PlatynUI.core.adapter import TStrategyBase
from PlatynUI.core.types import Point, Rect
from PlatynUI.ui.strategies import Element

from .adapter_impl_base import AdapterImplBase

if TYPE_CHECKING:
    from PlatynUI.Runtime.Core import IAdapter, IElement

    from .technology_impl import TechnologyImpl


class ElementImpl(Element, AdapterImplBase):
    @property
    def element_interface(self) -> "IElement":
        return cast("IElement", self.adapter_interface)

    @property
    def is_readonly(self) -> bool:
        # TODO: Implement this
        return False

    @property
    def is_enabled(self) -> bool:
        return self.element_interface.IsEnabled

    @property
    def is_visible(self) -> bool:
        return self.element_interface.IsVisible

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
        return self.element_interface.TopLevelParentIsActive

    @property
    def bounding_rectangle(self) -> Rect:
        r = self.element_interface.BoundingRectangle
        return Rect(r.X, r.Y, r.Width, r.Height)

    @property
    def visible_rectangle(self) -> Rect:
        result = self.bounding_rectangle

        top_level_parent = self.top_level_parent
        if top_level_parent is None or not top_level_parent.supports_strategy(Element):
            return result

        return top_level_parent.get_strategy(Element).bounding_rectangle.intersected(result)

    @property
    def top_level_parent(self) -> Optional[Adapter]:
        # TODO: check if this is correct
        return None

    @property
    def default_click_position(self) -> Point:
        p = self.element_interface.DefaultClickPosition
        if p is None:
            return Point()

        return Point(p.X, p.Y)

    def try_ensure_visible(self) -> bool:
        return self.element_interface.TryEnsureVisible()

    def try_ensure_application_is_ready(self) -> bool:
        return self.element_interface.TryEnsureApplicationIsReady()

    def try_ensure_toplevel_parent_is_active(self) -> bool:
        return self.element_interface.TryEnsureToplevelParentIsActive()

    def try_bring_into_view(self) -> bool:
        return self.element_interface.TryBringIntoView()


class AdapterImpl(Adapter, ElementImpl):
    def __init__(self, adapter_interface: "IAdapter", technology: "TechnologyImpl") -> None:
        super().__init__()
        self._technology = technology
        self._adapter_interface: Optional["IAdapter"] = adapter_interface

        self._strategies_cache: Dict[str, Any] = {}
        self.last_children_search_data: Optional[Tuple[str, bool]] = None

    @property
    def adapter_interface(self) -> "IAdapter":
        if not self.valid or self._adapter_interface is None:
            raise RuntimeError("Adapter is not valid")

        return self._adapter_interface

    @property
    def valid(self) -> bool:
        if self._adapter_interface is None:
            return False

        v = False
        try:
            v = self._adapter_interface.IsValid()
        except BaseException:
            v = False

        if not v:
            self.invalidate()

        return self._adapter_interface is not None

    def invalidate(self) -> None:
        self.last_children_search_data = None

        if self._adapter_interface is not None:
            self._adapter_interface.Invalidate()

        self._adapter_interface = None

    @property
    def id(self) -> str:
        return self.adapter_interface.Id

    @property
    def name(self) -> str:
        return self.adapter_interface.Name

    @property
    def class_name(self) -> str:
        return self.adapter_interface.ClassName

    @property
    def tag_name(self) -> str:
        return self.role

    @property
    def role(self) -> str:
        return self.adapter_interface.Role

    @property
    def supported_roles(self) -> Set[str]:
        return set(self.adapter_interface.SupportedRoles)

    @property
    def type(self) -> str:
        return self.adapter_interface.Type

    @property
    def supported_types(self) -> Set[str]:
        return set(self.adapter_interface.SupportedTypes)

    @property
    def framework_id(self) -> str:
        return self.adapter_interface.FrameworkId

    @property
    def runtime_id(self) -> str:
        return self.adapter_interface.RuntimeId

    @property
    def technology(self) -> "TechnologyImpl":
        return self._technology

    @property
    def parent(self) -> Optional["Adapter"]:
        return self.technology.adapter_factory.get_parent_adapter(self, False)

    @property
    def children(self) -> List["Adapter"]:
        raise NotImplementedError("children")

    @property
    def supported_strategies(self) -> Set[str]:
        result = super().supported_strategies
        if self.valid:
            result.union(set(self.adapter_interface.SupportedStrategies))

        return result

    @overload
    def get_strategy(self, strategy_type: Type[TStrategyBase]) -> TStrategyBase: ...

    @overload
    def get_strategy(self, strategy_type: Type[TStrategyBase], raise_exception: bool) -> Optional[TStrategyBase]: ...

    def get_strategy(self, strategy_type: Type[TStrategyBase], raise_exception: bool = True) -> Optional[TStrategyBase]:
        if hasattr(strategy_type, "strategy_name"):
            strategy_name = getattr(strategy_type, "strategy_name")

            if strategy_name is not None and hasattr(self.adapter_interface, "GetStrategy"):
                result = self.adapter_interface.GetStrategy(strategy_name, False)
                if result is not None:
                    self._strategy_cache[strategy_name] = result
                    return cast(TStrategyBase, result)

        return super().get_strategy(strategy_type, raise_exception)
