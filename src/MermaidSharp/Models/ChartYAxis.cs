using System.Globalization;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents the configuration for a chart's Y-axis, including its title and value range.
    /// </summary>
    public class ChartYAxis
    {
        /// <summary>
        /// Gets or sets the title associated with the current object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the minimum allowable value.
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value allowed or applicable for the associated context.
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Initializes a new instance of the ChartYAxis class with the specified title and optional minimum and maximum
        /// values.
        /// </summary>
        /// <param name="title">The title to display for the Y-axis.</param>
        /// <param name="min">The minimum value to display on the Y-axis. If null, the minimum is determined automatically.</param>
        /// <param name="max">The maximum value to display on the Y-axis. If null, the maximum is determined automatically.</param>
        public ChartYAxis(string title, double? min = null, double? max = null)
        {
            Title = title;
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            var returned = "y-axis";
            if (!string.IsNullOrEmpty(Title))
            {
                returned += $" \"{Title}\"";
            }

            if (Min.HasValue && Max.HasValue)
            {
                returned += $" {Min.Value.ToString("G", CultureInfo.InvariantCulture)} --> {Max.Value.ToString("G", CultureInfo.InvariantCulture)}";
            }

            return returned;
        }
    }
}
