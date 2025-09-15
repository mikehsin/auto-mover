using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseAutoMover
{
    public partial class MainForm : Form
    {
        // Import Windows API functions for mouse cursor manipulation
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);

        private Random random;
        private bool isMoving = false;

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
            LoadSettingsIcon();
            
            // Enable Cancel button initially disabled
            btnCancel.Enabled = false;
        }

        private void LoadSettingsIcon()
        {
            try
            {
                // Create a simple settings icon using graphics if the resource file doesn't exist
                CreateSettingsIcon();
            }
            catch (Exception ex)
            {
                // If icon loading fails, create a simple placeholder
                CreateSettingsIcon();
            }
        }

        private void CreateSettingsIcon()
        {
            // Create a simple gear/settings icon programmatically
            Bitmap settingsIcon = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(settingsIcon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                
                // Draw a gear-like settings icon
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    // Outer circle
                    g.DrawEllipse(pen, 6, 6, 20, 20);
                    
                    // Inner circle
                    g.DrawEllipse(pen, 11, 11, 10, 10);
                    
                    // Gear teeth (simplified)
                    for (int i = 0; i < 8; i++)
                    {
                        double angle = i * Math.PI / 4;
                        int x1 = (int)(16 + 12 * Math.Cos(angle));
                        int y1 = (int)(16 + 12 * Math.Sin(angle));
                        int x2 = (int)(16 + 8 * Math.Cos(angle));
                        int y2 = (int)(16 + 8 * Math.Sin(angle));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }
            picSettings.Image = settingsIcon;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtInterval.Text, out int interval) && interval > 0)
            {
                // Convert seconds to milliseconds
                timer.Interval = interval * 1000;
                timer.Start();
                
                isMoving = true;
                btnStart.Enabled = false;
                btnCancel.Enabled = true;
                txtInterval.Enabled = false;
                
                lblStatus.Text = $"Running - Moving every {interval} seconds";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                MessageBox.Show("Please enter a valid positive number for the interval.", 
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInterval.Focus();
                txtInterval.SelectAll();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer.Stop();
            isMoving = false;
            
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
            txtInterval.Enabled = true;
            
            lblStatus.Text = "Stopped";
            lblStatus.ForeColor = Color.Blue;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveMouse();
        }

        private void MoveMouse()
        {
            try
            {
                // Get current mouse position
                Point currentPos;
                if (GetCursorPos(out currentPos))
                {
                    // Generate small random movement (1-3 pixels in any direction)
                    int deltaX = random.Next(-3, 4); // -3 to 3 pixels
                    int deltaY = random.Next(-3, 4); // -3 to 3 pixels
                    
                    // Calculate new position
                    int newX = currentPos.X + deltaX;
                    int newY = currentPos.Y + deltaY;
                    
                    // Ensure the new position is within screen bounds
                    Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
                    newX = Math.Max(0, Math.Min(newX, screenBounds.Width - 1));
                    newY = Math.Max(0, Math.Min(newY, screenBounds.Height - 1));
                    
                    // Move the mouse cursor
                    SetCursorPos(newX, newY);
                    
                    // Update status with last movement info
                    lblStatus.Text = $"Running - Last moved: {DateTime.Now:HH:mm:ss}";
                }
            }
            catch (Exception ex)
            {
                // If movement fails, stop the timer and show error
                timer.Stop();
                btnCancel_Click(this, EventArgs.Empty);
                MessageBox.Show($"Error moving mouse: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop the timer when closing the form
            if (timer.Enabled)
            {
                timer.Stop();
            }
            base.OnFormClosing(e);
        }

        // Handle Enter key in the interval text box
        private void txtInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace, and Enter
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Enter && btnStart.Enabled)
            {
                btnStart_Click(sender, e);
            }
        }
    }
}
