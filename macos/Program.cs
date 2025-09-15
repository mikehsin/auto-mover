using System;
using System.Threading.Tasks;
using MouseAutoMover.Shared;

namespace MouseAutoMover.MacOS
{
    internal static class Program
    {
        private static MouseAutoMoverCore? _autoMover;
        private static bool _isRunning = false;

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Mouse Auto Mover for macOS ===");
            Console.WriteLine("This application will move your mouse cursor slightly every 30 seconds");
            Console.WriteLine("to prevent your computer from going to sleep or showing as away.\n");

            // Check if we have accessibility permissions
            Console.WriteLine("Note: This app requires accessibility permissions on macOS.");
            Console.WriteLine("If prompted, please grant accessibility permissions in System Preferences > Security & Privacy > Accessibility\n");

            var mouseMover = new MacOSMouseMover();
            if (!mouseMover.IsSupported)
            {
                Console.WriteLine("Error: This application only works on macOS.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            _autoMover = new MouseAutoMoverCore(mouseMover);
            _autoMover.StatusChanged += OnStatusChanged;
            _autoMover.MouseMoved += OnMouseMoved;

            Console.WriteLine("Commands:");
            Console.WriteLine("  start - Start the auto-mover");
            Console.WriteLine("  stop  - Stop the auto-mover");
            Console.WriteLine("  quit  - Exit the application");
            Console.WriteLine("  help  - Show this help");
            Console.WriteLine();

            // Handle Ctrl+C gracefully
            Console.CancelKeyPress += async (sender, e) =>
            {
                e.Cancel = true;
                Console.WriteLine("\nShutting down...");
                if (_autoMover != null && _autoMover.IsRunning)
                {
                    _autoMover.Stop();
                    await Task.Delay(1000); // Give it time to stop
                }
                Environment.Exit(0);
            };

            await RunInteractiveLoop();
        }

        private static async Task RunInteractiveLoop()
        {
            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                switch (input)
                {
                    case "start":
                        if (_autoMover!.IsRunning)
                        {
                            Console.WriteLine("Auto-mover is already running.");
                        }
                        else
                        {
                            Console.WriteLine("Starting auto-mover...");
                            _ = Task.Run(async () => await _autoMover.StartAsync(30, 1));
                            _isRunning = true;
                        }
                        break;

                    case "stop":
                        if (!_autoMover!.IsRunning)
                        {
                            Console.WriteLine("Auto-mover is not running.");
                        }
                        else
                        {
                            Console.WriteLine("Stopping auto-mover...");
                            _autoMover.Stop();
                            _isRunning = false;
                        }
                        break;

                    case "status":
                        Console.WriteLine($"Auto-mover status: {(_autoMover!.IsRunning ? "Running" : "Stopped")}");
                        break;

                    case "help":
                        Console.WriteLine("Commands:");
                        Console.WriteLine("  start  - Start the auto-mover");
                        Console.WriteLine("  stop   - Stop the auto-mover");
                        Console.WriteLine("  status - Show current status");
                        Console.WriteLine("  quit   - Exit the application");
                        Console.WriteLine("  help   - Show this help");
                        break;

                    case "quit":
                    case "exit":
                        if (_autoMover!.IsRunning)
                        {
                            Console.WriteLine("Stopping auto-mover...");
                            _autoMover.Stop();
                            await Task.Delay(1000);
                        }
                        Console.WriteLine("Goodbye!");
                        return;

                    case "":
                        // Empty input, just continue
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {input}. Type 'help' for available commands.");
                        break;
                }
            }
        }

        private static void OnStatusChanged(object? sender, string status)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {status}");
        }

        private static void OnMouseMoved(object? sender, MouseMoveEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Mouse moved from ({e.PreviousPosition.X}, {e.PreviousPosition.Y}) to ({e.NewPosition.X}, {e.NewPosition.Y})");
        }
    }
}
