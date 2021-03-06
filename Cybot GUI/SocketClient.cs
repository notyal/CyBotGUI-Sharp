﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cybot_GUI
{
	/// <summary>
	/// Handles serial data transmission over a socket.
	/// </summary>
	public class SocketClient
	{
		/// <summary>
		/// Get or set the port to connect to.
		/// </summary>
		public int Port;

		/// <summary>
		/// The command timeout in ms. Default is 10000ms.
		/// </summary>
		public uint CommandTimeout = 3000;

		/// <summary>
		/// The last exception that was thrown.
		/// </summary>
		public static Exception lastException;

		private IPAddress ip;
		private Socket socket = null;
		private volatile bool AwaitingCommand;
		private volatile String AwaitedCommand;

		/// <summary>
		/// Get or set the Host IP address as a string.
		/// </summary>
		/// <value>The IP Address.</value>
		public string Ip {
			get { return ip.ToString(); }

			set {
				if (!IPAddress.TryParse(value, out ip))
					throw new Exception("Error: IP Address format could not be parsed.");
			}
		}

		/// <summary>
		/// Gets the last error message.
		/// Only updated when an exception is thrown.
		/// Not updated upon success.
		/// </summary>
		/// <value>The error message if it exists, otherwise an empty string.</value>
		public string LastErrorMessage {
			get {
				if (lastException == null) return "";
				return lastException.Message;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CyBotGtk.SocketClient"/> class
		/// with a default port of 42880.
		/// </summary>
		/// <param name="Host">Host IP Address.</param>
		/// <param name="Port">Port if not 42880.</param>
		public SocketClient(string Host, int Port = 42880)
		{
			Ip = Host;
			this.Port = Port;
		}

		/// <summary>
		/// Attempt to connect to the server.
		/// </summary>
		/// <returns><c>true</c>, if connected successfully, <c>false</c> otherwise.</returns>
		public bool Connect()
		{
			try {
				// make sure things are reset
				AwaitingCommand = false;
				AwaitedCommand = "";

				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect(ip, Port);
			} catch (SocketException ex) {
				lastException = ex;
			} catch (Exception ex) {
				WriteException(ex);
			}

			if (socket == null) return false;
			return socket.Connected;
		}

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		/// <returns><c>true</c>, if not connected to server, <c>false</c> otherwise.</returns>
		public bool Disconnect()
		{
			if (socket == null) return true;

			// close socket without needing to reuse it
			try {
				socket.Disconnect(false);
			} catch (Exception ex) {
				WriteException(ex);
				return false;
			}

			return !socket.Connected;
		}

		/// <summary>
		/// If connected to server.
		/// </summary>
		/// <returns><c>true</c>, if connected to server, <c>false</c> otherwise.</returns>
		public bool IsConnected()
		{
			if (socket == null) return false;
			return socket.Connected;
		}

		/// <summary>
		/// Write the specified message to the server.
		/// </summary>
		/// <returns>If write was successful.</returns>
		/// <param name="msg">Message to write.</param>
		public bool Write(string msg)
		{
			if (socket == null || !socket.Connected) return false;

			try {
				socket.Send(Encoding.ASCII.GetBytes(msg));
				return true;
			} catch (Exception ex) {
				WriteException(ex);
				return false;
			}
		}


		/// <summary>
		/// Writes a message with a newline to the server.
		/// </summary>
		/// <returns><c>true</c>, if write was successful, <c>false</c> otherwise.</returns>
		/// <param name="msg">Message.</param>
		public bool WriteLine(string msg)
		{
			return Write(msg + "\n");
		}

		/// <summary>
		/// Sends command and waits for a reply.
		/// </summary>
		/// <returns>Reply from server in response to command.</returns>
		/// <param name="msg">Command string.</param>
		public String SendCommand(string msg)
		{
			AwaitingCommand = true;
			WriteLine(msg);

			uint time = 0;
			while (AwaitingCommand && IsConnected() && time < CommandTimeout) {
				Thread.Sleep(1);
				time++;
			}
			if (time >= CommandTimeout) return "TIMEOUT:" + time + "\n";
			return AwaitedCommand;
		}

		/// <summary>
		/// Checks the socket status; also checks the CancellationToken
		/// </summary>
		/// <returns><c>true</c>, if socket is still open, <c>false</c> otherwise.</returns>
		/// <param name="ct">Cancelation token.</param>
		private bool CheckSocketStatus(CancellationToken ct)
		{
			return (!ct.IsCancellationRequested && !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0));
		}

		/// <summary>
		/// Thread that processes received data
		/// </summary>
		/// <param name="log">Log output.</param>
		/// <param name="scandata">Raw scandata.</param>
		/// <param name="ct">Token to cancel the thread.</param>
		/// <param name="sensordata">Optional: Additonal Sensor Data.</param>
		public void ReceiveThread(IProgress<string> log, IProgress<string> scandata, CancellationToken ct, IProgress<string> sensordata = null)
		{
			if (socket == null || !socket.Connected) return;

			// check for data as long as we have no cancelation token and the socket is still alive
			// http://stackoverflow.com/a/2661876
			while (CheckSocketStatus(ct)) {
				try {
					byte[] bytes = new byte[1];
					string serialData = "";

					while (!serialData.Contains("\n") && CheckSocketStatus(ct)) {
						// receive data (blocking)
						socket.Receive(bytes);
						serialData += Encoding.ASCII.GetString(bytes);
					}

					Console.WriteLine(serialData);
					ProcessData(log, scandata, serialData, sensordata);


					//ProcessData(log, scandata, bytes, size);
				} catch (SocketException ex) when (ex.ErrorCode == 10038) {
					// ignore "The descriptor is not a socket"
					// it usually happens when we kill the thread
				} catch (SocketException ex) {
					lastException = ex;
					Console.WriteLine("SocketException Code: " + ex.ErrorCode);
					Console.WriteLine(ex);
				} catch (Exception ex) {
					WriteException(ex);
				}
			}

			log.Report("Connection ended.\n");
		}

		/// <summary>
		/// Processes the scan data.
		/// </summary>
		/// <param name="log">Log.</param>
		/// <param name="scandata">Scan Data.</param>
		/// <param name="inputSerial">Input serial.</param>
		/// <param name="sensordata">Optional: Additonal Sensor Data.</param>
		private void ProcessData(IProgress<string> log, IProgress<string> scandata, string inputSerial, IProgress<string> sensordata = null)
		{
			// handle scandata
			// having this before AwaitingCommand will allow us to process the data as it comes in
			//   even if it comes before the awaited command
			switch (inputSerial.ToCharArray(0, 1)[0]) {
				case 'S': {
						scandata.Report(inputSerial);
						return;
					}
				//case 'C':
				case 'N':
				case 'G':
				//case 'B': 
					{
						if (sensordata != null) sensordata.Report(inputSerial);
						else log.Report("[unprocessed sensor]: " + inputSerial + "\n");
						return;
					}
			}

			// handle if we are awaiting a command
			// *not* recommended for scan data
			if (AwaitingCommand) {
				AwaitedCommand = inputSerial;
				AwaitingCommand = false;
				return;
			}

			// log unprocessed data
			log.Report("[unprocessed]: " + inputSerial + "\n");
		}

		/// <summary>
		/// Utility to write an exception to the console.
		/// </summary>
		/// <param name="ex">Exception.</param>
		public static void WriteException(Exception ex)
		{
			lastException = ex;
			Console.WriteLine();
			Console.WriteLine("[sc]Caught Exception: " + ex.Message);
			Console.WriteLine(ex);
			Console.WriteLine();
		}
	}
}
