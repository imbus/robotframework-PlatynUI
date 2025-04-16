// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import AppKit
import Foundation

import AppKit
import Foundation

@_cdecl("GetBoundingRectangle")
public func GetBoundingRectangle(_ x: UnsafeMutablePointer<Double>, _ y: UnsafeMutablePointer<Double>, _ width: UnsafeMutablePointer<Double>, _ height: UnsafeMutablePointer<Double>) {
    let screens = NSScreen.screens

    if screens.isEmpty {
        x.pointee = 0.0
        y.pointee = 0.0
        width.pointee = 0.0
        height.pointee = 0.0
        return
    }

    // Berechne den Vereinigungsbereich (union) aller Screens (Cocoa-Koordinatensystem: Ursprung unten links)
    var minX = screens[0].frame.minX
    var minY = screens[0].frame.minY
    var maxX = screens[0].frame.maxX
    var maxY = screens[0].frame.maxY

    for screen in screens {
        let frame = screen.frame
        minX = min(minX, frame.minX)
        minY = min(minY, frame.minY)
        maxX = max(maxX, frame.maxX)
        maxY = max(maxY, frame.maxY)
    }

    let unionWidth = maxX - minX
    let unionHeight = maxY - minY

    // Statt einer Anpassung anhand des Hauptbildschirms verwenden wir hier direkt die
    // im Cocoa-Koordinatensystem vorliegenden Werte, die bereits die vertikale Verschiebung der einzelnen Screens berücksichtigen.
    x.pointee = minX
    y.pointee = minY
    width.pointee = unionWidth
    height.pointee = unionHeight
}
