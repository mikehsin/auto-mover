# Mouse Auto Mover

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
