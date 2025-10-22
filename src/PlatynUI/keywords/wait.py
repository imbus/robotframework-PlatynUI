# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from datetime import timedelta
from typing import Optional

from robotlibcore import keyword

from .types import ElementDescriptor

__all__ = ["Wait"]


class Wait:
    @keyword
    def ensure_exists(
        self, descriptor: ElementDescriptor, timeout: Optional[timedelta] = None, raise_exception: bool = True
    ) -> bool:
        """Ensures that the element exists within an optional timeout.

        Resolves ``descriptor`` and checks whether the element **exists**. If the
        element is not found within ``timeout`` and ``raise_exception`` is ``True``,
        the keyword raises an error. If ``raise_exception`` is ``False``, the keyword
        returns ``False`` instead of failing the test.

        | =Arguments=        | =Description= |
        | ``descriptor``     | An ``ElementDescriptor`` that resolves to the target element. |
        | ``timeout``        | Optional time to wait for existence (e.g., ``5 seconds``, ``1.5 s``). If omitted, uses the library default. |
        | ``raise_exception``| If ``True`` (default), fails the keyword on timeout; if ``False``, returns ``False`` instead of failing. |

        | =Returns= | =Description= |
        | ``bool``  | ``True`` if the element exists (within the timeout). With ``raise_exception=True``, the keyword fails instead of returning ``False``. |

        Examples (checking that the Calculator window \"Rechner\" exists):

        # 1) Using a pre-defined variable (descriptor object) |
        | `Ensure Exists`    ${rechner_window}    timeout=3 s |    # passes or fails; no variable needed |

        # 2) Building a simple variable (locator as string) |
        | ${win}=            Set Variable    xpath=//Window[@Name="Rechner"] |
        | `Ensure Exists`    ${win}    timeout=3 s |                # assert-style (fails on timeout) |
        | ${exists}=         `Ensure Exists`    ${win}    timeout=3 s    raise_exception=False |
        | Should Be True     ${exists} |

        # 3) Passing the descriptor/locator directly |
        | `Ensure Exists`    xpath=//Window[@Name="Rechner"]    timeout=3 s |
        | ${exists}=         `Ensure Exists`    xpath=//Window[@Name="Rechner"]    timeout=5 s    raise_exception=False |
        | Should Be True     ${exists} |

        See also: `Activate`, `Deactivate`, `Mouse Click`, `Get Text`, `Is Active`.
        """
        return descriptor(False).exists(
            timeout=None if timeout is None else timeout.total_seconds(), raise_exception=raise_exception
        )
