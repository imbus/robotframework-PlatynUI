# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import Optional

from robotlibcore import keyword

from ..core.types import Point
from ..ui.core.devices.mousebutton import MouseButton
from ..ui.locator import Locator
from ..ui.strategies import HasMouse
from .assertable import assertable
from .types import ElementDescriptor

__all__ = ["Mouse"]

desktop_descriptor: ElementDescriptor[HasMouse] = ElementDescriptor(Locator(path="/."))


class Mouse:
    @keyword
    def mouse_click(
        self,
        descriptor: ElementDescriptor[HasMouse] | None = None,
        x: float | None = None,
        y: float | None = None,
        button: Optional[MouseButton] = None,
        times: int = 1,
    ) -> Point:
        """Clicks an element (or the desktop) and returns the mouse position.

        Resolves ``descriptor`` (if provided) and performs a mouse click using the
        element's native mouse device. If no ``descriptor`` is given, the click is
        executed on the **desktop** (global/screen coordinates).

        If ``x``/``y`` are omitted with an element descriptor, the element's **default
        click position** is used. When ``descriptor`` is omitted (desktop clicks),
        ``x``/``y`` are interpreted as **screen coordinates** (origin depends on the UI
        backend/OS).

        | =Arguments=  | =Description= |
        | ``descriptor`` | Optional ``ElementDescriptor[HasMouse]`` resolving to the clickable target. If omitted, clicks the desktop. |
        | ``x``          | Optional X offset/coordinate. With an element: typically relative to the element's bounds. Without element (desktop): screen X. |
        | ``y``          | Optional Y offset/coordinate. With an element: typically relative to the element's bounds. Without element (desktop): screen Y. |
        | ``button``     | Optional mouse button (e.g., ``${MouseButton.Left}``, ``${MouseButton.Right}``, ``${MouseButton.Middle}``). Defaults to library/system default (usually Left). |
        | ``times``      | Number of click repetitions (e.g., ``2`` for double-click). Defaults to ``1``. |

        | =Returns= | =Description= |
        | ``Point`` | The mouse position after the click (structure with X/Y). |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${pt}=    `Mouse Click`    ${submit_button} |                         # default position, left click |
        | ${pt}=    `Mouse Click`    ${submit_button}    button=${MouseButton.Right} |  # context menu |
        | ${pt}=    `Mouse Click`    ${file_item}    times=2 |                  # double-click |

        # 2) Building a simple variable (locator as string) |
        | ${btn}=   Set Variable    xpath=//Button[@AutomationId="submit_button"] |
        | ${pt}=    `Mouse Click`    ${btn}    x=10    y=5 |                    # small offset within element |
        | ${pt}=    `Mouse Click`    ${btn}    button=${MouseButton.Middle} |

        # 3) Passing the descriptor/locator directly |
        | ${pt}=    `Mouse Click`    xpath=//ListItem[@Name="Documents"] |      # default position |
        | ${pt}=    `Mouse Click`    xpath=//ListItem[@Name="Documents"]    times=2 |

        # 4) Desktop click (no descriptor → screen coordinates) |
        | ${pt}=    `Mouse Click`    x=600    y=300 |                           # clicks the desktop at (600,300) |

        See also: `Mouse Position`, `Ensure Exists`, `Get Property Value`, `Get Text`, `Press Keys`, `Type Keys`.
        """
        descriptor = descriptor or desktop_descriptor
        descriptor().mouse.click(x=x, y=y, button=button, times=times)
        return self.mouse_position()

    @keyword
    def mouse_double_click(
        self,
        descriptor: ElementDescriptor[HasMouse] | None = None,
        x: float | None = None,
        y: float | None = None,
        button: Optional[MouseButton] = None,
    ) -> None:
        """Double-clicks an element (or the desktop) and returns the mouse position.

        Resolves ``descriptor`` (if provided) and performs a **double click** using the
        element's native mouse device. If no ``descriptor`` is given, the double click
        is executed on the **desktop** (global/screen coordinates).

        If ``x``/``y`` are omitted with an element descriptor, the element's **default
        click position** is used. When ``descriptor`` is omitted (desktop double-click),
        ``x``/``y`` are interpreted as **screen coordinates**.

        | =Arguments=  | =Description= |
        | ``descriptor`` | Optional ``ElementDescriptor[HasMouse]`` resolving to the target element. If omitted, double-clicks the desktop. |
        | ``x``          | Optional X offset/coordinate. With an element: relative to the element bounds. Without element: screen X. |
        | ``y``          | Optional Y offset/coordinate. With an element: relative to the element bounds. Without element: screen Y. |
        | ``button``     | Optional mouse button (e.g., ``${MouseButton.Left}``, ``${MouseButton.Right}``, ``${MouseButton.Middle}``). Defaults to library/system default (usually Left). |

        | =Returns= | =Description= |
        | ``Point`` | The mouse position after the double click. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${pt}=    `Mouse Double Click`    ${file_item} |                             # default position |
        | ${pt}=    `Mouse Double Click`    ${file_item}    button=${MouseButton.Left} |

        # 2) Building a simple variable (locator as string) |
        | ${item}=  Set Variable    xpath=//ListItem[@Name="Documents"] |
        | ${pt}=    `Mouse Double Click`    ${item} |                                   # open item |
        | ${pt}=    `Mouse Double Click`    ${item}    x=8    y=6 |                     # offset within element |

        # 3) Passing the descriptor/locator directly |
        | ${pt}=    `Mouse Double Click`    xpath=//Button[@AutomationId="open"] |      # invoke default action |
        | ${pt}=    `Mouse Double Click`    xpath=//TreeItem[@Name="Folder"] |

        # 4) Desktop double-click (no descriptor → screen coordinates) |
        | ${pt}=    `Mouse Double Click`    x=600    y=300 |                             # double-click desktop at (600,300) |

        See also: `Mouse Click`, `Mouse Position`, `Ensure Exists`, `Get Text`, `Get Property Value`, `Press Keys`, `Type Keys`.
        """
        descriptor = descriptor or desktop_descriptor
        descriptor().mouse.double_click(x=x, y=y, button=button)
        return self.mouse_position()

    @keyword
    def mouse_move_to(
        self,
        descriptor: ElementDescriptor[HasMouse] | None = None,
        x: float | None = None,
        y: float | None = None,
    ) -> None:
        """Moves the mouse to an element (or to screen coordinates) and returns the mouse position.

        Resolves ``descriptor`` (if provided) and moves the mouse cursor. If no
        ``descriptor`` is given, the movement targets the **desktop** using global/screen
        coordinates.

        With an element descriptor, if ``x``/``y`` are omitted, the cursor moves to the
        element's **default hover position**. When ``descriptor`` is omitted (desktop move),
        ``x``/``y`` are interpreted as **screen coordinates**.

        | =Arguments=  | =Description= |
        | ``descriptor`` | Optional ``ElementDescriptor[HasMouse]`` resolving to the target element. If omitted, uses desktop/screen. |
        | ``x``          | Optional X offset/coordinate. With element: relative to element bounds. Without element: screen X. |
        | ``y``          | Optional Y offset/coordinate. With element: relative to element bounds. Without element: screen Y. |

        | =Returns= | =Description= |
        | ``Point`` | The mouse position after the move. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${pt}=    `Mouse Move To`    ${menu_item} |                         # default hover position |
        | ${pt}=    `Mouse Move To`    ${menu_item}    x=10    y=4 |          # offset within element |

        # 2) Building a simple variable (locator as string) |
        | ${item}=  Set Variable    xpath=//ListItem[@Name="Settings"] |
        | ${pt}=    `Mouse Move To`    ${item} |
        | ${pt}=    `Mouse Move To`    ${item}    x=5    y=12 |

        # 3) Passing the descriptor/locator directly |
        | ${pt}=    `Mouse Move To`    xpath=//Button[@AutomationId="open"] |
        | ${pt}=    `Mouse Move To`    xpath=//TreeItem[@Name="Folder"]    x=8    y=6 |

        # 4) Desktop move (no descriptor → screen coordinates) |
        | ${pt}=    `Mouse Move To`    x=900    y=500 |

        See also: `Mouse Click`, `Mouse Double Click`, `Mouse Position`, `Ensure Exists`, `Get Property Value`, `Get Text`.
        """
        descriptor = descriptor or desktop_descriptor
        descriptor().mouse.move_to(x=x, y=y)
        return self.mouse_position()

    @keyword
    def mouse_press(
        self,
        descriptor: ElementDescriptor[HasMouse] | None = None,
        x: float | None = None,
        y: float | None = None,
        button: Optional[MouseButton] = None,
    ) -> None:
        """Presses (holds down) a mouse button on an element or the desktop and returns the mouse position.

        Resolves ``descriptor`` (if provided) and performs a mouse **press** without
        releasing it. The button remains **held down** until explicitly released with
        ``Mouse Release``. If no ``descriptor`` is given, the press happens on the
        **desktop** (global/screen coordinates).

        With an element descriptor, omitting ``x``/``y`` uses the element's default
        press position. Without a descriptor (desktop press), ``x``/``y`` are treated
        as **screen coordinates**.

        | =Arguments=  | =Description= |
        | ``descriptor`` | Optional ``ElementDescriptor[HasMouse]`` resolving to the target element. If omitted, uses desktop/screen. |
        | ``x``          | Optional X offset/coordinate. With element: relative to element bounds. Without element: screen X. |
        | ``y``          | Optional Y offset/coordinate. With element: relative to element bounds. Without element: screen Y. |
        | ``button``     | Optional mouse button (e.g., ``${MouseButton.Left}``, ``${MouseButton.Right}``, ``${MouseButton.Middle}``). Defaults to library/system default (usually Left). |

        | =Returns= | =Description= |
        | ``Point`` | The mouse position after the press. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${pt}=    `Mouse Press`    ${draggable_item} |                          # hold left button on item |
        | ${pt}=    `Mouse Move To`  ${drop_target} |                              # drag while holding |
        | ${pt}=    `Mouse Release`  ${drop_target} |                              # release to drop |

        # 2) Building a simple variable (locator as string) |
        | ${item}=  Set Variable    xpath=//ListItem[@Name="File.txt"] |
        | ${pt}=    `Mouse Press`    ${item}    button=${MouseButton.Left} |
        | ${pt}=    `Mouse Move To`  ${item}    x=40    y=5 |                      # move while pressed |
        | ${pt}=    `Mouse Release`  ${item} |

        # 3) Passing the descriptor/locator directly |
        | ${pt}=    `Mouse Press`    xpath=//Button[@AutomationId="resizeHandle"]    button=${MouseButton.Left} |
        | ${pt}=    `Mouse Move To`  xpath=//Button[@AutomationId="resizeHandle"]    x=80    y=0 |
        | ${pt}=    `Mouse Release`  xpath=//Button[@AutomationId="resizeHandle"] |

        # 4) Desktop press (no descriptor → screen coordinates) |
        | ${pt}=    `Mouse Press`    x=600    y=300 |                                # start drag from (600,300) |
        | ${pt}=    `Mouse Move To`  x=900    y=500 |                                # drag on desktop |
        | ${pt}=    `Mouse Release`  x=900    y=500 |

        See also: `Mouse Release`, `Mouse Click`, `Mouse Double Click`, `Mouse Move To`, `Mouse Position`, `Ensure Exists`, `Get Text`.
        """
        descriptor = descriptor or desktop_descriptor
        descriptor().mouse.press(x=x, y=y, button=button)
        return self.mouse_position()

    @keyword
    def mouse_release(
        self,
        descriptor: ElementDescriptor[HasMouse] | None = None,
        x: float | None = None,
        y: float | None = None,
        button: Optional[MouseButton] = None,
    ) -> None:
        """Releases a held mouse button on an element or the desktop and returns the mouse position.

        Resolves ``descriptor`` (if provided) and performs a mouse **release**. Use this
        to end operations started with ``Mouse Press`` (e.g., drag-and-drop, resize).
        If no ``descriptor`` is given, the release happens on the **desktop** (global/screen
        coordinates).

        With an element descriptor, omitting ``x``/``y`` releases at the element's default
        position. Without a descriptor (desktop release), ``x``/``y`` are interpreted as
        **screen coordinates**.

        | =Arguments=  | =Description= |
        | ``descriptor`` | Optional ``ElementDescriptor[HasMouse]`` resolving to the target element. If omitted, uses desktop/screen. |
        | ``x``          | Optional X offset/coordinate. With element: relative to element bounds. Without element: screen X. |
        | ``y``          | Optional Y offset/coordinate. With element: relative to element bounds. Without element: screen Y. |
        | ``button``     | Optional mouse button (e.g., ``${MouseButton.Left}``, ``${MouseButton.Right}``, ``${MouseButton.Middle}``). Defaults to library/system default (usually Left). |

        | =Returns= | =Description= |
        | ``Point`` | The mouse position after the release. |

        Examples:

        # 1) Using a pre-defined variable (descriptor object) |
        | ${pt}=    `Mouse Press`    ${draggable_item} |                       # hold |
        | ${pt}=    `Mouse Move To`  ${drop_target} |                           # drag |
        | ${pt}=    `Mouse Release`  ${drop_target} |                           # drop |

        # 2) Building a simple variable (locator as string) |
        | ${handle}= Set Variable    xpath=//Button[@AutomationId="resizeHandle"] |
        | ${pt}=     `Mouse Press`    ${handle}    button=${MouseButton.Left} |
        | ${pt}=     `Mouse Move To`  ${handle}    x=80    y=0 |                # resize while held |
        | ${pt}=     `Mouse Release`  ${handle} |                               # finish resize |

        # 3) Passing the descriptor/locator directly |
        | ${pt}=    `Mouse Press`    xpath=//ListItem[@Name="File.txt"] |
        | ${pt}=    `Mouse Move To`  xpath=//List[@AutomationId="TargetFolder"] |
        | ${pt}=    `Mouse Release`  xpath=//List[@AutomationId="TargetFolder"] |

        # 4) Desktop release (no descriptor → screen coordinates) |
        | ${pt}=    `Mouse Press`    x=600    y=300 |
        | ${pt}=    `Mouse Move To`  x=900    y=500 |
        | ${pt}=    `Mouse Release`  x=900    y=500 |

        See also: `Mouse Press`, `Mouse Click`, `Mouse Double Click`, `Mouse Move To`, `Mouse Position`, `Ensure Exists`, `Get Text`.
        """
        descriptor = descriptor or desktop_descriptor
        descriptor().mouse.release(x=x, y=y, button=button)
        return self.mouse_position()

    @keyword
    @assertable
    def mouse_position(self) -> Point:
        """Returns the current mouse cursor position.

        Queries the mouse device for the current cursor location on the screen and
        returns it as a ``Point`` (with X/Y coordinates, screen space).

        | =Arguments= | =Description= |
        | *(none)*    | This keyword takes no arguments. |

        | =Returns= | =Description= |
        | ``Point`` | Current mouse position in screen coordinates. |

        Examples:

        # 1) Simple read & log |
        | ${pt}=    `Mouse Position` |
        | Log To Console    ${pt} |

        # 2) After moving to an element |
        | ${item}=  Set Variable    xpath=//ListItem[@Name="Settings"] |
        | `Mouse Move To`    ${item} |
        | ${pt}=    `Mouse Position` |
        | Log To Console    ${pt} |

        # 3) After moving on the desktop (screen coordinates) |
        | `Mouse Move To`    x=900    y=500 |
        | ${pt}=    `Mouse Position` |
        | Log To Console    ${pt} |

        See also: `Mouse Click`, `Mouse Double Click`, `Mouse Move To`, `Mouse Press`, `Mouse Release`, `Ensure Exists`.
        """
        return desktop_descriptor().mouse.mouse_device.get_position()
