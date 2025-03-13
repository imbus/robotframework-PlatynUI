# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from ..core import context
from .control import Control

__all__ = ["Group", "Pane"]


@context
class Pane(Control):
    pass


@context
class Group(Control):
    pass
