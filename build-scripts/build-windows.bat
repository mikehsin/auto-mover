@echo off
echo === Building Mouse Auto Mover for Windows ===

REM Check if .NET is installed
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not installed.
    echo Please install .NET SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

REM Check .NET version
echo Checking .NET version...
dotnet --version

REM Navigate to Windows project directory
cd /d "%~dp0\..\windows"

echo Building Windows version...
dotnet build -c Release

if %errorlevel%==0 (
    echo.
    echo ✅ Build successful!
    echo.
    echo To run the application:
    echo   cd windows
    echo   dotnet run
    echo.
    echo Or run the built executable:
    echo   cd windows\bin\Release\net8.0-windows
    echo   MouseAutoMover.exe
) else (
    echo ❌ Build failed!
)

pause
