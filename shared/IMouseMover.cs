using System.Drawing;

namespace MouseAutoMover.Shared
{
    /// <summary>
    /// Interface for platform-specific mouse movement implementations
    /// </summary>
    public interface IMouseMover
    {
        /// <summary>
        /// Get the current cursor position
        /// </summary>
        Point GetCursorPosition();
        
        /// <summary>
        /// Set the cursor position to the specified coordinates
        /// </summary>
        bool SetCursorPosition(int x, int y);
        
        /// <summary>
        /// Check if mouse movement is supported on this platform
        /// </summary>
        bool IsSupported { get; }
    }
}
