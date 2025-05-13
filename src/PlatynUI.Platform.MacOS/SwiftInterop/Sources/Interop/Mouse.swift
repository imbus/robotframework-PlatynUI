// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import AppKit
import CoreGraphics
import Foundation

@_cdecl("GetMousePosition")
public func getMousePosition(_ x: UnsafeMutablePointer<Double>, _ y: UnsafeMutablePointer<Double>) {

    let p = CGEvent(source: nil)?.location ?? .zero

    x.pointee = p.x
    y.pointee = p.y
}

@_cdecl("MouseMove")
public func mouseMove(_ x: Double, _ y: Double) {
    do {
        let point = CGPoint(x: x, y: y)
        let mouseMoveEvent = CGEvent(
            mouseEventSource: nil, mouseType: .mouseMoved,
            mouseCursorPosition: point, mouseButton: .left)
        mouseMoveEvent?.post(tap: .cghidEventTap)
    }
}

@_cdecl("MousePress")
func mousePress(button: Int) {
    let currentPosition = CGEvent(source: nil)?.location ?? .zero
    let mouseType: CGEventType
    let cgMouseButton: CGMouseButton

    switch button {
    case 0:
        mouseType = .leftMouseDown
        cgMouseButton = .left
    case 1:
        mouseType = .rightMouseDown
        cgMouseButton = .right
    case 2:
        mouseType = .otherMouseDown
        cgMouseButton = .center
    default:
        mouseType = .otherMouseDown
        cgMouseButton = .center
    }

    let mouseDownEvent = CGEvent(
        mouseEventSource: nil, mouseType: mouseType,
        mouseCursorPosition: currentPosition, mouseButton: cgMouseButton)

    // Für X1, X2 Buttons (3, 4, etc.)
    if button >= 3 {
        mouseDownEvent?.setIntegerValueField(.mouseEventButtonNumber, value: Int64(button))
    }

    mouseDownEvent?.post(tap: .cghidEventTap)
}

@_cdecl("MouseRelease")
func mouseRelease(button: Int) {
    let currentPosition = CGEvent(source: nil)?.location ?? .zero
    let mouseType: CGEventType
    let cgMouseButton: CGMouseButton

    switch button {
    case 0:
        mouseType = .leftMouseUp
        cgMouseButton = .left
    case 1:
        mouseType = .rightMouseUp
        cgMouseButton = .right
    case 2:
        mouseType = .otherMouseUp
        cgMouseButton = .center
    default:
        mouseType = .otherMouseUp
        cgMouseButton = .center
    }

    let mouseUpEvent = CGEvent(  // Korrigiert: mouseDownEvent -> mouseUpEvent
        mouseEventSource: nil, mouseType: mouseType,
        mouseCursorPosition: currentPosition, mouseButton: cgMouseButton)

    // Für X1, X2 Buttons (3, 4, etc.)
    if button >= 3 {
        mouseUpEvent?.setIntegerValueField(.mouseEventButtonNumber, value: Int64(button))
    }

    mouseUpEvent?.post(tap: .cghidEventTap)
}

@_cdecl("GetMouseDoubleClickInterval")
func getMouseDoubleClickInterval() -> Double {
    return NSEvent.doubleClickInterval
}

@_cdecl("GetMouseDoubleClickTolerance")
func getMouseDoubleClickTolerance() -> Double {
    let defaultTolerance = 4.0

    if let tolerance = UserDefaults.standard.object(forKey: "com.apple.mouse.doubleClickThreshold")
        as? Double
    {
        return tolerance
    }

    return defaultTolerance
}
