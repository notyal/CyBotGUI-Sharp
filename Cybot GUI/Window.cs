﻿using OxyPlot.WindowsForms;
using System;
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
	/// <summary>
	/// CyBot GUI.
	/// </summary>
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

			//Initializes our log and graph
			var log = new Progress<string>(s => WriteToLog(s + "\n", true));
			Radar = new RadarChart(radarPlot, log);

			//Waits for connection to enable buttons
			SetMovementControlsEnabled(false);
			//Initializes sensors
			ResetSensorLabels();

			//DEBUG TODO
			//connectionIP.Text = "127.0.0.1";
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
		/// Scroll a ListBox to the bottom.
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
		/// <summary>
		/// Resets the line bump labels.
		/// </summary>
		private void ResetLineBumpLabels(){
			lineBumpR.BackColor = Color.White;
			lineBumpL.BackColor = Color.White;
			lineBumpFR.BackColor = Color.White;
			lineBumpFL.BackColor = Color.White;
		}

		/// <summary>
		/// Resets the light bump labels.
		/// </summary>
		private void ResetLightBumpLabels()
		{
			lightBumpL.BackColor = Color.White;
			lightBumpR.BackColor = Color.White;

			lightBumpCL.BackColor = Color.White;
			lightBumpCR.BackColor = Color.White;

			lightBumpFR.BackColor = Color.White;
			lightBumpFL.BackColor = Color.White;
		}

		/// <summary>
		/// Resets the sensor labels.
		/// </summary>
		private void ResetSensorLabels()
		{
			ResetLineBumpLabels();
			ResetLightBumpLabels();
		}

		//Connect
		/// <summary>
		/// Click handler for Connection Button.
		/// Async task to handle the connection thread.
		/// Will switch text to resemble a disconnect button when connected.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private async void ConnectButton_Click(object sender, EventArgs e)
		{
			ResetSensorLabels();
			if (ConnectButton.Text == connectText) {
				// connected
				ConnectButton.Text = disconnectText;

				// setup socketclient
				client = new SocketClient(connectionIP.Text);
				WriteToLog(string.Format("Trying to connect to {0}:{1}...", client.Ip, client.Port));

				// check if connected
				if (!client.Connect()) {
					WriteToLog(client.LastErrorMessage + ".\n");
					ConnectButton.Text = connectText;
				} else {
					// actions to perform after successful connection
					// http://stackoverflow.com/a/18033198
					SetMovementControlsEnabled(true);
					WriteToLog("Success.\n");

					var log = new Progress<string>(s => WriteToLog(s + "\n", true));
					var scandata = new Progress<string>(Radar.AddData);

					// set line bump labels
					var linesensor = new Progress<UInt16>((type) => {
						ResetLineBumpLabels();
						switch (type) {
							case 1:
								// left
								lineBumpL.BackColor = Color.Red;
								break;
							case 2:
								// front left
								lineBumpFL.BackColor = Color.Red;
								break;
							case 3:
								// front right
								lineBumpFR.BackColor = Color.Red;
								break;
							case 4:
								// right
								lineBumpR.BackColor = Color.Red;
								break;
						}
					});

					// set bump light labels
					var bumplightsensor = new Progress<UInt16>((type) => {
						ResetLightBumpLabels();
						switch (type) {
							case 1:
								// left
								lightBumpL.BackColor = Color.Red;
								break;
							case 2:
								// front left
								lightBumpFL.BackColor = Color.Red;
								break;
							case 3:
								// center left
								lightBumpCL.BackColor = Color.Red;
								break;
							case 4:
								// center right
								lightBumpCR.BackColor = Color.Red;
								break;
							case 5:
								// front right
								lightBumpFR.BackColor = Color.Red;
								break;
							case 6:
								// right
								lightBumpR.BackColor = Color.Red;
								break;
						}
					});
					var sensordata = new Progress<string>((s) => LocationMap.ProcessData(s, log, linesensor, bumplightsensor));
					await Task.Factory.StartNew(() => client.ReceiveThread(log, scandata, ReceiveThreadCancel.Token, sensordata), TaskCreationOptions.LongRunning);
					// the above line will block further lines until its thread ends

					WriteToLog("Receive thread ended.\n");
					ReceiveThreadCancel = new CancellationTokenSource();
					ConnectButton.Text = connectText;
					SetMovementControlsEnabled(false);
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

		/// <summary>
		/// Enable or disable movement buttons.
		/// </summary>
		/// <param name="b">If set to <c>true</c> enable the buttons, if <c>false</c> disable them.</param>
		private void SetMovementControlsEnabled(bool b)
		{
			scanButton.Enabled = b;
			forwardButton.Enabled = b;
			macroForward.Enabled = b;
			rightButton.Enabled = b;
			leftButton.Enabled = b;
			left90.Enabled = b;
			right90.Enabled = b;
			getData.Enabled = b;
		}


		// ----- MOVEMENT BUTTONS --------------------------------------------------------------------------------------
		//Forward Button
		/// <summary>
		/// Click handler for Forward button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void forwardButton_Click(object sender, EventArgs e)
		{
			try {
				ResetSensorLabels();
				WriteToLog("Moving forward 50mm\n");

				String r = client.SendCommand("m");

				// parse response
				try {
					String[] res = r.Split(' ');
					// todo handle rotations
					if (res.Length >= 1) {
						int dist = Int16.Parse(res[1]);
						Radar.BotDistX += dist;
					}
				} catch (Exception ex) {
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r+ "\n");
			} catch (Exception a) {
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}

		}

		/// <summary>
		/// Click handler for Macro Forward button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void macroForward_Click(object sender, EventArgs e)
		{
			try
			{
				ResetSensorLabels();
				WriteToLog("Moving forward 300mm\n");

				String r = client.SendCommand("M");

				// parse response
				try
				{
					String[] res = r.Split(' ');
					// todo handle rotations
					if (res.Length >= 1)
					{
						int dist = Int16.Parse(res[1]);
						Radar.BotDistX += dist;
					}
				}
				catch (Exception ex)
				{
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r + "\n");
			}
			catch (Exception a)
			{
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}

		}

		//Left Button
		/// <summary>
		/// Click handler for Left Button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void leftButton_Click(object sender, EventArgs e)
		{
			try {
				WriteToLog("Turning left\n");

				String r = client.SendCommand("l");

				// parse response
				try {
					String[] res = r.Split(' ');
					if (res.Length >= 1) {
						int lAmt = Int16.Parse(res[1]);
						// todo handle rotations
						//Radar.BotDistX += dist;
					}
				} catch (Exception ex) {
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r+ "\n");
			} catch (Exception a) {
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}

		}

		//Right Button
		/// <summary>
		/// Click Handler for Right 90 button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void rightButton_Click(object sender, EventArgs e)
		{
			try {
				WriteToLog("Turning right\n");

				String r = client.SendCommand("r");

				// parse response
				try {
					String[] res = r.Split(' ');
					if (res.Length >= 1) {
						int rAmt = Int16.Parse(res[1]);
						// todo handle rotations
						//if (Radar.BotDegY + rAmt < 180)
						//Radar.BotDistX += dist;
					}
				} catch (Exception ex) {
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r + "\n");
			} catch (Exception a) {
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}
		}

		/// <summary>
		/// Click handler for Left 90 button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void left90_Click(object sender, EventArgs e)
		{
			try
			{
				WriteToLog("Turning left\n");

				String r = client.SendCommand("L");

				// parse response
				try
				{
					String[] res = r.Split(' ');
					if (res.Length >= 1)
					{
						int lAmt = Int16.Parse(res[1]);
						// todo handle rotations
						//Radar.BotDistX += dist;
					}
				}
				catch (Exception ex)
				{
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r + "\n");
			}
			catch (Exception a)
			{
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}
		}

		/// <summary>
		/// Click handler for Right 90 Button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void right90_Click(object sender, EventArgs e)
		{
			try
			{
				WriteToLog("Turning right 90\n");

				String r = client.SendCommand("R");

				// parse response
				try
				{
					String[] res = r.Split(' ');
					if (res.Length >= 1)
					{
						int lAmt = Int16.Parse(res[1]);
						// todo handle rotations
						//Radar.BotDistX += dist;
					}
				}
				catch (Exception ex)
				{
					SocketClient.WriteException(ex);
				}

				WriteToLog("Command Response: " + r + "\n");
			}
			catch (Exception a)
			{
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}

		}

		/// <summary>
		/// Click Handler for Scan button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void scanButton_Click(object sender, EventArgs e)
		{
			Radar.ClearData();
			WriteToLog("Scanning...\n");
			client.WriteLine("S");
		}

		/// <summary>
		/// Click handler for getData button.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void getData_Click(object sender, EventArgs e)
		{
			WriteToLog("Sending Data...\n");
			client.WriteLine("D");
		}
		// ----- END MOVEMENT BUTTONS ----------------------------------------------------------------------------------


		// ----- TOOL STRIP --------------------------------------------------------------------------------------------

		/// <summary>
		/// Handler for Clear Graph Menu Item
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void clearGraphToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			WriteToLog("Clearing log data...\n");
			Radar.ClearData();
			ResetSensorLabels();
		}

		/// <summary>
		/// Handler for Clear Logs Menu Item
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			logBox.Items.Clear();
		}

		/// <summary>
		/// Handler for Exit Menu Item
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExitApplication();
		}

		/// <summary>
		/// Handler for "Save Graph to PNG" menu item.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void saveGraphPNGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var pngExporter = new OxyPlot.WindowsForms.PngExporter { Width = 600, Height = 400, Background = OxyPlot.OxyColors.White };
			var bitmap = pngExporter.ExportToBitmap(Radar.GetPlotModel());
			Clipboard.SetImage(bitmap);
			pngExporter.ExportToFile(Radar.GetPlotModel(), "graphPlot.jpg");
		}

		/// <summary>
		/// Click handler for Radar Plot.
		/// If you click on the graph it will copy it to clipboard.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void radarPlot_Click(object sender, EventArgs e)
		{
			var pngExporter = new OxyPlot.WindowsForms.PngExporter { Width = 600, Height = 400, Background = OxyPlot.OxyColors.White };
			var bitmap = pngExporter.ExportToBitmap(Radar.GetPlotModel());
			Clipboard.SetImage(bitmap);
		}

		/// <summary>
		/// Click handler for "Play Song 1" menu item.
		/// Will play the Smash Bros Melee Theme.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void playSong1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteToLog("Playing Melee Theme...\n");
			client.WriteLine("X");
		}

		/// <summary>
		/// Click handler for "Play Song 2" menu item.
		/// Will play the Gamecube boot song.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void playSong2ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			WriteToLog("Playing Gamecube Theme...\n");
			client.WriteLine("Y");
		}

		//Does a little dance and plays our two songs
		/// <summary>
		/// Click handler for "Victory Dance" menu item.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void victoryDanceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteToLog("We think we are winning\n");
			client.WriteLine("V");
		}

		/// <summary>
		/// Click handler for "Play Song 3" menu item.
		/// Should have played All Stars by smash mouth but never worked, on the bot side, weirdly.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void playSong3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Song never loaded correctly on the bot, so DEPRECIATED.
			WriteToLog("All Stars\n");
			client.WriteLine("Z");
		}

		// ----- END TOOL STRIP ----------------------------------------------------------------------------------------


		// ----- LOGBOX ------------------------------------------------------------------------------------------------
		/// <summary>
		/// Selected index changed handler for logBox.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void logBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// DoubleClick handler for logBox.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void logBox_DoubleClick(object sender, EventArgs e)
		{

		}
		// ----- END LOGBOX --------------------------------------------------------------------------------------------


		// ----- CONNECTION IP -----------------------------------------------------------------------------------------
		/// <summary>
		/// Mask input handler for Connection IP.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void connectionIP_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{

		}

		/// <summary>
		/// KeyDown handler for Connection IP.
		/// </summary>
		/// <param name="sender">Sender Object.</param>
		/// <param name="e">EventArgs.</param>
		private void connectionIP_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				e.SuppressKeyPress = true;
				//ConnectButton_Click(sender, e);
			}
		}
		// ----- END CONNECTION IP -------------------------------------------------------------------------------------

	}
}
