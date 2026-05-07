using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents the configuration for the X-axis of a chart, including its title and label collection.
    /// </summary>
    public class ChartXAxis
    {
        /// <summary>
        /// Gets or sets the title associated with the current object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the collection of labels associated with the current instance.
        /// </summary>
        public List<string> Labels { get; } = new List<string>();

        /// <summary>
        /// Initializes a new instance of the ChartXAxis class with the specified title and optional labels.
        /// </summary>
        /// <param name="title">The title to display for the X axis.</param>
        /// <param name="labels">A list of labels to display along the X axis. If null, no labels are added.</param>
        public ChartXAxis(string title, List<string> labels = null)
        {
            Title = title;
            if (labels != null)
            {
                Labels.AddRange(labels);
            }
        }

        public override string ToString()
        {
            var returned = "x-axis";
            if (!string.IsNullOrEmpty(Title))
            {
                returned += $" \"{Title}\"";
            }
            returned += $" [{string.Join(", ", Labels.Select(label => $"\"{label}\""))}]";
            return returned;
        }
    }
}
