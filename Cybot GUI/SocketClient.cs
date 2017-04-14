using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cybot_GUI
{
	public class SocketClient
	{

		/// <summary>
		/// Get or set the port to connect to.
		/// </summary>
		public int Port;

		static Exception lastException;
		IPAddress ip;
		Socket socket = null;

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
		/// Thread that processes received data
		/// </summary>
		/// <param name="output">Output.</param>
		public void ReceiveThread(IProgress<string> output, CancellationToken ct)
		{
			if (socket == null || !socket.Connected) return;

			// check for data as long as we have no cancelation token and the socket is still alive
			// http://stackoverflow.com/a/2661876
			while (!ct.IsCancellationRequested && !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0)) {
				byte[] bytes = new byte[256];
				int size;
				try {
					// receive data (blocking)
					size = socket.Receive(bytes);

					// only report data if buffer is >0
					if (size > 0) {
						String inputSerial = Encoding.UTF8.GetString(bytes);
						output.Report(inputSerial);
					}
				} catch (Exception ex) {
					WriteException(ex);
				}
			}

			output.Report("Connection ended.");
		}

		/// <summary>
		/// Utility to write an exception to the console.
		/// </summary>
		/// <param name="ex">Exception.</param>
		public static void WriteException(Exception ex)
		{
			lastException = ex;
			Console.WriteLine();
			Console.WriteLine("SocketClient Caught Exception : " + ex.Message);
			Console.WriteLine(ex);
			Console.WriteLine();
		}
	}
}
