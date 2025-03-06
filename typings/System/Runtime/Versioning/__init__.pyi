# This file was generated by PythonNetStubGenerator
# mypy: ignore-errors
import typing, abc
from System import Attribute, IEquatable_1, Version

class ComponentGuaranteesAttribute(Attribute):
    def __init__(self, guarantees: ComponentGuaranteesOptions) -> None: ...
    @property
    def Guarantees(self) -> ComponentGuaranteesOptions: ...
    @property
    def TypeId(self) -> typing.Any: ...

class ComponentGuaranteesOptions(typing.SupportsInt):
    @typing.overload
    def __init__(self, value: int) -> None: ...
    @typing.overload
    def __init__(self, value: int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...

    # Values:
    None_: ComponentGuaranteesOptions  # 0
    Exchange: ComponentGuaranteesOptions  # 1
    Stable: ComponentGuaranteesOptions  # 2
    SideBySide: ComponentGuaranteesOptions  # 4

class FrameworkName(IEquatable_1[FrameworkName]):
    @typing.overload
    def __init__(self, frameworkName: str) -> None: ...
    @typing.overload
    def __init__(self, identifier: str, version: Version) -> None: ...
    @typing.overload
    def __init__(self, identifier: str, version: Version, profile: typing.Optional[str]) -> None: ...
    @property
    def FullName(self) -> str: ...
    @property
    def Identifier(self) -> str: ...
    @property
    def Profile(self) -> str: ...
    @property
    def Version(self) -> Version: ...
    def GetHashCode(self) -> int: ...
    def __eq__(self, left: typing.Optional[FrameworkName], right: typing.Optional[FrameworkName]) -> bool: ...
    def __ne__(self, left: typing.Optional[FrameworkName], right: typing.Optional[FrameworkName]) -> bool: ...
    def ToString(self) -> str: ...
    # Skipped Equals due to it being static, abstract and generic.

    Equals: Equals_MethodGroup

    class Equals_MethodGroup:
        @typing.overload
        def __call__(self, other: typing.Optional[FrameworkName]) -> bool: ...
        @typing.overload
        def __call__(self, obj: typing.Optional[typing.Any]) -> bool: ...

class ObsoletedOSPlatformAttribute(OSPlatformAttribute):
    @typing.overload
    def __init__(self, platformName: str) -> None: ...
    @typing.overload
    def __init__(self, platformName: str, message: typing.Optional[str]) -> None: ...
    @property
    def Message(self) -> typing.Optional[str]: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...
    @property
    def Url(self) -> typing.Optional[str]: ...
    @Url.setter
    def Url(self, value: typing.Optional[str]) -> typing.Optional[str]: ...

class OSPlatformAttribute(Attribute):
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class RequiresPreviewFeaturesAttribute(Attribute):
    @typing.overload
    def __init__(self) -> None: ...
    @typing.overload
    def __init__(self, message: typing.Optional[str]) -> None: ...
    @property
    def Message(self) -> typing.Optional[str]: ...
    @property
    def TypeId(self) -> typing.Any: ...
    @property
    def Url(self) -> typing.Optional[str]: ...
    @Url.setter
    def Url(self, value: typing.Optional[str]) -> typing.Optional[str]: ...

class ResourceConsumptionAttribute(Attribute):
    @typing.overload
    def __init__(self, resourceScope: ResourceScope) -> None: ...
    @typing.overload
    def __init__(self, resourceScope: ResourceScope, consumptionScope: ResourceScope) -> None: ...
    @property
    def ConsumptionScope(self) -> ResourceScope: ...
    @property
    def ResourceScope(self) -> ResourceScope: ...
    @property
    def TypeId(self) -> typing.Any: ...

class ResourceExposureAttribute(Attribute):
    def __init__(self, exposureLevel: ResourceScope) -> None: ...
    @property
    def ResourceExposureLevel(self) -> ResourceScope: ...
    @property
    def TypeId(self) -> typing.Any: ...

class ResourceScope(typing.SupportsInt):
    @typing.overload
    def __init__(self, value: int) -> None: ...
    @typing.overload
    def __init__(self, value: int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...

    # Values:
    None_: ResourceScope  # 0
    Machine: ResourceScope  # 1
    Process: ResourceScope  # 2
    AppDomain: ResourceScope  # 4
    Library: ResourceScope  # 8
    Private: ResourceScope  # 16
    Assembly: ResourceScope  # 32

class SupportedOSPlatformAttribute(OSPlatformAttribute):
    def __init__(self, platformName: str) -> None: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class SupportedOSPlatformGuardAttribute(OSPlatformAttribute):
    def __init__(self, platformName: str) -> None: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class TargetFrameworkAttribute(Attribute):
    def __init__(self, frameworkName: str) -> None: ...
    @property
    def FrameworkDisplayName(self) -> typing.Optional[str]: ...
    @FrameworkDisplayName.setter
    def FrameworkDisplayName(self, value: typing.Optional[str]) -> typing.Optional[str]: ...
    @property
    def FrameworkName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class TargetPlatformAttribute(OSPlatformAttribute):
    def __init__(self, platformName: str) -> None: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class UnsupportedOSPlatformAttribute(OSPlatformAttribute):
    @typing.overload
    def __init__(self, platformName: str) -> None: ...
    @typing.overload
    def __init__(self, platformName: str, message: typing.Optional[str]) -> None: ...
    @property
    def Message(self) -> typing.Optional[str]: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class UnsupportedOSPlatformGuardAttribute(OSPlatformAttribute):
    def __init__(self, platformName: str) -> None: ...
    @property
    def PlatformName(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class VersioningHelper(abc.ABC):
    # Skipped MakeVersionSafeName due to it being static, abstract and generic.

    MakeVersionSafeName: MakeVersionSafeName_MethodGroup

    class MakeVersionSafeName_MethodGroup:
        @typing.overload
        def __call__(self, name: typing.Optional[str], from_: ResourceScope, to: ResourceScope) -> str: ...
        @typing.overload
        def __call__(
            self,
            name: typing.Optional[str],
            from_: ResourceScope,
            to: ResourceScope,
            type: typing.Optional[typing.Type[typing.Any]],
        ) -> str: ...
