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
			this.musicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playSong1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playSong2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playSong3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.forwardButton = new System.Windows.Forms.Button();
			this.rightButton = new System.Windows.Forms.Button();
			this.leftButton = new System.Windows.Forms.Button();
			this.radarPlot = new OxyPlot.WindowsForms.PlotView();
			this.scanButton = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.getData = new System.Windows.Forms.Button();
			this.left90 = new System.Windows.Forms.Button();
			this.right90 = new System.Windows.Forms.Button();
			this.funToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.victoryDanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.macroForward = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// connectionIP
			// 
			this.connectionIP.Location = new System.Drawing.Point(26, 42);
			this.connectionIP.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.connectionIP.Name = "connectionIP";
			this.connectionIP.Size = new System.Drawing.Size(397, 26);
			this.connectionIP.TabIndex = 11;
			this.connectionIP.Text = "192.168.1.1";
			this.connectionIP.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.connectionIP_MaskInputRejected);
			this.connectionIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.connectionIP_KeyDown);
			// 
			// ConnectButton
			// 
			this.ConnectButton.Location = new System.Drawing.Point(437, 42);
			this.ConnectButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(144, 31);
			this.ConnectButton.TabIndex = 1;
			this.ConnectButton.Text = "Connect";
			this.ConnectButton.UseVisualStyleBackColor = true;
			this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// logBox
			// 
			this.logBox.FormattingEnabled = true;
			this.logBox.ItemHeight = 20;
			this.logBox.Location = new System.Drawing.Point(26, 262);
			this.logBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.logBox.Name = "logBox";
			this.logBox.Size = new System.Drawing.Size(621, 824);
			this.logBox.TabIndex = 2;
			this.logBox.SelectedIndexChanged += new System.EventHandler(this.logBox_SelectedIndexChanged);
			this.logBox.DoubleClick += new System.EventHandler(this.logBox_DoubleClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.musicToolStripMenuItem,
            this.funToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(2152, 35);
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
			this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.clearLogsToolStripMenuItem.Text = "Clear Logs";
			this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
			// 
			// exitAltF4ToolStripMenuItem
			// 
			this.exitAltF4ToolStripMenuItem.Name = "exitAltF4ToolStripMenuItem";
			this.exitAltF4ToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
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
			this.clearGraphToolStripMenuItem1.Click += new System.EventHandler(this.clearGraphToolStripMenuItem1_Click);
			// 
			// musicToolStripMenuItem
			// 
			this.musicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playSong1ToolStripMenuItem,
            this.playSong2ToolStripMenuItem,
            this.playSong3ToolStripMenuItem});
			this.musicToolStripMenuItem.Name = "musicToolStripMenuItem";
			this.musicToolStripMenuItem.Size = new System.Drawing.Size(70, 29);
			this.musicToolStripMenuItem.Text = "Music";
			// 
			// playSong1ToolStripMenuItem
			// 
			this.playSong1ToolStripMenuItem.Name = "playSong1ToolStripMenuItem";
			this.playSong1ToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.playSong1ToolStripMenuItem.Text = "Play Song 1";
			this.playSong1ToolStripMenuItem.Click += new System.EventHandler(this.playSong1ToolStripMenuItem_Click);
			// 
			// playSong2ToolStripMenuItem
			// 
			this.playSong2ToolStripMenuItem.Name = "playSong2ToolStripMenuItem";
			this.playSong2ToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.playSong2ToolStripMenuItem.Text = "Play Song 2";
			this.playSong2ToolStripMenuItem.Click += new System.EventHandler(this.playSong2ToolStripMenuItem_Click_1);
			// 
			// playSong3ToolStripMenuItem
			// 
			this.playSong3ToolStripMenuItem.Name = "playSong3ToolStripMenuItem";
			this.playSong3ToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.playSong3ToolStripMenuItem.Text = "Play Song 3";
			this.playSong3ToolStripMenuItem.Click += new System.EventHandler(this.playSong3ToolStripMenuItem_Click);
			// 
			// forwardButton
			// 
			this.forwardButton.Location = new System.Drawing.Point(26, 95);
			this.forwardButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new System.Drawing.Size(183, 35);
			this.forwardButton.TabIndex = 4;
			this.forwardButton.Text = "Micro Forward";
			this.forwardButton.UseVisualStyleBackColor = true;
			this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
			// 
			// rightButton
			// 
			this.rightButton.Location = new System.Drawing.Point(26, 185);
			this.rightButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.rightButton.Name = "rightButton";
			this.rightButton.Size = new System.Drawing.Size(183, 35);
			this.rightButton.TabIndex = 5;
			this.rightButton.Text = "Right 10";
			this.rightButton.UseVisualStyleBackColor = true;
			this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
			// 
			// leftButton
			// 
			this.leftButton.Location = new System.Drawing.Point(26, 140);
			this.leftButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.leftButton.Name = "leftButton";
			this.leftButton.Size = new System.Drawing.Size(183, 35);
			this.leftButton.TabIndex = 6;
			this.leftButton.Text = "Left 10";
			this.leftButton.UseVisualStyleBackColor = true;
			this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
			// 
			// radarPlot
			// 
			this.radarPlot.Location = new System.Drawing.Point(656, 42);
			this.radarPlot.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.radarPlot.Name = "radarPlot";
			this.radarPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.radarPlot.Size = new System.Drawing.Size(1374, 1132);
			this.radarPlot.TabIndex = 12;
			this.radarPlot.Text = "Radar";
			this.radarPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.radarPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.radarPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			this.radarPlot.Click += new System.EventHandler(this.radarPlot_Click);
			// 
			// scanButton
			// 
			this.scanButton.Location = new System.Drawing.Point(437, 91);
			this.scanButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.scanButton.Name = "scanButton";
			this.scanButton.Size = new System.Drawing.Size(144, 39);
			this.scanButton.TabIndex = 13;
			this.scanButton.Text = "Scan";
			this.scanButton.UseVisualStyleBackColor = true;
			this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
			// 
			// getData
			// 
			this.getData.Location = new System.Drawing.Point(437, 138);
			this.getData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.getData.Name = "getData";
			this.getData.Size = new System.Drawing.Size(144, 39);
			this.getData.TabIndex = 15;
			this.getData.Text = "Get Data";
			this.getData.UseVisualStyleBackColor = true;
			this.getData.Click += new System.EventHandler(this.getData_Click);
			// 
			// left90
			// 
			this.left90.Location = new System.Drawing.Point(218, 140);
			this.left90.Name = "left90";
			this.left90.Size = new System.Drawing.Size(136, 37);
			this.left90.TabIndex = 16;
			this.left90.Text = "Left 90";
			this.left90.UseVisualStyleBackColor = true;
			this.left90.Click += new System.EventHandler(this.left90_Click);
			// 
			// right90
			// 
			this.right90.Location = new System.Drawing.Point(218, 183);
			this.right90.Name = "right90";
			this.right90.Size = new System.Drawing.Size(136, 37);
			this.right90.TabIndex = 17;
			this.right90.Text = "Right 90";
			this.right90.UseVisualStyleBackColor = true;
			this.right90.Click += new System.EventHandler(this.right90_Click);
			// 
			// funToolStripMenuItem
			// 
			this.funToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.victoryDanceToolStripMenuItem});
			this.funToolStripMenuItem.Name = "funToolStripMenuItem";
			this.funToolStripMenuItem.Size = new System.Drawing.Size(53, 29);
			this.funToolStripMenuItem.Text = "Fun";
			// 
			// victoryDanceToolStripMenuItem
			// 
			this.victoryDanceToolStripMenuItem.BackColor = System.Drawing.Color.Lime;
			this.victoryDanceToolStripMenuItem.Name = "victoryDanceToolStripMenuItem";
			this.victoryDanceToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.victoryDanceToolStripMenuItem.Text = "Victory Dance";
			this.victoryDanceToolStripMenuItem.Click += new System.EventHandler(this.victoryDanceToolStripMenuItem_Click);
			// 
			// macroForward
			// 
			this.macroForward.Location = new System.Drawing.Point(218, 95);
			this.macroForward.Name = "macroForward";
			this.macroForward.Size = new System.Drawing.Size(136, 35);
			this.macroForward.TabIndex = 18;
			this.macroForward.Text = "Macro Forward";
			this.macroForward.UseVisualStyleBackColor = true;
			this.macroForward.Click += new System.EventHandler(this.macroForward_Click);
			// 
			// CyBotGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(2152, 1201);
			this.Controls.Add(this.macroForward);
			this.Controls.Add(this.right90);
			this.Controls.Add(this.left90);
			this.Controls.Add(this.getData);
			this.Controls.Add(this.scanButton);
			this.Controls.Add(this.radarPlot);
			this.Controls.Add(this.leftButton);
			this.Controls.Add(this.rightButton);
			this.Controls.Add(this.forwardButton);
			this.Controls.Add(this.logBox);
			this.Controls.Add(this.ConnectButton);
			this.Controls.Add(this.connectionIP);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.MaximizeBox = false;
			this.Name = "CyBotGUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
		private OxyPlot.WindowsForms.PlotView radarPlot;
		private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveGraphPNGToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearGraphToolStripMenuItem1;
		private System.Windows.Forms.Button scanButton;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolStripMenuItem musicToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playSong1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playSong2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playSong3ToolStripMenuItem;
		private System.Windows.Forms.Button getData;
		private System.Windows.Forms.Button left90;
		private System.Windows.Forms.Button right90;
		private System.Windows.Forms.ToolStripMenuItem funToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem victoryDanceToolStripMenuItem;
		private System.Windows.Forms.Button macroForward;
	}
}

