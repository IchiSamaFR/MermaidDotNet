using MermaidSharp.Configs;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents a Mermaid class diagram, including namespaces, classes, and relationships.
    /// </summary>
    /// <remarks>Use this class to construct and generate Mermaid class diagrams programmatically. The diagram
    /// can include multiple namespaces, classes, and links between them. Inherit from this class to extend or customize
    /// class diagram generation.</remarks>
    public class ClassDiagram : ADiagram<ClassDiagramConfig, ClassNode, ClassLink>
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "classDiagram";

        /// <summary>
        /// Gets the collection of class namespaces represented in the model.
        /// </summary>
        public List<ClassNamespace> Namespaces { get; } = new List<ClassNamespace>();

        /// <summary>
        /// Initializes a new instance of the ClassDiagram class with the specified title.
        /// </summary>
        /// <param name="title">The title of the class diagram. If not specified, the title is set to an empty string.</param>
        /// <param name="config">The configuration settings to apply to the class diagram. If null, default configuration is used.</param>
        public ClassDiagram(string title = "", ClassDiagramConfig config = null) : base(title, config)
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

            lines.AddRange(Namespaces.Select(n => n.ToString()).Indent());

            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToClassString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
