using MermaidSharp.Attributes;
using MermaidSharp.Configs.Enums;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [ConfigVariable("width")]
        /// <summary>
        /// Gets or sets the chart width.
        /// </summary>
        public int? Width { get; set; }

        [ConfigVariable("height")]
        /// <summary>
        /// Gets or sets the chart height.
        /// </summary>
        public int? Height { get; set; }

        [ConfigVariable("titlePadding")]
        /// <summary>
        /// Gets or sets the title's top and bottom padding.
        /// </summary>
        public int? TitlePadding { get; set; }

        [ConfigVariable("titleFontSize")]
        /// <summary>
        /// Gets or sets the font size of the title.
        /// </summary>
        public int? TitleFontSize { get; set; }

        [ConfigVariable("showTitle")]
        /// <summary>
        /// Gets or sets a value indicating whether the title is displayed.
        /// </summary>
        public bool? ShowTitle { get; set; }

        [ConfigVariable("xAxis")]
        /// <summary>
        /// Gets or sets the X axis configuration.
        /// </summary>
        public XAxisPosition? XAxis { get; set; }

        [ConfigVariable("yAxis")]
        /// <summary>
        /// Gets or sets the Y axis configuration.
        /// </summary>
        public YAxisPosition? YAxis { get; set; }


        [ConfigVariable("chartOrientation")]
        /// <summary>
        /// Gets or sets the chart orientation.
        /// </summary>
        public ChartOrientation? ChartOrientationValue { get; set; }

        [ConfigVariable("plotReservedSpacePercent")]
        /// <summary>
        /// Gets or sets the minimum reserved space percentage for plots inside the chart.
        /// </summary>
        public int? PlotReservedSpacePercent { get; set; }

        [ConfigVariable("showDataLabel")]
        /// <summary>
        /// Gets or sets a value indicating whether to display the data label inside the bar.
        /// </summary>
        public bool? ShowDataLabel { get; set; }

        [ConfigVariable("showDataLabelOutsideBar")]
        /// <summary>
        /// Gets or sets a value indicating whether to display the data label outside the bar.
        /// </summary>
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