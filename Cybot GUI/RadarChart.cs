using System;
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
			#region debug
			// https://github.com/oxyplot/oxyplot/blob/release/v1.0.0/Source/Examples/ExampleLibrary/Axes/PolarPlotExamples.cs#L129
			Model = new PlotModel {
				Title = "Semi-circle polar plot",
				PlotType = PlotType.Polar,
				PlotAreaBorderThickness = new OxyThickness(0),
				PlotMargins = new OxyThickness(60, 20, 4, 40)
			};
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
			Model.Axes.Add(new MagnitudeAxis {
				Minimum = 0,
				Maximum = 1,
				MajorGridlineStyle = LineStyle.Solid,
				MinorGridlineStyle = LineStyle.Solid
			});
			Model.Series.Add(new FunctionSeries(x => Math.Sin(x / 180 * Math.PI), t => t, 0, 180, 0.01));
			#endregion //debug

			// update model
			Refresh();
		}

		/// <summary>
		/// Refresh the Plot.
		/// </summary>
		public void Refresh()
		{
			Plot.Model = Model;
		}

		/// <summary>
		/// Add data to the graph.
		/// </summary>
		/// <param name="s">S.</param>
		public void AddData(string s)
		{
			// see the following for performance...
			// http://docs.oxyplot.org/en/latest/guidelines/performance.html
			// https://github.com/oxyplot/oxyplot/blob/release/v1.0.0/Source/Examples/ExampleLibrary/Examples/ItemsSourceExamples.cs
			//TODO
			ProcessData(s);
		}

		/// <summary>
		/// Clears the data from the graph.
		/// </summary>
		public void ClearData()
		{
			//TODO
		}

		/// <summary>
		/// Processes the data.
		/// </summary>
		/// <param name="s">Data</param>
		private void ProcessData(string s)
		{
			log.Report("Radar GOT DATA: " + s);
			String[] data = s.Split(' ');
			if (data.Length != 5); //error
			//TODO
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
