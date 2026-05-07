using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a data series in an XY chart, containing a name, type, and a collection of points.
    /// </summary>
    public class XYSeries
    {
        private ChartXAxis _xAxis;

        /// <summary>
        /// Gets or sets the type of the series (e.g., line, bar, scatter).
        /// </summary>
        public XYSeriesType Type { get; set; }

        /// <summary>
        /// Gets the collection of points in the series.
        /// </summary>
        public List<double> Points { get; } = new List<double>();

		/// <summary>
		/// Initializes a new instance of the <see cref="XYSeries"/> class.
		/// </summary>
		/// <param name="type">The type of the series.</param>
		/// <param name="points">An optional collection of points to initialize the series with.</param>
		public XYSeries(XYSeriesType type, IEnumerable<double> points = null)
        {
            Type = type;

            if (points == null)
				points = new List<double>();

			Points.AddRange(points);
		}

        /// <summary>
        /// Sets the collection of labels for the series.
        /// </summary>
        /// <param name="xAxis">The X-axis configuration containing the labels to set for the series. If null, an empty X-axis configuration is used.</param>
        /// <returns>The current instance of the series with updated labels.</returns>
        internal XYSeries SetXAxis(ChartXAxis xAxis)
        {
            _xAxis = xAxis;
            return this;
        }

        /// <summary>
        /// Adds a point to the series.
        /// </summary>
        /// <param name="point">The point to add.</param>
        /// <returns>The current instance for method chaining.</returns>
        public XYSeries AddPoint(double point)
        {
            Points.Add(point);
            return this;
        }

        /// <summary>
        /// Adds one or more points to the series.
        /// </summary>
        /// <param name="points">Points to add to the series.</param>
        /// <returns>The current instance for method chaining.</returns>
        public XYSeries AddPoints(params double[] points)
		{
			Points.AddRange(points);
			return this;
		}

		/// <summary>
		/// Sets the value of the point at the specified index, expanding the collection with zeros if necessary.
		/// </summary>
		/// <remarks>If the specified index is greater than the current number of points, the collection
		/// is automatically expanded and new points are initialized to zero.</remarks>
        /// <param name="index">The zero-based index of the point to set. Must be non-negative.</param>
        /// <param name="value">The value to assign to the point at the specified index.</param>
        /// <returns>The current instance for method chaining.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when index is less than zero.</exception>
		public XYSeries SetPoint(int index, double value)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");

            while (Points.Count <= index)
            {
                Points.Add(0);
            }
            Points[index] = value;
            return this;
        }

        /// <summary>
        /// Sets the value of the point identified by the specified key.
        /// </summary>
        /// <param name="key">The label key that identifies the point to set. Must exist in the labels collection.</param>
        /// <param name="value">The value to assign to the point associated with the specified key.</param>
        /// <returns>The current instance with the updated point value.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the labels collection has not been set before calling this method.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified key does not exist in the labels collection.</exception>
        public XYSeries SetPoint(string key, double value)
        {
            if (_xAxis == null || _xAxis.Labels == null)
                throw new InvalidOperationException("Labels must be set before using string keys.");
            int index = _xAxis.Labels.IndexOf(key);
            if (index == -1)
                throw new ArgumentException($"Key '{key}' not found in labels.", nameof(key));
            return SetPoint(index, value);
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return $"{Type.PrimaryString()} [{string.Join(", ", Points.Select(i => i.ToString("G", CultureInfo.InvariantCulture)))}]";
        }
    }
}
