import asyncio
from dataclasses import dataclass
from pathlib import Path
from typing import Any

from robotcode.jsonrpc2.client import JsonRPCCient
from robotcode.jsonrpc2.protocol import JsonRPCProtocol, rpc_method


@dataclass
class MyParams:
    message: str


class MyJsonRPCProtocol(JsonRPCProtocol):
    def do_something(self) -> None:
        print("Doing something in MyJsonRPCProtocol")

    @rpc_method(name="back_to_back", param_type=MyParams)
    def _back_to_back(self, message: str, *args: Any, **kwargs: Any) -> str:
        print(f"Back to back: {message}")
        return message + " back"

    @rpc_method(name="get_rect")
    def get_rect(self, data: Any) -> str:
        print(f"Back to back: {data}")
        return "ok"


async def main():
    print(Path())
    print(Path("./src/PlatynUI.Server/bin/Debug/net8.0/PlatynUI.Server.exe").exists())
    client = JsonRPCCient(protocol_type=MyJsonRPCProtocol)
    # await client.connect_pipe(
    #     "./src/PlatynUI.Server/bin/Debug/net8.0/PlatynUI.Server.exe"
    #     if sys.platform == "win32"
    #     else "./src/PlatynUI.Server/bin/Debug/net8.0/PlatynUI.Server"
    # )
    await client.connect_tcp("localhost", 5000)

    try:
        for i in range(1000):
            print(await client.protocol.send_request_async("echo", ["Hello, world!"]))
            print(await client.protocol.send_request_async("Add", [1, i]))
            print(await client.protocol.send_request_async("send_something_back", ["Hello, world!"]))
    finally:
        await client.terminate()


if __name__ == "__main__":
    asyncio.run(main())
