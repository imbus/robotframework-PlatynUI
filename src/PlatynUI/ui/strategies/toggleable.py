# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from abc import abstractmethod

from ...core import StrategyBase
from ..togglestate import ToggleState

__all__ = ["HasToggleState", "ToggleState", "Toggleable"]


class HasToggleState(StrategyBase):
    strategy_name = "org.platynui.strategies.HasToggleState"

    @property
    @abstractmethod
    def state(self) -> ToggleState:
        pass


class Toggleable(StrategyBase):
    strategy_name = "org.platynui.strategies.Toggleable"

    @abstractmethod
    def toggle(self) -> None:
        pass
