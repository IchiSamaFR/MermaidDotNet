using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MermaidSharp.AutoDiagram.Options;
using MermaidSharp.Diagrams;
using MermaidSharp.Models;

namespace MermaidSharp.AutoDiagram.Diagrams
{
	/// <summary>
	/// Provides extension methods for converting assemblies into Mermaid flowchart diagrams.
	/// </summary>
	public static class FlowchartDiagramExtension
	{
		/// <summary>
		/// Converts a collection of assemblies into a Mermaid flowchart diagram showing assembly dependencies.
		/// </summary>
		/// <param name="assemblies">Collection of assemblies to convert into flowchart nodes.</param>
		/// <param name="options">Optional configuration settings for the diagram generation.</param>
		/// <returns>Flowchart diagram representing the assemblies as nodes with links showing their references.</returns>
		public static FlowchartDiagram ToMermaidFlowchartDiagram(this IEnumerable<Assembly> assemblies, FlowchartDiagramOptions options = null)
		{
			if (options == null)
				options = new FlowchartDiagramOptions();

			var flowchartDiagram = new FlowchartDiagram();
			var nodesContent = flowchartDiagram.Nodes;

			if (options.IsSubgraphGroupLabelEnabled)
			{
				var subgraph = new FlowSubGraph(options.SubgraphGroupLabel);
				nodesContent = subgraph.Nodes;
				flowchartDiagram.SubGraphs.Add(subgraph);
			}

			var nodes = assemblies
				.Select(t => new FlowNode(t.GetName().Name, t.GetName().Name))
				.ToList();
			nodesContent.AddRange(nodes);

			var links = new List<FlowLink>();
            foreach (var assembly in assemblies)
			{
				var referencedAssemblies = assembly.GetReferencedAssemblies();
				links.AddRange(referencedAssemblies
                    .Select(r => new FlowLink(assembly.GetName().Name, r.Name)));
            }
            flowchartDiagram.Links.AddRange(links
				.OrderBy(l => l.SourceNode)
				.ThenBy(l => l.DestinationNode));

            return flowchartDiagram;
		}
	}
}
