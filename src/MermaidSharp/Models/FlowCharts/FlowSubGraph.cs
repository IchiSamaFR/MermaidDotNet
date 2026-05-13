using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a subgraph within a flowchart, containing a collection of nodes and links with an optional layout
    /// direction.
    /// </summary>
    /// <remarks>Use this class to group related nodes and links within a flowchart, allowing for logical
    /// organization and custom layout direction. The subgraph can be configured to use different flow directions, such
    /// as left-to-right or top-to-bottom, to control the visual arrangement of its elements.</remarks>
    public class FlowSubGraph
    {
        /// <summary>
        /// Gets or sets the name of the subgraph.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the flow direction for the diagram layout.
        /// </summary>
        public FlowDirection Direction { get; set; }

        /// <summary>
        /// Gets the collection of nodes contained in the current structure.
        /// </summary>
        public List<FlowNode> Nodes { get; }
        /// <summary>
        /// Gets the collection of links associated with the current instance.
        /// </summary>
        public List<FlowLink> Links { get; }

        /// <summary>
        /// Initializes a new instance of the FlowSubGraph class with the specified name, nodes, links, and optional
        /// direction.
        /// </summary>
        /// <remarks>Use this constructor to define a subgraph within a flowchart, specifying its nodes,
        /// links, and layout direction as needed.</remarks>
        /// <param name="name">The unique identifier for the subgraph. Spaces will be removed from this value.</param>
        /// <param name="nodes">The collection of nodes to include in the subgraph. If null, an empty list is used.</param>
        /// <param name="links">The collection of links connecting nodes within the subgraph. If null, an empty list is used.</param>
        /// <param name="direction">The layout direction for the subgraph. If null, the default direction is used.</param>
        public FlowSubGraph(string name, List<FlowNode> nodes = null, List<FlowLink> links = null, FlowDirection direction = FlowDirection.None)
        {
            Name = name.Replace(" ", "");
            Nodes = nodes ?? new List<FlowNode>();
            Links = links ?? new List<FlowLink>();
            Direction = direction;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var lines = new List<string>();
            lines.Add($"subgraph {Name}");
            if (Direction != FlowDirection.None)
            {
                lines.Add($"direction {Direction.PrimaryString()}");
            }
            lines.AddRange(Nodes.Select(n => n.ToString()));
            lines.AddRange(Links.Select(n => n.ToString()));
            lines.Add("end");
            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
