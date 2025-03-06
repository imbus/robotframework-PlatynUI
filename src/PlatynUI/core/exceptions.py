# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

__all__ = [
    "AdapterNotFoundError",
    "AdapterNotFoundFatalError",
    "AdapterNotSupportsStrategyError",
    "AdapterNotValidError",
    "CannotEnsureError",
    "InvalidArgumentError",
    "NoDisplayProxyError",
    "NoKeyboardProxyError",
    "NoLocatorDefinedError",
    "NoMouseProxyError",
    "NotAStrategyTypeError",
    "NotSupportedError",
    "PlatyUiError",
    "PlatynUiException",
    "PlatynUiFatalError",
]


class NotSupportedError(Exception):
    pass


class InvalidArgumentError(Exception):
    pass


class PlatynUiException(Exception):  # noqa: N818
    pass


class PlatyUiError(PlatynUiException):
    pass


class PlatynUiFatalError(PlatynUiException):
    pass


class AdapterNotValidError(PlatynUiException):
    pass


class AdapterNotFoundError(PlatynUiException):
    pass


class AdapterNotFoundFatalError(PlatynUiFatalError):
    pass


class NotAStrategyTypeError(PlatyUiError):
    pass


class AdapterNotSupportsStrategyError(PlatyUiError):
    pass


class CannotEnsureError(PlatyUiError):
    pass


class NoKeyboardProxyError(PlatyUiError):
    pass


class NoDisplayProxyError(PlatyUiError):
    pass


class NoMouseProxyError(PlatyUiError):
    pass


class NoLocatorDefinedError(PlatyUiError):
    pass
