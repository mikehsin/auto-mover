@echo off
echo Building Mouse Auto Mover (Single File Version)...

REM Find the .NET Framework compiler (try both 64-bit and 32-bit)
set CSC_PATH=""

REM Try .NET Framework 4.x first (64-bit)
for /d %%i in ("%WINDIR%\Microsoft.NET\Framework64\v4*") do (
    if exist "%%i\csc.exe" set CSC_PATH="%%i\csc.exe"
)

REM If not found, try 32-bit
if %CSC_PATH%=="" (
    for /d %%i in ("%WINDIR%\Microsoft.NET\Framework\v4*") do (
        if exist "%%i\csc.exe" set CSC_PATH="%%i\csc.exe"
    )
)

REM Try older versions if needed
if %CSC_PATH%=="" (
    for /d %%i in ("%WINDIR%\Microsoft.NET\Framework64\v3*") do (
        if exist "%%i\csc.exe" set CSC_PATH="%%i\csc.exe"
    )
)

if %CSC_PATH%=="" (
    echo .NET Framework compiler not found. 
    echo Please make sure .NET Framework is installed on your system.
    echo You can download it from: https://dotnet.microsoft.com/download/dotnet-framework
    pause
    exit /b 1
)

echo Using compiler: %CSC_PATH%

REM Compile the single file application
%CSC_PATH% /target:winexe /out:MouseAutoMover.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll MouseAutoMover_SingleFile.cs

if %errorlevel%==0 (
    echo.
    echo ==========================================
    echo Build successful! 
    echo You can now run: MouseAutoMover.exe
    echo ==========================================
    echo.
    if exist MouseAutoMover.exe (
        echo File created: MouseAutoMover.exe
        dir MouseAutoMover.exe
    )
) else (
    echo Build failed! Error code: %errorlevel%
)

pause
