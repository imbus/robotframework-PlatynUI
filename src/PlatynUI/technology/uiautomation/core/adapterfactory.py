# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import List, Optional, Type, overload

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
from .impls import *  # noqa: F403
from .loader import DotNetInterface
from .technology import UiaTechnology
from .uiaadapter import UiaAdapter


class UiaAdapterFactory(AdapterFactory):
    def __init__(self, technology: UiaTechnology) -> None:
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
            return None

        parent_adapter = parent.adapter if parent else None
        while parent_adapter is not None and isinstance(parent_adapter, AdapterProxy):
            parent_adapter = parent_adapter.adapter

        uia_parent = parent_adapter.element if parent_adapter and isinstance(parent_adapter, UiaAdapter) else None
        path = locator.get_path(parent, context_type=context_type)

        result = None
        try:
            result = DotNetInterface.finder().FindSingleElement(uia_parent, path, False)
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

        return AdapterProxyFactory.find_proxy_for(UiaAdapter(result, self._technology))

    def get_children_adapters(
        self,
        parent: Optional[ContextBase],
        context_type: Optional[Type[TContextBase]],
        locator: LocatorBase,
        raise_error: Optional[bool] = True,
    ) -> List["Adapter"]:
        # TODO
        return []
