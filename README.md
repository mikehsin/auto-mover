# Mouse Auto Mover

A cross-platform application that automatically moves your mouse cursor slightly in a certain period of time to allow your computer not going to sleep or showing as away.

## 🖥️ Platform Support

- **Windows**: GUI application using Windows Forms
- **macOS**: Console application with interactive commands

## 🚀 **Download & Installation**

### **Option 1: Direct Download from Repository** (Easiest)

| Platform | Download Link | Size | Instructions |
|----------|---------------|------|--------------|
| **Windows** | [`MouseAutoMover.exe`](./dist/windows/MouseAutoMover.exe) | ~45MB | Double-click to run |
| **macOS** | [`MouseAutoMover.MacOS`](./dist/macos/MouseAutoMover.MacOS) | ~47MB | Run in Terminal |

*👆 Right-click and "Save Link As" to download*

### **Option 2: Download from GitHub Releases**

1. Go to [Releases](../../releases)
2. Download the latest version:
   - **Windows**: `MouseAutoMover-Windows.zip`
   - **macOS**: `MouseAutoMover-macOS.tar.gz`

### **Option 3: GitHub Actions Artifacts**

1. Go to [Actions](../../actions)
2. Click on the latest successful build
3. Download artifacts for your platform

## 📱 **How to Run**

### **Windows Users** ✅ **Simple Double-Click**

1. **Extract** the ZIP file
2. **Double-click** `MouseAutoMover.exe`
3. **GUI interface** will open with Start/Stop buttons
4. **No additional setup** required!

### **macOS Users** ⚠️ **Terminal Required**

1. **Extract** the TAR.GZ file
2. **Open Terminal** in the extracted folder
3. **Make executable**: `chmod +x MouseAutoMover.MacOS`
4. **Run**: `./MouseAutoMover.MacOS`
5. **Grant permissions** when macOS prompts for:
   - Accessibility permissions (for mouse control)
   - Allow app from unidentified developer
6. **Use commands**: `start`, `stop`, `status`, `help`, `quit`

### **Why Terminal for macOS?**

- **Security**: macOS Gatekeeper blocks unsigned apps from running via double-click
- **Permissions**: App needs accessibility permissions to control mouse
- **Console Interface**: Text-based commands instead of GUI

## 🎯 **What This Application Does**

- Automatically moves your mouse cursor by **small amounts** (1-2 pixels) at regular intervals
- Prevents your computer from going to sleep or showing as "away" in messaging apps
- Useful for maintaining active status during remote work or presentations
- **Default interval**: 30 seconds
- **Movement**: Barely noticeable random movements

## 🔒 **Security & Privacy**

- ✅ **No internet connection** required or used
- ✅ **No data collection** - works entirely offline
- ✅ **Open source** - all code is visible in this repository
- ✅ **Self-contained** - includes all necessary runtime files
- ⚠️ **Requires mouse control permissions** on macOS

## 📦 **Technical Details**

- **Framework**: .NET 8 (cross-platform)
- **Windows**: Windows Forms GUI with Win32 API mouse control
- **macOS**: Console application with native mouse control via Python/PyObjC
- **File Size**: ~45MB per platform (includes .NET runtime)
- **Dependencies**: None (self-contained executables)

## 📋 **Troubleshooting**

### **Windows**

- **Windows Defender warning**: Click "More info" → "Run anyway"
- **Mouse doesn't move**: Check Windows privacy settings for mouse control
- **App won't start**: Ensure Windows 10 or later

### **macOS**

- **"Cannot be opened"**: Run from Terminal with `./MouseAutoMover.MacOS`
- **Permission denied**: Run `chmod +x MouseAutoMover.MacOS` first
- **Mouse not moving**: Grant Accessibility permissions in System Preferences > Security & Privacy > Accessibility
- **Command not found**: Make sure you're in the correct directory with the executable

## 🛠️ **For Developers - Build from Source**

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Git

### Clone and Build

```bash
# Clone repository
git clone https://github.com/mikehsin/auto-mover.git
cd auto-mover

# Build Windows version
cd windows
dotnet publish -c Release -r win-x64 --self-contained

# Build macOS version
cd ../macos
dotnet publish -c Release -r osx-x64 --self-contained
```

### Project Structure

```
auto-mover/
├── windows/          # Windows GUI application
├── macos/           # macOS console application
├── shared/          # Cross-platform core logic
└── build-scripts/   # Platform build scripts
```

## 📄 **License**

MIT License - see [LICENSE](LICENSE) file for details.

## ⚠️ **Disclaimer**

This tool is intended for legitimate use cases such as preventing your computer from sleeping during presentations or maintaining active status during remote work. Please use responsibly and in accordance with your organization's policies.

---

**Need help?** [Open an issue](../../issues) or check the troubleshooting section above.
