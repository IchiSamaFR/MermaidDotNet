using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Provides configuration options for a Mermaid quadrant chart.
    /// </summary>
    public class QuadrantChartConfig : AConfig<QuadrantChartThemeVariables>
    {
        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
        protected override string SectionName => "quadrantChart";

        /// <summary>
        /// Gets or sets the width of the chart.
        /// </summary>
        [ConfigVariable("chartWidth")]
        public int? ChartWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the chart.
        /// </summary>
        [ConfigVariable("chartHeight")]
        public int? ChartHeight { get; set; }

        /// <summary>
        /// Gets or sets the top and bottom padding of the title.
        /// </summary>
        [ConfigVariable("titlePadding")]
        public int? TitlePadding { get; set; }

        /// <summary>
        /// Gets or sets the title font size.
        /// </summary>
        [ConfigVariable("titleFontSize")]
        public int? TitleFontSize { get; set; }

        /// <summary>
        /// Gets or sets the padding outside all the quadrants.
        /// </summary>
        [ConfigVariable("quadrantPadding")]
        public int? QuadrantPadding { get; set; }

        /// <summary>
        /// Gets or sets the quadrant text top padding when text is drawn on top.
        /// </summary>
        [ConfigVariable("quadrantTextTopPadding")]
        public int? QuadrantTextTopPadding { get; set; }

        /// <summary>
        /// Gets or sets the quadrant text font size.
        /// </summary>
        [ConfigVariable("quadrantLabelFontSize")]
        public int? QuadrantLabelFontSize { get; set; }

        /// <summary>
        /// Gets or sets the border stroke width inside the quadrants.
        /// </summary>
        [ConfigVariable("quadrantInternalBorderStrokeWidth")]
        public int? QuadrantInternalBorderStrokeWidth { get; set; }

        /// <summary>
        /// Gets or sets the quadrant external border stroke width.
        /// </summary>
        [ConfigVariable("quadrantExternalBorderStrokeWidth")]
        public int? QuadrantExternalBorderStrokeWidth { get; set; }

        /// <summary>
        /// Gets or sets the top and bottom padding of x-axis text.
        /// </summary>
        [ConfigVariable("xAxisLabelPadding")]
        public int? XAxisLabelPadding { get; set; }

        /// <summary>
        /// Gets or sets the x-axis texts font size.
        /// </summary>
        [ConfigVariable("xAxisLabelFontSize")]
        public int? XAxisLabelFontSize { get; set; }

        /// <summary>
        /// Gets or sets the position of x-axis (top, bottom).
        /// </summary>
        [ConfigVariable("xAxisPosition")]
        public XAxisPosition? XAxisPosition { get; set; }

        /// <summary>
        /// Gets or sets the left and right padding of y-axis text.
        /// </summary>
        [ConfigVariable("yAxisLabelPadding")]
        public int? YAxisLabelPadding { get; set; }

        /// <summary>
        /// Gets or sets the y-axis texts font size.
        /// </summary>
        [ConfigVariable("yAxisLabelFontSize")]
        public int? YAxisLabelFontSize { get; set; }

        /// <summary>
        /// Gets or sets the position of y-axis (left, right).
        /// </summary>
        [ConfigVariable("yAxisPosition")]
        public YAxisPosition? YAxisPosition { get; set; }

        /// <summary>
        /// Gets or sets the padding between point and the below text.
        /// </summary>
        [ConfigVariable("pointTextPadding")]
        public int? PointTextPadding { get; set; }

        /// <summary>
        /// Gets or sets the point text font size.
        /// </summary>
        [ConfigVariable("pointLabelFontSize")]
        public int? PointLabelFontSize { get; set; }

        /// <summary>
        /// Gets or sets the radius of the point to be drawn.
        /// </summary>
        [ConfigVariable("pointRadius")]
        public int? PointRadius { get; set; }

        /// <summary>
        /// Initializes a new instance of the QuadrantChartConfig class with default settings.
        /// </summary>
        public QuadrantChartConfig() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the QuadrantChartConfig class with the specified theme and theme variables.
        /// </summary>
        /// <param name="theme">The theme to apply to the chart configuration. Use ConfigTheme.None for the default theme.</param>
        /// <param name="themeVariables">The theme variables to customize the appearance of the chart. If not specified, default variables are used.</param>
        public QuadrantChartConfig(ConfigTheme theme = ConfigTheme.None, QuadrantChartThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
        }
    }
}