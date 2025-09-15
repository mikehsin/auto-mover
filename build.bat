@echo off
echo Building Mouse Auto Mover...

REM Find the .NET Framework compiler
set CSC_PATH=""
for /d %%i in ("%WINDIR%\Microsoft.NET\Framework64\v*") do (
    if exist "%%i\csc.exe" set CSC_PATH="%%i\csc.exe"
)

if %CSC_PATH%=="" (
    echo .NET Framework compiler not found. Please install .NET Framework or .NET SDK.
    pause
    exit /b 1
)

REM Compile the application
%CSC_PATH% /target:winexe /out:MouseAutoMover.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll Program.cs MainForm.cs MainForm.Designer.cs

if %errorlevel%==0 (
    echo Build successful! You can run MouseAutoMover.exe
) else (
    echo Build failed!
)

pause
