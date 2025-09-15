using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MouseAutoMover.Shared
{
    /// <summary>
    /// macOS implementation of mouse movement using Core Graphics
    /// </summary>
    public class MacOSMouseMover : IMouseMover
    {
        [DllImport("ApplicationServices.framework/ApplicationServices")]
        private static extern void CGWarpMouseCursorPosition(CGPoint newCursorPosition);

        [DllImport("ApplicationServices.framework/ApplicationServices")]
        private static extern CGPoint CGEventGetLocation(IntPtr eventRef);

        [DllImport("ApplicationServices.framework/ApplicationServices")]
        private static extern IntPtr CGEventCreate(IntPtr source);

        [DllImport("ApplicationServices.framework/ApplicationServices")]
        private static extern void CFRelease(IntPtr cf);

        [StructLayout(LayoutKind.Sequential)]
        private struct CGPoint
        {
            public double x;
            public double y;

            public CGPoint(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public bool IsSupported => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public Point GetCursorPosition()
        {
            if (!IsSupported)
                return Point.Empty;

            try
            {
                // Alternative approach using system_profiler if P/Invoke fails
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python3",
                        Arguments = "-c \"import Quartz; print(f'{int(Quartz.NSEvent.mouseLocation().x)},{int(Quartz.NSEvent.mouseLocation().y)}')\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                
                if (!string.IsNullOrEmpty(output))
                {
                    var parts = output.Trim().Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
                    {
                        return new Point(x, y);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get cursor position: {ex.Message}");
            }

            return Point.Empty;
        }

        public bool SetCursorPosition(int x, int y)
        {
            if (!IsSupported)
                return false;

            try
            {
                // Use Python with PyObjC for reliable mouse movement
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python3",
                        Arguments = $"-c \"import Quartz; Quartz.CGWarpMouseCursorPosition(({x}, {y}))\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set cursor position: {ex.Message}");
                return false;
            }
        }
    }
}
