# This file was generated by PythonNetStubGenerator
# mypy: ignore-errors
import typing, clr, abc
from PlatynUI.Extension.Win32.UiAutomation.Client import (
    IUIAutomationTreeWalker,
    IUIAutomationElement,
    IUIAutomation,
    tagRECT,
)
from PlatynUI.Extension.Win32.UiAutomation import Point, Rect
from System import Array_1
from System.Collections.Generic import IEnumerable_1
from System.Xml.Xsl import XsltContext, IXsltContextFunction, IXsltContextVariable
from System.Xml import XmlNameTable
from System.Xml.XPath import XPathNavigator, XPathResultType

class Automation:
    def __init__(self) -> None: ...
    @classmethod
    @property
    def ContentViewWalker(cls) -> IUIAutomationTreeWalker: ...
    @classmethod
    @property
    def ControlViewWalker(cls) -> IUIAutomationTreeWalker: ...
    @classmethod
    @property
    def NotSupportedValue(cls) -> typing.Any: ...
    @classmethod
    @property
    def RawViewWalker(cls) -> IUIAutomationTreeWalker: ...
    @classmethod
    @property
    def RootElement(cls) -> IUIAutomationElement: ...
    @classmethod
    @property
    def UiAutomation(cls) -> IUIAutomation: ...
    @staticmethod
    def CompareElements(
        e1: typing.Optional[IUIAutomationElement], e2: typing.Optional[IUIAutomationElement]
    ) -> bool: ...
    @staticmethod
    def ControlTypeNameFromId(id: int) -> str: ...
    @staticmethod
    def ConvertPropertyValue(name: str, value: typing.Any) -> typing.Optional[typing.Any]: ...
    @staticmethod
    def ElementFromHandle(handle: int) -> IUIAutomationElement: ...
    @staticmethod
    def FromPoint(point: Point) -> typing.Optional[IUIAutomationElement]: ...
    @staticmethod
    def GetCachedRootElement() -> IUIAutomationElement: ...
    @staticmethod
    def GetPropertyValue(element: IUIAutomationElement, propertyName: str) -> typing.Optional[typing.Any]: ...
    @staticmethod
    def GetSupportedPatternIds(element: IUIAutomationElement) -> Array_1[int]: ...
    @staticmethod
    def GetSupportedPatternNames(element: IUIAutomationElement) -> Array_1[str]: ...
    @staticmethod
    def GetSupportedPropertyIdAndNames(element: IUIAutomationElement) -> Array_1[Automation.PropertyIdAndName]: ...
    @staticmethod
    def GetSupportedPropertyIds(element: IUIAutomationElement) -> Array_1[int]: ...
    @staticmethod
    def GetSupportedPropertyNames(element: IUIAutomationElement) -> Array_1[str]: ...
    @staticmethod
    def SupportsPatternId(element: IUIAutomationElement, id: int) -> bool: ...
    # Skipped GetCachedPattern due to it being static, abstract and generic.

    GetCachedPattern: GetCachedPattern_MethodGroup

    class GetCachedPattern_MethodGroup:
        def __getitem__(self, t: typing.Type[GetCachedPattern_1_T1]) -> GetCachedPattern_1[GetCachedPattern_1_T1]: ...

        GetCachedPattern_1_T1 = typing.TypeVar("GetCachedPattern_1_T1")

        class GetCachedPattern_1(typing.Generic[GetCachedPattern_1_T1]):
            GetCachedPattern_1_T = Automation.GetCachedPattern_MethodGroup.GetCachedPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> GetCachedPattern_1_T: ...

    # Skipped GetCurrentPattern due to it being static, abstract and generic.

    GetCurrentPattern: GetCurrentPattern_MethodGroup

    class GetCurrentPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[GetCurrentPattern_1_T1]
        ) -> GetCurrentPattern_1[GetCurrentPattern_1_T1]: ...

        GetCurrentPattern_1_T1 = typing.TypeVar("GetCurrentPattern_1_T1")

        class GetCurrentPattern_1(typing.Generic[GetCurrentPattern_1_T1]):
            GetCurrentPattern_1_T = Automation.GetCurrentPattern_MethodGroup.GetCurrentPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> GetCurrentPattern_1_T: ...

    # Skipped SupportsPattern due to it being static, abstract and generic.

    SupportsPattern: SupportsPattern_MethodGroup

    class SupportsPattern_MethodGroup:
        def __getitem__(self, t: typing.Type[SupportsPattern_1_T1]) -> SupportsPattern_1[SupportsPattern_1_T1]: ...

        SupportsPattern_1_T1 = typing.TypeVar("SupportsPattern_1_T1")

        class SupportsPattern_1(typing.Generic[SupportsPattern_1_T1]):
            SupportsPattern_1_T = Automation.SupportsPattern_MethodGroup.SupportsPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> bool: ...

    # Skipped TryGetCachedPattern due to it being static, abstract and generic.

    TryGetCachedPattern: TryGetCachedPattern_MethodGroup

    class TryGetCachedPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[TryGetCachedPattern_1_T1]
        ) -> TryGetCachedPattern_1[TryGetCachedPattern_1_T1]: ...

        TryGetCachedPattern_1_T1 = typing.TypeVar("TryGetCachedPattern_1_T1")

        class TryGetCachedPattern_1(typing.Generic[TryGetCachedPattern_1_T1]):
            TryGetCachedPattern_1_T = Automation.TryGetCachedPattern_MethodGroup.TryGetCachedPattern_1_T1
            def __call__(
                self, element: IUIAutomationElement, pattern: typing.Optional[clr.Reference[TryGetCachedPattern_1_T]]
            ) -> bool: ...

    # Skipped TryGetCurrentPattern due to it being static, abstract and generic.

    TryGetCurrentPattern: TryGetCurrentPattern_MethodGroup

    class TryGetCurrentPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[TryGetCurrentPattern_1_T1]
        ) -> TryGetCurrentPattern_1[TryGetCurrentPattern_1_T1]: ...

        TryGetCurrentPattern_1_T1 = typing.TypeVar("TryGetCurrentPattern_1_T1")

        class TryGetCurrentPattern_1(typing.Generic[TryGetCurrentPattern_1_T1]):
            TryGetCurrentPattern_1_T = Automation.TryGetCurrentPattern_MethodGroup.TryGetCurrentPattern_1_T1
            def __call__(
                self, element: IUIAutomationElement, pattern: typing.Optional[clr.Reference[TryGetCurrentPattern_1_T]]
            ) -> bool: ...

    class PropertyIdAndName:
        def __init__(self, id: int, name: str) -> None: ...
        Id: int
        Name: str

class UiAutomationElementExtensions(abc.ABC):
    @classmethod
    @property
    def CurrentProcessId(cls) -> int: ...
    @staticmethod
    def EnumerateChildren(
        element: typing.Optional[IUIAutomationElement],
        walker: typing.Optional[IUIAutomationTreeWalker],
        findVirtual: bool = ...,
    ) -> IEnumerable_1[IUIAutomationElement]: ...
    @staticmethod
    def GetCurrentControlTypeName(element: IUIAutomationElement) -> str: ...
    @staticmethod
    def GetCurrentParent(element: IUIAutomationElement) -> typing.Optional[IUIAutomationElement]: ...
    @staticmethod
    def GetDisplayName(element: IUIAutomationElement) -> str: ...
    @staticmethod
    def GetSupportedPatternIds(element: IUIAutomationElement) -> Array_1[int]: ...
    @staticmethod
    def GetSupportedPatternNames(element: IUIAutomationElement) -> Array_1[str]: ...
    @staticmethod
    def GetSupportedPropertyIds(element: IUIAutomationElement) -> Array_1[int]: ...
    @staticmethod
    def GetSupportedPropertyNames(element: IUIAutomationElement) -> Array_1[str]: ...
    @staticmethod
    def IsItemContainer(element: IUIAutomationElement) -> bool: ...
    @staticmethod
    def IsVirtualized(element: IUIAutomationElement) -> bool: ...
    @staticmethod
    def Realize(element: IUIAutomationElement) -> IUIAutomationElement: ...
    @staticmethod
    def SupportsPatternId(element: IUIAutomationElement, id: int) -> bool: ...
    @staticmethod
    def ToRect(rect: tagRECT) -> Rect: ...
    # Skipped GetCachedPattern due to it being static, abstract and generic.

    GetCachedPattern: GetCachedPattern_MethodGroup

    class GetCachedPattern_MethodGroup:
        def __getitem__(self, t: typing.Type[GetCachedPattern_1_T1]) -> GetCachedPattern_1[GetCachedPattern_1_T1]: ...

        GetCachedPattern_1_T1 = typing.TypeVar("GetCachedPattern_1_T1")

        class GetCachedPattern_1(typing.Generic[GetCachedPattern_1_T1]):
            GetCachedPattern_1_T = UiAutomationElementExtensions.GetCachedPattern_MethodGroup.GetCachedPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> GetCachedPattern_1_T: ...

    # Skipped GetCurrentPattern due to it being static, abstract and generic.

    GetCurrentPattern: GetCurrentPattern_MethodGroup

    class GetCurrentPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[GetCurrentPattern_1_T1]
        ) -> GetCurrentPattern_1[GetCurrentPattern_1_T1]: ...

        GetCurrentPattern_1_T1 = typing.TypeVar("GetCurrentPattern_1_T1")

        class GetCurrentPattern_1(typing.Generic[GetCurrentPattern_1_T1]):
            GetCurrentPattern_1_T = UiAutomationElementExtensions.GetCurrentPattern_MethodGroup.GetCurrentPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> GetCurrentPattern_1_T: ...

    # Skipped SupportsPattern due to it being static, abstract and generic.

    SupportsPattern: SupportsPattern_MethodGroup

    class SupportsPattern_MethodGroup:
        def __getitem__(self, t: typing.Type[SupportsPattern_1_T1]) -> SupportsPattern_1[SupportsPattern_1_T1]: ...

        SupportsPattern_1_T1 = typing.TypeVar("SupportsPattern_1_T1")

        class SupportsPattern_1(typing.Generic[SupportsPattern_1_T1]):
            SupportsPattern_1_T = UiAutomationElementExtensions.SupportsPattern_MethodGroup.SupportsPattern_1_T1
            def __call__(self, element: IUIAutomationElement) -> bool: ...

    # Skipped TryGetCachedPattern due to it being static, abstract and generic.

    TryGetCachedPattern: TryGetCachedPattern_MethodGroup

    class TryGetCachedPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[TryGetCachedPattern_1_T1]
        ) -> TryGetCachedPattern_1[TryGetCachedPattern_1_T1]: ...

        TryGetCachedPattern_1_T1 = typing.TypeVar("TryGetCachedPattern_1_T1")

        class TryGetCachedPattern_1(typing.Generic[TryGetCachedPattern_1_T1]):
            TryGetCachedPattern_1_T = (
                UiAutomationElementExtensions.TryGetCachedPattern_MethodGroup.TryGetCachedPattern_1_T1
            )
            def __call__(
                self, element: IUIAutomationElement, pattern: typing.Optional[clr.Reference[TryGetCachedPattern_1_T]]
            ) -> bool: ...

    # Skipped TryGetCurrentPattern due to it being static, abstract and generic.

    TryGetCurrentPattern: TryGetCurrentPattern_MethodGroup

    class TryGetCurrentPattern_MethodGroup:
        def __getitem__(
            self, t: typing.Type[TryGetCurrentPattern_1_T1]
        ) -> TryGetCurrentPattern_1[TryGetCurrentPattern_1_T1]: ...

        TryGetCurrentPattern_1_T1 = typing.TypeVar("TryGetCurrentPattern_1_T1")

        class TryGetCurrentPattern_1(typing.Generic[TryGetCurrentPattern_1_T1]):
            TryGetCurrentPattern_1_T = (
                UiAutomationElementExtensions.TryGetCurrentPattern_MethodGroup.TryGetCurrentPattern_1_T1
            )
            def __call__(
                self, element: IUIAutomationElement, pattern: typing.Optional[clr.Reference[TryGetCurrentPattern_1_T]]
            ) -> bool: ...

class UiaXsltContext(XsltContext):
    def __init__(self) -> None: ...
    @property
    def DefaultNamespace(self) -> str: ...
    @property
    def NameTable(self) -> typing.Optional[XmlNameTable]: ...
    @property
    def Whitespace(self) -> bool: ...
    def CompareDocument(self, baseUri: str, nextbaseUri: str) -> int: ...
    def HasNamespace(self, prefix: str) -> bool: ...
    def LookupNamespace(self, prefix: str) -> typing.Optional[str]: ...
    def PreserveWhitespace(self, node: XPathNavigator) -> bool: ...
    def ResolveFunction(self, prefix: str, name: str, ArgTypes: Array_1[XPathResultType]) -> IXsltContextFunction: ...
    def ResolveVariable(self, prefix: str, name: str) -> IXsltContextVariable: ...
