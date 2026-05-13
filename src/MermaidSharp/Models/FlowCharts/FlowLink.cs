using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System.Text;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional or bidirectional link between two nodes in a Mermaid flowchart, including label, style,
    /// and arrow customization options.
    /// </summary>
    /// <remarks>Use this class to define advanced flowchart connections with custom labels, styles, and arrow
    /// types. The link can be configured as bidirectional and supports various visual styles for integration with
    /// Mermaid diagrams.</remarks>
    public class FlowLink : ALink
    {
        /// <summary>
        /// Gets or sets the label text associated with this element.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the style applied to links in the diagram.
        /// </summary>
        public string LinkStyle { get; set; }
        /// <summary>
        /// Gets a value indicating whether the relationship is bidirectional.
        /// </summary>
        public bool IsBidirectional { get; }
        /// <summary>
        /// Gets or sets the type of flow link represented by this instance.
        /// </summary>
        public FlowLinkType Type { get; set; }
        /// <summary>
        /// Gets or sets the arrow style used for the flow link.
        /// </summary>
        public FlowLinkArrowType Arrow { get; set; }

        /// <summary>
        /// Initializes a new instance of the FlowLink class, representing a connection between two nodes in a flowchart
        /// with optional labeling, styling, directionality, and link or arrow types.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node from which the link originates. Cannot be null or empty.</param>
        /// <param name="destinationNode">The identifier of the destination node to which the link points. Cannot be null or empty.</param>
        /// <param name="label">The label to display on the link. If not specified, the link will have no label.</param>
        /// <param name="linkstyle">The style to apply to the link, such as line type or color. If not specified, the default style is used.</param>
        /// <param name="isBidirectional">true if the link should be bidirectional; otherwise, false.</param>
        /// <param name="linkType">The type of link to use, such as normal or dotted. Specifies the visual representation of the link.</param>
        /// <param name="arrowType">The type of arrow to use at the end of the link, such as normal or open.</param>
        public FlowLink(string sourceNode, string destinationNode, string label = "", string linkstyle = "", bool isBidirectional = false, FlowLinkType linkType = FlowLinkType.Normal, FlowLinkArrowType arrowType = FlowLinkArrowType.Normal)
            : base(sourceNode, destinationNode)
        {
            Label = label;
            IsBidirectional = isBidirectional;
            LinkStyle = linkstyle;
            Type = linkType;
            Arrow = arrowType;
        }

        /// <summary>
        /// Generates a Mermaid link style string for the specified link index.
        /// </summary>
        /// <param name="index">The zero-based index of the link to which the style should be applied.</param>
        /// <returns>A Mermaid link style string if a style is defined; otherwise, an empty string.</returns>
        public string ToStyleString(int index)
        {
            if (string.IsNullOrEmpty(LinkStyle))
            {
                return string.Empty;
            }
            return $"linkStyle {index} {LinkStyle}";
        }

        /// <summary>
        /// Builds and returns the Mermaid diagram link representation based on the current arrow, type, and label
        /// settings.
        /// </summary>
        /// <remarks>The returned string reflects the directionality, type, and optional label of the link
        /// as defined by the object's properties. The format is suitable for direct inclusion in Mermaid
        /// diagrams.</remarks>
        /// <returns>A string containing the Mermaid link syntax representing the configured relationship.</returns>
        protected override string GetLink()
        {
            StringBuilder sb = new StringBuilder();
            if (IsBidirectional)
            {
                sb.Append(Arrow.PrimaryString());
            }
            sb.Append(Type.PrimaryString());
            if (!string.IsNullOrEmpty(Label))
            {
                sb.Append(Label);
                sb.Append(Type.SecondaryString());
            }
            sb.Append(Arrow.SecondaryString());
            return sb.ToString();
        }
    }
}
