namespace MermaidSharp.AutoDiagram.Options
{
    /// <summary>
    /// Represents configuration options for generating a Mermaid flowchart diagram from a collection of assemblies.
    /// </summary>
    public class FlowchartDiagramOptions
    {
        /// <summary>
        /// Gets or sets the label for the subgraph group that contains all the nodes in the flowchart diagram.
        /// If this property is set to a non-empty string, a subgraph will be created with the specified label,
        /// and all nodes will be placed inside it. If this property is null or empty, no subgraph will be created,
        /// and all nodes will be added directly to the main graph.
        /// </summary>
        public string SubgraphGroupLabel { get; set; } = "Project";

        /// <summary>
        /// Gets a value indicating whether the subgraph group label is enabled.
        /// </summary>
        public bool IsSubgraphGroupLabelEnabled => !string.IsNullOrEmpty(SubgraphGroupLabel);
    }
}
