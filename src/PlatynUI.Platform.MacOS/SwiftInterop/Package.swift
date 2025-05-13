// swift-tools-version: 6.1
// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import PackageDescription

let package = Package(
    name: "PlatynUI.Platform.MacOS.Interop",
    platforms: [.macOS(.v14)],
    products: [
        // Products define the executables and libraries a package produces, making them visible to other packages.
        .library(
            name: "PlatynUI.Platform.MacOS.Interop",
            type: .dynamic,
            targets: ["Interop"]),
        .executable(name: "PlatynUI.Platform.MacOS.Highlighter", targets: ["Highlighter"]),
    ],
    dependencies: [
        .package(url: "https://github.com/apple/swift-argument-parser.git", from: "1.5.0"),
    ],
    targets: [
        // Targets are the basic building blocks of a package, defining a module or a test suite.
        // Targets can depend on other targets in this package and products from dependencies.
        .target(
            name: "Interop",),
        .executableTarget(
            name: "Highlighter",
            dependencies: [
                .target(name: "Interop"),
                .product(name: "ArgumentParser", package: "swift-argument-parser"),
            ]),
    ]
)
