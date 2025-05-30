# This file was generated by PythonNetStubGenerator
# mypy: ignore-errors
import typing
from System import SystemException, Exception
from System.Collections import IDictionary
from System.Reflection import MethodBase

class CryptographicException(SystemException):
    @typing.overload
    def __init__(self) -> None: ...
    @typing.overload
    def __init__(self, format: str, insert: typing.Optional[str]) -> None: ...
    @typing.overload
    def __init__(self, hr: int) -> None: ...
    @typing.overload
    def __init__(self, message: typing.Optional[str]) -> None: ...
    @typing.overload
    def __init__(self, message: typing.Optional[str], inner: typing.Optional[Exception]) -> None: ...
    @property
    def Data(self) -> IDictionary: ...
    @property
    def HelpLink(self) -> typing.Optional[str]: ...
    @HelpLink.setter
    def HelpLink(self, value: typing.Optional[str]) -> typing.Optional[str]: ...
    @property
    def HResult(self) -> int: ...
    @HResult.setter
    def HResult(self, value: int) -> int: ...
    @property
    def InnerException(self) -> typing.Optional[Exception]: ...
    @property
    def Message(self) -> str: ...
    @property
    def Source(self) -> typing.Optional[str]: ...
    @Source.setter
    def Source(self, value: typing.Optional[str]) -> typing.Optional[str]: ...
    @property
    def StackTrace(self) -> typing.Optional[str]: ...
    @property
    def TargetSite(self) -> typing.Optional[MethodBase]: ...
