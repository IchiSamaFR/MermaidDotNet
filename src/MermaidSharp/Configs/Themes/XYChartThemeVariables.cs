using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Configs.Themes
{
    /// <summary>
    /// Represents theme variables specific to XY chart visualizations.
    /// </summary>
    /// <remarks>Use this class to define or override appearance settings for XY charts when customizing chart
    /// themes. Inherits common theme variable functionality from AThemeVariables.</remarks>
    public class XYChartThemeVariables : AThemeVariables
    {
        /// <summary>
        /// Gets the configuration settings for the XY chart theme.
        /// </summary>
        [ThemeVariable]
        public XYChartThemeChildConfig XYChartConfig { get; set; } = new XYChartThemeChildConfig();
    }
}
