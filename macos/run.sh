#!/bin/bash

echo "=== Mouse Auto Mover for macOS ==="
echo "Starting application..."
echo ""

# Navigate to macOS project directory
cd "$(dirname "$0")" || exit 1

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed."
    echo "Please install .NET SDK from: https://dotnet.microsoft.com/download"
    exit 1
fi

# Run the application
dotnet run
