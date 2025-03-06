# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from abc import abstractmethod

from ...core import StrategyBase

__all__ = ["Closeable", "HasCanClose"]


class HasCanClose(StrategyBase):
    strategy_name = "org.platynui.strategies.HasCanClose"

    @property
    @abstractmethod
    def can_close(self) -> bool:
        pass


class Closeable(StrategyBase):
    strategy_name = "org.platynui.strategies.Closeable"

    @abstractmethod
    def close(self) -> None:
        pass
