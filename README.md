<!--
SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>

SPDX-License-Identifier: Apache-2.0
-->

# robotframework-PlatynUI

[![PyPI - Version](https://img.shields.io/pypi/v/robotframework-platynui.svg)](https://pypi.org/project/robotframework-platynui)
[![PyPI - Python Version](https://img.shields.io/pypi/pyversions/robotframework-platynui.svg)](https://pypi.org/project/robotframework-platynui)

-----

## Table of Contents

- [Project Description](#project-description)
- [Key Features](#key-features)
- [Testable Frameworks](#known-testable-frameworks-and-application-structures)
- [Known Limitations](#known-limitations-or-unsupported-features)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Example](#example)
- [Contributing](#contributing)
- [Contact / Support](#contact--support)
- [Versioning / Changelog](#versioning--changelog)
- [License](#license)

## Project Description

This repository extends Robot Framework by providing a cross-platform solution for automating desktop GUI tests. Its main goal is to make it easier for testers and developers to identify, interact with, and verify various UI elements, regardless of the underlying framework. By incorporating a built-in spy tool and low-level keywords that bridge directly with Robot Framework, it helps teams streamline their testing process, reduce manual effort, and maintain more consistent UI test coverage.

## Key Features

- Built-in Spy Tool

    This repository includes an integrated spy tool that allows you to identify and inspect UI objects quickly and accurately.

- Direct Integration with Robot Framework

    It provides low-level keywords out of the box, ensuring a seamless link between the testing framework and the UI elements.

- Broad Compatibility

    Supports various application types and operating Systems, making it easier to automate tests across different environments.

## Known (Testable) Frameworks and Application Structures

- Desktop Applications
    - Windows-based apps (e.g., Win32, .NET)
    - Linux/X11 or Wayland apps
- WPF (Windows Presentation Foundation)

    - Often compatible through Microsoft UI Automation (UIA).

    - WPF applications can be tested if the automation tool recognizes standard UIA interfaces.
- Hybrid or Custom Frameworks
    - Java-based GUIs (e.g., Swing, JavaFX)
    - Other known UI technologies, as long as they are recognized by the spy tool
- Avalonia

## Known Limitations or Unsupported Features

- Unrecognized UI Components

    - Certain custom or highly specialized UI components may not be identifiable or automatable, due to missing hooks or limited accessibility APIs.

- Operating System Restrictions

    - Support for macOS 
    - Limited support for specific Linux distributions?.
    - Potential issues with newer or less common window managers (e.g., certain Wayland implementations).
- Dependency Requirements
    - Dependency to .Net, how does that inpact the use of the tool

- Web Applications 
    - Not optimized for Browsers or embedded web views

## Installation

- What are the prerequisites (dependencies, versions, etc.)?
- How do you install the project/program/script?
```console
pip install robotframework-platynui
```
- Which steps are necessary to get it running?

## Usage

- Short examples or instructions on how to run the project.
- Typical commands, code snippets, or screenshots

--
## Configuration

- Information on which settings can be adjusted.
- For example, how to edit a configuration file (TOML, JSON, etc.).
  
## Example

- Typical scenarios or use cases.
- Example scripts, demo data, or integration tests.
  
## Contributing

- How can someone contribute to the project?
- Which guidelines should be followed (Contribution Guidelines)?
- How are pull requests created and reviewed?
  
## Contact / Support

- Who can be contacted for questions?
- Where to report bugs (Issues)?
- Any mailing lists or chat channels (Slack, Discord, etc.)?

## Versioning / Changelog

- Brief note on how versions are managed (e.g., Semantic Versioning).
- Possibly a link to a changelog.
  
## License

`robotframework-platynui` is distributed under the terms of the [Apache 2.0](https://spdx.org/licenses/Apache-2.0.html) license.
