﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cybot_GUI
{
	public partial class CyBotGUI : Form
	{
		// connection button strings
		private readonly string connectText = "Connect";
		private readonly string disconnectText = "Disconnect";

		private SocketClient client;
		private static CancellationTokenSource ReceiveThreadCancel = new CancellationTokenSource();
		private RadarChart Radar;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Cybot_GUI.CyBotGUI"/> class.
		/// </summary>
		public CyBotGUI()
		{
			InitializeComponent();
			ConnectButton.Text = connectText;
			Radar = new RadarChart(radarPlot);
			SetMovementControlsEnabled(false);
		}

		/// <summary>
		/// Called before application closes
		/// </summary>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			Dispose(true);
			ExitApplication();
		}

		/// <summary>
		/// Exits the application.
		/// </summary>
		private void ExitApplication()
		{
			// close receiveThread if it is running
			ReceiveThreadCancel.Cancel();

			Application.Exit();
		}

		/// <summary>
		/// Autos the scroll.
		/// </summary>
		/// <param name="l">ListBox to autoscroll</param>
		public static new void AutoScroll(ListBox l)
		{
			// https://stackoverflow.com/q/28285130
			// autoscroll to end
			l.SelectedIndex = l.Items.Count - 1;
			l.SelectedIndex = -1;  // deselect the item
		}

		/// <summary>
		/// Enable or disable movement buttons.
		/// </summary>
		/// <param name="b">If set to <c>true</c> enable the buttons, if <c>false</c> disable them.</param>
		private void SetMovementControlsEnabled(bool b)
		{
			forwardButton.Enabled = b;
			rightButton.Enabled = b;
			leftButton.Enabled = b;
			forwardValue.Enabled = b;
			rightValue.Enabled = b;
			leftValue.Enabled = b;
		}

		//Connect
		private async void ConnectButton_Click(object sender, EventArgs e)
		{
			if (ConnectButton.Text == connectText) {
				// connected
				ConnectButton.Text = disconnectText;

				// setup socketclient
				client = new SocketClient(connectionIP.Text);
				WriteToLog(string.Format("Trying to connect to {0}:{1}...", client.Ip, client.Port));

				// check if connected
				if (!client.Connect()) {
					WriteToLog(client.LastErrorMessage + "...\n");
					ConnectButton.Text = connectText;
				} else {
					// actions to perform after successful connection
					// http://stackoverflow.com/a/18033198
					SetMovementControlsEnabled(true);
					var output = new Progress<string>(s => WriteToLog(s, false));
					await Task.Factory.StartNew(() => client.ReceiveThread(output, ReceiveThreadCancel.Token), TaskCreationOptions.LongRunning);
					WriteToLog("Receive thread ended.\n");
				}

			} else {
				// disconnected
				ConnectButton.Text = connectText;

				WriteToLog("Disconnecting...");
				SetMovementControlsEnabled(false);

				// tell receive to cancel
				ReceiveThreadCancel.Cancel();

				if (!client.Disconnect()) {
					WriteToLog(client.LastErrorMessage + "...\n");
					ConnectButton.Text = connectText;
					SetMovementControlsEnabled(true);
				} else {
					WriteToLog("Disconnection done.\n");
				}
			}
		}

		private void logBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void logBox_DoubleClick(object sender, EventArgs e)
		{

		}

		//
		//Text boxes for the movement controls.
		//
		private void rightValue_TextChanged(object sender, EventArgs e)
		{

		}

		private void leftValue_TextChanged(object sender, EventArgs e)
		{

		}

		private void forwardValue_TextChanged(object sender, EventArgs e)
		{

		}

		//Forward Button
		private void forwardButton_Click(object sender, EventArgs e)
		{
            if (client.IsConnected())
            {
                try
                {
                    int fV = Convert.ToInt16(forwardValue.Text);
                    if (fV > 0)  WriteToLog("Moving forward " + fV + "mm");

					String r = client.SendCommand("M" + fV);
					WriteToLog("Command Response: " + r);
                }
                catch (Exception a)
                {
                    WriteToLog("INVALID FORMAT" + a.Message);
                }
            }
            else
            {
                WriteToLog("Cannot move, not Connected.");
            }
		}

		//Left Button
		private void leftButton_Click(object sender, EventArgs e)
		{
            if (client.IsConnected())
            {
                try
                {
                    int lV = Convert.ToInt16(leftValue.Text);
                    if (lV > 0)
                    {
                        WriteToLog("Turning left " + lV + " degrees");
                    }
					//Insert Socket push here for left movement.
					String r = client.SendCommand("L" + lV);
					WriteToLog("Command Response: " + r);
                }
                catch (Exception a)
                {
                    WriteToLog("INVALID FORMAT " + a.Message);
                }
            }
            else
            {
                WriteToLog("Cannot move, not Connected.");
            }
        }

        //Right Button
        private void rightButton_Click(object sender, EventArgs e)
		{
            if (client.IsConnected())
            {
                try
                {
                    int rV = Convert.ToInt16(rightValue.Text);
                    if (rV > 0) WriteToLog("Turning right " + rV + " degrees");
					//Insert Socket push here for right movement.
					String r = client.SendCommand("R" + rV);
					WriteToLog("Command Response: " + r);
                }
                catch (Exception a)
                {
                    WriteToLog("INVALID FORMAT " + a.Message);
                }
            }
            else
            {
                WriteToLog("Cannot move, not Connected.");
            }
        }

        private void clearGraphToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			logBox.Items.Clear();
		}

		private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExitApplication();
		}

		private void Window_Load(object sender, EventArgs e)
		{

		}

		private void connectionIP_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			
		}

		private void connectionIP_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
					e.SuppressKeyPress = true;
					//ConnectButton_Click(sender, e);
				}
		}


		/// <summary>
		/// Appends item to logBox as well as the System Console.
		/// </summary>
		/// <param name="msg">Message to write</param>
		/// <param name="writeConsole">If set to <c>false</c>, disable writing to the console.</param>
		private void WriteToLog(string msg, bool writeConsole = true)
		{
			logBox.Items.Add(msg);
			if (writeConsole) Console.Write(msg);

			AutoScroll(logBox);
		}

        private void radarPlot_Click(object sender, EventArgs e)
        {

        }

        private void saveGraphPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
