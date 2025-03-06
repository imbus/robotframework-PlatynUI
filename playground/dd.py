# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from pythonnet import get_runtime_info

# load("netfx")
# load("coreclr")

# #import clr
# from System import Environment
# from System.Runtime.InteropServices import RuntimeInformation

# print(get_runtime_info().kind)
# print(Environment.Version)
# print(RuntimeInformation.ProcessArchitecture)
# print(RuntimeInformation.FrameworkDescription)

print(get_runtime_info().kind)
