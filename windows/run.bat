@echo off
echo === Mouse Auto Mover for Windows ===
echo Starting application...
echo.

REM Navigate to Windows project directory
cd /d "%~dp0"

REM Check if .NET is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not installed.
    echo Please install .NET SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Run the application
dotnet run
