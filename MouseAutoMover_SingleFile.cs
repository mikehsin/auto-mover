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
        private System.Windows.Forms.Timer timer;

        // UI Controls
        private Label lblInterval;
        private TextBox txtInterval;
        private Label lblSeconds;
        private Button btnStart;
        private Button btnCancel;
        private Label lblStatus;
        private PictureBox picSettings;
        private Label lblTitle;

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
            LoadSettingsIcon();
            
            // Enable Cancel button initially disabled
            btnCancel.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.lblInterval = new Label();
            this.txtInterval = new TextBox();
            this.lblSeconds = new Label();
            this.btnStart = new Button();
            this.btnCancel = new Button();
            this.lblStatus = new Label();
            this.picSettings = new PictureBox();
            this.timer = new System.Windows.Forms.Timer();
            this.lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).BeginInit();
            this.SuspendLayout();
            
            // lblInterval
            this.lblInterval.AutoSize = true;
            this.lblInterval.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblInterval.Location = new Point(15, 60);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new Size(142, 19);
            this.lblInterval.TabIndex = 0;
            this.lblInterval.Text = "Movement Interval:";
            
            // txtInterval
            this.txtInterval.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.txtInterval.Location = new Point(163, 57);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new Size(80, 25);
            this.txtInterval.TabIndex = 1;
            this.txtInterval.Text = "30";
            this.txtInterval.TextAlign = HorizontalAlignment.Center;
            this.txtInterval.KeyPress += new KeyPressEventHandler(this.txtInterval_KeyPress);
            
            // lblSeconds
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblSeconds.Location = new Point(249, 60);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new Size(62, 19);
            this.lblSeconds.TabIndex = 2;
            this.lblSeconds.Text = "seconds";
            
            // btnStart
            this.btnStart.BackColor = Color.LightGreen;
            this.btnStart.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnStart.Location = new Point(15, 100);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(120, 40);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            
            // btnCancel
            this.btnCancel.BackColor = Color.LightCoral;
            this.btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnCancel.Location = new Point(191, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblStatus.ForeColor = Color.Blue;
            this.lblStatus.Location = new Point(15, 155);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(53, 15);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Stopped";
            
            // picSettings
            this.picSettings.Location = new Point(15, 12);
            this.picSettings.Name = "picSettings";
            this.picSettings.Size = new Size(32, 32);
            this.picSettings.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picSettings.TabIndex = 6;
            this.picSettings.TabStop = false;
            
            // timer
            this.timer.Tick += new EventHandler(this.timer_Tick);
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.Location = new Point(53, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(148, 21);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Mouse Auto Mover";
            
            // MainForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(334, 190);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picSettings);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.lblInterval);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Mouse Auto Mover";
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadSettingsIcon()
        {
            try
            {
                CreateSettingsIcon();
            }
            catch
            {
                CreateSettingsIcon();
            }
        }

        private void CreateSettingsIcon()
        {
            Bitmap settingsIcon = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(settingsIcon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    g.DrawEllipse(pen, 6, 6, 20, 20);
                    g.DrawEllipse(pen, 11, 11, 10, 10);
                    
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
            int interval;
            if (int.TryParse(txtInterval.Text, out interval) && interval > 0)
            {
                timer.Interval = interval * 1000;
                timer.Start();
                
                isMoving = true;
                btnStart.Enabled = false;
                btnCancel.Enabled = true;
                txtInterval.Enabled = false;
                
                lblStatus.Text = string.Format("Running - Moving every {0} seconds", interval);
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
                Point currentPos;
                if (GetCursorPos(out currentPos))
                {
                    int deltaX = random.Next(-3, 4);
                    int deltaY = random.Next(-3, 4);
                    
                    int newX = currentPos.X + deltaX;
                    int newY = currentPos.Y + deltaY;
                    
                    Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
                    newX = Math.Max(0, Math.Min(newX, screenBounds.Width - 1));
                    newY = Math.Max(0, Math.Min(newY, screenBounds.Height - 1));
                    
                    SetCursorPos(newX, newY);
                    
                    lblStatus.Text = string.Format("Running - Last moved: {0}", DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            catch (Exception ex)
            {
                timer.Stop();
                btnCancel_Click(this, EventArgs.Empty);
                MessageBox.Show(string.Format("Error moving mouse: {0}", ex.Message), 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            base.OnFormClosing(e);
        }

        private void txtInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
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

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
