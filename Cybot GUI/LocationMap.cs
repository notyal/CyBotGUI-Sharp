using System;
namespace Cybot_GUI
{
	public class LocationMap
	{
		public LocationMap()
		{
		}

		/// <summary>
		/// Processes the sensordata.
		/// </summary>
		/// <param name="s">sensordata.</param>
		/// <param name="log">log output.</param>
		public static void ProcessData(string s,
		                               IProgress<string> log,
		                               IProgress<UInt16> linesensor,
		                               IProgress<UInt16> bumplightsensor)
		{
			try {
				string[] data = s.Split(' ');
				switch (data[0][0]) {
					// Cliff Sensor
					case 'C': {
							if (data.Length != 5) DataLengthIncorrect(data);
							bool left = bool.Parse(data[1]);
							bool frontLeft = bool.Parse(data[2]);
							bool frontRight = bool.Parse(data[3]);
							bool right = bool.Parse(data[4]);
							// todo handle
							break;
						}

					// LiNe Sensor
					case 'N': {
							if (data.Length != 2) DataLengthIncorrect(data);
							UInt16 type = UInt16.Parse(data[1]);

							//UInt16 left = UInt16.Parse(data[1]);
							//UInt16 frontLeft = UInt16.Parse(data[2]);
							//UInt16 frontRight = UInt16.Parse(data[3]);
							//UInt16 right = UInt16.Parse(data[4]);

							// todo handle
							linesensor.Report(type);
							//switch (type) {
							//	case 1:
							//		// left
							//		break;
							//	case 2:
							//		// front left
							//		break;
							//	case 3:
							//		// front right
							//		break;
							//	case 4:
							//		// right
							//		break;
							//}

							break;
						}

					// Bump LiGht Sensor
					case 'G': {
							if (data.Length != 2) DataLengthIncorrect(data);
							UInt16 type = UInt16.Parse(data[1]);

							//UInt16 left = UInt16.Parse(data[1]);
							//UInt16 frontLeft = UInt16.Parse(data[2]);
							//UInt16 centerLeft = UInt16.Parse(data[3]);
							//UInt16 centerRight = UInt16.Parse(data[4]);
							//UInt16 frontRight = UInt16.Parse(data[5]);
							//UInt16 right = UInt16.Parse(data[6]);

							// todo handle
							bumplightsensor.Report(type);
							//switch (type) {
							//	case 1:
							//		// left
							//		break;
							//	case 2:
							//		// front left
							//		break;
							//	case 3:
							//		// center left
							//		break;
							//	case 4:
							//		// center right
							//		break;
							//	case 5:
							//		// front right
							//		break;
							//	case 6:
							//		// right
							//		break;
							//}
							break;
						}

					// Bump Sensor
					case 'B': {
							if (data.Length != 3) DataLengthIncorrect(data);
							bool left = bool.Parse(data[1]);
							bool right = bool.Parse(data[2]);
							// todo handle
							break;
						}
				}
			} catch (Exception ex) {
				SocketClient.WriteException(ex);
			}

		}

		/// <summary>
		/// Throws exception for use when the data format is incorrect.
		/// </summary>
		/// <param name="data">Data that was incorrect.</param>
		public static void DataLengthIncorrect(string[] data)
		{
			throw new FormatException(String.Format("[{0}] data is incorrect, length is {1}", data[0], data.Length));
		}
	}
}
