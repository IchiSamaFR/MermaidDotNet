using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the configuration settings for rendering a Mermaid flowchart diagram.
    /// </summary>
    public class FlowChartConfig : AConfig<FlowChartThemeVariables>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlowChartConfig"/> class with default settings.
        /// </summary>
        public FlowChartConfig() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlowChartConfig"/> class with the specified theme and theme variables.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
        /// <param name="themeVariables">
        /// The theme variables to apply to the diagram. If null, a new empty <see cref="FlowChartThemeVariables"/> instance is used.
        /// </param>
        public FlowChartConfig(ConfigTheme theme = ConfigTheme.None, FlowChartThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
        }
    }
}
