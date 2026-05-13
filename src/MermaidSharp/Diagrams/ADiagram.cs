using MermaidSharp.Configs;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for diagram types that consist of nodes and links, providing common functionality for
    /// diagram generation.
    /// </summary>
    public abstract class ADiagram<TConfig, TNode, TLink> : AMermaid<TConfig>
        where TConfig : IConfig, new()
        where TNode : ANode
        where TLink : ALink
    {
        /// <summary>
        /// Gets the collection of nodes contained in the current structure.
        /// </summary>
        public List<TNode> Nodes { get; } = new List<TNode>();

        /// <summary>
        /// Gets the collection of links associated with the current instance.
        /// </summary>
        public List<TLink> Links { get; } = new List<TLink>();

        /// <summary>
        /// Initializes a new instance of the ADiagram class with the specified title.
        /// </summary>
        /// <param name="title">The title to assign to the diagram. If not specified, the title is set to an empty string.</param>
        /// <param name="config">The configuration settings for the diagram. If not specified, default settings will be used.</param>
        protected ADiagram(string title = "", TConfig config = default) : base(title, config)
        {
        }

        /// <summary>
        /// Generates the complete Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid diagram.</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            lines.Add(Name);

            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToClassString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
