namespace B32Machine
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS14SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS12SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS1SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS2SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS3SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS4SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mS5SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realTimeNoDelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlRegisters = new System.Windows.Forms.Panel();
            this.lblRegisters = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.b32Screen1 = new B32Machine.B32Screen();
            this.msMainMenu.SuspendLayout();
            this.pnlRegisters.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.speedToolStripMenuItem,
            this.pauseProgramToolStripMenuItem,
            this.resumeProgramToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(874, 24);
            this.msMainMenu.TabIndex = 1;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mS14SecondToolStripMenuItem,
            this.mS12SecondToolStripMenuItem,
            this.mS1SecondToolStripMenuItem,
            this.mS2SecondToolStripMenuItem,
            this.mS3SecondToolStripMenuItem,
            this.mS4SecondToolStripMenuItem,
            this.mS5SecondToolStripMenuItem,
            this.realTimeNoDelayToolStripMenuItem});
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.speedToolStripMenuItem.Text = "Speed";
            // 
            // mS14SecondToolStripMenuItem
            // 
            this.mS14SecondToolStripMenuItem.Name = "mS14SecondToolStripMenuItem";
            this.mS14SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS14SecondToolStripMenuItem.Text = "250MS (1/4 Second)";
            this.mS14SecondToolStripMenuItem.Click += new System.EventHandler(this.mS14SecondToolStripMenuItem_Click);
            // 
            // mS12SecondToolStripMenuItem
            // 
            this.mS12SecondToolStripMenuItem.Name = "mS12SecondToolStripMenuItem";
            this.mS12SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS12SecondToolStripMenuItem.Text = "500MS (1/2 Second)";
            this.mS12SecondToolStripMenuItem.Click += new System.EventHandler(this.mS12SecondToolStripMenuItem_Click);
            // 
            // mS1SecondToolStripMenuItem
            // 
            this.mS1SecondToolStripMenuItem.Name = "mS1SecondToolStripMenuItem";
            this.mS1SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS1SecondToolStripMenuItem.Text = "1000MS (1 Second)";
            this.mS1SecondToolStripMenuItem.Click += new System.EventHandler(this.mS1SecondToolStripMenuItem_Click);
            // 
            // mS2SecondToolStripMenuItem
            // 
            this.mS2SecondToolStripMenuItem.Name = "mS2SecondToolStripMenuItem";
            this.mS2SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS2SecondToolStripMenuItem.Text = "2000MS (2 Second)";
            this.mS2SecondToolStripMenuItem.Click += new System.EventHandler(this.mS2SecondToolStripMenuItem_Click);
            // 
            // mS3SecondToolStripMenuItem
            // 
            this.mS3SecondToolStripMenuItem.Name = "mS3SecondToolStripMenuItem";
            this.mS3SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS3SecondToolStripMenuItem.Text = "3000MS (3 Second)";
            this.mS3SecondToolStripMenuItem.Click += new System.EventHandler(this.mS3SecondToolStripMenuItem_Click);
            // 
            // mS4SecondToolStripMenuItem
            // 
            this.mS4SecondToolStripMenuItem.Name = "mS4SecondToolStripMenuItem";
            this.mS4SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS4SecondToolStripMenuItem.Text = "4000MS (4 Second)";
            this.mS4SecondToolStripMenuItem.Click += new System.EventHandler(this.mS4SecondToolStripMenuItem_Click);
            // 
            // mS5SecondToolStripMenuItem
            // 
            this.mS5SecondToolStripMenuItem.Name = "mS5SecondToolStripMenuItem";
            this.mS5SecondToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mS5SecondToolStripMenuItem.Text = "5000MS (5 Second)";
            this.mS5SecondToolStripMenuItem.Click += new System.EventHandler(this.mS5SecondToolStripMenuItem_Click);
            // 
            // realTimeNoDelayToolStripMenuItem
            // 
            this.realTimeNoDelayToolStripMenuItem.Name = "realTimeNoDelayToolStripMenuItem";
            this.realTimeNoDelayToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.realTimeNoDelayToolStripMenuItem.Text = "Real Time (No Delay)";
            this.realTimeNoDelayToolStripMenuItem.Click += new System.EventHandler(this.realTimeNoDelayToolStripMenuItem_Click);
            // 
            // pauseProgramToolStripMenuItem
            // 
            this.pauseProgramToolStripMenuItem.Name = "pauseProgramToolStripMenuItem";
            this.pauseProgramToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.pauseProgramToolStripMenuItem.Text = "Pause Program";
            this.pauseProgramToolStripMenuItem.Click += new System.EventHandler(this.pauseProgramToolStripMenuItem_Click);
            // 
            // resumeProgramToolStripMenuItem
            // 
            this.resumeProgramToolStripMenuItem.Name = "resumeProgramToolStripMenuItem";
            this.resumeProgramToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.resumeProgramToolStripMenuItem.Text = "Resume Program";
            this.resumeProgramToolStripMenuItem.Click += new System.EventHandler(this.resumeProgramToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // pnlRegisters
            // 
            this.pnlRegisters.Controls.Add(this.lblRegisters);
            this.pnlRegisters.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegisters.Location = new System.Drawing.Point(0, 24);
            this.pnlRegisters.Name = "pnlRegisters";
            this.pnlRegisters.Size = new System.Drawing.Size(230, 564);
            this.pnlRegisters.TabIndex = 2;
            // 
            // lblRegisters
            // 
            this.lblRegisters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRegisters.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegisters.Location = new System.Drawing.Point(0, 0);
            this.lblRegisters.Name = "lblRegisters";
            this.lblRegisters.Size = new System.Drawing.Size(230, 564);
            this.lblRegisters.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "B32";
            this.openFileDialog1.Filter = "B32 Files|*.B32";
            // 
            // b32Screen1
            // 
            this.b32Screen1.BackColor = System.Drawing.Color.Black;
            this.b32Screen1.Dock = System.Windows.Forms.DockStyle.Right;
            this.b32Screen1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.b32Screen1.Location = new System.Drawing.Point(231, 24);
            this.b32Screen1.Name = "b32Screen1";
            this.b32Screen1.ScreenMemoryLocation = ((ushort)(40960));
            this.b32Screen1.Size = new System.Drawing.Size(643, 564);
            this.b32Screen1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 588);
            this.Controls.Add(this.pnlRegisters);
            this.Controls.Add(this.b32Screen1);
            this.Controls.Add(this.msMainMenu);
            this.MainMenuStrip = this.msMainMenu;
            this.Name = "MainForm";
            this.Text = "VIrtual Machine";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.pnlRegisters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private B32Screen b32Screen1;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Panel pnlRegisters;
        private System.Windows.Forms.Label lblRegisters;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS14SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS12SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS1SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS2SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS3SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS4SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mS5SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realTimeNoDelayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
    }
}

