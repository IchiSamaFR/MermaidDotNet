using MermaidSharp.Attributes;
using MermaidSharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Configs.Themes
{
    /// <summary>
    /// Represents a set of theme variables for customizing the appearance of pie charts, including colors, text styles,
    /// and stroke settings.
    /// </summary>
    /// <remarks>Use this class to configure the visual aspects of pie charts, such as section colors, title
    /// and legend text styles, and opacity. These settings allow for consistent theming and branding of pie chart
    /// visualizations across an application.</remarks>
    public class PieChartThemeVariables : AThemeVariables
    {
        /// <summary>
        /// Gets or sets the collection of pie slices represented in the chart.
        /// </summary>
        internal IReadOnlyList<PieSlice> PieSlices { get; set; }

        /// <summary>
        /// Gets the list of colors used for the pie chart sections.
        /// Each color should be specified in a valid CSS color format (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red").
        /// </summary>
        [ThemeVariable("pie{index}")]
        public IReadOnlyList<string> PieColors => PieSlices?.Select(s => s.Color).ToList() ?? new List<string>();

        /// <summary>
        /// Gets or sets the font size for the pie chart title text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        [ThemeVariable("pieTitleTextSize")]
        public string PieTitleTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart title text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        [ThemeVariable("pieTitleTextColor")]
        public string PieTitleTextColor { get; set; }

        /// <summary>
        /// Gets or sets the font size for the pie chart section text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        [ThemeVariable("pieSectionTextSize")]
        public string PieSectionTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart section text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        [ThemeVariable("pieSectionTextColor")]
        public string PieSectionTextColor { get; set; }

        /// <summary>
        /// Gets or sets the font size for the pie chart legend text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        [ThemeVariable("pieLegendTextSize")]
        public string PieLegendTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart legend text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        [ThemeVariable("pieLegendTextColor")]
        public string PieLegendTextColor { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart stroke. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        [ThemeVariable("pieStrokeColor")]
        public string PieStrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the width of the pie chart's outer stroke. (e.g., "1px", "0.5em", "thin")
        /// </summary>
        [ThemeVariable("pieOuterStrokeWidth")]
        public string PieOuterStrokeWidth { get; set; }

        /// <summary>
        /// Gets or sets the opacity level for the pie chart, where 0.0 is fully transparent and 1.0 is fully opaque.
        /// </summary>
        [ThemeVariable("pieOpacity")]
        public double? PieOpacity { get; set; }
    }
}