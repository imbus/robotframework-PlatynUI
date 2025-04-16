# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Any, Callable, Dict, List, Optional, Set, Type, TypeVar, cast, overload

from .adapter import Adapter, TStrategyBase
from .strategybase import StrategyBase
from .technology import Technology
from .weight_calculator import WeightCalculator

__all__ = [
    "AdapterProxy",
    "AdapterProxyFactory",
    "adapter_proxy_for",
]


class AdapterProxy(Adapter):
    def __init__(self, adapter: Adapter):
        self._adapter = adapter

    @property
    def adapter(self) -> Adapter:
        return self._adapter

    @property
    def valid(self) -> bool:
        return self.adapter.valid

    @property
    def id(self) -> str:
        return self.adapter.id

    @property
    def name(self) -> str:
        return self.adapter.name

    @property
    def class_name(self) -> str:
        return self.adapter.class_name

    @property
    def tag_name(self) -> str:
        return self.adapter.tag_name

    @property
    def role(self) -> str:
        return self.adapter.role

    @property
    def supported_roles(self) -> Set[str]:
        return self.adapter.supported_roles

    @property
    def type(self) -> str:
        return self.adapter.type

    @property
    def supported_types(self) -> Set[str]:
        return self.adapter.supported_types

    @property
    def framework_id(self) -> str:
        return self.adapter.framework_id

    @property
    def runtime_id(self) -> Any:
        return self.adapter.runtime_id

    @property
    def technology(self) -> Technology:
        return self.adapter.technology

    @property
    def _self_implemented_strategies(self) -> Set[str]:
        return set(
            [
                getattr(i, "strategy_name")
                for i in type(self).mro()
                if issubclass(i, StrategyBase) and i != StrategyBase and hasattr(i, "strategy_name")
            ]
        )

    @property
    def supported_strategies(self) -> Set[str]:
        return self._self_implemented_strategies.union(self.adapter.supported_strategies)

    @overload
    def get_strategy(self, strategy_type: Type[TStrategyBase]) -> TStrategyBase: ...

    @overload
    def get_strategy(self, strategy_type: Type[TStrategyBase], raise_exception: bool) -> Optional[TStrategyBase]: ...

    def get_strategy(self, strategy_type: Type[TStrategyBase], raise_exception: bool = True) -> Optional[TStrategyBase]:
        if hasattr(strategy_type, "strategy_name"):
            if strategy_type.strategy_name is not None:
                if strategy_type.strategy_name in self._self_implemented_strategies:
                    return cast(strategy_type, self)  # type: ignore

        return self.adapter.get_strategy(strategy_type, raise_exception)

    @property
    def parent(self) -> Optional["Adapter"]:
        return self.adapter.parent

    @property
    def children(self) -> List["Adapter"]:
        return self.adapter.children


TAdapter = TypeVar("TAdapter", bound=Adapter)


class AdapterProxyFactory:
    class Entry:
        def __init__(self, proxy_type: Type[AdapterProxy], criterias: Dict[str, object]):
            self.proxy_type = proxy_type
            self.criterias = criterias

    registered_adapters: List[Entry] = []

    @classmethod
    def register_proxy(cls, adapter_cls: Type[AdapterProxy], criterias: Dict[str, Any]) -> None:
        cls.registered_adapters.append(AdapterProxyFactory.Entry(adapter_cls, criterias.copy()))

    @classmethod
    def find_proxy_for(cls, adapter: TAdapter) -> TAdapter:
        weights = []
        calculator = WeightCalculator(adapter)

        for e in cls.registered_adapters:
            weights.append((calculator.calculate(e.criterias), e.proxy_type))

        if len(weights) > 0:
            last = sorted(weights, key=lambda x: x[0])[-1]

            if last[0] > 0:
                return cast(TAdapter, last[1](adapter))

        return adapter


def adapter_proxy_for(
    role: Optional[str] = None,
    framework_id: Optional[str] = None,
    class_name: Optional[str] = None,
    tag_name: Optional[str] = None,
    properties: Optional[Dict[str, str]] = None,
    technology: Optional[Type[Any]] = None,
    native_properties: Optional[Dict[str, str]] = None,
    **decorator_kwargs: Any,
) -> Callable[[Type[AdapterProxy]], Type[AdapterProxy]]:
    def decorator(cls: Type[AdapterProxy]) -> Type[AdapterProxy]:
        AdapterProxyFactory.register_proxy(
            cls,
            {
                "role": role,
                "framework_id": framework_id,
                "class_name": class_name,
                "tag_name": tag_name,
                "technology": technology,
                "properties": properties,
                "native_properties": native_properties,
                **decorator_kwargs,
            },
        )
        return cls

    return decorator
