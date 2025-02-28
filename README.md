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
  - [Key Features](#key-features)
  - [Known (Testable) Frameworks and Application Structures](#known-testable-frameworks-and-application-structures)
  - [Known Limitations or Unsupported Features](#known-limitations-or-unsupported-features)
  - [Installation](#installation)
  - [Spy Tool](#spy-tool)
  - [Usage](#usage)
    - [Calculator Example](#calculator-example)
    - [KeePass Example](#keepass-example)
  - [Contributing](#contributing)
  - [Contact / Support](#contact--support)
  - [Versioning / Changelog](#versioning--changelog)
  - [License](#license)

## Disclaimer

This project is still under development and should not be used productively **yet**.

At the current state expect:

- bugs
- missing features
- missing documentation
- it to be a cool solution soon, we are working on it :)

Feel free to contribute, create issue, provide documentation or test the implementation.

## Project Description

This library extends Robot Framework by providing a cross-platform solution for automating desktop GUI tests via usage of xpath-strategies. Its main goal is to make it easier for testers and developers to identify, interact with, and verify various UI elements, regardless of the underlying framework. By incorporating a built-in spy tool and low-level keywords that bridge directly with Robot Framework, it helps teams streamline their testing process, reduce manual effort, and maintain more consistent UI test coverage.

We aim to provide a Robot Framework-first library. Meaning that we do NOT implement an interface which allows other UI-automation technologies to work with Robot Framework. It means we build a tool that's specifically designed to enable Robot Framework users to automate UI applications.

## Key Features

- **Built-in Spy Tool**: This repository includes an integrated spy tool that allows you to quickly identify xpath-locators and inspect UI objects accurately.
- **Direct Integration with Robot Framework**: It provides low-level keywords out of the box, ensuring a seamless link between the testing framework and the UI elements.
- **Broad Compatibility**: Supports various application types and operating systems, making it easier to automate tests across different environments.
  - Current Scope:
    - Windows 10 or higher
    - Linux with AT-SPI interface
    - macOS

## Known (Testable) Frameworks and Application Structures

- **Desktop Applications**
  - Windows-based apps (e.g., Win32, .NET)
  - Linux/X11 or Wayland apps
- **WPF (Windows Presentation Foundation)**
  - Often compatible through Microsoft UI Automation (UIA).
  - WPF applications can be tested if the automation tool recognizes standard UIA interfaces.
- **Hybrid or Custom Frameworks**
  - Java-based GUIs (e.g., Swing, JavaFX)
  - Other known UI technologies, as long as they are recognized by the spy tool
- **Avalonia**

## Known Limitations or Unsupported Features

- **Unrecognized UI Components**: Certain custom or highly specialized UI components may not be identifiable or automatable, due to missing hooks or limited accessibility APIs.
- **Operating System Restrictions**: Support for macOS may have potential issues with newer or less common window managers (e.g., certain Wayland implementations).
- **Dependency Requirements**: Dependency on .NET.
- **Web Applications**: Not optimized for browsers or embedded web views.

## Installation

- **Prerequisites**:
  - .NET 8.0 or higher
  - Python 3.10 or higher
  - Hatch

- Clone the repository to your machine:

  ```console
  git clone https://github.com/your-repo/robotframework-platynui.git
  ```

- Create Hatch environment (also creates the .NET environment)

    ```console
    hatch env create
    ```

## Spy Tool

After installation and creation of the hatch environment, you can start the spy tool:

```console
Platyn.UI Spy
```

- Start any application
- Idetify Elements with their locators
- Build a valid Xpath
- Example: How to integrate Spy-Xpaths into your Robot Framework Keywords (tbd)

```robot
*** Test Cases ***
My Spy X-Path Test
    Activate    xpath=?????
```

## Usage

- See Robocon 2025 Demo: <https://www.youtube.com/watch?v=H3gOjp1VZWQ>

### Calculator Example

```robot
*** Test Cases ***
Calculator Test
    # Check if application is running
    Ensure Exists    Window[@Name='Calculator']    30s

    # Click 1 and 2 on the calculator
    Activate    Window[@Name='Calculator']//Button[@AutomationId="num1Button"]
    Activate    Window[@Name='Calculator']//Button[@AutomationId="num2Button"]

    # Click equal sign
    Activate    Window[@Name='Calculator']//Button[@AutomationId="equal"]

    # Check operation
    # tbd

    # Close Calculator and verify it's closed
    Type Keys    Window[@Name='Calculator']    <Alt+F4>
    Is Active    Window[@Name='Calculator']    should be    False
```

### KeePass Example

```robot
*** Test Cases ***
KeePass Test
    # Open KeePass

    # Enter Master Key

    # Create Database

    # Create Password

    # Delete Password
```

## Contributing

We welcome contributions to the project! Here are some ways you can contribute:

- **Report Bugs**: Use [GitHub Issues](https://github.com/imbus/robotframework-PlatynUI/issues) to report bugs.
- **Suggest Features**: Use [GitHub Issues](https://github.com/imbus/robotframework-PlatynUI/issues) to suggest new features.
- **Submit Pull Requests**: Fork the repository, make your changes, and submit a pull request. Please ensure your code follows our coding standards and includes tests.

Contribution guidelines are currently in creation and will be available soon.

## Contact / Support

For any questions or support, you can:

- **Report Bugs**: Use [GitHub Issues](https://github.com/imbus/robotframework-PlatynUI/issues) to report bugs.
- **General Inquiries**: Contact us via [GitHub Discussions](https://github.com/imbus/robotframework-PlatynUI/discussions) for general questions and support.

## Versioning / Changelog

We use [Semantic Versioning](https://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/robotframework-PlatynUI/tags).

Changelog is maintained using conventional commits and can be found [here](https://github.com/robotframework-PlatynUI/CHANGELOG.md).

## License

`robotframework-platynui` is distributed under the terms of the [Apache 2.0](https://spdx.org/licenses/Apache-2.0.html) license.
