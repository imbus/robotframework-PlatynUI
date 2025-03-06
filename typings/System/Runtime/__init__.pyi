# This file was generated by PythonNetStubGenerator
# mypy: ignore-errors
import typing, abc
from System import Exception, Attribute, Action, IDisposable, ValueTuple_2, TimeSpan
from System.Collections import IDictionary
from System.Reflection import MethodBase
from System.Threading import CancellationToken
from System.Runtime.ConstrainedExecution import CriticalFinalizerObject

class AmbiguousImplementationException(Exception):
    @typing.overload
    def __init__(self) -> None: ...
    @typing.overload
    def __init__(self, message: typing.Optional[str]) -> None: ...
    @typing.overload
    def __init__(self, message: typing.Optional[str], innerException: typing.Optional[Exception]) -> None: ...
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

class AssemblyTargetedPatchBandAttribute(Attribute):
    def __init__(self, targetedPatchBand: str) -> None: ...
    @property
    def TargetedPatchBand(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...

class ControlledExecution(abc.ABC):
    @staticmethod
    def Run(action: Action, cancellationToken: CancellationToken) -> None: ...

class DependentHandle(IDisposable):
    def __init__(self, target: typing.Optional[typing.Any], dependent: typing.Optional[typing.Any]) -> None: ...
    @property
    def Dependent(self) -> typing.Optional[typing.Any]: ...
    @Dependent.setter
    def Dependent(self, value: typing.Optional[typing.Any]) -> typing.Optional[typing.Any]: ...
    @property
    def IsAllocated(self) -> bool: ...
    @property
    def Target(self) -> typing.Optional[typing.Any]: ...
    @Target.setter
    def Target(self, value: typing.Optional[typing.Any]) -> typing.Optional[typing.Any]: ...
    @property
    def TargetAndDependent(self) -> ValueTuple_2[typing.Any, typing.Any]: ...
    def Dispose(self) -> None: ...

class GCLargeObjectHeapCompactionMode(typing.SupportsInt):
    @typing.overload
    def __init__(self, value: int) -> None: ...
    @typing.overload
    def __init__(self, value: int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...

    # Values:
    Default: GCLargeObjectHeapCompactionMode  # 1
    CompactOnce: GCLargeObjectHeapCompactionMode  # 2

class GCLatencyMode(typing.SupportsInt):
    @typing.overload
    def __init__(self, value: int) -> None: ...
    @typing.overload
    def __init__(self, value: int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...

    # Values:
    Batch: GCLatencyMode  # 0
    Interactive: GCLatencyMode  # 1
    LowLatency: GCLatencyMode  # 2
    SustainedLowLatency: GCLatencyMode  # 3
    NoGCRegion: GCLatencyMode  # 4

class GCSettings(abc.ABC):
    @classmethod
    @property
    def IsServerGC(cls) -> bool: ...
    @classmethod
    @property
    def LargeObjectHeapCompactionMode(cls) -> GCLargeObjectHeapCompactionMode: ...
    @classmethod
    @LargeObjectHeapCompactionMode.setter
    def LargeObjectHeapCompactionMode(
        cls, value: GCLargeObjectHeapCompactionMode
    ) -> GCLargeObjectHeapCompactionMode: ...
    @classmethod
    @property
    def LatencyMode(cls) -> GCLatencyMode: ...
    @classmethod
    @LatencyMode.setter
    def LatencyMode(cls, value: GCLatencyMode) -> GCLatencyMode: ...

class JitInfo(abc.ABC):
    @staticmethod
    def GetCompilationTime(currentThread: bool = ...) -> TimeSpan: ...
    @staticmethod
    def GetCompiledILBytes(currentThread: bool = ...) -> int: ...
    @staticmethod
    def GetCompiledMethodCount(currentThread: bool = ...) -> int: ...

class MemoryFailPoint(CriticalFinalizerObject, IDisposable):
    def __init__(self, sizeInMegabytes: int) -> None: ...
    def Dispose(self) -> None: ...

class ProfileOptimization(abc.ABC):
    @staticmethod
    def SetProfileRoot(directoryPath: str) -> None: ...
    @staticmethod
    def StartProfile(profile: typing.Optional[str]) -> None: ...

class TargetedPatchingOptOutAttribute(Attribute):
    def __init__(self, reason: str) -> None: ...
    @property
    def Reason(self) -> str: ...
    @property
    def TypeId(self) -> typing.Any: ...
