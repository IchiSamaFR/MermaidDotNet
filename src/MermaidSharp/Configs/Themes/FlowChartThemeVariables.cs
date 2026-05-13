using MermaidSharp.Attributes;

namespace MermaidSharp.Configs.Themes
{
    /// <summary>
    /// Represents a set of theme variables for customizing the appearance of flowcharts, including node, cluster, and link colors.
    /// </summary>
    /// <remarks>
    /// Use this class to configure the visual aspects of flowcharts, such as node borders, cluster backgrounds, and text colors.
    /// </remarks>
    public class FlowChartThemeVariables : AThemeVariables
    {
        /// <summary>
        /// Gets or sets the color of the node border.
        /// </summary>
        [ThemeVariable("nodeBorder")]
        public string NodeBorder { get; set; }

        /// <summary>
        /// Gets or sets the background color for clusters (subgraphs).
        /// </summary>
        [ThemeVariable("clusterBkg")]
        public string ClusterBkg { get; set; }

        /// <summary>
        /// Gets or sets the border color for clusters.
        /// </summary>
        [ThemeVariable("clusterBorder")]
        public string ClusterBorder { get; set; }

        /// <summary>
        /// Gets or sets the default color for links.
        /// </summary>
        [ThemeVariable("defaultLinkColor")]
        public string DefaultLinkColor { get; set; }

        /// <summary>
        /// Gets or sets the color for the flowchart title.
        /// </summary>
        [ThemeVariable("titleColor")]
        public string TitleColor { get; set; }

        /// <summary>
        /// Gets or sets the background color for edge labels.
        /// </summary>
        [ThemeVariable("edgeLabelBackground")]
        public string EdgeLabelBackground { get; set; }

        /// <summary>
        /// Gets or sets the color for text inside nodes.
        /// </summary>
        [ThemeVariable("nodeTextColor")]
        public string NodeTextColor { get; set; }
    }
}