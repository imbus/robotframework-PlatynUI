# This file was generated by PythonNetStubGenerator
# mypy: ignore-errors
import typing, abc
from System import Array_1
from System.Xml.XPath import XPathResultType, XPathNavigator
from System.Xml import XmlNamespaceManager, XmlNameTable

class IXsltContextFunction(typing.Protocol):
    @property
    def ArgTypes(self) -> Array_1[XPathResultType]: ...
    @property
    def Maxargs(self) -> int: ...
    @property
    def Minargs(self) -> int: ...
    @property
    def ReturnType(self) -> XPathResultType: ...
    @abc.abstractmethod
    def Invoke(self, xsltContext: XsltContext, args: Array_1[typing.Any], docContext: XPathNavigator) -> typing.Any: ...

class IXsltContextVariable(typing.Protocol):
    @property
    def IsLocal(self) -> bool: ...
    @property
    def IsParam(self) -> bool: ...
    @property
    def VariableType(self) -> XPathResultType: ...
    @abc.abstractmethod
    def Evaluate(self, xsltContext: XsltContext) -> typing.Any: ...

class XsltContext(XmlNamespaceManager, abc.ABC):
    @property
    def DefaultNamespace(self) -> str: ...
    @property
    def NameTable(self) -> typing.Optional[XmlNameTable]: ...
    @property
    def Whitespace(self) -> bool: ...
    @abc.abstractmethod
    def CompareDocument(self, baseUri: str, nextbaseUri: str) -> int: ...
    @abc.abstractmethod
    def PreserveWhitespace(self, node: XPathNavigator) -> bool: ...
    @abc.abstractmethod
    def ResolveFunction(self, prefix: str, name: str, ArgTypes: Array_1[XPathResultType]) -> IXsltContextFunction: ...
    @abc.abstractmethod
    def ResolveVariable(self, prefix: str, name: str) -> IXsltContextVariable: ...
