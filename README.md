# Mouse Auto Mover

A cross-platform application that automatically moves your mouse cursor slightly in a certain period of time to allow your computer not going to sleep or showing as away.

## 🖥️ Platform Support

- **Windows**: GUI application using Windows Forms
- **macOS**: Console application with interactive commands

## � **Quick Download & Run**

| Platform    | Download Link                                                   | Type                |
| ----------- | --------------------------------------------------------------- | ------------------- |
| **Windows** | [`MouseAutoMover.exe`](./releases/windows/MouseAutoMover.exe)   | GUI Application     |
| **macOS**   | [`MouseAutoMover.MacOS`](./releases/macos/MouseAutoMover.MacOS) | Console Application |

_Ready-to-run executables - no installation required!_

## �📁 Project Structure

```
auto-mover/
├── windows/                    # Windows-specific implementation
│   ├── Program.cs             # Windows Forms entry point
│   ├── MainForm.cs            # GUI interface
│   ├── MainForm.Designer.cs   # GUI design
│   ├── MouseAutoMover.csproj  # Windows project file
│   ├── build.bat              # Legacy batch build script
│   └── build_simple.bat       # Simple batch build script
├── macos/                     # macOS-specific implementation
│   ├── Program.cs             # Console application entry point
│   └── MouseAutoMover.MacOS.csproj  # macOS project file
├── shared/                    # Cross-platform core logic
│   ├── IMouseMover.cs         # Platform abstraction interface
│   ├── MouseAutoMoverCore.cs  # Core movement logic
│   ├── WindowsMouseMover.cs   # Windows mouse implementation
│   └── MacOSMouseMover.cs     # macOS mouse implementation
└── build-scripts/             # Build scripts for both platforms
    ├── build-windows.bat      # Windows build script
    └── build-macos.sh         # macOS build script
```

## 🚀 Quick Start

### Windows

1. **Prerequisites**: Install [.NET 8 SDK](https://dotnet.microsoft.com/download)
2. **Build**: Run `build-scripts/build-windows.bat`
3. **Run**:
   ```bash
   cd windows
   dotnet run
   ```

### macOS

1. **Prerequisites**:
   - Install [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - Install Python 3 with PyObjC: `pip3 install pyobjc`
2. **Build**: Run `./build-scripts/build-macos.sh`
3. **Run**:
   ```bash
   cd macos
   dotnet run
   ```
4. **Grant Permissions**: When prompted, grant accessibility permissions in System Preferences > Security & Privacy > Accessibility

## 🎮 Usage

### Windows (GUI)

- Launch the application to see a simple Windows Forms interface
- Click "Start" to begin automatic mouse movement
- Click "Cancel" to stop
- The application will move your mouse by 1 pixel every 30 seconds

### macOS (Console)

- Launch the application in Terminal
- Available commands:
  - `start` - Start the auto-mover
  - `stop` - Stop the auto-mover
  - `status` - Show current status
  - `help` - Show available commands
  - `quit` - Exit the application
- Press Ctrl+C to exit gracefully

## 🔧 Building from Source

### Windows

```bash
# Using modern .NET CLI (recommended)
cd windows
dotnet build -c Release
dotnet run

# Using legacy batch script
build-scripts/build-windows.bat
```

### macOS

```bash
# Using shell script
./build-scripts/build-macos.sh

# Using .NET CLI directly
cd macos
dotnet build -c Release
dotnet run
```

## ⚙️ Configuration

The default settings are:

- **Movement interval**: 30 seconds
- **Movement distance**: 1 pixel (random direction)
- **Boundary limits**: 2560x1440 (can be adjusted in code)

To modify these settings, edit the values in the respective `Program.cs` files.

## 🔒 Permissions

### Windows

- No special permissions required
- Uses standard Win32 API calls

### macOS

- Requires **Accessibility permissions**
- Uses Python with PyObjC for reliable mouse control
- System will prompt for permissions on first run

## 🛠️ Technical Details

### Architecture

- **Shared Core**: Common logic in `MouseAutoMoverCore.cs`
- **Platform Abstraction**: `IMouseMover` interface
- **Platform Implementations**: Separate classes for Windows and macOS
- **Async/Await**: Non-blocking mouse movement with cancellation support

### Dependencies

- **.NET 8**: Cross-platform runtime
- **System.Drawing.Common**: For Point structures
- **Windows**: Win32 API (user32.dll)
- **macOS**: Python 3 with PyObjC for Quartz framework access

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test on both platforms (if possible)
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

This tool is intended for legitimate use cases such as preventing your computer from sleeping during presentations or maintaining active status during remote work. Please use responsibly and in accordance with your organization's policies.

A simple Windows application that automatically moves your mouse cursor at user-defined intervals to keep your computer active and prevent it from going to sleep or showing as inactive.

## Features

- 🖱️ **Automatic Mouse Movement**: Moves the mouse cursor slightly (1-3 pixels) in random directions
- ⏱️ **Customizable Interval**: Set any interval from 1 second to hours between movements
- 🎮 **Easy Controls**: Simple Start and Cancel buttons
- 🔧 **Settings Icon**: Clean interface with a gear/wrench icon
- 📊 **Status Display**: Shows current status and last movement time
- 🛡️ **Safe Operation**: Keeps mouse movements within screen boundaries
- ⌨️ **Keyboard Shortcut**: Press Enter in the interval field to start quickly

## Screenshots

The application features a clean, user-friendly interface with:

- Movement interval input field (default: 30 seconds)
- Green "Start" button to begin auto-movement
- Red "Cancel" button to stop the process
- Status indicator showing current state
- Settings gear icon for visual appeal

## Installation

### Option 1: Use the Pre-built Executable

1. Download or locate `MouseAutoMover.exe`
2. Double-click to run (no installation required)

### Option 2: Build from Source

1. Ensure you have .NET Framework 4.0 or later installed
2. Run the build script:
   ```batch
   build_simple.bat
   ```
3. The executable `MouseAutoMover.exe` will be created

## Usage

1. **Launch the Application**: Double-click `MouseAutoMover.exe`
2. **Set Interval**: Enter the desired number of seconds between mouse movements
3. **Start**: Click the "Start" button or press Enter
4. **Monitor**: Watch the status display for confirmation
5. **Stop**: Click the "Cancel" button when you want to stop

### Example Use Cases

- Prevent computer from going to sleep during presentations
- Keep status as "active" during work-from-home sessions
- Maintain connection to remote desktop sessions
- Prevent screensavers from activating during long processes

## Technical Details

### System Requirements

- Windows operating system
- .NET Framework 4.0 or later
- No additional dependencies required

### How It Works

- Uses Windows API (`user32.dll`) for precise cursor control
- Generates random small movements (1-3 pixels)
- Respects screen boundaries to prevent cursor loss
- Lightweight application (< 11KB executable)

### Files in Project

```
MouseAutoMover/
├── MouseAutoMover.exe           # Main executable (ready to run)
├── MouseAutoMover_SingleFile.cs # Complete source code
├── build_simple.bat            # Build script for compilation
├── README.md                    # This documentation
├── MouseAutoMover.csproj        # .NET 6 project file (optional)
├── MainForm.cs                  # Form logic (separated version)
├── MainForm.Designer.cs         # Form designer (separated version)
└── Program.cs                   # Entry point (separated version)
```

## Development

### Building from Source

The project includes two versions:

1. **Single File Version** (`MouseAutoMover_SingleFile.cs`):

   - All code in one file
   - Compatible with older .NET Framework compilers
   - Use `build_simple.bat` to compile

2. **Separated Version** (Multiple .cs files):
   - Standard Visual Studio project structure
   - Requires .NET 6 SDK or Visual Studio
   - Use `dotnet build` or Visual Studio

### Code Structure

- **Mouse Movement**: Uses `SetCursorPos` and `GetCursorPos` from Windows API
- **Timer Control**: Windows Forms Timer for interval management
- **Random Movement**: Random class generates small pixel offsets
- **UI Components**: Windows Forms with custom settings icon

## Safety and Privacy

- **No Network Activity**: Application works entirely offline
- **No Data Collection**: No user data is stored or transmitted
- **Minimal Resource Usage**: Very low CPU and memory footprint
- **Reversible**: Stop anytime with the Cancel button
- **Non-Intrusive**: Mouse movements are barely noticeable (1-3 pixels)

## Troubleshooting

### Common Issues

**Application Won't Start**

- Ensure .NET Framework 4.0+ is installed
- Try running as administrator
- Check Windows Defender/antivirus settings

**Mouse Not Moving**

- Verify the interval is set to a positive number
- Check that "Start" button was clicked
- Ensure application has focus and isn't minimized

**Build Errors**

- Install .NET Framework Developer Pack
- Use `build_simple.bat` for older systems
- Check that Windows SDK is available

### Error Messages

- **"Invalid Input"**: Enter a positive number for interval
- **"Compiler not found"**: Install .NET Framework
- **"Access denied"**: Run with administrator privileges

## License

This project is provided as-is for personal and educational use. Feel free to modify and distribute according to your needs.

## Contributing

Contributions are welcome! Feel free to:

- Report bugs or issues
- Suggest new features
- Submit pull requests
- Improve documentation

## Version History

- **v1.0** (2025-09-15): Initial release
  - Basic mouse auto-movement functionality
  - Customizable intervals
  - Clean Windows Forms interface
  - Single executable distribution

## Support

For issues or questions:

1. Check the troubleshooting section above
2. Verify your system meets the requirements
3. Try rebuilding from source if using the executable fails

---

**Note**: This application is designed for legitimate productivity purposes. Please use responsibly and in accordance with your organization's policies.
