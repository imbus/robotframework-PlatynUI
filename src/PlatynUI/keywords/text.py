# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from robotlibcore import keyword

from ..ui.strategies import EditableText, Text
from .assertable import assertable
from .types import ElementDescriptor


class TextKeywords:
    @keyword
    def set_text(self, descriptor: ElementDescriptor[EditableText], text: str) -> None:
        """Sets the element's text content.

        Resolves ``descriptor`` and replaces the current text/value of the element
        with ``text`` using the element's native editable interface. This keyword
        does not emulate keystrokes; it sets the value directly.

        | =Arguments=  | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[EditableText]`` that resolves to an element supporting text editing. |
        | ``text``       | The string to set as the element's content. |

        | =Returns= | =Description= |
        | ``None``  | This keyword does not return a value. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Set Text`    ${username_field}    testuser1 |

        # 2) Building a simple variable (locator as string) |
        | ${field}=     Set Variable    xpath=//Edit[@Name="username_edit"] |
        | `Set Text`    ${field}    test_user1 |

        # 3) Passing the descriptor/locator directly |
        | `Set Text`    xpath=//Edit[@AutomationId="comment_edit"]    Hello world! |

        See also: `Get Text`, `Type Keys`, `Press Keys`, `Ensure Exists`, `Mouse Click`.
        """
        descriptor().set_text(text)

    @keyword
    @assertable
    def get_text(self, descriptor: ElementDescriptor[Text]) -> str:
        """Returns the element's text content.

        Resolves ``descriptor`` and reads the element's text as exposed by the UI
        framework. This keyword does not modify the element state and is suitable
        for use in assertions.

        | =Arguments=  | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[Text]`` that resolves to an element exposing a ``text`` property. |

        | =Returns= | =Description= |
        | ``str``    | The text content of the element. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${value}=    `Get Text`    ${username_field} |
        | Should Contain    ${value}    testuser1 |

        # 2) Building a simple variable (locator as string) |
        | ${label}=    Set Variable    xpath=//Text[@Name="status"] |
        | ${txt}=      `Get Text`    ${label} |
        | Should Be Equal    ${txt}    Ready |

        # 3) Passing the descriptor/locator directly |
        | ${title}=    `Get Text`    xpath=//Text[@AutomationId="WindowTitle"] |
        | Should Not Be Empty    ${title} |

        See also: `Set Text`, `Ensure Exists`, `Is Active`, `Mouse Click`, `Type Keys`, `Press Keys`.
        """
        result = descriptor().text
        return result
