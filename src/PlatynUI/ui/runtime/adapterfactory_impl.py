# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import TYPE_CHECKING, List, Optional, Type, cast, overload

from PlatynUI.core import (
    Adapter,
    AdapterFactory,
    AdapterProxy,
    AdapterProxyFactory,
    ContextBase,
    LocatorBase,
    TContextBase,
)
from PlatynUI.core.exceptions import AdapterNotFoundError

from ..locator import Locator
from .adapter_impl import AdapterImpl
from .dotnet_interface import DotNetInterface

if TYPE_CHECKING:
    from PlatynUI.Runtime.Core import IAdapter, INode

    from .technology_impl import TechnologyImpl


class AdapterFactoryImpl(AdapterFactory):
    def __init__(self, technology: "TechnologyImpl") -> None:
        self._technology = technology

    @overload
    def get_adapter(
        self,
        locator: "LocatorBase",
        parent: Optional["ContextBase"] = None,
        context_type: Optional[Type["ContextBase"]] = None,
    ) -> Adapter:
        pass

    @overload
    def get_adapter(
        self,
        locator: "LocatorBase",
        parent: Optional["ContextBase"] = None,
        context_type: Optional[Type["ContextBase"]] = None,
        raise_error: bool = True,
    ) -> Optional["Adapter"]:
        pass

    def get_adapter(
        self,
        locator: "LocatorBase",
        parent: Optional["ContextBase"] = None,
        context_type: Optional[Type["ContextBase"]] = None,
        raise_error: bool = True,
    ) -> Optional["Adapter"]:
        if not isinstance(locator, Locator):
            if raise_error:
                raise ValueError(f"Invalid locator type: {locator}")
            return None

        parent_adapter = parent.adapter if parent else None
        while parent_adapter is not None and isinstance(parent_adapter, AdapterProxy):
            parent_adapter = parent_adapter.adapter

        parent_impl = (
            parent_adapter._adapter_interface if parent_adapter and isinstance(parent_adapter, AdapterImpl) else None
        )
        path = locator.get_path(parent, context_type=context_type)

        result = None
        try:
            refresh = parent_adapter is None
            if isinstance(parent_adapter, AdapterImpl):
                last_xpath, last_result = parent_adapter.last_children_search_data or (None, None)
                if last_xpath == path and not last_result:
                    result = last_result
                    refresh = True

            result = DotNetInterface.finder().FindSingleNode(cast("INode", parent_impl), path, False, refresh)

            if isinstance(parent_adapter, AdapterImpl):
                parent_adapter.last_children_search_data = (path, result is not None)

            if result is None and raise_error:
                raise AdapterNotFoundError(
                    f"Element for '{path}' not found"
                    if parent is None
                    else f"Element for {path:r} not found in {parent}"
                )
        except AdapterNotFoundError:
            if raise_error:
                raise

        if result is None:
            return None

        return AdapterProxyFactory.find_proxy_for(AdapterImpl(cast("IAdapter", result), self._technology))

    def get_children_adapters(
        self,
        parent: Optional[ContextBase],
        context_type: Optional[Type[TContextBase]],
        locator: LocatorBase,
        raise_error: Optional[bool] = True,
    ) -> List["Adapter"]:
        # TODO
        return []

    def get_parent_adapter(
        self,
        adapter: Optional["ContextBase"],
        raise_error: bool = True,
    ) -> Optional["Adapter"]:
        result = DotNetInterface.finder().FindSingleNode(cast("INode", adapter.adapter_interface), "..", False, False)
        if result is None and raise_error:
            raise AdapterNotFoundError(f"Parent for '{result}' not found")
        return AdapterProxyFactory.find_proxy_for(AdapterImpl(cast("IAdapter", result), self._technology))
