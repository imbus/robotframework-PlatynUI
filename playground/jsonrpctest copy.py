import contextlib
import os
import subprocess
import sys
import time
from pathlib import Path

if __name__ == "__main__" and not __package__:
    file = Path(__file__).resolve()
    parent, top = file.parent, file.parents[1]

    if str(top) not in sys.path:
        sys.path.append(str(top))

    with contextlib.suppress(ValueError):
        sys.path.remove(str(parent))

    __package__ = "playground"

from .jsonpeer import JsonRpcPeer


def main():
    # Server-Prozess starten
    proc = subprocess.Popen(
        [
            "artifacts/bin/PlatynUI.Platform.MacOS/debug/runtimes/osx/native/PlatynUI.Platform.MacOS.Highlighter",
            "--server",
        ],
        stdin=subprocess.PIPE,
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,  # stderr geht in die angegebene Datei
        bufsize=0,  # Keine Pufferung,
        text=False,  # Binärmodus
    )

    # JsonRpcPeer erstellen, der mit dem Serverprozess kommuniziert
    peer = JsonRpcPeer(proc.stdout, proc.stdin)

    # Peer starten
    peer.start()

    try:
        peer.send_request("Initialize", {"processId": os.getpid()})

        for x in range(40, 1000, 1):
            peer.send_request("Show", {"x": x, "y": x, "width": 100, "height": 100, "timeout": 3})
            time.sleep(0.001)

        time.sleep(3)  # Warten, um die Anzeige zu sehen

        # Eine Notification senden
        peer.send_notification("Exit", {})

        # Kurze Pause, um dem Server Zeit zum Verarbeiten zu geben
        time.sleep(0.5)

    except Exception as e:
        print(f"Fehler: {e}")
    finally:
        # Peer und Prozess ordnungsgemäß beenden
        peer.stop()

        print("Prozess beenden...")
        proc.terminate()
        proc.wait(timeout=2)

        # Stderr des Servers ausgeben (zur Fehlersuche)
        if proc.stderr is not None:
            stderr_output = proc.stderr.read().decode("utf-8")
            if stderr_output:
                print(f"Server stderr:\n{stderr_output}")


if __name__ == "__main__":
    main()
