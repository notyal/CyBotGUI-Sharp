using System;
using System.Collections.Generic;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;

namespace Cybot_GUI
{
	/// <summary>
	/// Handles data processing from a SocketClient to a RadarChart.
	/// </summary>
	public class RadarChart
	{
		/// <summary>
		/// Scan data.
		/// </summary>
		struct ScanData
		{
			public uint DegBegin;
			public uint DegEnd;
			public double Dist;
		}

		PlotView Plot;
		PlotModel Model;
		ScatterSeries botPosition;
		IProgress<string> log;
		double distX;
		double degY;

		/// <summary>
		/// Gets or sets the bot distance position from the initial scan.
		/// </summary>
		/// <value>The bot distance x-value.</value>
		public double BotDistX {
			get { return distX; }
			set { SetBotPosition(value, degY); }
		}

		/// <summary>
		/// Gets or sets the bot degree position from the initial scan.
		/// </summary>
		/// <value>The bot degree y-value.</value>
		public double BotDegY {
			get { return degY; }
			set { SetBotPosition(distX, value); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Cybot_GUI.RadarChart"/> class.
		/// </summary>
		/// <param name="Plot">PlotView.</param>
		/// <param name="log">Log output.</param>
		public RadarChart(PlotView Plot, IProgress<string> log)
		{
			this.Plot = Plot;
			this.log = log;

			PlotInit();
		}

		private void PlotInit()
		{
			// https://github.com/oxyplot/oxyplot/blob/release/v1.0.0/Source/Examples/ExampleLibrary/Axes/PolarPlotExamples.cs#L129

			// setup plot
			Model = new PlotModel {
				Title = "Radar Data",
				PlotType = PlotType.Polar,
				PlotAreaBorderThickness = new OxyThickness(0),
				PlotMargins = new OxyThickness(20, 30, 20, 5)
			};

			// setup max angle (y-axis)
			Model.Axes.Add(new AngleAxis {
				Minimum = 0,
				Maximum = 180,
				MajorStep = 10,
				MinorStep = 5,
				StartAngle = 0,
				EndAngle = 180,
				MajorGridlineStyle = LineStyle.Solid,
				MinorGridlineStyle = LineStyle.Solid,
			});

			// set max distance (x-axis)
			Model.Axes.Add(new MagnitudeAxis {
				Minimum = 15,
				Maximum = 100,
				MajorStep = 10,
				MinorStep = 5,
				MajorGridlineStyle = LineStyle.Solid,
				MinorGridlineStyle = LineStyle.Solid
			});

			Plot.Model = Model;

			// update model
			Refresh();

			botPosition = new ScatterSeries();

			// set default position
			SetBotPosition(10, 90);
		}

		/// <summary>
		/// Sets the bot position. 90 deg is center
		/// </summary>
		/// <param name="distX">Dist x.</param>
		/// <param name="degY">Deg y.</param>
		public void SetBotPosition(double distX, double degY)
		{
			this.distX = distX;
			this.degY = degY;

			Model.Series.Remove(botPosition);
			botPosition.Points.Clear();

			botPosition.Points.Add(new ScatterPoint(distX, degY, 10));

			// set color
			botPosition.MarkerType = MarkerType.Circle;
			botPosition.MarkerFill = OxyColor.FromAColor(204, OxyColors.DarkSlateBlue); //alpha: 255 * .8 = 204
			botPosition.MarkerStroke = botPosition.MarkerFill.Complementary();

			Model.Series.Add(botPosition);
			Refresh();
		}

		/// <summary>
		/// Refresh the Plot.
		/// </summary>
		public void Refresh()
		{
			Model.InvalidatePlot(true);
		}

		/// <summary>
		/// Add data to the graph.
		/// </summary>
		/// <param name="s">S.</param>
		public void AddData(string s)
		{
			// see the following for performance...
			// https://github.com/oxyplot/oxyplot/tree/develop/Source/Examples/WPF/WpfExamples/Examples/RealtimeDemo
			// http://docs.oxyplot.org/en/latest/guidelines/performance.html
			// https://github.com/oxyplot/oxyplot/blob/release/v1.0.0/Source/Examples/ExampleLibrary/Examples/ItemsSourceExamples.cs

			// attempt to process the data
			try {
				ScanData d = ProcessData(s);
				log.Report(String.Format("F:{0} T:{1} D:{2}", d.DegBegin, d.DegEnd, d.Dist));

				// add points to graph
				LineSeries l = new LineSeries();
				l.Points.Add(new DataPoint(d.Dist, d.DegBegin));
				l.Points.Add(new DataPoint(d.Dist, d.DegEnd));
				Model.Series.Add(l);
				Refresh();
			} catch (FormatException ex) {
				log.Report(ex.Message);
			} catch (Exception ex) {
				SocketClient.WriteException(ex);
			}

		}

		/// <summary>
		/// Clears the data from the graph.
		/// </summary>
		public void ClearData()
		{
			Model.Series.Clear();
			
			// set default position
			SetBotPosition(10, 90);

			Refresh();
		}

		/// <summary>
		/// Processes the data.
		/// </summary>
		/// <param name="s">Data</param>
		private ScanData ProcessData(string s)
		{
			log.Report("Radar GOT DATA: " + s + "\n");
			String[] data = s.Split(' ');

			#region test
			//Console.WriteLine();
			//Console.WriteLine("Length: " + data.Length);
			//for (int i = 1; i < data.Length; i++) {
			//	Console.WriteLine(String.Format("\tdata[{0}]={1}", i, data[i]));
			//}
			//Console.WriteLine();
			#endregion

			// Length: 4
			//data[1] =[DEG_BEGIN]
			//data[2] =[DEG_END]
			//data[3] =[DIST]

			if (data.Length != 4) {
				throw new FormatException("Invalid scan data: incorrect length: " + data.Length);
			}

			ScanData ret;
			ret.DegBegin = UInt16.Parse(data[1]);
			ret.DegEnd = UInt16.Parse(data[2]);
			ret.Dist = double.Parse(data[3]);
			return ret;


		}
		/// <summary>
		/// Saves graph and openes file dialog to choose save location.
		/// </summary>
		public PlotModel GetPlotModel()
		{
			return Model;
		}
	}
}
