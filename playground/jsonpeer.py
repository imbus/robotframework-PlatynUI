import asyncio
import dataclasses
import enum
import json
import traceback
from dataclasses import dataclass, field
from enum import IntEnum
from typing import Any, Callable, Coroutine, Dict, List, Optional, Set, Tuple, Type, TypeVar, Union, cast

__field_name_cache: Dict[Tuple[Type[Any], dataclasses.Field[Any]], str] = {}
__NOT_SET = object()


def encode_case_for_field_name(obj: Any, field: dataclasses.Field[Any]) -> str:
    t = obj if isinstance(obj, type) else type(obj)
    name = __field_name_cache.get((t, field), __NOT_SET)
    if name is __NOT_SET:
        alias = field.metadata.get("alias", None)
        if alias:
            name = str(alias)
        elif hasattr(obj, "_encode_case"):
            name = str(obj._encode_case(field.name))
        else:
            name = field.name
        __field_name_cache[(t, field)] = name

    return cast(str, name)


NONETYPE = type(None)

__dataclasses_cache: Dict[Type[Any], Tuple[dataclasses.Field[Any], ...]] = {}


def get_dataclass_fields(t: Type[Any]) -> Tuple[dataclasses.Field[Any], ...]:
    fields = __dataclasses_cache.get(t)
    if fields is None:
        fields = __dataclasses_cache[t] = dataclasses.fields(t)
    return fields


def _default(o: Any) -> Any:
    if dataclasses.is_dataclass(o):
        fields = get_dataclass_fields(type(o))

        return {
            encode_case_for_field_name(o, field): getattr(o, field.name)
            for field in fields
            if ((field.init or field.metadata.get("force_json", False)) and not field.metadata.get("nosave", False))
            and not (getattr(o, field.name) is None and field.default != dataclasses.MISSING)
        }

    if isinstance(o, enum.Enum):
        return o.value
    if isinstance(o, Set):
        return list(o)

    raise TypeError(f"Can't get default value for {type(o)} with value {o!r}")


T = TypeVar("T")

PROTOCOL_VERSION = "2.0"


class JsonRpcErrorCode(IntEnum):
    PARSE_ERROR = -32700
    INVALID_REQUEST = -32600
    METHOD_NOT_FOUND = -32601
    INVALID_PARAMS = -32602
    INTERNAL_ERROR = -32603


@dataclass
class JsonRpcMessage:
    jsonrpc: str = field(default=PROTOCOL_VERSION, init=False, metadata={"force_json": True})


@dataclass
class JsonRpcRequest(JsonRpcMessage):
    method: str
    id: Union[str, int]
    params: Optional[Union[Dict[str, Any], List[Any]]] = None


@dataclass
class JsonRpcNotification(JsonRpcMessage):
    method: str
    params: Optional[Union[Dict[str, Any], List[Any]]] = None


@dataclass
class JsonResponseBase(JsonRpcMessage):
    pass


@dataclass
class JsonRpcResponse(JsonResponseBase):
    id: Union[str, int]
    result: Any = None


@dataclass
class JsonRpcError:
    code: int
    message: str
    data: Optional[Any] = None


@dataclass
class JsonRpcErrorResponse(JsonResponseBase):
    error: JsonRpcError
    id: Union[str, int, None]


class JsonRpcPeer:
    def __init__(self, reader: asyncio.StreamReader, writer: asyncio.StreamWriter):
        self.reader = reader
        self.writer = writer

        self.request_handlers: Dict[str, Callable[[Optional[Any], Dict[str, Any]], Coroutine[Any, Any, Any]]] = {}
        self.notification_handlers: Dict[str, Callable[[Optional[Any], Dict[str, Any]], Coroutine[Any, Any, None]]] = {}

        self.pending_requests: Dict[str, "asyncio.Future[Any]"] = {}
        self.id_counter = 1
        self.id_lock = asyncio.Lock()

        self.default_encoding = "utf-8"

        self.running = False
        self._completion_future: "asyncio.Future[bool]" = asyncio.Future()
        self._read_task = None

        self.json_serializer_options = {"ignore_none": True}

    @property
    def completion(self) -> asyncio.Future[bool]:
        return self._completion_future

    async def start(self) -> None:
        self.running = True
        self._read_task = asyncio.create_task(self.io_loop())

    async def stop(self) -> None:
        self.running = False
        if self._read_task and not self._read_task.done():
            self._read_task.cancel()
            try:
                await self._read_task
            except asyncio.CancelledError:
                pass
        await self.completion

    def register_request_handler(
        self, method_name: str, handler: Callable[[Optional[Any], Dict[str, Any]], Any]
    ) -> None:
        if not method_name or method_name.isspace():
            raise ValueError("Method name cannot be null or whitespace")

        if method_name in self.request_handlers:
            raise ValueError(f"Request handler for method '{method_name}' already exists")

        self.request_handlers[method_name] = handler

    def register_notification_handler(
        self, method_name: str, handler: Callable[[Optional[Any], Dict[str, Any]], None]
    ) -> None:
        if not method_name or method_name.isspace():
            raise ValueError("Method name cannot be null or whitespace")

        if method_name in self.notification_handlers:
            raise ValueError(f"Notification handler for method '{method_name}' already exists")

        self.notification_handlers[method_name] = handler

    async def send_message(self, json_str: str) -> None:
        body_bytes = (json_str + "\r\n").encode(self.default_encoding)
        header = f"Content-Length: {len(body_bytes)}\r\n\r\n"
        header_bytes = header.encode("ascii")

        self.writer.write(memoryview(header_bytes + body_bytes))

        await self.writer.drain()

    async def send_notification(self, method: str, params: Optional[Any] = None) -> None:
        message = JsonRpcNotification(method=method)
        if params is not None:
            message.params = params

        json_str = json.dumps(
            message, default=_default, skipkeys=self.json_serializer_options.get("ignore_none", False)
        )
        await self.send_message(json_str)

    async def send_request(self, method: str, params: Optional[Any] = None) -> Any:
        return await self.send_request_typed(method, params, object)

    async def send_request_typed(self, method: str, params: Optional[Any] = None, response_type: Type[T] = object) -> T:
        async with self.id_lock:
            id_str = str(self.id_counter)
            self.id_counter += 1

        message = JsonRpcRequest(method=method, id=id_str)
        message.jsonrpc = PROTOCOL_VERSION
        if params is not None:
            message.params = params
        else:
            message.params = {}

        json_str = json.dumps(
            message, default=_default, skipkeys=self.json_serializer_options.get("ignore_none", False)
        )

        tcs = asyncio.Future()
        self.pending_requests[id_str] = tcs

        await self.send_message(json_str)

        response = await asyncio.wait_for(tcs, timeout=30.0)

        if isinstance(response.result, dict) and response_type is object:
            return cast(response_type, response.result)

        return response.result

    async def io_loop(self) -> None:
        buffer = bytearray()
        content_length = 0
        headers_complete = False
        encoding = self.default_encoding

        try:
            while self.running:
                try:
                    data = await self.reader.read(4096)
                    if not data:
                        self.running = False
                        break
                except asyncio.CancelledError:
                    self.running = False
                    raise

                buffer.extend(data)

                while True:
                    if not headers_complete:
                        header_end = buffer.find(b"\r\n\r\n")
                        if header_end == -1:
                            break

                        encoding = self.default_encoding

                        headers = buffer[:header_end].decode("ascii")
                        for line in headers.split("\r\n"):
                            if line.startswith("Content-Length: "):
                                content_length = int(line[16:])
                            elif line.startswith("Content-Type: "):
                                content_type = line[14:]
                                parts = content_type.split(";")
                                for part in parts:
                                    part = part.strip()
                                    if part.startswith("charset="):
                                        charset = part[8:].strip().strip('"')
                                        if charset:
                                            encoding = charset

                        buffer = buffer[header_end + 4 :]
                        headers_complete = True

                    if headers_complete and len(buffer) >= content_length:
                        body = buffer[:content_length]
                        message = body.decode(encoding)

                        await self.handle_message(message)

                        buffer = buffer[content_length:]
                        headers_complete = False
                        content_length = 0
                    else:
                        break

        except Exception as e:
            self._completion_future.set_exception(e)
            return
        finally:
            self.running = False
            self._completion_future.set_result(True)

    async def handle_message(self, json_str: str) -> None:
        try:
            json_element = json.loads(json_str)

            if isinstance(json_element, list):
                responses = []

                for element in json_element:
                    response = await self.process_single_message(element)
                    if response is not None:
                        responses.append(response)

                if responses:
                    batch_response = json.dumps(responses)
                    await self.send_message(batch_response)
            else:
                response = await self.process_single_message(json_element)
                if response is not None:
                    single_response = json.dumps(
                        response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False)
                    )
                    await self.send_message(single_response)

        except json.JSONDecodeError as ex:
            error_response = JsonRpcErrorResponse(
                error=JsonRpcError(code=JsonRpcErrorCode.PARSE_ERROR, message=str(ex), data=traceback.format_exc()),
                id=None,
            )
            await self.send_message(
                json.dumps(error_response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False))
            )

        except Exception as ex:
            error_response = JsonRpcErrorResponse(
                error=JsonRpcError(code=JsonRpcErrorCode.INVALID_REQUEST, message=str(ex), data=traceback.format_exc()),
                id=None,
            )
            await self.send_message(
                json.dumps(error_response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False))
            )

    async def process_single_message(self, json_element: Dict[str, Any]) -> Optional[JsonResponseBase]:
        if "id" in json_element and "method" not in json_element:
            id_value = json_element.get("id")
            id_str = str(id_value) if id_value is not None else None

            if id_value is not None and id_str in self.pending_requests:
                tcs = self.pending_requests.pop(id_str)

                if "error" in json_element:
                    error = JsonRpcError(**json_element["error"])
                    tcs.set_exception(Exception(f"RPC Error {error.code}: {error.message} ({error.data})"))
                elif "result" in json_element:
                    response = JsonRpcResponse(id=id_value, result=json_element.get("result"))
                    tcs.set_result(response)
                else:
                    tcs.set_exception(Exception("RPC Error: Invalid response format"))

                return None

            if id_value is None and "error" in json_element:
                error = JsonRpcError(**json_element["error"])
                print(f"Received error with null ID: {error.code}: {error.message} ({error.data})")
                return None

        elif "method" in json_element:
            if "id" in json_element and json_element["id"] is not None:
                request = JsonRpcRequest(**json_element)
                return await self.process_request(request)

            notification = JsonRpcNotification(**json_element)
            return await self.process_notification(notification)

        return None

    async def process_request(self, message: JsonRpcRequest) -> JsonResponseBase:
        if message.method in self.request_handlers:
            handler = self.request_handlers[message.method]
            try:
                result = await handler(message.params, self.json_serializer_options)
                return JsonRpcResponse(id=message.id, result=result)
            except Exception as ex:
                return JsonRpcErrorResponse(
                    id=message.id,
                    error=JsonRpcError(
                        code=JsonRpcErrorCode.INTERNAL_ERROR, message=str(ex), data=traceback.format_exc()
                    ),
                )
        else:
            return JsonRpcErrorResponse(
                id=message.id,
                error=JsonRpcError(
                    code=JsonRpcErrorCode.METHOD_NOT_FOUND, message=f"Method for request {message.method} not found"
                ),
            )

    async def process_notification(self, message: JsonRpcNotification) -> Optional[JsonResponseBase]:
        if message.method in self.notification_handlers:
            handler = self.notification_handlers[message.method]
            try:
                await handler(message.params, self.json_serializer_options)
                return None
            except Exception as ex:
                return JsonRpcErrorResponse(
                    id=None,
                    error=JsonRpcError(
                        code=JsonRpcErrorCode.INTERNAL_ERROR, message=str(ex), data=traceback.format_exc()
                    ),
                )
        else:
            return JsonRpcErrorResponse(
                id=None,
                error=JsonRpcError(
                    code=JsonRpcErrorCode.METHOD_NOT_FOUND,
                    message=f"Method for notification {message.method} not found",
                ),
            )
