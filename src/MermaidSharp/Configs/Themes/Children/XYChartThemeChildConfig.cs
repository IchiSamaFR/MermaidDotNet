using MermaidSharp.Attributes;
using MermaidSharp.Extensions;
using System.Collections.Generic;

namespace MermaidSharp.Configs.Themes.Children
{
    /// <summary>
    /// Represents the configuration for XY chart theme variables in Mermaid diagrams.
    /// </summary>
    /// <remarks>This class provides theme variable configuration lines specific to XY charts for use in
    /// Mermaid diagram output. It extends configurable behavior to support XY chart theming.</remarks>
    public class XYChartThemeChildConfig : AConfigurable, IThemeVariables
    {
        private const string Name = "xyChart";

        /// <summary>
        /// Gets or sets the background color of the whole chart.
        /// </summary>
        [ThemeVariable("backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the title text.
        /// </summary>
        [ThemeVariable("titleColor")]
        public string TitleColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the data labels (if shown).
        /// </summary>
        [ThemeVariable("dataLabelColor")]
        public string DataLabelColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the x-axis labels.
        /// </summary>
        [ThemeVariable("xAxisLabelColor")]
        public string XAxisLabelColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the x-axis title.
        /// </summary>
        [ThemeVariable("xAxisTitleColor")]
        public string XAxisTitleColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the x-axis tick.
        /// </summary>
        [ThemeVariable("xAxisTickColor")]
        public string XAxisTickColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the x-axis line.
        /// </summary>
        [ThemeVariable("xAxisLineColor")]
        public string XAxisLineColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the y-axis labels.
        /// </summary>
        [ThemeVariable("yAxisLabelColor")]
        public string YAxisLabelColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the y-axis title.
        /// </summary>
        [ThemeVariable("yAxisTitleColor")]
        public string YAxisTitleColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the y-axis tick.
        /// </summary>
        [ThemeVariable("yAxisTickColor")]
        public string YAxisTickColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the y-axis line.
        /// </summary>
        [ThemeVariable("yAxisLineColor")]
        public string YAxisLineColor { get; set; }

        /// <summary>
        /// Gets or sets the string of colors separated by comma for the plot color palette.
        /// </summary>
        [ThemeVariable("plotColorPalette")]
        public string PlotColorPalette { get; set; }

        /// <summary>
        /// Returns indented theme variable configuration lines for Mermaid output.
        /// Prepends the <c>themeVariables:</c> section only when theme variable values are present.
        /// </summary>
        /// <returns>
        /// A list of formatted theme variable configuration lines, or an empty list when no theme variables are set.
        /// </returns>
        public override List<string> GetConfigLines()
        {
            var paramsList = GetThemeVariableParams().Indent();
            if (paramsList.Count == 0)
                return new List<string>();

            paramsList.Insert(0, $"{Name}:");

            return paramsList;
        }
    }
}
