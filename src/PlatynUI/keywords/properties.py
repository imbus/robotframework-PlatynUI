# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from enum import Enum, auto
from typing import Union

from robotlibcore import keyword

from ..core.strategies import Properties
from .assertable import assertable
from .types import ElementDescriptor


class PropertyName(Enum):
    BoundingRectangle = auto()
    Enabled = auto()
    Visible = auto()


class Properties:
    @keyword
    @assertable
    def get_property_value(self, descriptor: ElementDescriptor[Properties], name: Union[PropertyName, str]) -> any:
        """Returns the value of a UI property.

        Resolves ``descriptor`` and queries the element's property identified by
        ``name``. The property can be addressed either by the ``PropertyName`` enum
        or by its string representation (case-sensitive unless defined otherwise by
        the backend).

        Supported enum members include (non-exhaustive): ``PropertyName.BoundingRectangle``,
        ``PropertyName.Enabled``, ``PropertyName.Visible``.

        | =Arguments=  | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[Properties]`` that resolves to an element exposing property access. |
        | ``name``       | Property identifier as ``PropertyName`` enum or ``str`` (e.g., ``PropertyName.Enabled`` or ``"Enabled"``). |

        | =Returns= | =Description= |
        | ``any``   | The value of the requested property (e.g., ``bool`` for ``Enabled``/``Visible``, a rectangle/object for ``BoundingRectangle``, etc.). |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${enabled}=    `Get Property Value`    ${submit_button}    ${PropertyName.Enabled} |
        | Should Be True    ${enabled} |
        | ${rect}=       `Get Property Value`    ${submit_button}    ${PropertyName.BoundingRectangle} |
        | Should Not Be Empty    ${rect} |

        # 2) Building a simple variable (locator as string) |
        | ${btn}=        Set Variable    xpath=//Button[@AutomationId="submit_button"] |
        | ${visible}=    `Get Property Value`    ${btn}    Visible |
        | Should Be True    ${visible} |
        | ${rect}=       `Get Property Value`    ${btn}    BoundingRectangle |
        | Should Contain    ${rect}    Width |

        # 3) Passing the descriptor/locator directly |
        | ${enabled}=    `Get Property Value`    xpath=//Edit[@Name="username"]    Enabled |
        | Should Be True    ${enabled} |

        See also: `Get Text`, `Ensure Exists`, `Is Active`, `Mouse Click`, `Set Text`.
        """
        return descriptor().get_property_value(name)

    @keyword
    @assertable
    def get_property_names(self, descriptor: ElementDescriptor[Properties]) -> list[str]:
        """Returns all available property names of the element.

        Resolves ``descriptor`` and queries the set of property identifiers
        supported by the underlying element.

        | =Arguments=  | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[Properties]`` that resolves to an element exposing property access. |

        | =Returns= | =Description= |
        | ``list[str]`` | List of property names (e.g., ``["Enabled", "Visible", "BoundingRectangle", ...]``). |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${names}=    `Get Property Names`    ${submit_button} |
        | List Should Contain Value    ${names}    Enabled |

        # 2) Building a simple variable (locator as string) |
        | ${btn}=      Set Variable    xpath=//Button[@AutomationId="submit_button"] |
        | ${names}=    `Get Property Names`    ${btn} |
        | List Should Contain Value    ${names}    Visible |

        # 3) Passing the descriptor/locator directly |
        | ${names}=    `Get Property Names`    xpath=//Edit[@AutomationId="username"] |
        | Length Should Be    ${names}    > 0 |

        # 4) Alternative style (no pipes), logging a Window's properties |
        ${properties}=    Get Property Names    ${Anmeldefenster}
        Log To Console    ${properties}
        # Example output (typical Window properties, not exhaustive):
        # [ 'Enabled',
        #   'Visible',
        #   'BoundingRectangle',
        #   'Name',
        #   'AutomationId',
        #   'ClassName',
        #   'ControlType',
        #   'ProcessId',
        #   'NativeWindowHandle',
        #   'FrameworkId',
        #   'HasKeyboardFocus',
        #   'IsKeyboardFocusable',
        #   'IsModal',
        #   'IsTopmost',
        #   'CanMaximize',
        #   'CanMinimize',
        #   'IsMinimized',
        #   'IsMaximized' ]

        See also: `Get Property Value`, `Ensure Exists`, `Get Text`, `Is Active`, `Mouse Click`, `Set Text`.
        """
        return descriptor().get_property_names()
