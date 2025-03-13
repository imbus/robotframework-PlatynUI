# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from abc import abstractmethod
from typing import Any, ClassVar, List

from ..strategybase import StrategyBase

__all__ = ["NativeProperties", "Properties"]


class Properties(StrategyBase):
    strategy_name: ClassVar[str] = "io.platynui.strategies.Properties"

    @abstractmethod
    def get_property_names(self) -> List[str]: ...

    @abstractmethod
    def get_property_value(self, name: str) -> Any: ...


class NativeProperties(StrategyBase):
    strategy_name: ClassVar[str] = "io.platynui.strategies.NativeProperties"

    @abstractmethod
    def get_native_property_names(self) -> List[str]: ...

    @abstractmethod
    def get_native_property_value(self, name: str) -> Any: ...
