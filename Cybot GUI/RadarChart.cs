using System;
using System.Collections.Generic;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
			public uint Dist;
		}

		PlotView Plot;
		PlotModel Model;
		IProgress<string> log;

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
				PlotMargins = new OxyThickness(5, 20, 5, 5)
			};

			// setup max angle (y-axis)
			Model.Axes.Add(
				new AngleAxis {
					Minimum = 0,
					Maximum = 180,
					MajorStep = 45,
					MinorStep = 9,
					StartAngle = 0,
					EndAngle = 180,
					MajorGridlineStyle = LineStyle.Solid,
					MinorGridlineStyle = LineStyle.Solid
				});

			// set max distance (x-axis)
			Model.Axes.Add(new MagnitudeAxis {
				Minimum = 20,
				Maximum = 100,
				MajorGridlineStyle = LineStyle.Solid,
				MinorGridlineStyle = LineStyle.Solid
			});
			//Model.Series.Add(new FunctionSeries(x => Math.Sin(x / 180 * Math.PI), t => t, 0, 180, 0.01));
			Plot.Model = Model;
			// update model
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
			// nvm, it is fast as hell
			// https://github.com/oxyplot/oxyplot/tree/develop/Source/Examples/WPF/WpfExamples/Examples/RealtimeDemo
			// http://docs.oxyplot.org/en/latest/guidelines/performance.html
			// https://github.com/oxyplot/oxyplot/blob/release/v1.0.0/Source/Examples/ExampleLibrary/Examples/ItemsSourceExamples.cs

			// attempt to process the data
			try {
				ScanData d = ProcessData(s);
				log.Report(String.Format("F:{0} T:{1} D:{2}", d.DegBegin, d.DegEnd, d.Dist));

				// TODO add points to graph
				LineSeries a = new LineSeries();
				a.Points.Add(new DataPoint(d.Dist, d.DegBegin));
				a.Points.Add(new DataPoint(d.Dist, d.DegEnd));
				Model.Series.Add(a);
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
			ret.Dist = UInt16.Parse(data[3]);
			return ret;


		}
		/// <summary>
		/// Saves graph and openes file dialog to choose save location.
		/// </summary>
		public void CreateImage(string fileName, PlotModel plotModel)
		{
			var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
			pngExporter.ExportToFile(plotModel, fileName);
		}
	}
}
