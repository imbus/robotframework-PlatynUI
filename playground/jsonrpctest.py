import asyncio
import contextlib
import os
import socket
import sys
import tempfile
import uuid
from pathlib import Path
from typing import TYPE_CHECKING, cast

if TYPE_CHECKING:
    from asyncio.windows_events import ProactorEventLoop

if __name__ == "__main__" and not __package__:
    file = Path(__file__).resolve()
    parent, top = file.parent, file.parents[1]

    if str(top) not in sys.path:
        sys.path.append(str(top))

    with contextlib.suppress(ValueError):
        sys.path.remove(str(parent))

    __package__ = "playground"

from .jsonpeer import JsonRpcPeer


async def create_process_streams_stdio(
    program: str, *args: str, cwd: str | None = None
) -> tuple[asyncio.subprocess.Process, asyncio.StreamReader, asyncio.StreamWriter]:
    """Erstellt asyncio Streams für einen Subprocess"""
    # Process mit Pipes erstellen
    process = await asyncio.create_subprocess_exec(
        program,
        *args,
        stdin=asyncio.subprocess.PIPE,
        stdout=asyncio.subprocess.PIPE,
        stderr=asyncio.subprocess.PIPE,
        cwd=cwd,
    )

    # In asyncio.create_subprocess_exec sind stdin, stdout und stderr bereits StreamWriter bzw. StreamReader
    # Wir müssen sie nicht manuell umwandeln
    reader = process.stdout
    writer = process.stdin

    return process, reader, writer


def generate_random_pipe_name(prefix: str | None = None) -> str:
    r"""
    Erzeugt einen plattformübergreifenden zufälligen Namen für eine Named Pipe.

    Windows: Gibt eine Pipe im Format "\\.\pipe\{uuid}" zurück
    Unix/Linux/macOS: Gibt einen Pfad zu einer Pipe im temporären Verzeichnis zurück

    Returns:
        str: Ein zufälliger Name für eine Named Pipe, der auf der aktuellen Plattform funktioniert
    """
    prefix = prefix or ""
    if prefix and not prefix.endswith("_"):
        prefix += "_"

    random_id = f"{prefix}{uuid.uuid4()}"

    if os.name == "nt":  # Windows
        pipe_name = f"\\\\.\\pipe\\{random_id}"
    else:  # Unix/Linux/macOS
        temp_dir = Path(tempfile.gettempdir())
        pipe_name = str(temp_dir / f"pipe_{random_id}")

    return pipe_name


async def create_process_streams_named_pipe_client(
    pipe_name: str, program: str, *args: str, cwd: str | None = None, timeout: float = 30.0
) -> tuple[asyncio.subprocess.Process, asyncio.StreamReader, asyncio.StreamWriter]:
    process = await asyncio.create_subprocess_exec(
        program,
        *args,
        stdin=asyncio.subprocess.PIPE,
        stdout=asyncio.subprocess.PIPE,
        stderr=asyncio.subprocess.PIPE,
        cwd=cwd,
    )

    loop = asyncio.get_running_loop()
    start_time = asyncio.get_event_loop().time()

    while True:
        try:
            if os.name == "nt":  # Windows
                # Für Windows verwendet den ProactorEventLoop und seine pipe_connection-Methode
                reader = asyncio.StreamReader()
                protocol = asyncio.StreamReaderProtocol(reader)

                # Verwende create_pipe_connection für Windows-Named-Pipes
                transport, _ = await cast("ProactorEventLoop", loop).create_pipe_connection(lambda: protocol, pipe_name)

                writer = asyncio.StreamWriter(transport, protocol, reader, loop)
                return process, reader, writer
            # Unix/Linux/macOS

            reader, writer = await asyncio.open_unix_connection(pipe_name)
            return process, reader, writer
        except (FileNotFoundError, ConnectionRefusedError, PermissionError) as e:
            elapsed = asyncio.get_event_loop().time() - start_time
            if elapsed >= timeout:
                raise TimeoutError(
                    f"Timeout: Unable to connect to named pipe {pipe_name} after {timeout} seconds."
                ) from e
            await asyncio.sleep(0.5)


async def connect_tcp_socket(address: str, port: int) -> tuple[asyncio.StreamReader, asyncio.StreamWriter]:
    reader, writer = await asyncio.open_connection(address, port)
    transport = writer.transport
    sock = transport.get_extra_info("socket")
    if sock is not None:
        sock.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
    return reader, writer


async def create_process_streams_tcp(
    address: str, port: int, program: str, *args: str, cwd: str | None = None, timeout: float = 30.0
) -> tuple[asyncio.subprocess.Process, asyncio.StreamReader, asyncio.StreamWriter]:
    """Creates process and connects to it via TCP socket

    Args:
        address: The TCP server address to connect to
        port: The TCP port to connect to
        program: Program to execute
        args: Program arguments
        cwd: Current working directory for the process
        timeout: Connection timeout in seconds

    Returns:
        tuple: (Process, StreamReader, StreamWriter)
    """
    process = await asyncio.create_subprocess_exec(
        program,
        *args,
        stdin=asyncio.subprocess.PIPE,
        stdout=asyncio.subprocess.PIPE,
        stderr=asyncio.subprocess.PIPE,
        cwd=cwd,
    )

    start_time = asyncio.get_event_loop().time()

    while True:
        try:
            reader, writer = await asyncio.open_connection(address, port)
            return process, reader, writer
        except (ConnectionRefusedError, OSError) as e:
            elapsed = asyncio.get_event_loop().time() - start_time
            if elapsed >= timeout:
                raise TimeoutError(
                    f"Timeout: Unable to connect to TCP server {address}:{port} after {timeout} seconds."
                ) from e
            await asyncio.sleep(0.5)


async def main():
    # Server-Prozess starten
    # cmd = [
    #     "dotnet",
    #     "run",
    #     "--project",
    #     "src/PlatynUI.Server",
    #     "--stdio",
    # ]

    # process, reader, writer = await create_process_streams_stdio(*cmd)

    # pipe_name = generate_random_pipe_name("platynui")

    # cmd = ["dotnet", "run", "--project", "src/PlatynUI.Server", "--", "--verbose", "--pipe-server", pipe_name]

    # process, reader, writer = await create_process_streams_named_pipe_client(pipe_name, *cmd)

    reader, writer = await connect_tcp_socket("10.211.55.7", 7720)

    # JsonRpcPeer erstellen, der mit dem Serverprozess kommuniziert
    peer = JsonRpcPeer(reader, writer)

    # Peer starten
    await peer.start()

    try:
        # Initialisierung senden
        print(await peer.send_request("displayDevice/getBoundingRectangle"))

        # Show-Anfragen in einer Schleife senden
        for x in range(40, 1000, 1):
            await peer.send_request(
                "displayDevice/highlightRect", {"x": x, "y": x, "width": 100, "height": 100, "timeout": 3}
            )
            # await asyncio.sleep(0.001)  # Kleine Pause zwischen Anfragen

        # Warten, um die Anzeige zu sehen
        await asyncio.sleep(5)

        # Eine Notification senden
        await peer.send_notification("Exit", {})

        # Kurze Pause, um dem Server Zeit zum Verarbeiten zu geben
        await asyncio.sleep(0.5)

    except Exception as e:
        print(f"Fehler: {e}")
    finally:
        # Peer und Prozess ordnungsgemäß beenden
        await peer.stop()

        print("Prozess beenden...")
        process.terminate()
        try:
            await asyncio.wait_for(process.wait(), timeout=2)
        except asyncio.TimeoutError:
            process.kill()

        if process.stderr is not None:
            # Stderr des Servers ausgeben (zur Fehlersuche)
            stderr_output = await process.stderr.read()
            if stderr_output:
                print(f"Server stderr:\n{stderr_output.decode('utf-8')}")


if __name__ == "__main__":
    asyncio.run(main())
