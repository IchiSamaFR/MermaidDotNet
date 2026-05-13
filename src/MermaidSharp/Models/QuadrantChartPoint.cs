using System;
using System.Globalization;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a data point in a Mermaid quadrant chart.
    /// </summary>
    public class QuadrantChartPoint
    {
        /// <summary>
        /// Gets or sets the label of the point.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the point. Value must be between 0 and 1.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the point. Value must be between 0 and 1.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the fill color of the point (e.g. #ff3300).
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the radius of the point.
        /// </summary>
        public int? Radius { get; set; }

        /// <summary>
        /// Gets or sets the border color of the point.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the border width of the point (e.g. 5px).
        /// </summary>
        public string StrokeWidth { get; set; }

        /// <summary>
        /// Returns the Mermaid syntax representation of the point.
        /// </summary>
        /// <returns>A string in Mermaid quadrant chart point syntax.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <see cref="Label"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <see cref="Label"/> is empty or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <see cref="X"/> or <see cref="Y"/> is not a finite number in the range [0, 1].</exception>
        public override string ToString()
        {
            if (Label == null)
                throw new ArgumentNullException(nameof(Label));
            if (string.IsNullOrWhiteSpace(Label))
                throw new ArgumentException("Point label cannot be empty or whitespace.", nameof(Label));
            if (double.IsNaN(X) || double.IsInfinity(X) || X < 0 || X > 1)
                throw new ArgumentOutOfRangeException(nameof(X), "Point X coordinate must be a finite number between 0 and 1.");
            if (double.IsNaN(Y) || double.IsInfinity(Y) || Y < 0 || Y > 1)
                throw new ArgumentOutOfRangeException(nameof(Y), "Point Y coordinate must be a finite number between 0 and 1.");
            var style = BuildStyle();
            string xStr = X.ToString("G", CultureInfo.InvariantCulture);
            string yStr = Y.ToString("G", CultureInfo.InvariantCulture);
            return string.IsNullOrEmpty(style)
                ? $"{Label}: [{xStr}, {yStr}]"
                : $"{Label}: [{xStr}, {yStr}] {style}";
        }

        private string BuildStyle()
        {
            var parts = new System.Collections.Generic.List<string>();

            if (!string.IsNullOrEmpty(Color))
                parts.Add($"color: {Color}");
            if (Radius.HasValue)
                parts.Add($"radius: {Radius.Value}");
            if (!string.IsNullOrEmpty(StrokeColor))
                parts.Add($"stroke-color: {StrokeColor}");
            if (!string.IsNullOrEmpty(StrokeWidth))
                parts.Add($"stroke-width: {StrokeWidth}");

            return string.Join(", ", parts);
        }
    }
}