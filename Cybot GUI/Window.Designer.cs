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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CyBotGUI));
            this.connectionIP = new System.Windows.Forms.MaskedTextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitAltF4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.forwardValue = new System.Windows.Forms.TextBox();
            this.leftValue = new System.Windows.Forms.TextBox();
            this.rightValue = new System.Windows.Forms.TextBox();
            this.obstacleGraph = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // connectionIP
            // 
            this.connectionIP.Location = new System.Drawing.Point(13, 27);
            this.connectionIP.Name = "connectionIP";
            this.connectionIP.Size = new System.Drawing.Size(208, 20);
            this.connectionIP.TabIndex = 11;
            this.connectionIP.Text = "192.168.1.1";
            this.connectionIP.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.connectionIP_MaskInputRejected);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(227, 27);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 20);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.Location = new System.Drawing.Point(13, 170);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(289, 277);
            this.logBox.TabIndex = 2;
            this.logBox.SelectedIndexChanged += new System.EventHandler(this.logBox_SelectedIndexChanged);
			this.logBox.DoubleClick += new System.EventHandler(this.logBox_DoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(929, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogsToolStripMenuItem,
            this.clearGraphToolStripMenuItem,
            this.exitAltF4ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.clearLogsToolStripMenuItem.Text = "Clear Logs";
            this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
            // 
            // clearGraphToolStripMenuItem
            // 
            this.clearGraphToolStripMenuItem.Name = "clearGraphToolStripMenuItem";
            this.clearGraphToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.clearGraphToolStripMenuItem.Text = "Clear Graph";
            this.clearGraphToolStripMenuItem.Click += new System.EventHandler(this.clearGraphToolStripMenuItem_Click);
            // 
            // exitAltF4ToolStripMenuItem
            // 
            this.exitAltF4ToolStripMenuItem.Name = "exitAltF4ToolStripMenuItem";
            this.exitAltF4ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitAltF4ToolStripMenuItem.Text = "Exit (Alt+F4)";
			this.exitAltF4ToolStripMenuItem.Click += new System.EventHandler(this.exitAltF4ToolStripMenuItem_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Location = new System.Drawing.Point(13, 62);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(95, 23);
            this.forwardButton.TabIndex = 4;
            this.forwardButton.Text = "Forward (mm)";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(13, 120);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(95, 23);
            this.rightButton.TabIndex = 5;
            this.rightButton.Text = "Right (Degrees)";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(13, 91);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(95, 23);
            this.leftButton.TabIndex = 6;
            this.leftButton.Text = "Left (Degrees)";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // forwardValue
            // 
            this.forwardValue.Location = new System.Drawing.Point(114, 62);
            this.forwardValue.MaxLength = 4;
            this.forwardValue.Name = "forwardValue";
            this.forwardValue.Size = new System.Drawing.Size(44, 20);
            this.forwardValue.TabIndex = 7;
            this.forwardValue.Text = "100";
            this.forwardValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.forwardValue.TextChanged += new System.EventHandler(this.forwardValue_TextChanged);
            // 
            // leftValue
            // 
            this.leftValue.Location = new System.Drawing.Point(114, 91);
            this.leftValue.Name = "leftValue";
            this.leftValue.Size = new System.Drawing.Size(44, 20);
            this.leftValue.TabIndex = 8;
            this.leftValue.Text = "90";
            this.leftValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.leftValue.TextChanged += new System.EventHandler(this.leftValue_TextChanged);
            // 
            // rightValue
            // 
            this.rightValue.Location = new System.Drawing.Point(114, 123);
            this.rightValue.Name = "rightValue";
            this.rightValue.Size = new System.Drawing.Size(44, 20);
            this.rightValue.TabIndex = 9;
            this.rightValue.Text = "90";
            this.rightValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rightValue.TextChanged += new System.EventHandler(this.rightValue_TextChanged);
            // 
            // obstacleGraph
            // 
            this.obstacleGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.obstacleGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.obstacleGraph.ImageLocation = "https://img.clipartfest.com/38f7d8e0f8a6d4918dcd3ce6582f2547_pin-coordinate-plane" +
    "-10-on-clipart-of-coordinate-plane_1005-1024.png";
            this.obstacleGraph.Location = new System.Drawing.Point(309, 27);
            this.obstacleGraph.Name = "obstacleGraph";
            this.obstacleGraph.Size = new System.Drawing.Size(608, 420);
            this.obstacleGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.obstacleGraph.TabIndex = 10;
            this.obstacleGraph.TabStop = false;
            // 
            // CyBotGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 459);
            this.Controls.Add(this.obstacleGraph);
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
            this.Name = "CyBotGUI";
            this.Text = "CyBot GUI";
            this.Load += new System.EventHandler(this.Window_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleGraph)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem clearGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitAltF4ToolStripMenuItem;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.TextBox forwardValue;
        private System.Windows.Forms.TextBox leftValue;
        private System.Windows.Forms.TextBox rightValue;
        private System.Windows.Forms.PictureBox obstacleGraph;
    }
}

