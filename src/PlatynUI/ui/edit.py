# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from ..core import context
from .text import Text

__all__ = ["Edit"]


@context
class Edit(Text):
    pass
