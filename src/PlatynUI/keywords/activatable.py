# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from robotlibcore import keyword

from ..ui.strategies import Activatable, Deactivatable, HasIsActive
from .assertable import assertable
from .types import ElementDescriptor

__all__ = ["ActivatableKeywords"]


class ActivatableKeywords:
    @keyword
    def activate(self, descriptor: ElementDescriptor[Activatable]) -> None:
        """Activates an element.

        Performs the element's native *activate* action provided by the UI
        framework. Use this when a control exposes an explicit activation
        semantic (e.g. button, menu item, toggle).

        This keyword resolves the given ``descriptor`` at call time and calls
        ``activate()`` on the resolved element. It does not simulate clicks or
        keystrokes.

        | =Arguments=    | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[Activatable]`` that resolves to a concrete element implementing ``Activatable``. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Activate`    ${submit_button} |

        # 2) Building a simple variable (locator as string) |
        | ${btn}=       Set Variable    xpath=//Button[@Name="submit"] |
        | `Activate`    ${btn} |

        # 3) Passing the descriptor/locator directly |
        | `Activate`    //Button[@AutomationId="submit_button"] |

        See also: `Deactivate`, `Mouse Click`, `Get Text`.
        """
        descriptor().activate()


    @keyword
    def deactivate(self, descriptor: ElementDescriptor[Deactivatable]) -> None:
        """Deactivates an element.

        Performs the element's native *deactivate* action as provided by the UI
        framework. Use this when a control supports an explicit deactivation
        semantic (e.g. deselect, collapse item, toggle off).

        This keyword resolves the given ``descriptor`` at call time and calls
        ``deactivate()`` on the resolved element. It does not simulate clicks or
        keystrokes.

        | =Arguments=    | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[Deactivatable]`` that resolves to a concrete element implementing ``Deactivatable``. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | `Deactivate`    ${toggle_control} |

        # 2) Building a simple variable (locator as string) |
        | ${toggle}=      Set Variable    //Toggle[@Name="featureX"] |
        | `Deactivate`    ${toggle} |

        # 3) Passing the descriptor/locator directly |
        | `Deactivate`    xpath=//Toggle[@AutomationId="featureX"] |

        See also: `Activate`, `Mouse Click`, `Get Text`.
        """
        descriptor().deactivate()

    @keyword
    @assertable
    def is_active(self, descriptor: ElementDescriptor[HasIsActive]) -> bool:
        """Returns whether the element is active.

        Queries the element's native *active* state (e.g., toggled on, selected,
        expanded — depending on the control's semantics). This keyword only reads
        state and does not change the element.

        | =Arguments=    | =Description= |
        | ``descriptor`` | An ``ElementDescriptor[HasIsActive]`` that resolves to a concrete element exposing ``is_active``. |

        | =Returns= | =Description= |
        | ``bool``  | ``True`` if the element is active, otherwise ``False``. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${state}=    `Is Active`    ${toggle_control} |
        | Should Be True    ${state} |

        # 2) Building a simple variable (locator as string) |
        | ${toggle}=   Set Variable    xpath=//Toggle[@AutomationId="featureX"] |
        | ${state}=    `Is Active`    ${toggle} |
        | Should Be False   ${state} |

        # 3) Passing the descriptor/locator directly |
        | ${state}=    `Is Active`    //Toggle[@Name="featureX"] |

        See also: `Activate`, `Deactivate`, `Mouse Click`, `Get Text`.
        """
        return descriptor().is_active
