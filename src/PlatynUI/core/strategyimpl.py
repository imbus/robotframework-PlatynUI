# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from collections import defaultdict
from typing import TYPE_CHECKING, Any, Callable, Dict, List, Optional, Type, TypeVar, cast

from .strategybase import StrategyBase
from .weight_calculator import WeightCalculator

if TYPE_CHECKING:
    from .adapter import Adapter

__all__ = [
    "StrategyImpl",
    "StrategyImplFactory",
    "strategy_impl_for",
]


class StrategyImpl(StrategyBase):
    def __init__(self, adapter: "Adapter"):
        self._adapter = adapter

    @property
    def adapter(self) -> "Adapter":
        return self._adapter

    @property
    def valid(self) -> bool:
        return self.adapter.valid


TStrategyImpl = TypeVar("TStrategyImpl", bound=StrategyBase)


class StrategyImplFactory:
    class Entry:
        def __init__(self, proxy_type: Type[StrategyImpl], criterias: Dict[str, object]):
            self.proxy_type = proxy_type
            self.criterias = criterias

    registered_impls: Dict[str, List[Entry]] = defaultdict(list)

    @classmethod
    def register_proxy(cls, impl_cls: Type[StrategyImpl], criterias: Dict[str, Any]) -> None:
        strategy_names = set(
            [
                getattr(i, "strategy_name")
                for i in impl_cls.mro()
                if issubclass(i, StrategyBase) and i != StrategyBase and hasattr(i, "strategy_name")
            ]
        )
        for name in strategy_names:
            cls.registered_impls[name].append(StrategyImplFactory.Entry(impl_cls, criterias.copy()))

    @classmethod
    def find_strategy_impl_for(cls, adapter: "Adapter", strategy_cls: Type[TStrategyImpl]) -> Optional[TStrategyImpl]:
        strategy_name = getattr(strategy_cls, "strategy_name", None)
        if strategy_name is None:
            return None

        weights = []
        calculator = WeightCalculator(adapter)

        for e in cls.registered_impls[strategy_name]:
            weights.append((calculator.calculate(e.criterias), e.proxy_type))

        if len(weights) > 0:
            last = sorted(weights, key=lambda x: x[0])[-1]

            if last[0] > 0:
                return cast(TStrategyImpl, last[1](adapter))

        return None


def strategy_impl_for(
    role: Optional[str] = None,
    framework_id: Optional[str] = None,
    class_name: Optional[str] = None,
    tag_name: Optional[str] = None,
    technology: Optional[Type[Any]] = None,
    properties: Optional[Dict[str, Any]] = None,
    native_properties: Optional[Dict[str, Any]] = None,
    **decorator_kwargs: Any,
) -> Callable[[Type[StrategyImpl]], Type[StrategyImpl]]:
    def decorator(cls: Type[StrategyImpl]) -> Type[StrategyImpl]:
        StrategyImplFactory.register_proxy(
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
