# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from inspect import ismethod
from typing import (
    Callable,
    Optional,
    TypeVar,
    Union,
    overload,
)

from robot.running.context import EXECUTION_CONTEXTS
from robot.running.librarykeyword import LibraryKeyword
from robot.running.model import Argument, Keyword
from robot.running.statusreporter import StatusReporter
from typing_extensions import ParamSpec

T = TypeVar("T")
P = ParamSpec("P")


@overload
def report_as_keyword(__func: Callable[P, T]) -> Callable[P, T]: ...


@overload
def report_as_keyword(
    *,
    name: Optional[str] = None,
) -> Callable[[Callable[P, T]], Callable[P, T]]: ...


def report_as_keyword(
    __func: Optional[Callable[P, T]] = None,
    *,
    name: Optional[str] = None,
) -> Union[Callable[P, T], Callable[[Callable[P, T]], Callable[P, T]]]:
    def decorator(func: Callable[P, T]) -> Callable[P, T]:
        implementation = LibraryKeyword(None, name or func.__qualname__)

        def wrapper(*args: P.args, **kwds: P.kwargs) -> T:
            report_args = (*args, *(Argument(k, v) for k, v in kwds.items()))
            if ismethod(func):
                report_args = report_args[1:]

            data = Keyword(implementation.name, args=report_args)

            if EXECUTION_CONTEXTS.current is None:
                return func(*args, **kwds)

            result = EXECUTION_CONTEXTS.current.steps[-1][1] if EXECUTION_CONTEXTS.current.steps else None

            if result is None:
                result = EXECUTION_CONTEXTS.current.test or EXECUTION_CONTEXTS.current.suite

            if result is None:
                return func(*args, **kwds)
            kw_result = result.body.create_keyword()
            kw_result.config(name=implementation.name, args=data.args)
            try:
                with StatusReporter(data, kw_result, EXECUTION_CONTEXTS.current, implementation=implementation):
                    return func(*args, **kwds)
            except BaseException as e:
                raise e

        return wrapper

    if __func is None:
        return decorator

    return decorator(__func)
