#!/bin/bash
# filepath: /Users/daniel/Develop/robot/robotframework-PlatynUI/src/PlatynUI.Server/send_json.sh

# Prüfe ob ein Argument übergeben wurde
if [ $# -lt 1 ]; then
    echo "Verwendung: $0 'JSON-String'"
    echo "Beispiel: $0 '{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[5,3],\"id\":1}'"
    exit 1
fi

# JSON-String vom ersten Argument
json_request="$1"

# Berechne die Länge des JSON-Strings OHNE die Zeilenumbrüche am Ende
content_length=${#json_request}

# Pfad zum Server
SERVER_PATH="dotnet run"

# Debug-Information
echo "Sende JSON mit Länge $content_length: $json_request" >&2
echo "" >&2


{
    #for i in {1..10000}; do
    printf "Content-Length: %d\r\n" "$content_length"
    printf "Content-Type: application/vscode-jsonrpc; charset=utf-8\r\n"
    printf "\r\n"  # Leerzeile als Trennung zwischen Header und Body
    printf "%s" "$json_request"  # JSON ohne extra Zeilenumbruch
    #done
} | $SERVER_PATH
