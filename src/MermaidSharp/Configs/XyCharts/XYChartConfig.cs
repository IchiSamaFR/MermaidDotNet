using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Provides configuration settings for an XY chart.
    /// </summary>
    /// <remarks>
    /// Use this class to customize XY chart appearance and behavior by specifying theme variables and configuration parameters.
    /// </remarks>
    public class XYChartConfig : AConfig<XYChartThemeVariables>
    {
        /// <summary>
        /// Gets the configuration section name for XY charts.
        /// </summary>
        protected override string SectionName => "xyChart";

        /// <summary>
        /// Gets or sets the chart width.
        /// </summary>
        [ConfigVariable("width")]
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the chart height.
        /// </summary>
        [ConfigVariable("height")]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the title's top and bottom padding.
        /// </summary>
        [ConfigVariable("titlePadding")]
        public int? TitlePadding { get; set; }

        /// <summary>
        /// Gets or sets the font size of the title.
        /// </summary>
        [ConfigVariable("titleFontSize")]
        public int? TitleFontSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the title is displayed.
        /// </summary>
        [ConfigVariable("showTitle")]
        public bool? ShowTitle { get; set; }

        /// <summary>
        /// Gets or sets the X axis configuration.
        /// </summary>
        [ConfigVariable("xAxis")]
        public XAxisPosition? XAxis { get; set; }

        /// <summary>
        /// Gets or sets the Y axis configuration.
        /// </summary>
        [ConfigVariable("yAxis")]
        public YAxisPosition? YAxis { get; set; }


        /// <summary>
        /// Gets or sets the chart orientation.
        /// </summary>
        [ConfigVariable("chartOrientation")]
        public ChartOrientation? ChartOrientation { get; set; }

        /// <summary>
        /// Gets or sets the minimum reserved space percentage for plots inside the chart.
        /// </summary>
        [ConfigVariable("plotReservedSpacePercent")]
        public int? PlotReservedSpacePercent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display the data label inside the bar.
        /// </summary>
        [ConfigVariable("showDataLabel")]
        public bool? ShowDataLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display the data label outside the bar.
        /// </summary>
        [ConfigVariable("showDataLabelOutsideBar")]
        public bool? ShowDataLabelOutsideBar { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="XYChartConfig"/> class with default settings.
        /// </summary>
        public XYChartConfig() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XYChartConfig"/> class with the specified theme and theme variables.
        /// </summary>
        /// <param name="theme">Theme to apply to the chart configuration. Use <see cref="ConfigTheme.None"/> for the default theme.</param>
        /// <param name="themeVariables">Theme variables to customize the chart appearance. If not specified, default variables are used.</param>
        public XYChartConfig(ConfigTheme theme = ConfigTheme.None, XYChartThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
        }
    }
}
