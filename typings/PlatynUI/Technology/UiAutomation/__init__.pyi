import typing, abc
from PlatynUI.Technology.UiAutomation.Client import IUIAutomationElement, IUIAutomationWindowPattern
from System import Array_1

class Adapter(abc.ABC):
    @staticmethod
    def GetClickablePoint(element: IUIAutomationElement) -> typing.Optional[typing.Optional[Point]]: ...
    @staticmethod
    def GetPropertyValue(element: IUIAutomationElement, propertyName: str) -> typing.Optional[typing.Any]: ...
    @staticmethod
    def GetRole(element: IUIAutomationElement) -> str: ...
    @staticmethod
    def GetRuntimeId(element: IUIAutomationElement) -> str: ...
    @staticmethod
    def GetSupportedPropertyNames(element: IUIAutomationElement) -> Array_1[str]: ...
    @staticmethod
    def GetTopLevelParent(element: typing.Optional[IUIAutomationElement]) -> typing.Optional[IUIAutomationElement]: ...
    @staticmethod
    def IsReadOnly(element: IUIAutomationElement) -> bool: ...
    @staticmethod
    def TryEnsureTopLevelParentIsActive(element: typing.Optional[IUIAutomationElement]) -> bool: ...


class DisplayDevice(abc.ABC):
    @staticmethod
    def GetBoundingRectangle() -> Rect: ...
    @staticmethod
    def HighlightRect(x: float, y: float, width: float, height: float, time: float) -> None: ...


class Finder(abc.ABC):
    @staticmethod
    def FindSingleElement(parent: typing.Optional[IUIAutomationElement], xpath: str, findVirtual: bool = ...) -> typing.Optional[IUIAutomationElement]: ...


class KeyboardDevice(abc.ABC):
    @staticmethod
    def KeyToKeyCode(key: typing.Optional[typing.Any]) -> Keycode: ...
    @staticmethod
    def SendKeyCode(keyCode: typing.Any, pressed: bool) -> bool: ...


class Keycode:
    def __init__(self, key: typing.Optional[typing.Any], code: typing.Optional[typing.Any], valid: bool, errorText: typing.Optional[str]) -> None: ...
    @property
    def Code(self) -> typing.Optional[typing.Any]: ...
    @property
    def ErrorText(self) -> typing.Optional[str]: ...
    @property
    def Key(self) -> typing.Optional[typing.Any]: ...
    @property
    def Valid(self) -> bool: ...
    def ToString(self) -> str: ...


class MouseButton(typing.SupportsInt):
    @typing.overload
    def __init__(self, value : int) -> None: ...
    @typing.overload
    def __init__(self, value : int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...
    
    # Values:
    Left : MouseButton # 0
    Right : MouseButton # 1
    Middle : MouseButton # 2
    X1 : MouseButton # 3
    X2 : MouseButton # 4


class MouseDevice(abc.ABC):
    @staticmethod
    def GetDoubleClickSize() -> Size: ...
    @staticmethod
    def GetDoubleClickTime() -> float: ...
    @staticmethod
    def GetPosition() -> Point: ...
    # Skipped Move due to it being static, abstract and generic.

    Move : Move_MethodGroup
    class Move_MethodGroup:
        @typing.overload
        def __call__(self, p: Point) -> None:...
        @typing.overload
        def __call__(self, x: float, y: float) -> None:...

    # Skipped Press due to it being static, abstract and generic.

    Press : Press_MethodGroup
    class Press_MethodGroup:
        @typing.overload
        def __call__(self, button: int) -> None:...
        @typing.overload
        def __call__(self, button: MouseButton) -> None:...

    # Skipped Release due to it being static, abstract and generic.

    Release : Release_MethodGroup
    class Release_MethodGroup:
        @typing.overload
        def __call__(self, button: int) -> None:...
        @typing.overload
        def __call__(self, button: MouseButton) -> None:...

    # Skipped ScreenToMouseCoords due to it being static, abstract and generic.

    ScreenToMouseCoords : ScreenToMouseCoords_MethodGroup
    class ScreenToMouseCoords_MethodGroup:
        @typing.overload
        def __call__(self, pt: Point) -> Point:...
        @typing.overload
        def __call__(self, point: Point, desktopRect: Rect) -> Point:...



class Patterns(abc.ABC):
    @staticmethod
    def GetWindowPattern(element: IUIAutomationElement) -> Patterns.WindowPattern: ...

    class WindowPattern:
        def __init__(self, element: IUIAutomationElement, pattern: IUIAutomationWindowPattern) -> None: ...
        @property
        def CanMaximize(self) -> bool: ...
        @property
        def CanMinimize(self) -> bool: ...
        @property
        def IsActive(self) -> bool: ...
        @property
        def IsMaximized(self) -> bool: ...
        @property
        def IsMinimized(self) -> bool: ...
        def Activate(self) -> None: ...
        def Close(self) -> None: ...
        def Maximize(self) -> None: ...
        def Minimize(self) -> None: ...
        def Restore(self) -> None: ...



class Point:
    def __init__(self, x: float, y: float) -> None: ...
    @property
    def X(self) -> float: ...
    @X.setter
    def X(self, value: float) -> float: ...
    @property
    def Y(self) -> float: ...
    @Y.setter
    def Y(self, value: float) -> float: ...
    def ToString(self) -> str: ...


class Rect:
    def __init__(self, x: float, y: float, width: float, height: float) -> None: ...
    Empty : Rect
    @property
    def Height(self) -> float: ...
    @Height.setter
    def Height(self, value: float) -> float: ...
    @property
    def Width(self) -> float: ...
    @Width.setter
    def Width(self, value: float) -> float: ...
    @property
    def X(self) -> float: ...
    @X.setter
    def X(self, value: float) -> float: ...
    @property
    def Y(self) -> float: ...
    @Y.setter
    def Y(self, value: float) -> float: ...
    def GetHashCode(self) -> int: ...
    def __eq__(self, rect1: Rect, rect2: Rect) -> bool: ...
    def __ne__(self, rect1: Rect, rect2: Rect) -> bool: ...
    def ToString(self) -> str: ...
    # Skipped Equals due to it being static, abstract and generic.

    Equals : Equals_MethodGroup
    class Equals_MethodGroup:
        @typing.overload
        def __call__(self, value: Rect) -> bool:...
        @typing.overload
        def __call__(self, o: typing.Optional[typing.Any]) -> bool:...
        @typing.overload
        def __call__(self, rect1: Rect, rect2: Rect) -> bool:...



class Size:
    def __init__(self, width: float, height: float) -> None: ...
    @property
    def Height(self) -> float: ...
    @Height.setter
    def Height(self, value: float) -> float: ...
    @property
    def Width(self) -> float: ...
    @Width.setter
    def Width(self, value: float) -> float: ...
    def ToString(self) -> str: ...


class VirtualKey:
    Add : VirtualKey
    Alt : VirtualKey
    AltGr : VirtualKey
    Apps : VirtualKey
    Back : VirtualKey
    BrowserBack : VirtualKey
    BrowserFavorites : VirtualKey
    BrowserForward : VirtualKey
    BrowserHome : VirtualKey
    BrowserRefresh : VirtualKey
    BrowserSearch : VirtualKey
    BrowserStop : VirtualKey
    Capital : VirtualKey
    Clear : VirtualKey
    Control : VirtualKey
    Ctrl : VirtualKey
    Decimal : VirtualKey
    Delete : VirtualKey
    Divide : VirtualKey
    Down : VirtualKey
    End : VirtualKey
    Enter : VirtualKey
    Escape : VirtualKey
    Execute : VirtualKey
    F1 : VirtualKey
    F10 : VirtualKey
    F11 : VirtualKey
    F12 : VirtualKey
    F13 : VirtualKey
    F14 : VirtualKey
    F15 : VirtualKey
    F16 : VirtualKey
    F17 : VirtualKey
    F18 : VirtualKey
    F19 : VirtualKey
    F2 : VirtualKey
    F20 : VirtualKey
    F21 : VirtualKey
    F22 : VirtualKey
    F23 : VirtualKey
    F24 : VirtualKey
    F3 : VirtualKey
    F4 : VirtualKey
    F5 : VirtualKey
    F6 : VirtualKey
    F7 : VirtualKey
    F8 : VirtualKey
    F9 : VirtualKey
    Help : VirtualKey
    Home : VirtualKey
    Insert : VirtualKey
    LaunchApp1 : VirtualKey
    LaunchApp2 : VirtualKey
    LaunchMail : VirtualKey
    LaunchMediaSelect : VirtualKey
    Lcontrol : VirtualKey
    Left : VirtualKey
    Lmenu : VirtualKey
    Lshift : VirtualKey
    Lwin : VirtualKey
    MediaNextTrack : VirtualKey
    MediaPlayPause : VirtualKey
    MediaPrevTrack : VirtualKey
    MediaStop : VirtualKey
    Menu : VirtualKey
    Multiply : VirtualKey
    Next : VirtualKey
    Numlock : VirtualKey
    Numpad0 : VirtualKey
    Numpad1 : VirtualKey
    Numpad2 : VirtualKey
    Numpad3 : VirtualKey
    Numpad4 : VirtualKey
    Numpad5 : VirtualKey
    Numpad6 : VirtualKey
    Numpad7 : VirtualKey
    Numpad8 : VirtualKey
    Numpad9 : VirtualKey
    PageDown : VirtualKey
    PageUp : VirtualKey
    Pause : VirtualKey
    Print : VirtualKey
    Prior : VirtualKey
    Rcontrol : VirtualKey
    Return : VirtualKey
    Right : VirtualKey
    Rmenu : VirtualKey
    Rshift : VirtualKey
    Rwin : VirtualKey
    Scroll : VirtualKey
    Select : VirtualKey
    Separator : VirtualKey
    Shift : VirtualKey
    Sleep : VirtualKey
    Snapshot : VirtualKey
    Space : VirtualKey
    Subtract : VirtualKey
    Tab : VirtualKey
    Up : VirtualKey
    VolumeDown : VirtualKey
    VolumeMute : VirtualKey
    VolumeUp : VirtualKey
    def Equals(self, obj: typing.Optional[typing.Any]) -> bool: ...
    def GetHashCode(self) -> int: ...
    def ToString(self) -> str: ...

