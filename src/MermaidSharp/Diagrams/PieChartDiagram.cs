using MermaidSharp.Configs;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
	/// <summary>
	/// Represents a Mermaid pie chart diagram, composed of labeled slices with associated numeric values.
	/// </summary>
	/// <remarks>
	/// Use this class to build and render pie chart diagrams by programmatically adding slices.
	/// Supports an optional title and an optional <c>showData</c> flag that renders the actual
	/// data values next to the legend labels.
	/// </remarks>
	public class PieChartDiagram : AGraph<PieChartConfig>
	{
		/// <summary>
		/// Gets the Mermaid keyword for the pie chart diagram type.
		/// </summary>
		protected override string Name => "pie";

		/// <summary>
		/// Gets or sets a value indicating whether the actual data values are rendered after each legend label.
		/// </summary>
		public bool ShowData { get; set; }

		/// <summary>
		/// Gets the collection of slices contained in this pie chart.
		/// </summary>
		public List<PieSlice> Slices { get; } = new List<PieSlice>();

		/// <summary>
		/// Initializes a new instance of the PieChartDiagram class with the specified title, ShowData option, and configuration.
		/// </summary>
		/// <param name="title">The title of the pie chart. If not specified, no title is rendered.</param>
		/// <param name="showData">
		/// A value indicating whether data values are displayed next to the legend labels. Defaults to <c>false</c>.
		/// </param>
		/// <param name="config">The configuration settings to apply to the pie chart. If null, default settings are used.</param>
		public PieChartDiagram(string title = "", bool showData = false, PieChartConfig config = null) : base(title, config)
		{
			ShowData = showData;
			Config.ThemeVariables.PieSlices = Slices;
		}

		/// <summary>
		/// Adds a slice to the pie chart with the specified label and value.
		/// </summary>
		/// <param name="label">The display label for the slice.</param>
		/// <param name="value">The positive numeric value for the slice. Supported up to two decimal places.</param>
		/// <param name="color">The optional color for the slice. If not specified, the default color is used.</param>
		/// <returns>The current <see cref="PieChartDiagram"/> instance, enabling method chaining.</returns>
		/// <exception cref="ArgumentNullException">Thrown when label is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when value is not greater than zero.</exception>
		public PieChartDiagram AddSlice(string label, double value, string color = "")
		{
			Slices.Add(new PieSlice(label, value, color));
			return this;
		}

		/// <summary>
		/// Retrieves the string representation of the current configuration header.
		/// </summary>
		/// <returns>A string that represents the configuration header, or null if the configuration is not set.</returns>
		protected override string GetHeaderString()
		{
			return Config?.ToString();
		}

		/// <summary>
		/// Generates the complete Mermaid pie chart diagram as a formatted string.
		/// </summary>
		/// <returns>A string containing the full Mermaid pie chart diagram.</returns>
		public override string CalculateDiagram()
		{
			var lines = new List<string>();

			lines.Add(GetHeaderString());

			var header = ShowData ? $"{Name} showData" : Name;
			lines.Add(header);

			if (!string.IsNullOrEmpty(Title))
				lines.Add($"title {Title}".Indent());

			lines.AddRange(Slices.Select(s => s.ToString()).Indent());

			return string.Join(System.Environment.NewLine, lines.ClearNewLines());
		}
	}
}
