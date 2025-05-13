import dataclasses
import enum
import json
import queue
import selectors
import threading
import traceback
from concurrent.futures import Future, ThreadPoolExecutor
from dataclasses import dataclass, field
from enum import IntEnum
from typing import Any, BinaryIO, Callable, Dict, List, Optional, Set, Tuple, Type, TypeVar, Union, cast

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
    def __init__(self, reader_stream: BinaryIO, writer_stream: BinaryIO, thread_pool_size: Optional[int] = None):
        """
        Initializes a new JsonRpcPeer with thread pool for handlers

        :param reader_stream: A data stream for reading
        :param writer_stream: A data stream for writing
        :param thread_pool_size: Size of the thread pool for handlers (default: 4)
        """
        # Verify that streams are actually usable for reading/writing
        if not reader_stream.readable():
            raise ValueError("reader_stream must be readable")
        if not writer_stream.writable():
            raise ValueError("writer_stream must be writable")

        self.reader_stream = reader_stream
        self.writer_stream = writer_stream

        self.request_handlers: Dict[str, Callable[[Optional[Any], Dict[str, Any]], Any]] = {}
        self.notification_handlers: Dict[str, Callable[[Optional[Any], Dict[str, Any]], None]] = {}

        self.pending_requests = {}
        self.id_counter = 1
        self.id_lock = threading.Lock()

        # Thread pool for handlers
        self.handler_pool = ThreadPoolExecutor(max_workers=thread_pool_size)

        self.selector = selectors.DefaultSelector()
        self.write_queue = queue.Queue()
        self.default_encoding = "utf-8"  # Fallback encoding when charset is not specified

        self.io_thread = None
        self.running = False
        self._completion_future: Future[bool] = Future()

        self.json_serializer_options = {"ignore_none": True}

    @property
    def completion(self) -> Future[bool]:
        """Completion object for asynchronous termination"""
        return self._completion_future.result()

    def start(self) -> None:
        """Starts the JSON-RPC peer"""
        if self.io_thread is None:
            self.running = True

            try:
                self.selector.register(self.reader_stream, selectors.EVENT_READ)
            except ValueError:
                # If the stream object is already registered
                pass

            self.io_thread = threading.Thread(target=self.io_loop)
            self.io_thread.daemon = True
            self.io_thread.start()

    def stop(self) -> None:
        """Stops the JSON-RPC peer"""
        self.running = False

        if self.io_thread and self.io_thread.is_alive():
            self.io_thread.join(timeout=1.0)

        self.io_thread = None
        self.selector.close()

        # Properly shut down the thread pool
        self.handler_pool.shutdown(wait=False)

    def register_request_handler(
        self, method_name: str, handler: Callable[[Optional[Any], Dict[str, Any]], Any]
    ) -> None:
        """Registers a handler for request messages"""
        if not method_name or method_name.isspace():
            raise ValueError("Method name cannot be null or whitespace")

        if method_name in self.request_handlers:
            raise ValueError(f"Request handler for method '{method_name}' already exists")

        self.request_handlers[method_name] = handler

    def register_notification_handler(
        self, method_name: str, handler: Callable[[Optional[Any], Dict[str, Any]], None]
    ) -> None:
        """Registers a handler for notification messages"""
        if not method_name or method_name.isspace():
            raise ValueError("Method name cannot be null or whitespace")

        if method_name in self.notification_handlers:
            raise ValueError(f"Notification handler for method '{method_name}' already exists")

        self.notification_handlers[method_name] = handler

    def send_message(self, json_str: str) -> None:
        """Sends a JSON message"""
        body_bytes = (json_str + "\r\n").encode(self.default_encoding)
        header = f"Content-Length: {len(body_bytes)}\r\n\r\n"
        header_bytes = header.encode("ascii")

        self.writer_stream.write(header_bytes)
        self.writer_stream.write(body_bytes)
        self.writer_stream.flush()

    def send_notification(self, method: str, params: Optional[Any] = None) -> None:
        """Sends a notification"""
        message = JsonRpcNotification(method=method)
        if params is not None:
            message.params = params

        json_str = json.dumps(
            message, default=_default, skipkeys=self.json_serializer_options.get("ignore_none", False)
        )
        self.send_message(json_str)

    def send_request(self, method: str, params: Optional[Any] = None) -> Any:
        """Sends a request and waits for response"""
        return self.send_request_typed(method, params, object)

    def send_request_typed(self, method: str, params: Optional[Any] = None, response_type: Type[T] = object) -> T:
        """Sends a request with a specific return type"""
        with self.id_lock:
            id_str = str(self.id_counter)
            self.id_counter += 1

        message = JsonRpcRequest(method=method, id=id_str)
        message.jsonrpc = PROTOCOL_VERSION
        if params is not None:
            message.params = params

        json_str = json.dumps(
            message, default=_default, skipkeys=self.json_serializer_options.get("ignore_none", False)
        )

        tcs = Future()
        self.pending_requests[id_str] = tcs

        self.send_message(json_str)

        # Wait for response with timeout
        response = tcs.result(timeout=30.0)  # 30 seconds timeout

        if isinstance(response.result, dict) and response_type is object:
            return cast(response_type, response.result)

        return response.result

    def io_loop(self) -> None:
        """IO loop for reading and writing messages"""
        buffer = bytearray()
        content_length = 0
        headers_complete = False
        encoding = self.default_encoding  # Default encoding for messages

        try:
            while self.running:
                # Wait for events with timeout to check the termination condition
                events = self.selector.select(timeout=0.1)

                for key, mask in events:
                    if mask & selectors.EVENT_READ:
                        # Read from stream
                        data = self.reader_stream.read(4096)
                        if not data:
                            # Connection closed
                            self.running = False
                            break

                        buffer.extend(data)

                        # Process all complete messages in the buffer
                        while True:
                            # Parse headers
                            if not headers_complete:
                                header_end = buffer.find(b"\r\n\r\n")
                                if header_end == -1:
                                    break  # No complete headers yet

                                # Reset encoding for new message
                                encoding = self.default_encoding

                                headers = buffer[:header_end].decode("ascii")
                                for line in headers.split("\r\n"):
                                    if line.startswith("Content-Length: "):
                                        content_length = int(line[16:])
                                    elif line.startswith("Content-Type: "):
                                        content_type = line[14:]
                                        # Parse content type for charset
                                        parts = content_type.split(";")
                                        for part in parts:
                                            part = part.strip()
                                            if part.startswith("charset="):
                                                charset = part[8:].strip().strip('"')
                                                if charset:
                                                    encoding = charset

                                buffer = buffer[header_end + 4 :]  # 4 = len("\r\n\r\n")
                                headers_complete = True

                            # Parse body if enough data is available
                            if headers_complete and len(buffer) >= content_length:
                                body = buffer[:content_length]
                                message = body.decode(encoding)  # Use the encoding from headers for this message

                                # Execute handler method in thread pool
                                self.handle_message(message)

                                buffer = buffer[content_length:]
                                headers_complete = False
                                content_length = 0
                            else:
                                break  # Not enough data for the body yet

        except Exception as e:
            self._completion_future.set_exception(e)
            return

        self._completion_future.set_result(True)

    def handle_message(self, json_str: str) -> None:
        """
        Processes an incoming JSON message
        This method is called separately for each message
        """
        try:
            json_element = json.loads(json_str)

            if isinstance(json_element, list):
                responses = []

                for element in json_element:
                    response = self.process_single_message(element)
                    if response is not None:
                        responses.append(response)

                if responses:
                    batch_response = json.dumps(responses)
                    self.send_message(batch_response)
            else:
                response = self.process_single_message(json_element)
                if response is not None:
                    single_response = json.dumps(
                        response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False)
                    )
                    self.send_message(single_response)

        except json.JSONDecodeError as ex:
            error_response = JsonRpcErrorResponse(
                error=JsonRpcError(code=JsonRpcErrorCode.PARSE_ERROR, message=str(ex), data=traceback.format_exc()),
                id=None,
            )
            self.send_message(
                json.dumps(error_response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False))
            )

        except Exception as ex:
            error_response = JsonRpcErrorResponse(
                error=JsonRpcError(code=JsonRpcErrorCode.INVALID_REQUEST, message=str(ex), data=traceback.format_exc()),
                id=None,
            )
            self.send_message(
                json.dumps(error_response.__dict__, skipkeys=self.json_serializer_options.get("ignore_none", False))
            )

    def process_single_message(self, json_element: Dict[str, Any]) -> Optional[JsonResponseBase]:
        """Processes a single JSON-RPC message"""
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

            # Handle error with null ID
            elif id_value is None and "error" in json_element:
                error = JsonRpcError(**json_element["error"])
                print(f"Received error with null ID: {error.code}: {error.message} ({error.data})")
                return None

        elif "method" in json_element:
            if "id" in json_element and json_element["id"] is not None:
                # Execute request in thread pool
                request = JsonRpcRequest(**json_element)
                return self.process_request(request)
            else:
                # Execute notification in thread pool
                notification = JsonRpcNotification(**json_element)
                return self.process_notification(notification)

        return None

    def process_request(self, message: JsonRpcRequest) -> JsonResponseBase:
        """
        Processes an incoming request
        The actual handler method runs in the thread pool
        """
        if message.method in self.request_handlers:
            handler = self.request_handlers[message.method]
            try:
                # Execute request in thread pool
                future = self.handler_pool.submit(handler, message.params, self.json_serializer_options)
                result = future.result(timeout=30.0)  # 30 seconds timeout
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

    def process_notification(self, message: JsonRpcNotification) -> Optional[JsonResponseBase]:
        """
        Processes an incoming notification
        The actual handler method runs in the thread pool
        """
        if message.method in self.notification_handlers:
            handler = self.notification_handlers[message.method]
            try:
                # Execute notification in thread pool (non-blocking)
                self.handler_pool.submit(handler, message.params, self.json_serializer_options)
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
