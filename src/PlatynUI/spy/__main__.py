# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0
import logging
import os
import subprocess
import sys
from pathlib import Path

logger = logging.getLogger(__name__)
LOGLEVEL = os.environ.get("LOGLEVEL", None)
if LOGLEVEL:
    logging.basicConfig(level=LOGLEVEL.upper())


def main() -> None:
    if os.name == "nt":
        exe_name = "PlatynUI.Spy.exe"
    else:
        exe_name = "PlatynUI.Spy"

    debug_path = (Path(__file__).parent.parent.parent.parent / f"artifacts/bin/PlatynUI.Spy/debug/{exe_name}").resolve()
    runtime_path = (Path(__file__).parent.parent / f"ui/runtime/coreclr/{exe_name}").resolve()

    if debug_path.exists():
        logger.debug(f"Starting {exe_name} from {debug_path}")  # noqa: G004
        subprocess.run([debug_path], shell=True)
    elif runtime_path.exists():
        logger.debug(f"Starting {exe_name} from {runtime_path}")  # noqa: G004
        subprocess.run([runtime_path])
    else:
        print(f"Can't find the executable {exe_name} in the debug or runtime folder.", file=sys.stderr)
        print("Search paths:", file=sys.stderr)
        print(f"  {debug_path}", file=sys.stderr)
        print(f"  {runtime_path}", file=sys.stderr)
        sys.exit(1)


if __name__ == "__main__":
    main()
