// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import AppKit
import ArgumentParser
import Cocoa
import Foundation
import Interop

struct HighlighterArguments: ParsableCommand {
    @Flag(name: .shortAndLong, help: "Start a server on stdio to show or hide.")
    public var server: Bool = false

    @Option(name: .shortAndLong, help: "The x-coordinate of the rectangle.")
    public var x: Double = 5

    @Option(name: .shortAndLong, help: "The y-coordinate of the rectangle.")
    public var y: Double = 5

    @Option(name: .shortAndLong, help: "The width of the rectangle.")
    public var width: Double = 105

    @Option(name: .shortAndLong, help: "The height of the rectangle.")
    public var height: Double = 105

    @Option(name: .shortAndLong, help: "Timeout in seconds before the program exits.")
    public var timeout: Double = 3
}

@MainActor private var overlayWindow: NSWindow?
@MainActor private var hideTimer: Timer?
@MainActor private var app: NSApplication!
@MainActor private var arguments: HighlighterArguments!

func watchProcessExit(pid: Int, completion: @escaping @Sendable () -> Void) {
    DispatchQueue.global(qos: .background).async {
        let kq = kqueue()
        guard kq != -1 else {
            return
        }

        defer { close(kq) }

        var ke = kevent()
        ke.ident = UInt(pid)
        ke.filter = Int16(EVFILT_PROC)
        ke.flags = UInt16(EV_ADD)
        ke.fflags = UInt32(NOTE_EXIT)
        ke.data = 0
        ke.udata = nil

        if kevent(kq, &ke, 1, nil, 0, nil) == -1 {
            return
        }

        var eventList = kevent()
        while kevent(kq, nil, 0, &eventList, 1, nil) > 0 {
            if eventList.fflags & UInt32(NOTE_EXIT) != 0 {
                DispatchQueue.main.async {
                    completion()
                }
                break
            }
        }
    }
}

func convertTopLeftRectRelativeToMainScreen(_ rect: NSRect) throws -> NSRect {
    guard let mainScreen = NSScreen.screens.first else {
        throw MainScreenNotFoundException(message: "Main screen not found")
    }

    let mainFrame = mainScreen.frame
    let originMacOS = CGPoint(
        x: rect.origin.x + mainFrame.origin.x,
        y: mainFrame.maxY - rect.origin.y - rect.height
    )

    return NSRect(origin: originMacOS, size: rect.size)
}

func showHighlight(x: Double, y: Double, width: Double, height: Double, time: Double?) throws {
    let borderWidth = 3.0
    let halfBorderWidth = borderWidth / 2.0

    let topLeftRect = NSRect(
        x: x - halfBorderWidth,
        y: y - halfBorderWidth,
        width: width + borderWidth,
        height: height + borderWidth)

    let rect = try convertTopLeftRectRelativeToMainScreen(topLeftRect)

    DispatchQueue.main.async {

        if overlayWindow == nil {
            overlayWindow = NSWindow(
                contentRect: rect,
                styleMask: .borderless,
                backing: .buffered,
                defer: false)
            overlayWindow?.level = .popUpMenu
            overlayWindow?.backgroundColor = .clear
            overlayWindow?.isOpaque = false
            overlayWindow?.hasShadow = false
            overlayWindow?.ignoresMouseEvents = true
            overlayWindow?.collectionBehavior = [.canJoinAllSpaces, .stationary]
            overlayWindow?.isReleasedWhenClosed = false
            overlayWindow?.orderFront(nil)

            let borderView = NSView(frame: rect)
            borderView.wantsLayer = true
            borderView.layer?.borderWidth = borderWidth - 1
            borderView.layer?.borderColor = NSColor.green.cgColor
            overlayWindow?.contentView = borderView
        } else {
            overlayWindow?.setFrame(rect, display: true)
        }

        hideTimer?.invalidate()
        if time == nil {
            hideTimer = nil
        } else {
            hideTimer = Timer.scheduledTimer(withTimeInterval: time!, repeats: false) { _ in
                closeHighlight()
            }
        }
    }
}

func closeHighlight() {
    DispatchQueue.main.async {
        overlayWindow?.orderOut(nil)
        overlayWindow = nil
        hideTimer?.invalidate()
        hideTimer = nil
        if !arguments.server {
            app.terminate(nil)
        }
    }
}

func handleError(_ error: Error) {
    FileHandle.standardError.write("Error: \(error)\n".data(using: .utf8)!)
}

@main
@MainActor
struct Main {
    static func main() {
        app = NSApplication.shared
        app.setActivationPolicy(.accessory)
        app.activate(ignoringOtherApps: true)

        arguments = HighlighterArguments.parseOrExit()

        if arguments.server {
            let inputStream = InputStream(fileAtPath: "/dev/stdin")!
            inputStream.open()

            let outputStream = OutputStream(toFileAtPath: "/dev/stdout", append: false)!
            outputStream.open()

            let peer = JsonRpcPeer(reader: inputStream, writer: outputStream)

            Task.detached {

                await peer.registerRequestHandler(method: "initialize") {
                    @Sendable params, decoder in
                    guard let params = params else {
                        throw JsonRpcError(
                            code: JsonRpcErrorCode.invalidParams.rawValue,
                            message: "Missing parameters"
                        )
                    }
                    struct Params: Codable, Hashable, Sendable {
                        public var processId: Int

                        public init(
                            processId: Int

                        ) {
                            self.processId = processId
                        }
                    }

                    let p: Params = try params.decode()

                    if p.processId != 0 {

                        Task.detached {
                            watchProcessExit(pid: p.processId) {
                                Task { @MainActor in
                                    app.terminate(nil)
                                }
                            }
                        }
                    }
                    return JSONValue.null
                }
                await peer.registerRequestHandler(method: "hide") {
                    @Sendable params, decoder in

                    closeHighlight()

                    return JSONValue.null
                }

                await peer.registerRequestHandler(method: "show") {
                    @Sendable params, decoder in
                    guard let params = params else {
                        throw JsonRpcError(
                            code: JsonRpcErrorCode.invalidParams.rawValue,
                            message: "Missing parameters"
                        )
                    }
                    struct Params: Codable, Hashable, Sendable {
                        public var x: Double
                        public var y: Double
                        public var width: Double
                        public var height: Double
                        public var timeout: Double?

                        public init(
                            x: Double,
                            y: Double,
                            width: Double,
                            height: Double,
                            timeout: Double?
                        ) {
                            self.x = x
                            self.y = y
                            self.width = width
                            self.height = height
                            self.timeout = timeout
                        }
                    }
                    let p: Params = try params.decode()

                    try showHighlight(
                        x: p.x,
                        y: p.y,
                        width: p.width,
                        height: p.height,
                        time: p.timeout
                    )
                    return JSONValue.null
                }
                await peer.registerNotificationHandler(method: "exit") {
                    @Sendable params, decoder in

                    await peer.stop()
                }

                do {
                    let task = try await peer.start()

                    _ = try await task.value

                } catch {
                    handleError(error)
                }

                Task { @MainActor in
                    app.terminate(nil)
                }
            }

        } else {
            do {
                try showHighlight(
                    x: arguments.x,
                    y: arguments.y,
                    width: arguments.width,
                    height: arguments.height,
                    time: arguments.timeout
                )
            } catch {
                handleError(error)
            }
        }

        app.run()
    }
}
