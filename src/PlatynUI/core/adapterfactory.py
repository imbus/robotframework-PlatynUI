# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from abc import ABC, abstractmethod
from typing import TYPE_CHECKING, List, Optional, Type, overload

if TYPE_CHECKING:
    from .adapter import Adapter
    from .contextbase import ContextBase
    from .locatorbase import LocatorBase

__all__ = ["AdapterFactory"]


class AdapterFactory(ABC):
    @overload
    def get_adapter(
        self,
        locator: "LocatorBase",
        parent: Optional["ContextBase"] = None,
        context_type: Optional[Type["ContextBase"]] = None,
    ) -> "Adapter":
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

    @abstractmethod
    def get_adapter(
        self,
        locator: "LocatorBase",
        parent: Optional["ContextBase"] = None,
        context_type: Optional[Type["ContextBase"]] = None,
        raise_error: bool = True,
    ) -> Optional["Adapter"]:
        pass

    @abstractmethod
    def get_children_adapters(
        self,
        parent: Optional["ContextBase"],
        context_type: Optional[Type["ContextBase"]],
        locator: "LocatorBase",
        raise_error: bool = True,
    ) -> List["Adapter"]:
        pass

    @abstractmethod
    def get_parent_adapter(
        self,
        adapter: Optional["ContextBase"],
        raise_error: bool = True,
    ) -> Optional["Adapter"]:
        pass
