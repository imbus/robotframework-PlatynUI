# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import weakref
from abc import ABCMeta
from typing import Any, Callable, Dict, Iterator, List, Literal, Optional, Set, Type, Union, overload

from typing_extensions import Self, TypeVar

from .adapter import Adapter
from .ensure import Ensure
from .exceptions import NoLocatorDefinedError, PlatynUiFatalError
from .locatorbase import LocatorBase
from .locatorscope import LocatorScope
from .predicate import predicate
from .settings import Settings
from .strategies import NativeProperties, Properties
from .weight_calculator import WeightCalculator

__all__ = ["ContextBase", "ContextFactory", "TContextBase", "UnknownContext", "context"]


class ContextBase(metaclass=ABCMeta):
    default_role: Optional[str] = None
    default_prefix: Optional[str] = None

    _locator: Optional[LocatorBase] = None
    __context_parent: Optional["ContextBase"] = None

    def __init__(
        self,
        locator: LocatorBase = None,
        context_parent: Optional["ContextBase"] = None,
        adapter: Optional[Adapter] = None,
    ) -> None:
        self._context_children: weakref.WeakSet["ContextBase"] = weakref.WeakSet()

        if locator is not None:
            self._locator = locator.copy_from(self._locator)
        else:
            self._locator = self._locator.copy() if self._locator is not None else None

        if self._locator is not None:
            self._locator.context = self

        self.__context_parent = context_parent
        if context_parent is not None and isinstance(context_parent, ContextBase):
            context_parent._context_children.add(self)

        self._adapter = adapter

    def __repr__(self) -> str:
        return "%s(locator=%s)" % (self.__class__.__name__, self._locator)

    @property
    def locator(self) -> LocatorBase:
        if self._locator is None:
            raise NoLocatorDefinedError(f"no locator defined for {self!r}")

        return self._locator

    @locator.setter
    def locator(self, v: LocatorBase) -> None:
        self.invalidate()
        self._locator = v

    @property
    def context_parent(self) -> Optional["ContextBase"]:
        return self.__context_parent

    @context_parent.setter
    def context_parent(self, v: Optional["ContextBase"]) -> None:
        if self.context_parent is not None and isinstance(self.context_parent, ContextBase):
            self.context_parent._context_children.remove(self)
        self.__context_parent = v
        self.invalidate()
        if self.context_parent is not None and isinstance(self.context_parent, ContextBase):
            self.context_parent._context_children.add(self)

    @property
    def adapter(self) -> Adapter:
        self.ensure_that(self._adapter_exists)

        result = self.get_adapter(raise_exception=True)

        if result is None:
            raise PlatynUiFatalError("Adapter is not valid")

        return result

    @adapter.setter
    def adapter(self, v: Adapter) -> None:
        self._adapter = v

    def get_adapter(self, timeout: Optional[float] = None, raise_exception: bool = True) -> Optional[Adapter]:
        self.ensure_that(self._adapter_exists, timeout=timeout, raise_exception=raise_exception)

        try:
            result = self._try_get_adapter(raise_exception)
        except BaseException:
            if raise_exception:
                raise
            return None

        return result

    def _try_get_adapter(self, raise_exception: bool = False) -> Optional[Adapter]:
        if self._adapter is not None and not self._adapter.valid:
            self.invalidate()
        if self._adapter is None:
            self._adapter = self._get_adapter(raise_exception)
        return self._adapter

    def invalidate(self) -> None:
        for c in self._context_children:
            c.invalidate()
        self._adapter = None

    def _get_adapter(self, raise_exception: bool) -> Optional[Adapter]:
        self.ensure_that(self._parent_exists)

        if self._locator is not None:
            return self._get_adapter_from_technology(raise_exception)
        return None

    def _get_adapter_from_technology(
        self,
        raise_exception: bool = True,
    ) -> Optional[Adapter]:
        return self.locator.technology.adapter_factory.get_adapter(
            self.locator, self.context_parent, type(self), raise_exception
        )

    def full_repr(self) -> str:
        result = repr(self)
        if self.context_parent is not None:
            return self.context_parent.full_repr() + "." + result
        return result

    @predicate("{0} exists")
    def _adapter_exists(self, raise_exception: bool = False) -> bool:
        self.ensure_that(self._parent_exists)

        a = self._try_get_adapter(raise_exception)

        return a is not None and a.valid

    @predicate("parent for {0} exists")
    def _parent_exists(self) -> bool:
        if self.context_parent is None:
            return True

        return self.context_parent._adapter_exists(True)

    def ensure_that(
        self,
        *predicates: Optional[Callable[[], bool]],
        timeout: Optional[float] = None,
        raise_exception: Optional[bool] = None,
    ) -> bool:
        return Ensure.that(
            self, *predicates, timeout=timeout, raise_exception=raise_exception, failed_func=self.invalidate
        )

    @property
    def is_valid(self) -> bool:
        return self._adapter is not None and self._adapter.valid

    def exists(self, timeout: Optional[float] = None, raise_exception: bool = False) -> bool:
        if timeout is None:
            timeout = Settings.current().exists_timeout

        return self.ensure_that(self._adapter_exists, timeout=timeout, raise_exception=raise_exception)

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
    def supported_strategies(self) -> Set[str]:
        return self.adapter.supported_strategies

    @property
    def framework_id(self) -> str:
        return self.adapter.framework_id

    @property
    def runtime_id(self) -> Any:
        self.ensure_that(self._adapter_exists, raise_exception=False)

        if not self.is_valid:
            return self

        return self.adapter.runtime_id

    def __enter__(self) -> "Self":
        return self

    def __exit__(self, exc_type: Any, exc_val: Any, exc_tb: Any) -> Literal[False]:
        return False

    def get(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> "TContextBase":
        if locator is None:
            if self.locator is None:
                raise NoLocatorDefinedError("no locator defined for %s" % self)

            locator = self.locator.create_children_locator(*args, **kwargs)

        return locator.copy().create_context(self, context_class)

    def ancestor(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> "TContextBase":
        return self.get(context_class, scope=LocatorScope.Ancestor, locator=locator, *args, **kwargs)

    def ancestors(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> List["TContextBase"]:
        return self.get_all(context_class, scope=LocatorScope.Ancestor, locator=locator, *args, **kwargs)

    def get_child(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> "TContextBase":
        return self.get(context_class, scope=LocatorScope.Children, locator=locator, *args, **kwargs)

    def iter_all(
        self,
        context_class: Optional[Type["TContextBase"]],
        *args: Any,
        locator: Optional[LocatorBase] = None,
        **kwargs: Any,
    ) -> Iterator["TContextBase"]:
        if locator is None:
            if self.locator is None:
                raise NoLocatorDefinedError("no locator defined for %s" % self)

            locator = self.locator.create_children_locator(*args, **kwargs)

        children = self.locator.technology.adapter_factory.get_children_adapters(self, context_class, locator)

        for a in children:
            loc = locator.make_unique_locator(a)
            res = loc.create_context(self, ContextFactory.find_context_class_for(a, context_class))
            res.adapter = a
            if context_class is None or isinstance(res, context_class):
                yield res

    def get_all(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> List["TContextBase"]:
        return list(self.iter_all(context_class, *args, locator=locator, **kwargs))

    def get_children(
        self, context_class: Optional[Type["TContextBase"]], *args: Any, locator: LocatorBase = None, **kwargs: Any
    ) -> List["TContextBase"]:
        return self.get_all(context_class, scope=LocatorScope.Children, locator=locator, *args, **kwargs)

    @property
    def parent(self) -> Optional["ContextBase"]:
        adapter = self.adapter

        if adapter is None or not adapter.valid:
            return None

        parent = adapter.parent
        if parent is None:
            return None

        loc = self.locator.create_parent_locator(parent)

        result: "ContextBase" = loc.create_context(self, ContextFactory.find_context_class_for(parent))
        result.adapter = parent

        return result

    def __iter__(self) -> Iterator["ContextBase"]:
        adapter = self.adapter

        if adapter is None or not adapter.valid:
            return

        if self.locator is not None:
            for a in adapter.children:
                loc = self.locator.create_child_locator(a)
                res: ContextBase = loc.create_context(self, ContextFactory.find_context_class_for(a))
                res.adapter = a
                yield res

    @property
    def children(self) -> List["ContextBase"]:
        return list(self)

    def get_properties(self) -> List[str]:
        self.ensure_that(self._adapter_exists)

        return self.adapter.get_strategy(Properties).get_property_names()

    def get_property_value(self, name: str) -> Any:
        self.ensure_that(self._adapter_exists)

        return self.adapter.get_strategy(Properties).get_property_value(name)

    def get_native_properties(self) -> List[str]:
        self.ensure_that(self._adapter_exists)

        return self.adapter.get_strategy(NativeProperties).get_native_property_names()

    def get_native_property_value(self, name: str) -> Any:
        self.ensure_that(self._adapter_exists)

        return self.adapter.get_strategy(NativeProperties).get_native_property_value(name)


class UnknownContext(ContextBase):
    pass


TContextBase = TypeVar("TContextBase", bound=ContextBase, default=ContextBase)


class ContextFactory:
    class Entry:
        def __init__(self, proxy_type: Type[ContextBase], criterias: Dict[str, object]):
            self.proxy_type = proxy_type
            self.criterias = criterias

    registered_adapters: List[Entry] = []

    @classmethod
    def register_context(cls, adapter_cls: Type[ContextBase], criterias: Dict[str, Any]) -> None:
        cls.registered_adapters.append(ContextFactory.Entry(adapter_cls, criterias.copy()))

    @overload
    @classmethod
    def find_context_class_for(cls, adapter: Adapter, context_type: Optional[Type[TContextBase]]) -> Type[TContextBase]:
        pass

    @overload
    @classmethod
    def find_context_class_for(cls, adapter: Adapter) -> Type[ContextBase]:
        pass

    @classmethod
    def find_context_class_for(
        cls, adapter: Adapter, context_type: Optional[Type[TContextBase]] = None
    ) -> Type[Union[TContextBase, ContextBase]]:
        if context_type is not None:
            return context_type

        weights = []
        calculator = WeightCalculator(adapter)

        for e in cls.registered_adapters:
            weights.append((calculator.calculate(e.criterias), e.proxy_type))

        if len(weights) > 0:
            last = sorted(weights, key=lambda x: x[0])[-1]

            if last[0] > 0:
                return last[1]

        return UnknownContext

    @overload
    @classmethod
    def create_context(
        cls,
        locator: "LocatorBase",
        context_type: Optional[Type[TContextBase]],
        parent: Optional["ContextBase"] = None,
    ) -> TContextBase:
        pass

    @overload
    @classmethod
    def create_context(
        cls,
        locator: "LocatorBase",
        context_type: Optional[Type[TContextBase]] = None,
        parent: Optional["ContextBase"] = None,
    ) -> TContextBase:
        pass

    @classmethod
    def create_context(
        cls,
        locator: "LocatorBase",
        context_type: Optional[Type[TContextBase]] = None,
        parent: Optional["ContextBase"] = None,
        raise_error: bool = True,
    ) -> Optional[TContextBase]:
        adapter = locator.technology.adapter_factory.get_adapter(locator, parent, context_type, raise_error)
        if adapter is None:
            return None

        context_type = cls.find_context_class_for(adapter, context_type)

        return context_type(locator, parent, adapter)


@overload
def context(__cls: Type[TContextBase]) -> Type[TContextBase]: ...


@overload
def context(
    *,
    role: Optional[str] = None,
    framework_id: Optional[str] = None,
    class_name: Optional[str] = None,
    tag_name: Optional[str] = None,
    properties: Optional[Dict[str, str]] = None,
    native_properties: Optional[Dict[str, str]] = None,
    **decorator_kwargs: Any,
) -> Callable[[Type[TContextBase]], Type[TContextBase]]: ...


def context(
    __cls: Optional[Type[TContextBase]] = None,
    *,
    role: Optional[str] = None,
    framework_id: Optional[str] = None,
    class_name: Optional[str] = None,
    tag_name: Optional[str] = None,
    properties: Optional[Dict[str, str]] = None,
    native_properties: Optional[Dict[str, str]] = None,
    prefix: Optional[str] = None,
    **decorator_kwargs: Any,
) -> Union[Type[TContextBase], Callable[[Type[TContextBase]], Type[TContextBase]]]:
    def decorator(cls: Type[TContextBase]) -> Type[TContextBase]:
        setattr(cls, "default_role", role or cls.__name__)

        if prefix is not None:
            setattr(cls, "default_prefix", prefix)

        ContextFactory.register_context(
            cls,
            {
                "role": cls.__name__ if role is None else role,
                "framework_id": framework_id,
                "class_name": class_name,
                "tag_name": tag_name,
                "properties": properties,
                "native_properties": native_properties,
                **decorator_kwargs,
            },
        )

        return cls

    if __cls is None:
        return decorator

    return decorator(__cls)
