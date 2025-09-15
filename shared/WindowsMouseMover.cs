using System.Drawing;
using System.Runtime.InteropServices;

namespace MouseAutoMover.Shared
{
    /// <summary>
    /// Windows implementation of mouse movement using Win32 API
    /// </summary>
    public class WindowsMouseMover : IMouseMover
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        public bool IsSupported => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public Point GetCursorPosition()
        {
            if (!IsSupported)
                return Point.Empty;

            GetCursorPos(out Point point);
            return point;
        }

        public bool SetCursorPosition(int x, int y)
        {
            if (!IsSupported)
                return false;

            return SetCursorPos(x, y);
        }
    }
}
