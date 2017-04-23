using OxyPlot.WindowsForms;
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

			var log = new Progress<string>(s => WriteToLog(s + "\n", true));
			Radar = new RadarChart(radarPlot, log);

			SetMovementControlsEnabled(false);

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

		private void Window_Load(object sender, EventArgs e)
		{

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
					WriteToLog(client.LastErrorMessage + ".\n");
					ConnectButton.Text = connectText;
				} else {
					// actions to perform after successful connection
					// http://stackoverflow.com/a/18033198
					SetMovementControlsEnabled(true);
					WriteToLog("Success.\n");

					var log = new Progress<string>(s => WriteToLog(s + "\n", true));
					var scandata = new Progress<string>(Radar.AddData);
					await Task.Factory.StartNew(() => client.ReceiveThread(log, scandata, ReceiveThreadCancel.Token), TaskCreationOptions.LongRunning);
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
			rightButton.Enabled = b;
			leftButton.Enabled = b;
		}


		// ----- MOVEMENT BUTTONS --------------------------------------------------------------------------------------
		//Forward Button
		private void forwardButton_Click(object sender, EventArgs e)
		{
			try {
				WriteToLog("Moving forward\n");

				String r = client.SendCommand("M");

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

		//Left Button
		private void leftButton_Click(object sender, EventArgs e)
		{
			try {
				WriteToLog("Turning left\n");

				String r = client.SendCommand("L");

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
		private void rightButton_Click(object sender, EventArgs e)
		{
			try {
				WriteToLog("Turning right\n");

				String r = client.SendCommand("R");

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

				WriteToLog("Command Response: " + r+ "\n");
			} catch (Exception a) {
				WriteToLog("INVALID FORMAT " + a.Message + "\n");
			}
		}

		//Scan Button
		private void scanButton_Click(object sender, EventArgs e)
		{
			Radar.ClearData();
			WriteToLog("Scanning...\n");
			client.WriteLine("S");
		}

		private void getData_Click(object sender, EventArgs e)
		{
			WriteToLog("Sending Data...\n");
			client.WriteLine("D");
		}
		// ----- END MOVEMENT BUTTONS ----------------------------------------------------------------------------------


		// ----- TOOL STRIP --------------------------------------------------------------------------------------------
		//Clear Graph
		private void clearGraphToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			WriteToLog("Clearing log data...\n");
			Radar.ClearData();
		}

		//Clear Logs
		private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			logBox.Items.Clear();
		}

		//Exit
		private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExitApplication();
		}

		//Save Graph to PNG
		private void saveGraphPNGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var pngExporter = new OxyPlot.WindowsForms.PngExporter { Width = 600, Height = 400, Background = OxyPlot.OxyColors.White };
			var bitmap = pngExporter.ExportToBitmap(Radar.GetPlotModel());
			Clipboard.SetImage(bitmap);
			pngExporter.ExportToFile(Radar.GetPlotModel(), "graphPlot");
		}
		// ----- END TOOL STRIP ----------------------------------------------------------------------------------------


		// ----- LOGBOX ------------------------------------------------------------------------------------------------
		private void logBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void logBox_DoubleClick(object sender, EventArgs e)
		{

		}
		// ----- END LOGBOX --------------------------------------------------------------------------------------------


		// ----- CONNECTION IP -----------------------------------------------------------------------------------------
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
		// ----- END CONNECTION IP -------------------------------------------------------------------------------------


		private void radarPlot_Click(object sender, EventArgs e)
		{
			var pngExporter = new OxyPlot.WindowsForms.PngExporter { Width = 600, Height = 400, Background = OxyPlot.OxyColors.White };
			var bitmap = pngExporter.ExportToBitmap(Radar.GetPlotModel());
			Clipboard.SetImage(bitmap);
		}

		private void playSong1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteToLog("Playing Melee Theme...\n");
			client.WriteLine("X");
		}
		private void playSong2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteToLog("Playing Gamecube Theme...\n");
			client.WriteLine("Y");
		}

	}
}
