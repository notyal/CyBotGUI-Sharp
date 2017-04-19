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
					var output = new Progress<string>(s => WriteToLog(s, false));
					await Task.Factory.StartNew(() => client.ReceiveThread(output, ReceiveThreadCancel.Token), TaskCreationOptions.LongRunning);
					WriteToLog("Receive thread ended.\n");
				}

			} else {
				// disconnected
				ConnectButton.Text = connectText;

				WriteToLog("Disconnecting...");

				// tell receive to cancel
				ReceiveThreadCancel.Cancel();

				if (!client.Disconnect()) {
					WriteToLog(client.LastErrorMessage + "...\n");
					ConnectButton.Text = connectText;
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
			try {
				int fV = Convert.ToInt16(forwardValue.Text);
				if (fV > 0) {
					logBox.Items.Add("Moving forward " + fV + "mm");
				}
                //Insert Socket push here for forward movement.
                client.Write("M" + fV);
			} catch (Exception a) {
				logBox.Items.Add("INVALID FORMAT" + a.Message);
			}

		}

        //Depreciated.
		private void addObject(int x, int y)
		{
			SolidBrush myBrush = new SolidBrush(Color.Red);
			Graphics formGraphics;
			formGraphics = this.CreateGraphics();
			formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
			myBrush.Dispose();
			formGraphics.Dispose();
		}

		//Left Button
		private void leftButton_Click(object sender, EventArgs e)
		{
            try
            {
                int lV = Convert.ToInt16(leftValue.Text);
                if (lV > 0)
                {
                    logBox.Items.Add("Turning left " + lV + " degrees");
                }
                //Insert Socket push here for left movement.
            }
            catch (Exception a)
            {
                logBox.Items.Add("INVALID FORMAT " + a.Message);
            }

        }

        //Right Button
        private void rightButton_Click(object sender, EventArgs e)
		{
            try
            {
                int rV = Convert.ToInt16(rightValue.Text);
                if (rV > 0)
                {
                    logBox.Items.Add("Turning right " + rV + " degrees");
                }
                //Insert Socket push here for right movement.
            }
            catch (Exception a)
            {
                logBox.Items.Add("INVALID FORMAT " + a.Message);
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
