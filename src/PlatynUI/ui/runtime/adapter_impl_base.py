# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import TYPE_CHECKING, Protocol

from PlatynUI.core.technology import Technology

# pyright: reportMissingModuleSource=false

if TYPE_CHECKING:
    from PlatynUI.Runtime.Core import IAdapter


class AdapterImplBase(Protocol):
    @property
    def adapter_interface(self) -> "IAdapter": ...

    @property
    def valid(self) -> bool: ...

    def invalidate(self) -> None: ...

    @property
    def technology(self) -> "Technology": ...
