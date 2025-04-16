# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

import collections
import enum
import numbers
import xml.sax.saxutils as xmlutils
from typing import Any, Dict, List, Optional, Type, cast

from typing_extensions import Self

from PlatynUI import ui
from PlatynUI.core import Adapter, ContextBase, LocatorBase, LocatorScope, TContextBase, Technology

from .runtime.technology_impl import get_technology

__all__ = ["Locator", "LocatorScope", "locator"]


class Locator(LocatorBase):
    node: Optional[str] = None
    prefix: Optional[str] = None
    use_default_prefix: bool = False
    axis: Optional[str] = None
    path: Optional[str] = None
    __id: Optional[str] = None
    __name: Optional[str] = None
    __class_name: Optional[str] = None
    __role: Optional[str] = None
    __runtime_id: Optional[str] = None
    __framework_id: Optional[str] = None
    index: Optional[int] = None
    scope: Optional[LocatorScope] = None
    position: Optional[int] = None

    __xpath_axis = {
        LocatorScope.Root: "/",
        LocatorScope.Descendants: ".//",
        LocatorScope.Children: "",
        LocatorScope.Parent: "parent::",
        LocatorScope.Ancestor: "ancestor::",
        LocatorScope.AncestorOrSelf: "ancestor-or-self::",
        LocatorScope.DescendantsOrSelf: "descendant-or-self::",
        LocatorScope.Following: "following::",
        LocatorScope.FollowingSibling: "following-sibling::",
        LocatorScope.Preceding: "preceding::",
        LocatorScope.PrecedingSibling: "preceding-sibling::",
    }

    def __init__(
        self,
        *args: Any,
        path: Optional[str] = None,
        id: Optional[str] = None,
        name: Optional[str] = None,
        class_name: Optional[str] = None,
        role: Optional[str] = None,
        prefix: Optional[str] = None,
        use_default_prefix: Optional[bool] = None,
        runtime_id: Optional[str] = None,
        framework_id: Optional[str] = None,
        index: Optional[int] = None,
        node: Optional[str] = None,
        scope: Optional[LocatorScope] = None,
        axis: Optional[str] = None,
        position: Optional[int] = None,
        **kwargs: Any,
    ) -> None:
        super().__init__()

        self.custom_attributes: List[Any] = list(args)
        self.attributes: Dict[str, Any] = dict(kwargs)

        self.path = path
        self.node = node
        self.prefix = prefix

        self.axis = axis

        if id is not None:
            self.id = id

        if name is not None:
            self.name = name

        if class_name is not None:
            self.class_name = class_name

        if role is not None:
            self.role = role

        if runtime_id is not None:
            self.runtime_id = runtime_id

        if framework_id is not None:
            self.framework_id = framework_id

        if use_default_prefix is not None:
            self.use_default_prefix = use_default_prefix

        self.index = index
        self.position = position
        self.scope = scope

    @property
    def id(self) -> Optional[str]:
        return self.__id

    @id.setter
    def id(self, v: Optional[str]) -> None:
        self.__id = v
        if v is None:
            self.attributes.pop("Id", None)
        else:
            self.attributes["Id"] = self.__id

    @property
    def name(self) -> Optional[str]:
        return self.__name

    @name.setter
    def name(self, v: Optional[str]) -> None:
        self.__name = v
        if v is None:
            self.attributes.pop("Name", None)
        else:
            self.attributes["Name"] = self.__name

    @property
    def class_name(self) -> Optional[str]:
        return self.__class_name

    @class_name.setter
    def class_name(self, v: Optional[str]) -> None:
        self.__class_name = v
        if v is None:
            self.attributes.pop("ClassName", None)
        else:
            self.attributes["ClassName"] = self.__class_name

    @property
    def role(self) -> Optional[str]:
        return self.__role

    @role.setter
    def role(self, v: Optional[str]) -> None:
        self.__role = v

    @property
    def framework_id(self) -> Optional[str]:
        return self.__framework_id

    @framework_id.setter
    def framework_id(self, v: Optional[str]) -> None:
        self.__framework_id = v
        if v is None:
            self.attributes.pop("FrameworkId", None)
        else:
            self.attributes["FrameworkId"] = self.__framework_id

    @property
    def runtime_id(self) -> Optional[str]:
        return self.__runtime_id

    @runtime_id.setter
    def runtime_id(self, v: Optional[str]) -> None:
        self.__runtime_id = v
        if v is None:
            self.attributes.pop("RuntimeId", None)
        else:
            self.attributes["RuntimeId"] = self.__runtime_id

    def _create_technology(self) -> Technology:
        return get_technology()

    def create_context(
        self, context_parent: Optional[ContextBase], context_type: Optional[Type[TContextBase]]
    ) -> TContextBase:
        if isinstance(context_parent, ui.DesktopBase):
            context_parent = None

        if context_type is None:
            context_type = cast(Type[TContextBase], ui.Element)

        return super().create_context(context_parent=context_parent, context_type=context_type)

    def __repr__(self) -> str:
        if self.__last_path is not None:
            params = "path=%s" % repr(self.__last_path)
        else:
            params = ""

            if self.path is not None:
                if params != "":
                    params += ", "
                params += "path=%s" % repr(self.path)

            if self.axis is not None:
                if params != "":
                    params += ", "
                params += "axis=%s" % repr(self.axis)

            if self.node is not None:
                if params != "":
                    params += ", "
                params += "node=%s" % repr(self.node)

            if self.prefix is not None:
                if params != "":
                    params += ", "
                params += "prefix=%s" % repr(self.prefix)

            for v in self.custom_attributes:
                if v is not None:
                    if params != "":
                        params += ", "
                    params += '"%s"' % str(v)

            for n, v in self.attributes.items():
                if v is not None:
                    if params != "":
                        params += ", "
                    params += "%s=%s" % (n, repr(v))

            if self.scope is not None:
                if params != "":
                    params += ", "
                params += "scope=%s" % repr(self.scope)

            if self.position is not None:
                if params != "":
                    params += ", "
                params += "position=%s" % repr(self.position)

        return "locator(%s)" % params

    __last_path: Optional[str] = None

    __last_parent: Optional[ContextBase] = None
    __last_context_type: Optional[Type[Any]] = None
    __last_attributes: Optional[Dict[str, Any]] = None
    __last_custom_attributes: Optional[List[Any]] = None

    def get_path(self, parent: Optional[ContextBase], context_type: Optional[Type[TContextBase]]) -> str:
        combined = (
            self.copy_from(getattr(context_type, "_locator"))
            if context_type is not None and hasattr(context_type, "_locator")
            else self
        )

        if (
            combined.__last_path is not None
            and combined.__last_parent == parent
            and combined.__last_context_type == context_type
            and combined.attributes == combined.__last_attributes
            and combined.__last_custom_attributes == combined.custom_attributes
        ):
            return combined.__last_path

        combined.__last_parent = parent
        combined.__last_context_type = context_type
        combined.__last_attributes = combined.attributes.copy()
        combined.__last_custom_attributes = combined.custom_attributes.copy()

        result = ""

        real_path = combined.path if combined.path is not None else None

        if real_path is not None:
            result = real_path
        else:
            attrs = ""

            for n, v in combined.attributes.items():
                if v is not None:
                    if attrs != "":
                        attrs += " and "
                    attrs += "@%s=%s" % (n, combined.xquery_repr(v))

            for v in combined.custom_attributes:
                if v is not None:
                    if attrs != "":
                        attrs += " and "
                    attrs += str(v)

            if combined.position is not None:
                if attrs != "":
                    attrs += " and "
                attrs += "position()=%s" % combined.position

            role = self.role or getattr(context_type, "default_role", None)
            if combined.node is not None:
                result = combined.node + result
            elif role:
                result = role + result
            else:
                result = "*" + result

            prefix = self.prefix or getattr(context_type, "default_prefix", None)

            if combined.prefix is not None:
                result = combined.prefix + ":" + result
            elif self.use_default_prefix and prefix:
                result = prefix + ":" + result

            if combined.axis is not None:
                result = combined.axis + result
            else:
                real_scope = combined.scope
                if real_scope is None:
                    if parent is None:
                        real_scope = LocatorScope.Children
                    else:
                        real_scope = (
                            LocatorScope.Children
                            if isinstance(parent, (ui.Application, ui.DesktopBase))
                            else LocatorScope.Descendants
                        )

                result = combined.__xpath_axis[real_scope] + result

            if attrs != "":
                attrs = "[%s]" % attrs
            if combined.index is not None:
                attrs += "[%s]" % combined.index

            result += attrs

        self.__last_path = result

        return result

    def copy_from(self, other: Optional[LocatorBase]) -> Self:
        if other is None:
            return self

        if isinstance(other, type(self)):
            for n in [
                "path",
                "id",
                "name",
                "class_name",
                "role",
                "prefix",
                "framework_id",
                "index",
                "node",
                "axis",
                "scope",
                "runtime_id",
                "position",
                "use_default_prefix",
            ]:
                v = getattr(self, n)
                if v is None:
                    setattr(self, n, getattr(other, n))

            o = cast(Locator, other)

            if o._technology is not None:
                self._technology = o._technology

            for a in o.custom_attributes:
                if a not in self.custom_attributes:
                    self.custom_attributes.append(a)

            for a in o.attributes:
                if a not in self.attributes:
                    self.attributes[a] = o.attributes[a]

        return self

    def copy(self) -> "Locator":
        # return copy.deepcopy(self)
        result = Locator()

        result._technology = self._technology

        result.node = self.node
        result.prefix = self.prefix
        result.axis = self.axis
        result.path = self.path
        result.name = self.name
        result.id = self.id
        result.class_name = self.class_name
        result.role = self.role
        result.framework_id = self.framework_id
        result.index = self.index
        result.scope = self.scope
        result.position = self.position
        result.runtime_id = self.runtime_id
        result.attributes = self.attributes.copy()
        result.custom_attributes = self.custom_attributes.copy()
        result.use_default_prefix = self.use_default_prefix

        return result

    def create_children_locator(self, *args: Any, **kwargs: Any) -> LocatorBase:
        return Locator(*args, **kwargs)

    def make_unique_locator(self, adapter: Adapter) -> LocatorBase:
        result = self.copy()
        result.runtime_id = adapter.runtime_id
        return result

    def create_child_locator(self, adapter: Adapter) -> LocatorBase:
        return Locator(scope=LocatorScope.Descendants, runtime_id=adapter.runtime_id)

    def create_parent_locator(self, adapter: Adapter) -> LocatorBase:
        return Locator(path="..")

    def create_top_level_locator(self, adapter: "Adapter") -> Optional["LocatorBase"]:
        return Locator(runtime_id=adapter.runtime_id, scope=LocatorScope.Root)

    @staticmethod
    def xquery_repr(v: Any) -> str:
        if isinstance(v, enum.Enum):
            return '"' + repr(v.value) + '"'
        if isinstance(v, bool):
            return repr(v).lower() + "()"
        if isinstance(v, str):
            return xmlutils.quoteattr(v)
        if isinstance(v, numbers.Number):
            return repr(v)
        if isinstance(v, dict):
            return "(" + ", ".join([Locator.xquery_repr(c) for c in v.items()]) + ")"
        if isinstance(v, collections.Iterable):
            return "(" + ", ".join([Locator.xquery_repr(c) for c in v]) + ")"
        return xmlutils.quoteattr(repr(v))


locator = Locator
