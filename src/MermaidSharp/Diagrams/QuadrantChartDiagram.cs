using MermaidSharp.Configs;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents a Mermaid quadrant chart diagram, including axes, quadrant labels, and data points.
    /// </summary>
    public class QuadrantChartDiagram : AGraph<QuadrantChartConfig>
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "quadrantChart";

        /// <summary>
        /// Gets or sets the left label of the x-axis.
        /// </summary>
        public string XAxisLeft { get; set; }

        /// <summary>
        /// Gets or sets the right label of the x-axis.
        /// </summary>
        public string XAxisRight { get; set; }

        /// <summary>
        /// Gets or sets the bottom label of the y-axis.
        /// </summary>
        public string YAxisBottom { get; set; }

        /// <summary>
        /// Gets or sets the top label of the y-axis.
        /// </summary>
        public string YAxisTop { get; set; }

        /// <summary>
        /// Gets or sets the label for quadrant 1 (top right).
        /// </summary>
        public string Quadrant1 { get; set; }

        /// <summary>
        /// Gets or sets the label for quadrant 2 (top left).
        /// </summary>
        public string Quadrant2 { get; set; }

        /// <summary>
        /// Gets or sets the label for quadrant 3 (bottom left).
        /// </summary>
        public string Quadrant3 { get; set; }

        /// <summary>
        /// Gets or sets the label for quadrant 4 (bottom right).
        /// </summary>
        public string Quadrant4 { get; set; }

        /// <summary>
        /// Gets the collection of data points in the quadrant chart.
        /// </summary>
        public List<QuadrantChartPoint> Points { get; } = new List<QuadrantChartPoint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadrantChartDiagram"/> class.
        /// </summary>
        /// <param name="title">The title of the chart.</param>
        /// <param name="config">The configuration for the chart.</param>
        public QuadrantChartDiagram(string title = "", QuadrantChartConfig config = null)
            : base(title, config)
        {
        }

        /// <summary>
        /// Generates the complete Mermaid quadrant chart as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid quadrant chart diagram.</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            lines.Add(Name);

            // x-axis
            if (!string.IsNullOrWhiteSpace(XAxisLeft) && !string.IsNullOrWhiteSpace(XAxisRight))
                lines.Add($"x-axis {XAxisLeft} --> {XAxisRight}".Indent());
            else if (!string.IsNullOrWhiteSpace(XAxisLeft))
                lines.Add($"x-axis {XAxisLeft}".Indent());
            else if (!string.IsNullOrWhiteSpace(XAxisRight))
                lines.Add($"x-axis --> {XAxisRight}".Indent());

            // y-axis
            if (!string.IsNullOrWhiteSpace(YAxisBottom) && !string.IsNullOrWhiteSpace(YAxisTop))
                lines.Add($"y-axis {YAxisBottom} --> {YAxisTop}".Indent());
            else if (!string.IsNullOrWhiteSpace(YAxisBottom))
                lines.Add($"y-axis {YAxisBottom}".Indent());
            else if (!string.IsNullOrWhiteSpace(YAxisTop))
                lines.Add($"y-axis --> {YAxisTop}".Indent());

            // Quadrant labels
            if (!string.IsNullOrWhiteSpace(Quadrant1))
                lines.Add($"quadrant-1 {Quadrant1}".Indent());
            if (!string.IsNullOrWhiteSpace(Quadrant2))
                lines.Add($"quadrant-2 {Quadrant2}".Indent());
            if (!string.IsNullOrWhiteSpace(Quadrant3))
                lines.Add($"quadrant-3 {Quadrant3}".Indent());
            if (!string.IsNullOrWhiteSpace(Quadrant4))
                lines.Add($"quadrant-4 {Quadrant4}".Indent());

            // Points
            foreach (var point in Points)
                lines.Add(point.ToString().Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}