// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import AppKit
import Foundation

public class MainScreenNotFoundException: Error, @unchecked Sendable {
    var message: String
    public init(message: String) {
        self.message = message
    }
}

public func getBoundingRect() throws -> NSRect {
    guard let mainScreen = NSScreen.screens.first else {
        throw MainScreenNotFoundException(message: "Main screen not found")
    }

    let mainFrame = mainScreen.frame
    let allScreens = NSScreen.screens

    // Bounding-Rect im nativen (unten links) Koordinatensystem
    var globalBounding = mainFrame
    for screen in allScreens {
        globalBounding = globalBounding.union(screen.frame)
    }

    let topLeftOrigin = CGPoint(
        x: globalBounding.origin.x - mainFrame.origin.x,
        y: mainFrame.maxY - globalBounding.maxY
    )

    return NSRect(origin: topLeftOrigin, size: globalBounding.size)
}

@_cdecl("GetBoundingRectangle")
public func getBoundingRectangle(
    _ x: UnsafeMutablePointer<Double>, _ y: UnsafeMutablePointer<Double>,
    _ width: UnsafeMutablePointer<Double>, _ height: UnsafeMutablePointer<Double>
) {
    do {
        let r = try getBoundingRect()

        x.pointee = r.minX
        y.pointee = r.minY
        width.pointee = r.width
        height.pointee = r.height
    } catch {
        FileHandle.standardError.write("Error: \(error)\n".data(using: .utf8)!)
        x.pointee = 0
        y.pointee = 0
        width.pointee = 0
        height.pointee = 0
    }
}
