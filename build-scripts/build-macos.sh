#!/bin/bash

echo "=== Building Mouse Auto Mover for macOS ==="

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed."
    echo "Please install .NET SDK from: https://dotnet.microsoft.com/download"
    exit 1
fi

# Check .NET version
echo "Checking .NET version..."
dotnet --version

# Navigate to macOS project directory
cd "$(dirname "$0")/../macos" || exit 1

echo "Building macOS version..."
dotnet build -c Release

if [ $? -eq 0 ]; then
    echo ""
    echo "✅ Build successful!"
    echo ""
    echo "To run the application:"
    echo "  cd macos"
    echo "  dotnet run"
    echo ""
    echo "Or run the built executable:"
    echo "  cd macos/bin/Release/net8.0"
    echo "  ./MouseAutoMover.MacOS"
else
    echo "❌ Build failed!"
    exit 1
fi
