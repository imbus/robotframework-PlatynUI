# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import inspect
import typing
from abc import abstractmethod
from typing import TYPE_CHECKING, Any, Callable, Dict, Optional, Type, TypeVar, Union, cast, get_origin

from .exceptions import NotSupportedError
from .internal import decorator
from .technology import Technology

if TYPE_CHECKING:
    from .adapter import Adapter
    from .contextbase import ContextBase, TContextBase

__all__ = ["LocatorBase"]

TCallable = TypeVar("TCallable", bound=Callable[..., Any])


class LocatorBase(decorator.Decorator):
    _default_name = None

    def __init__(self) -> None:
        self._attributes: Dict[str, Any] = {}

    @property
    def attributes(self) -> Dict[str, Any]:
        return self._attributes

    @attributes.setter
    def attributes(self, value: Dict[str, Any]) -> None:
        self._attributes = value

    def decorate_instance(self, instance: Any, *decorator_args: Any, **decorator_kwargs: Any) -> Any:
        if hasattr(instance, "locator"):
            v = getattr(instance, "locator")
            if v is not None and isinstance(v, type(self)):
                self.copy_from(v)
        return instance

    def decorate_func(self, func: TCallable, *decorator_args: Any, **decorator_kwargs: Any) -> TCallable:
        self._default_name = func.__name__
        prop_name = "_%s_getter_" % func.__name__
        typ = inspect.signature(func).return_annotation

        if typ == inspect.Signature.empty:
            typ = None
        else:
            if get_origin(typ) is Union:
                args = typing.get_args(typ)
                if len(args) < 1:
                    raise NotSupportedError("unsupported union type {}".format(typ))

                typ = cast(Type["ContextBase"], args[0])

            if isinstance(typ, typing.ForwardRef):
                typ = (typ, func)
            elif isinstance(typ, str):
                typ = (typ, func)
            else:
                if not isinstance(typ, type):
                    # TODO what to do when typ is str?
                    raise NotImplementedError("%s as type %s is not allowed for locator" % (repr(typ), type(typ)))

        def get_context(context: Any) -> Any:
            if not hasattr(context, prop_name):
                setattr(context, prop_name, self.copy().create_context(context, typ))
            return getattr(context, prop_name)

        return cast(TCallable, get_context)

    def decorate_class(self, cls: Type[Any], *decorator_args: Any, **decorator_kwargs: Any) -> Type[Any]:
        if hasattr(cls, "_locator"):
            v = getattr(cls, "_locator")
            if v is not None and isinstance(v, type(self)):
                self.copy_from(v)

        cls._locator = self
        return cls

    @abstractmethod
    def __repr__(self) -> str:
        return "LocatorBase()"

    def create_context(
        self, context_parent: Optional["ContextBase"], context_type: Optional[Type["TContextBase"]]
    ) -> "TContextBase":
        if context_type is None:
            from .contextbase import UnknownContext

            context_type = cast(Type["TContextBase"], UnknownContext)

        if get_origin(context_type) is Union:
            args = typing.get_args(context_type)
            if len(args) < 1:
                raise NotSupportedError("unsupported union type {}".format(context_type))

            context_type = cast(Type["TContextBase"], args[0])

        if (
            isinstance(context_type, tuple)
            and len(context_type) == 2
            and isinstance(context_type[0], typing.ForwardRef)
        ):
            context_type = context_type[0]._evaluate(context_type[1].__globals__, None)

        elif isinstance(context_type, tuple) and len(context_type) == 2 and isinstance(context_type[0], str):
            context_type = eval(context_type[0], context_type[1].__globals__, None)

        if not isinstance(context_type, type):
            # TODO what to do when typ is str?
            raise NotImplementedError(
                "%s as type %s is not allowed for locator" % (repr(context_type), type(context_type))
            )

        result = context_type()

        if hasattr(result, "_locator"):
            v = getattr(result, "_locator")
            if v is not None and isinstance(v, type(self)):
                self.copy_from(v)

        self.context = result
        result.locator = self

        result.context_parent = context_parent

        return result

    __context: Optional["ContextBase"] = None

    @property
    def context(self) -> Optional["ContextBase"]:
        return self.__context

    @context.setter
    def context(self, v: "ContextBase") -> None:
        self.__context = v

    @abstractmethod
    def copy_from(self, other: Optional["LocatorBase"]) -> "LocatorBase":
        pass

    @abstractmethod
    def copy(self) -> "LocatorBase":
        pass

    def get_parent_locator(self) -> Optional["LocatorBase"]:
        return (
            self.context.context_parent.locator
            if self.context is not None
            and self.context.context_parent is not None
            and self.context.context_parent.locator is not None
            and isinstance(self.context.context_parent.locator, LocatorBase)
            else None
        )

    _technology: Optional[Technology] = None

    @abstractmethod
    def _create_technology(self) -> Technology: ...

    @property
    def technology(self) -> Technology:
        if self._technology is None:
            parent_locator = self.get_parent_locator()

            if parent_locator is not None:
                return parent_locator.technology

            return self._create_technology()

        return self._technology

    @technology.setter
    def technology(self, value: Technology) -> None:
        self._technology = value

    @abstractmethod
    def create_children_locator(self, *args: Any, **kwargs: Any) -> "LocatorBase":
        pass

    @abstractmethod
    def create_child_locator(self, adapter: "Adapter") -> "LocatorBase":
        pass

    @abstractmethod
    def make_unique_locator(self, adapter: "Adapter") -> "LocatorBase":
        pass

    @abstractmethod
    def create_parent_locator(self, adapter: "Adapter") -> "LocatorBase":
        pass

    def create_top_level_locator(self, adapter: "Adapter") -> typing.Optional["LocatorBase"]:
        return None
