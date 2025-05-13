// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

import Foundation

private let JSON_RPC_VERSION = "2.0"

enum JsonRpcErrorCode: Int, Codable {
    case parseError = -32700
    case invalidRequest = -32600
    case methodNotFound = -32601
    case invalidParams = -32602
    case internalError = -32603
}

protocol JsonRpcMessage: Codable {
    var jsonrpc: String { get }
}

extension JsonRpcMessage {
    var jsonrpc: String { JSON_RPC_VERSION }
}

struct JsonRpcRequest: JsonRpcMessage, Codable {
    let jsonrpc: String = JSON_RPC_VERSION
    let method: String
    let params: JSON?
    let id: JSON?

    enum CodingKeys: String, CodingKey {
        case jsonrpc, method, params, id
    }

    init(method: String, params: Encodable? = nil, id: Encodable) throws {
        self.method = method
        self.params = params != nil ? try JSON(from: params!) : nil
        self.id = try JSON(from: id)
    }
}

struct JsonRpcNotification: JsonRpcMessage, Codable {
    let jsonrpc: String = JSON_RPC_VERSION
    let method: String
    let params: JSON?

    enum CodingKeys: String, CodingKey {
        case jsonrpc, method, params
    }

    init(method: String, params: Encodable? = nil) throws {
        self.method = method
        self.params = params != nil ? try JSON(from: params!) : nil
    }
}

protocol JsonRpcResponseBase: JsonRpcMessage, Codable {}

struct JsonRpcResponse: JsonRpcResponseBase, Codable {
    let jsonrpc: String = JSON_RPC_VERSION
    let result: JSON?
    let id: JSON

    enum CodingKeys: String, CodingKey {
        case jsonrpc, result, id
    }
}

struct JsonRpcError: Codable, Error {
    let code: Int
    let message: String
    let data: JSON?

    init(code: Int, message: String, data: JSON? = nil) {
        self.code = code
        self.message = message
        self.data = data
    }
}

struct JsonRpcErrorResponse: JsonRpcResponseBase, Codable {
    let jsonrpc: String = JSON_RPC_VERSION
    let error: JsonRpcError
    let id: JSON?

    enum CodingKeys: String, CodingKey {
        case jsonrpc, error, id
    }
}

struct JSON: Codable, Equatable, Sendable {
    private let value: JSONValue

    init(from value: Encodable) throws {
        let data = try JSONEncoder().encode(value)
        self.value = try JSONDecoder().decode(JSONValue.self, from: data)
    }

    init(from value: Any) throws {
        if let encodableValue = value as? Encodable {
            let data = try JSONEncoder().encode(encodableValue)
            self.value = try JSONDecoder().decode(JSONValue.self, from: data)
        } else if let stringValue = value as? String {
            self.value = .string(stringValue)
        } else if let intValue = value as? Int {
            self.value = .int(intValue)
        } else if let doubleValue = value as? Double {
            self.value = .double(doubleValue)
        } else if let boolValue = value as? Bool {
            self.value = .bool(boolValue)
        } else if value is NSNull {
            self.value = .null
        } else if let arrayValue = value as? [Any] {
            self.value = .array(try arrayValue.map { try JSON(from: $0).value })
        } else if let dictValue = value as? [String: Any] {
            var dict: [String: JSONValue] = [:]
            for (key, val) in dictValue {
                dict[key] = try JSON(from: val).value
            }
            self.value = .dictionary(dict)
        } else {
            throw DecodingError.dataCorrupted(
                DecodingError.Context(
                    codingPath: [], debugDescription: "Unsupported type for JSON initialization"))
        }
    }

    init(from decoder: Decoder) throws {
        let container = try decoder.singleValueContainer()
        if container.decodeNil() {
            self.value = .null
        } else if let bool = try? container.decode(Bool.self) {
            self.value = .bool(bool)
        } else if let int = try? container.decode(Int.self) {
            self.value = .int(int)
        } else if let double = try? container.decode(Double.self) {
            self.value = .double(double)
        } else if let string = try? container.decode(String.self) {
            self.value = .string(string)
        } else if let array = try? container.decode([JSONValue].self) {
            self.value = .array(array)
        } else if let dict = try? container.decode([String: JSONValue].self) {
            self.value = .dictionary(dict)
        } else {
            throw DecodingError.dataCorruptedError(
                in: container,
                debugDescription: "Invalid JSON value"
            )
        }
    }

    func encode(to encoder: Encoder) throws {
        var container = encoder.singleValueContainer()

        switch value {
        case .null:
            try container.encodeNil()
        case .bool(let bool):
            try container.encode(bool)
        case .int(let int):
            try container.encode(int)
        case .double(let double):
            try container.encode(double)
        case .string(let string):
            try container.encode(string)
        case .array(let array):
            try container.encode(array)
        case .dictionary(let dict):
            try container.encode(dict)
        }
    }

    func decode<T: Decodable>() throws -> T {
        let data = try JSONEncoder().encode(value)
        return try JSONDecoder().decode(T.self, from: data)
    }

    static func == (lhs: JSON, rhs: JSON) -> Bool {
        lhs.value == rhs.value
    }
}

enum JSONValue: Codable, Equatable, Sendable {
    case null
    case bool(Bool)
    case int(Int)
    case double(Double)
    case string(String)
    case array([JSONValue])
    case dictionary([String: JSONValue])

    init(from decoder: Decoder) throws {
        let container = try decoder.singleValueContainer()
        if container.decodeNil() {
            self = .null
        } else if let bool = try? container.decode(Bool.self) {
            self = .bool(bool)
        } else if let int = try? container.decode(Int.self) {
            self = .int(int)
        } else if let double = try? container.decode(Double.self) {
            self = .double(double)
        } else if let string = try? container.decode(String.self) {
            self = .string(string)
        } else if let array = try? container.decode([JSONValue].self) {
            self = .array(array)
        } else if let dictionary = try? container.decode([String: JSONValue].self) {
            self = .dictionary(dictionary)
        } else {
            throw DecodingError.dataCorruptedError(
                in: container, debugDescription: "Invalid JSON value")
        }
    }

    func encode(to encoder: Encoder) throws {
        var container = encoder.singleValueContainer()
        switch self {
        case .null:
            try container.encodeNil()
        case .bool(let value):
            try container.encode(value)
        case .int(let value):
            try container.encode(value)
        case .double(let value):
            try container.encode(value)
        case .string(let value):
            try container.encode(value)
        case .array(let value):
            try container.encode(value)
        case .dictionary(let value):
            try container.encode(value)
        }
    }
}

extension String.Encoding {
    init?(ianaCharsetName: String) {
        switch ianaCharsetName.lowercased() {
        case "utf-8", "utf8":
            self = .utf8
        case "ascii":
            self = .ascii
        case "iso-8859-1", "latin1":
            self = .isoLatin1
        case "utf-16", "utf16":
            self = .utf16
        case "utf-16be", "utf16be":
            self = .utf16BigEndian
        case "utf-16le", "utf16le":
            self = .utf16LittleEndian
        default:
            return nil
        }
    }
}

actor JsonRpcPeer {
    private let reader: InputStream
    private let writer: OutputStream
    private var idCounter = 1
    private var pendingRequests: [String: CheckedContinuation<JsonRpcResponse, Error>] = [:]
    private var requestHandlers: [String: (JSON?, JSONDecoder) async throws -> any Sendable] = [:]
    private var notificationHandlers: [String: (JSON?, JSONDecoder) async throws -> Void] = [:]
    private var isRunning = false
    private var processingTask: Task<Void, Error>?

    private let jsonEncoder = JSONEncoder()
    private let jsonDecoder = JSONDecoder()

    private let defaultEncoding = String.Encoding.utf8
    private let defaultContentType = "application/vscode-jsonrpc"

    init(reader: InputStream, writer: OutputStream) {
        self.reader = reader
        self.writer = writer

        jsonEncoder.keyEncodingStrategy = .useDefaultKeys
        jsonDecoder.keyDecodingStrategy = .convertFromSnakeCase
    }

    func start() throws -> Task<Void, Error> {
        guard !isRunning else { return processingTask! }
        isRunning = true

        processingTask = Task {
            do {
                try await messageLoop()
            } catch {
                FileHandle.standardError.write("Error: \(error)\n".data(using: .utf8)!)
                throw error
            }
        }

        return processingTask!
    }

    func stop() {
        isRunning = false
        processingTask?.cancel()
        processingTask = nil
    }

    func registerRequestHandler<T: Encodable & Sendable>(
        method: String,
        handler: @escaping (JSON?, JSONDecoder) async throws -> T
    ) {
        guard !method.isEmpty else {
            preconditionFailure("Method name cannot be empty")
        }

        guard requestHandlers[method] == nil else {
            preconditionFailure("Request handler for method '\(method)' already exists")
        }

        requestHandlers[method] = handler
    }

    func registerNotificationHandler(
        method: String,
        handler: @escaping (JSON?, JSONDecoder) async throws -> Void
    ) {
        guard !method.isEmpty else {
            preconditionFailure("Method name cannot be empty")
        }

        guard notificationHandlers[method] == nil else {
            preconditionFailure("Notification handler for method '\(method)' already exists")
        }

        notificationHandlers[method] = handler
    }

    func sendNotification(method: String, params: Encodable? = nil) async throws {
        let notification = try JsonRpcNotification(method: method, params: params)
        let json = try jsonEncoder.encode(notification)
        try await sendMessage(json: String(data: json, encoding: .utf8)!)
    }

    func sendRequest<T: Decodable>(method: String, params: Encodable? = nil) async throws -> T {
        let id = String(nextId())
        let request = try JsonRpcRequest(method: method, params: params, id: id)

        return try await withCheckedThrowingContinuation {
            (continuation: CheckedContinuation<T, Error>) in
            Task {
                do {
                    let json = try jsonEncoder.encode(request)
                    pendingRequests[id] =
                        continuation as? CheckedContinuation<JsonRpcResponse, Error>

                    try await sendMessage(json: String(data: json, encoding: .utf8)!)
                } catch {
                    continuation.resume(throwing: error)
                    pendingRequests.removeValue(forKey: id)
                }
            }
        }
    }

    private func nextId() -> Int {
        defer { idCounter += 1 }
        return idCounter
    }

    private func sendMessage(json: String) async throws {
        let bodyData = json.appending("\r\n").data(using: defaultEncoding)!

        var header = "Content-Length: \(bodyData.count)\r\n"

        if defaultEncoding != .utf8 {
            let charsetName =
                CFStringConvertEncodingToIANACharSetName(
                    CFStringConvertNSStringEncodingToEncoding(defaultEncoding.rawValue)
                ) as String

            header.append("Content-Type: \(defaultContentType); charset=\(charsetName)\r\n")
        }

        header.append("\r\n")

        guard let headerData = header.data(using: .ascii) else {
            throw JsonRpcError(
                code: JsonRpcErrorCode.internalError.rawValue,
                message: "Failed to encode header"
            )
        }

        try writeData(headerData + bodyData)
    }

    private func writeData(_ data: Data) throws {
        var bytesRemaining = data.count
        var offset = 0

        while bytesRemaining > 0 {
            let bytesWritten = data.withUnsafeBytes { buffer in
                writer.write(
                    buffer.baseAddress!.advanced(by: offset).assumingMemoryBound(to: UInt8.self),
                    maxLength: bytesRemaining)
            }

            if bytesWritten <= 0 {
                throw JsonRpcError(
                    code: JsonRpcErrorCode.internalError.rawValue,
                    message: "Failed to write to stream"
                )
            }

            bytesRemaining -= bytesWritten
            offset += bytesWritten
        }
    }

    private func readMessage() async throws -> String? {
        var contentLength = 0
        var hasContentLength = false
        var encoding = String.Encoding.utf8

        var headerBuffer = Data()
        let headerComplete = false
        let bufferSize = 4096
        var buffer = [UInt8](repeating: 0, count: bufferSize)

        while !headerComplete && isRunning {
            let bytesRead = reader.read(&buffer, maxLength: bufferSize)

            if bytesRead <= 0 {
                if bytesRead == 0 {
                    // Stream has been closed
                    isRunning = false
                    return nil
                }
                throw JsonRpcError(
                    code: JsonRpcErrorCode.internalError.rawValue,
                    message: "Failed to read from stream"
                )
            }

            headerBuffer.append(buffer, count: bytesRead)

            if let range = headerBuffer.range(of: Data([13, 10, 13, 10])) {
                let headersData = headerBuffer.subdata(in: 0..<range.lowerBound)
                let headersString = String(data: headersData, encoding: .ascii) ?? ""

                for line in headersString.components(separatedBy: "\r\n") {
                    if let colonIndex = line.firstIndex(of: ":") {
                        let name = line[..<colonIndex].trimmingCharacters(in: .whitespaces)
                            .lowercased()
                        let value = line[line.index(after: colonIndex)...].trimmingCharacters(
                            in: .whitespaces)

                        if name == "content-length" {
                            if let length = Int(value) {
                                contentLength = length
                                hasContentLength = true
                            }
                        } else if name == "content-type" {
                            for part in value.components(separatedBy: ";") {
                                let trimmedPart = part.trimmingCharacters(in: .whitespaces)
                                if trimmedPart.lowercased().hasPrefix("charset=") {
                                    let charset = String(trimmedPart.dropFirst(8))
                                    if let enc = String.Encoding(ianaCharsetName: charset) {
                                        encoding = enc
                                    }
                                }
                            }
                        }
                    }
                }

                if !hasContentLength {
                    throw JsonRpcError(
                        code: JsonRpcErrorCode.invalidRequest.rawValue,
                        message: "Content-Length header missing"
                    )
                }

                let remainingData = headerBuffer.subdata(in: range.upperBound..<headerBuffer.count)

                var contentData = remainingData
                let bytesNeeded = contentLength - contentData.count

                if bytesNeeded > 0 {
                    var contentBuffer = [UInt8](repeating: 0, count: bytesNeeded)
                    var totalBytesRead = 0

                    while totalBytesRead < bytesNeeded && isRunning {
                        let bytesRead = reader.read(
                            &contentBuffer[totalBytesRead],
                            maxLength: bytesNeeded - totalBytesRead)

                        if bytesRead <= 0 {
                            // Stream has been closed during content reading
                            isRunning = false
                            return nil
                        }

                        totalBytesRead += bytesRead
                    }

                    contentData.append(contentBuffer, count: totalBytesRead)
                }

                return String(data: contentData, encoding: encoding)
            }
        }

        return nil
    }

    private func messageLoop() async throws {
        while isRunning {
            if let json = try await readMessage() {
                try await handleIncomingMessage(json: json)
            }
        }
    }

    private func handleIncomingMessage(json: String) async throws {
        guard let jsonData = json.data(using: .utf8) else {
            throw JsonRpcError(
                code: JsonRpcErrorCode.parseError.rawValue,
                message: "Failed to encode message as UTF-8"
            )
        }

        do {
            let jsonElement = try JSONSerialization.jsonObject(with: jsonData)

            if let jsonArray = jsonElement as? [[String: Any]] {
                var responses = [JsonRpcResponseBase]()

                for element in jsonArray {
                    if let response = try await processSingleMessage(jsonObject: element) {
                        responses.append(response)
                    }
                }

                if !responses.isEmpty {
                    let serializedResponses = try responses.map { response in
                        let responseData = try jsonEncoder.encode(response)
                        return try JSONSerialization.jsonObject(with: responseData)
                    }

                    let responseData = try JSONSerialization.data(
                        withJSONObject: serializedResponses)
                    try await sendMessage(json: String(data: responseData, encoding: .utf8)!)
                }
            } else if let jsonObject = jsonElement as? [String: Any] {
                if let response = try await processSingleMessage(jsonObject: jsonObject) {
                    let responseData = try jsonEncoder.encode(response)
                    try await sendMessage(json: String(data: responseData, encoding: .utf8)!)
                }
            }
        } catch {
            let errorResponse = JsonRpcErrorResponse(
                error: JsonRpcError(
                    code: JsonRpcErrorCode.parseError.rawValue,
                    message: error.localizedDescription,
                    data: try? JSON(from: error as! Encodable)
                ),
                id: nil
            )

            let errorData = try jsonEncoder.encode(errorResponse)
            try await sendMessage(json: String(data: errorData, encoding: .utf8)!)
        }
    }

    private func processRequest(method: String, params: JSON?, id: JSON) async throws
        -> JsonRpcResponseBase
    {
        if let handler = requestHandlers[method] {
            do {
                let result = try await handler(params, jsonDecoder)

                let resultValue: JSON?
                if let encodableResult = result as? Encodable {
                    resultValue = try JSON(from: encodableResult)
                } else {
                    throw JsonRpcError(
                        code: JsonRpcErrorCode.internalError.rawValue,
                        message: "Result is not encodable",
                        data: nil
                    )
                }

                return JsonRpcResponse(result: resultValue, id: id)
            } catch let error as JsonRpcError {
                return JsonRpcErrorResponse(error: error, id: id)
            } catch {
                return JsonRpcErrorResponse(
                    error: JsonRpcError(
                        code: JsonRpcErrorCode.internalError.rawValue,
                        message: error.localizedDescription
                    ),
                    id: id
                )
            }
        } else {
            return JsonRpcErrorResponse(
                error: JsonRpcError(
                    code: JsonRpcErrorCode.methodNotFound.rawValue,
                    message: "Method '\(method)' not found"
                ),
                id: id
            )
        }
    }

    private func processSingleMessage(jsonObject: [String: Any]) async throws
        -> JsonRpcResponseBase?
    {
        // Determine if this is a response (has id, no method)
        let isResponse = jsonObject["id"] != nil && jsonObject["method"] == nil

        // Check JSON-RPC version, but be tolerant for responses
        if let jsonrpc = jsonObject["jsonrpc"] as? String {
            // Only enforce version check for requests/notifications, not for responses
            if jsonrpc != JSON_RPC_VERSION && !isResponse {
                return JsonRpcErrorResponse(
                    error: JsonRpcError(
                        code: JsonRpcErrorCode.invalidRequest.rawValue,
                        message: "Unsupported JSON-RPC version: \(jsonrpc), expected \(JSON_RPC_VERSION)"
                    ),
                    id: try? JSON(from: jsonObject["id"] ?? NSNull())
                )
            }
        } else if !isResponse {
            // Only require jsonrpc property for requests/notifications
            return JsonRpcErrorResponse(
                error: JsonRpcError(
                    code: JsonRpcErrorCode.invalidRequest.rawValue,
                    message: "Missing 'jsonrpc' field, expected version \(JSON_RPC_VERSION)"
                ),
                id: try? JSON(from: jsonObject["id"] ?? NSNull())
            )
        }

        if let idValue = jsonObject["id"], !(jsonObject["method"] is String) {
            let id = try JSON(from: idValue)

            if let continuation = pendingRequests.removeValue(forKey: idValue as? String ?? "") {
                if let errorDict = jsonObject["error"] as? [String: Any] {
                    let code = errorDict["code"] as? Int ?? JsonRpcErrorCode.internalError.rawValue
                    let message = errorDict["message"] as? String ?? "Unknown error"
                    let error = JsonRpcError(code: code, message: message, data: nil)

                    continuation.resume(throwing: error)
                } else if let result = jsonObject["result"] {
                    let resultData = try JSONSerialization.data(withJSONObject: result)
                    let response = JsonRpcResponse(
                        result: try? JSONDecoder().decode(JSON.self, from: resultData),
                        id: id
                    )
                    continuation.resume(returning: response)
                } else {
                    continuation.resume(
                        throwing: JsonRpcError(
                            code: JsonRpcErrorCode.invalidRequest.rawValue,
                            message: "Invalid response format",
                            data: nil
                        ))
                }
                return nil
            }
        }

        if let method = jsonObject["method"] as? String {
            let paramsData =
                jsonObject["params"] != nil
                ? try JSONSerialization.data(withJSONObject: jsonObject["params"]!) : nil
            let params =
                paramsData != nil ? try? JSONDecoder().decode(JSON.self, from: paramsData!) : nil

            if let idValue = jsonObject["id"] {
                let id = try JSON(from: idValue)
                return try await processRequest(method: method, params: params, id: id)
            } else {
                try await processNotification(method: method, params: params)
                return nil
            }
        }

        return nil
    }

    private func processNotification(method: String, params: JSON?) async throws {
        if let handler = notificationHandlers[method] {
            try await handler(params, jsonDecoder)
        } else {
            throw JsonRpcError(
                code: JsonRpcErrorCode.methodNotFound.rawValue,
                message: "Method '\(method)' not found"
            )
        }
    }
}
