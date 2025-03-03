<!--
SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>

SPDX-License-Identifier: Apache-2.0
-->

# robotframework-PlatynUI

[![PyPI - Version](https://img.shields.io/pypi/v/robotframework-platynui.svg)](https://pypi.org/project/robotframework-platynui)
[![PyPI - Python Version](https://img.shields.io/pypi/pyversions/robotframework-platynui.svg)](https://pypi.org/project/robotframework-platynui)

-----

## Table of Contents

- [robotframework-PlatynUI](#robotframework-platynui)
  - [Table of Contents](#table-of-contents)
  - [Disclaimer](#disclaimer)
  - [Project Description](#project-description)
    - [Why PlatynUI?](#why-platynui)
  - [Testable Frameworks](#testable-frameworks)
  - [Installation](#installation)
  - [Spy Tool](#spy-tool)
  - [Demo](#demo)
  - [Application Example](#application-example)
  - [Roadmap](#roadmap)
  - [Contributing](#contributing)
    - [Setup Dev Environment](#setup-dev-environment)
  - [Versioning](#versioning)
  - [License](#license)

## Disclaimer

This project is still under development and should not be used productively **yet**.

At the current state expect:

- bugs
- missing features
- missing documentation

Feel free to contribute, create issues, provide documentation or test the implementation.

## Project Description

PlatynUI is a library for Robot Framework, providing a cross-platform solution for UI test automation. Its main goal is to make it easier for testers and developers to identify, interact with, and verify various UI elements.

We aim to provide a Robot Framework-first library.

### Why PlatynUI?

- Cross-platform capability with consistent API across Windows, Linux, and MacOS
- Direct access to native UI elements
- Simplified element identification
- Builtin ui spy tool

## Testable Frameworks

- **Linux**
  - X11
  - AT-SPI2
- **Windows**
  - Microsoft UI Automation (UIA)
- **MacOS**
  - Accessibility API

Extendable for any other ui technologies.

## Installation

```console
pip install robotframework-platynui
```

## Spy Tool

After installation, start the spy tool on the command line with this command:

```console
PlatynUI.Spy
```

- Start any application
- Identify elements and properties in your application
- Access elements with it's properties and build locators to access and simulate ui applications

## Demo

- [Robocon 2025](https://www.youtube.com/watch?v=H3gOjp1VZWQ)

## Application Example

```python
@locator(name="Rechner")
class CalculatorWindow(Window):
    @property
    @locator(AutomationId="num5Button")
    def n5(self) -> Button: ...
    @property
    @locator(AutomationId="num6Button")
    def n6(self) -> Button: ...
    @property
    @locator(AutomationId="plusButton")
    def plus(self) -> Button: ...
    @property
    @locator(AutomationId="equalButton")
    def equal(self) -> Button: ...
```

```robot
*** Settings ***
Library     PlatynUI
Variables   apps.calculator

*** Test Cases ***
Test Addition Of Two Numbers
    Activate    ${calculator.n5}
    Activate    ${calculator.plus}
    Activate    ${calculator.n6}
    Activate    ${calculator.equal}
    Get Text    ${calculator.result}    should be    11
```

## Roadmap

Roadmap will soon be displayed [here](https://github.com/imbus/robotframework-PlatynUI/projects)

## Contributing

We welcome contributions to the project! Here are some ways you can contribute:

- **Report Bugs**: Use [GitHub Issues](https://github.com/imbus/robotframework-PlatynUI/issues) to report bugs.
- **Suggest Features**: Use [GitHub Issues](https://github.com/imbus/robotframework-PlatynUI/issues) to suggest new features.
- **Submit Pull Requests**: Fork the repository, make your changes, and submit a pull request. Please ensure your code follows our coding standards and includes tests.

Contribution guidelines are currently in creation and will be available soon.

### Setup Dev Environment

If you want to start now, you can setup a dev environment with following steps:

- **Prerequisites**:
  - .NET 8.0
  - Python 3.10 or higher
  - Hatch

- Clone the repository from [Github](https://github.com/imbus/robotframework-PlatynUI)

- Create Hatch environment (also creates the .NET environment)

  ```console
  cd robotframework-PlatynUI
  hatch env create
  ```

## Versioning

We use [Semantic Versioning](https://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/imbus/robotframework-PlatynUI/tags).

[Changelog](https://github.com/imbus/robotframework-PlatynUI/blob/main/CHANGELOG.md) is maintained using conventional commits.

## License

`robotframework-PlatynUI` is distributed under the terms of the [Apache 2.0](https://spdx.org/licenses/Apache-2.0.html) license.
