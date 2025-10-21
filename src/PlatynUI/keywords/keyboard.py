# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from robotlibcore import keyword

from ..ui.strategies import HasKeyboard
from .types import ElementDescriptor


class Keyboard:
    @keyword
    def type_keys(self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None) -> None:
        """Types one or more keys into the element.

        Sends key events to the element resolved from ``descriptor`` using the
        element's native keyboard interface. This keyword does **not** clear the
        field automatically and does not paste text; it emits key events in order.

        Use this to send discrete keys (e.g., ``ENTER``, ``TAB``, ``BACKSPACE``) or
        short sequences. For longer text input, consider a dedicated text-typing
        keyword if available.

        | =Arguments= | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[HasKeyboard]`` that resolves to an element exposing a ``keyboard`` interface. |
        | ``*keys``      | One or more key strings to send in sequence (e.g., ``A``, ``ENTER``, ``BACKSPACE``). |
        | ``delay``      | Optional delay (seconds, float) between key events. Defaults to library/system default. |

        | =Returns= | =Description= |
        | ``None``  | This keyword does not return a value. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Type Keys`    ${username_field}    Andromeda |
        | `Type Keys`    ${username_field}    testuser1 |

        # 2) Building a simple variable (locator as string) |
        | ${field}=      Set Variable    xpath=//Edit[@AutomationId="username"] |
        | `Type Keys`    ${field}    <CTRL+a> |
        | `Type Keys`    ${field}    testuser1 |
        | `Type Keys`    .    <TAB> |

        # 3) Passing the descriptor/locator directly |
        | `Type Keys`    xpath=//Edit[@Name="username"]    testuser1 |
        | `Type Keys`    .    <TAB> |

        See also: `Focus`, `Mouse Click`, `Get Text`.
        """
        descriptor().keyboard.type_keys(*keys, delay=delay)

    @keyword
    def press_keys(self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None) -> None:
        """Presses keys on the element.

        Sends keyboard events to the element resolved from ``descriptor`` via its
        native keyboard interface.

        Behavior:
        - Chords like ``<CTRL+s>`` are pressed and released in a single step.
        - Passing a *modifier alone* (e.g., ``<CTRL>``) keeps it **held down**
          until explicitly released with ``Release Keys``.
        - Multiple key tokens are executed in order. ``delay`` is applied between tokens.

        Keys must be provided in PlatynUI's bracket notation, e.g. ``<CTRL+s>``, ``<TAB>``, ``<ESC>``, ``<CTRL>``.

        | =Arguments= | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[HasKeyboard]`` that resolves to an element exposing a ``keyboard`` interface. |
        | ``*keys``      | One or more key tokens (e.g., ``<CTRL+s>``, ``<TAB>``, ``<ESC>``, ``<CTRL>``). |
        | ``delay``      | Optional delay (seconds, float) between key tokens. |

        | =Returns= | =Description= |
        | ``None``  | This keyword does not return a value. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Press Keys`    ${editor}    <CTRL+s> |
        | `Press Keys`    ${editor}    <TAB> |

        # 2) Building a simple variable (locator as string) |
        | ${field}=       Set Variable    xpath=//Edit[@AutomationId="search"] |
        | `Press Keys`    ${field}    <CTRL> |     # hold CTRL down |
        | `Press Keys`    ${field}    <s> |          # types 's' while CTRL is held |
        | `Release Keys`  ${field}    <CTRL> |

        # 3) Passing the descriptor/locator directly |
        | `Press Keys`    xpath=//Edit[@AutomationId="username"]    <TAB> |
        | `Press Keys`    xpath=//Edit[@AutomationId="username"]    <CTRL+a>    <DELETE> |

        See also: `Type Keys`, `Release Keys`, `Mouse Click`, `Activate`, `Deactivate`, `Get Text`.
        """
        descriptor().keyboard.press_keys(*keys, delay=delay)

    @keyword
    def release_keys(
        self, descriptor: ElementDescriptor[HasKeyboard], *keys: str, delay: Optional[float] = None
    ) -> None:
        """Releases previously held keys on the element.

        Use this to release modifiers (e.g., ``<CTRL>``, ``<SHIFT>``, ``<ALT>``) or any
        keys that were intentionally kept down. Keys must be provided using
        PlatynUI's bracket notation, e.g. ``<CTRL>``, ``<SHIFT>``, ``<ALT>``.

        | =Arguments= | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[HasKeyboard]`` that resolves to an element exposing a ``keyboard`` interface. |
        | ``*keys``      | One or more key tokens in bracket notation to release (e.g., ``<CTRL>``, ``<SHIFT>``). |
        | ``delay``      | Optional delay (seconds, float) between releases of individual keys. |

        | =Returns= | =Description= |
        | ``None``  | This keyword does not return a value. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Press Keys`     ${editor}    <CTRL> |
        | `Press Keys`     ${editor}    <s> |
        | `Release Keys`   ${editor}    <CTRL> |

        # 2) Building a simple variable (locator as string) |
        | ${field}=        Set Variable    xpath=//Edit[@AutomationId="notes"] |
        | `Press Keys`     ${field}    <SHIFT> |
        | `Press Keys`     ${field}    <TAB> |
        | `Release Keys`   ${field}    <SHIFT>    delay=0.05 |

        # 3) Passing the descriptor/locator directly |
        | `Press Keys`     xpath=//Edit[@AutomationId="username"]    <CTRL> |
        | `Press Keys`     xpath=//Edit[@AutomationId="username"]    <a> |
        | `Release Keys`   xpath=//Edit[@AutomationId="username"]    <CTRL> |

        See also: `Press Keys`, `Type Keys`, `Mouse Click`, `Activate`, `Deactivate`, `Get Text`.
        """
        descriptor().keyboard.release_keys(*keys, delay=delay)
