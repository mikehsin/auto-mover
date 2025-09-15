using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutoMover.Shared
{
    /// <summary>
    /// Core mouse auto-mover logic that works across platforms
    /// </summary>
    public class MouseAutoMoverCore
    {
        private readonly IMouseMover _mouseMover;
        private readonly Random _random;
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _isRunning;

        public event EventHandler<MouseMoveEventArgs>? MouseMoved;
        public event EventHandler<string>? StatusChanged;

        public MouseAutoMoverCore(IMouseMover mouseMover)
        {
            _mouseMover = mouseMover ?? throw new ArgumentNullException(nameof(mouseMover));
            _random = new Random();
        }

        public bool IsRunning => _isRunning;

        public async Task StartAsync(int intervalSeconds = 30, int movementPixels = 1)
        {
            if (_isRunning)
                return;

            if (!_mouseMover.IsSupported)
            {
                StatusChanged?.Invoke(this, "Mouse movement not supported on this platform");
                return;
            }

            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
            StatusChanged?.Invoke(this, "Mouse auto-mover started");

            try
            {
                await RunMovementLoopAsync(intervalSeconds, movementPixels, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Expected when cancelled
            }
            finally
            {
                _isRunning = false;
                StatusChanged?.Invoke(this, "Mouse auto-mover stopped");
            }
        }

        public void Stop()
        {
            if (!_isRunning)
                return;

            _cancellationTokenSource?.Cancel();
        }

        private async Task RunMovementLoopAsync(int intervalSeconds, int movementPixels, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(intervalSeconds * 1000, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    break;

                var currentPos = _mouseMover.GetCursorPosition();
                
                // Generate small random movement
                int deltaX = _random.Next(-movementPixels, movementPixels + 1);
                int deltaY = _random.Next(-movementPixels, movementPixels + 1);
                
                // Ensure we actually move at least 1 pixel
                if (deltaX == 0 && deltaY == 0)
                {
                    deltaX = _random.Next(0, 2) == 0 ? -1 : 1;
                }

                int newX = currentPos.X + deltaX;
                int newY = currentPos.Y + deltaY;

                // Keep within reasonable bounds (basic screen boundary check)
                newX = Math.Max(0, Math.Min(2560, newX)); // Assume max 2560 width
                newY = Math.Max(0, Math.Min(1440, newY)); // Assume max 1440 height

                bool success = _mouseMover.SetCursorPosition(newX, newY);
                
                if (success)
                {
                    MouseMoved?.Invoke(this, new MouseMoveEventArgs(currentPos, new Point(newX, newY)));
                }
            }
        }
    }

    public class MouseMoveEventArgs : EventArgs
    {
        public Point PreviousPosition { get; }
        public Point NewPosition { get; }

        public MouseMoveEventArgs(Point previousPosition, Point newPosition)
        {
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
        }
    }
}
