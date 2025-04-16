// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import AppKit
import ArgumentParser
import Foundation
import JSONRPC

struct HighlighterArguments: ParsableCommand {
    @Flag(name: .shortAndLong, help: "Start a server on stdio to show or hide.")
    public var server: Bool = false

    @Option(name: .shortAndLong, help: "The x-coordinate of the rectangle.")
    public var x: Double = 0

    @Option(name: .shortAndLong, help: "The y-coordinate of the rectangle.")
    public var y: Double = 0

    @Option(name: .shortAndLong, help: "The width of the rectangle.")
    public var width: Double = 100

    @Option(name: .shortAndLong, help: "The height of the rectangle.")
    public var height: Double = 100

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
            print("Failed to create kqueue")
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

        // Register event
        if kevent(kq, &ke, 1, nil, 0, nil) == -1 {
            print("Failed to register kevent")
            return
        }

        // Wait for event
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

public func GetDisplayHeight() -> Double {
    let screens = NSScreen.screens

    if screens.isEmpty {
        return 0
    }

    var minY = screens[0].frame.minY
    var maxY = screens[0].frame.maxY

    for screen in screens {
        let frame = screen.frame
        minY = min(minY, frame.minY)
        maxY = max(maxY, frame.maxY)
    }

    return maxY - minY
}

func showHighlight(x: Double, y: Double, width: Double, height: Double, time: Double?) {
    DispatchQueue.main.async {

        let borderWidth = 6.0
        let halfBorderWidth = borderWidth / 2.0

        let adjustedY = GetDisplayHeight() - y - height

        let rect = NSRect(
            x: x - halfBorderWidth,
            y: adjustedY - halfBorderWidth,
            width: width + borderWidth,
            height: height + borderWidth)

        if overlayWindow == nil {
            overlayWindow = NSWindow(
                contentRect: rect,
                styleMask: .borderless,
                backing: .buffered,
                defer: false)
            overlayWindow?.level = .popUpMenu  // Höheres Level als .statusBar
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

private func handleNotification(_ anyNotification: AnyJSONRPCNotification, data: Data) async {
    do {
        switch anyNotification.method {
        case "Exit":
            DispatchQueue.main.async {
                app.terminate(nil)
            }
        default:
            return
        }
    }
}

struct InitializeParams: Codable, Hashable, Sendable {
    public var processId: Int

    public init(
        processId: Int

    ) {
        self.processId = processId
    }
}

struct ShowParams: Codable, Hashable, Sendable {
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

func handleRequest(
    _ anyRequest: AnyJSONRPCRequest, data: Data, handler: @escaping JSONRPCEvent.RequestHandler
) async {
    do {
        switch anyRequest.method {
        case "Initialize":
            let params = try JSONDecoder().decode(JSONRPCRequest<InitializeParams>.self, from: data)
                .params!

            watchProcessExit(pid: params.processId) {
                print("Parent process terminated, shutting down...")
                DispatchQueue.main.async {
                    app.terminate(nil)
                }
            }

            await handler(.success(JSONValue.null))
        case "Show":

            let params = try JSONDecoder().decode(JSONRPCRequest<ShowParams>.self, from: data)
                .params!

            showHighlight(
                x: params.x,
                y: params.y,
                width: params.width,
                height: params.height,
                time: params.timeout
            )

            await handler(.success(JSONValue.null))
        case "Hide":
            closeHighlight()
            await handler(.success(JSONValue.null))
        default:
            await handler(
                .failure(
                    AnyJSONRPCResponseError(
                        code: JSONRPCErrors.methodNotFound, message: "Method not found")))

        }
    } catch {
        await handler(
            .failure(
                AnyJSONRPCResponseError(
                    code: JSONRPCErrors.internalError, message: error.localizedDescription)))
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
            let channel = DataChannel.stdioPipe().withMessageFraming()
            let session = JSONRPCSession(channel: channel)

            Task {
                let seq = await session.eventSequence

                for await event in seq {
                    switch event {
                    case let .request(request, handler, data):
                        await handleRequest(request, data: data, handler: handler)
                    case let .notification(notification, data):
                        await handleNotification(notification, data: data)
                    case let .error(error):
                        handleError(error)
                    }
                }
            }

        } else {
            showHighlight(
                x: arguments.x,
                y: arguments.y,
                width: arguments.width,
                height: arguments.height,
                time: arguments.timeout
            )
        }

        app.run()
    }
}
