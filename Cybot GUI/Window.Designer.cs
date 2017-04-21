namespace Cybot_GUI
{
	partial class CyBotGUI
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
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CyBotGUI));
			this.connectionIP = new System.Windows.Forms.MaskedTextBox();
			this.ConnectButton = new System.Windows.Forms.Button();
			this.logBox = new System.Windows.Forms.ListBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitAltF4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveGraphPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearGraphToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.forwardButton = new System.Windows.Forms.Button();
			this.rightButton = new System.Windows.Forms.Button();
			this.leftButton = new System.Windows.Forms.Button();
			this.forwardValue = new System.Windows.Forms.TextBox();
			this.leftValue = new System.Windows.Forms.TextBox();
			this.rightValue = new System.Windows.Forms.TextBox();
			this.radarPlot = new OxyPlot.WindowsForms.PlotView();
			this.scanButton = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// connectionIP
			// 
			this.connectionIP.Location = new System.Drawing.Point(20, 42);
			this.connectionIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.connectionIP.Name = "connectionIP";
			this.connectionIP.Size = new System.Drawing.Size(310, 26);
			this.connectionIP.TabIndex = 11;
			this.connectionIP.Text = "192.168.1.1";
			this.connectionIP.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.connectionIP_MaskInputRejected);
			this.connectionIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.connectionIP_KeyDown);
			// 
			// ConnectButton
			// 
			this.ConnectButton.Location = new System.Drawing.Point(340, 42);
			this.ConnectButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(112, 31);
			this.ConnectButton.TabIndex = 1;
			this.ConnectButton.Text = "Connect";
			this.ConnectButton.UseVisualStyleBackColor = true;
			this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// logBox
			// 
			this.logBox.FormattingEnabled = true;
			this.logBox.ItemHeight = 20;
			this.logBox.Location = new System.Drawing.Point(20, 262);
			this.logBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.logBox.Name = "logBox";
			this.logBox.Size = new System.Drawing.Size(432, 424);
			this.logBox.TabIndex = 2;
			this.logBox.SelectedIndexChanged += new System.EventHandler(this.logBox_SelectedIndexChanged);
			this.logBox.DoubleClick += new System.EventHandler(this.logBox_DoubleClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.graphToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(1394, 35);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.clearLogsToolStripMenuItem,
			this.exitAltF4ToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// clearLogsToolStripMenuItem
			// 
			this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
			this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
			this.clearLogsToolStripMenuItem.Text = "Clear Logs";
			this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
			// 
			// exitAltF4ToolStripMenuItem
			// 
			this.exitAltF4ToolStripMenuItem.Name = "exitAltF4ToolStripMenuItem";
			this.exitAltF4ToolStripMenuItem.Size = new System.Drawing.Size(192, 30);
			this.exitAltF4ToolStripMenuItem.Text = "Exit (Alt+F4)";
			this.exitAltF4ToolStripMenuItem.Click += new System.EventHandler(this.exitAltF4ToolStripMenuItem_Click);
			// 
			// graphToolStripMenuItem
			// 
			this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveGraphPNGToolStripMenuItem,
			this.clearGraphToolStripMenuItem1});
			this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
			this.graphToolStripMenuItem.Size = new System.Drawing.Size(72, 29);
			this.graphToolStripMenuItem.Text = "Graph";
			// 
			// saveGraphPNGToolStripMenuItem
			// 
			this.saveGraphPNGToolStripMenuItem.Name = "saveGraphPNGToolStripMenuItem";
			this.saveGraphPNGToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
			this.saveGraphPNGToolStripMenuItem.Text = "Save Graph (PNG)";
			this.saveGraphPNGToolStripMenuItem.Click += new System.EventHandler(this.saveGraphPNGToolStripMenuItem_Click);
			// 
			// clearGraphToolStripMenuItem1
			// 
			this.clearGraphToolStripMenuItem1.Name = "clearGraphToolStripMenuItem1";
			this.clearGraphToolStripMenuItem1.Size = new System.Drawing.Size(237, 30);
			this.clearGraphToolStripMenuItem1.Text = "Clear Graph";
			// 
			// forwardButton
			// 
			this.forwardButton.Location = new System.Drawing.Point(20, 95);
			this.forwardButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new System.Drawing.Size(142, 35);
			this.forwardButton.TabIndex = 4;
			this.forwardButton.Text = "Forward (mm)";
			this.forwardButton.UseVisualStyleBackColor = true;
			this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
			// 
			// rightButton
			// 
			this.rightButton.Location = new System.Drawing.Point(20, 185);
			this.rightButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.rightButton.Name = "rightButton";
			this.rightButton.Size = new System.Drawing.Size(142, 35);
			this.rightButton.TabIndex = 5;
			this.rightButton.Text = "Right (Degrees)";
			this.rightButton.UseVisualStyleBackColor = true;
			this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
			// 
			// leftButton
			// 
			this.leftButton.Location = new System.Drawing.Point(20, 140);
			this.leftButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.leftButton.Name = "leftButton";
			this.leftButton.Size = new System.Drawing.Size(142, 35);
			this.leftButton.TabIndex = 6;
			this.leftButton.Text = "Left (Degrees)";
			this.leftButton.UseVisualStyleBackColor = true;
			this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
			// 
			// forwardValue
			// 
			this.forwardValue.Location = new System.Drawing.Point(171, 95);
			this.forwardValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.forwardValue.MaxLength = 4;
			this.forwardValue.Name = "forwardValue";
			this.forwardValue.Size = new System.Drawing.Size(64, 26);
			this.forwardValue.TabIndex = 7;
			this.forwardValue.Text = "100";
			this.forwardValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// leftValue
			// 
			this.leftValue.Location = new System.Drawing.Point(171, 140);
			this.leftValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.leftValue.Name = "leftValue";
			this.leftValue.Size = new System.Drawing.Size(64, 26);
			this.leftValue.TabIndex = 8;
			this.leftValue.Text = "90";
			this.leftValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// rightValue
			// 
			this.rightValue.Location = new System.Drawing.Point(171, 189);
			this.rightValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.rightValue.Name = "rightValue";
			this.rightValue.Size = new System.Drawing.Size(64, 26);
			this.rightValue.TabIndex = 9;
			this.rightValue.Text = "90";
			this.rightValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// radarPlot
			// 
			this.radarPlot.Location = new System.Drawing.Point(488, 42);
			this.radarPlot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radarPlot.Name = "radarPlot";
			this.radarPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.radarPlot.Size = new System.Drawing.Size(888, 646);
			this.radarPlot.TabIndex = 12;
			this.radarPlot.Text = "Radar";
			this.radarPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.radarPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.radarPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			this.radarPlot.Click += new System.EventHandler(this.radarPlot_Click);
			// 
			// scanButton
			// 
			this.scanButton.Location = new System.Drawing.Point(340, 91);
			this.scanButton.Name = "scanButton";
			this.scanButton.Size = new System.Drawing.Size(112, 39);
			this.scanButton.TabIndex = 13;
			this.scanButton.Text = "Scan";
			this.scanButton.UseVisualStyleBackColor = true;
			this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
			// 
			// CyBotGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1394, 706);
			this.Controls.Add(this.scanButton);
			this.Controls.Add(this.radarPlot);
			this.Controls.Add(this.rightValue);
			this.Controls.Add(this.leftValue);
			this.Controls.Add(this.forwardValue);
			this.Controls.Add(this.leftButton);
			this.Controls.Add(this.rightButton);
			this.Controls.Add(this.forwardButton);
			this.Controls.Add(this.logBox);
			this.Controls.Add(this.ConnectButton);
			this.Controls.Add(this.connectionIP);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "CyBotGUI";
			this.Text = "CyBot GUI";
			this.Load += new System.EventHandler(this.Window_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MaskedTextBox connectionIP;
		private System.Windows.Forms.Button ConnectButton;
		private System.Windows.Forms.ListBox logBox;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitAltF4ToolStripMenuItem;
		private System.Windows.Forms.Button forwardButton;
		private System.Windows.Forms.Button rightButton;
		private System.Windows.Forms.Button leftButton;
		private System.Windows.Forms.TextBox forwardValue;
		private System.Windows.Forms.TextBox leftValue;
		private System.Windows.Forms.TextBox rightValue;
		private OxyPlot.WindowsForms.PlotView radarPlot;
		private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveGraphPNGToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearGraphToolStripMenuItem1;
		private System.Windows.Forms.Button scanButton;
		private System.Windows.Forms.ColorDialog colorDialog1;
	}
}

