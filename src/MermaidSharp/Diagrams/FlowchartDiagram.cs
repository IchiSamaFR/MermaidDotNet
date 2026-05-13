using MermaidSharp.Configs;
using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
	/// <summary>
	/// Represents a Mermaid flowchart diagram, allowing configuration of direction, nodes, links, and subgraphs.
	/// </summary>
	/// <remarks>Use this class to construct and generate Mermaid-compatible flowchart diagrams with support
	/// for custom directions and subgraph grouping. The diagram can be rendered as a Mermaid syntax string for
	/// visualization or export. Only the directions "LR", "TD", "BT", "RL", and "TB" are supported. Attempting to use
	/// an unsupported direction will result in a NotSupportedException.</remarks>
	public class FlowchartDiagram : ADiagram<FlowChartConfig, FlowNode, FlowLink>
	{
		/// <summary>
		/// Gets the Mermaid name associated with the current instance.
		/// </summary>
		protected override string Name => "flowchart";

		/// <summary>
		/// Gets or sets the direction of the flowchart.
		/// </summary>
		public FlowDirection Direction { get; set; }

		/// <summary>
		/// Gets the collection of subgraphs contained within the flowchart.
		/// </summary>
		public List<FlowSubGraph> SubGraphs { get; } = new List<FlowSubGraph>();

		/// <summary>
		/// Initializes a new instance of the FlowchartDiagram class with the specified title, direction, nodes, links,
		/// and subgraphs.
		/// </summary>
		/// <param name="direction">The direction of the flowchart.</param>
		/// <param name="config">The configuration settings for the flowchart diagram. If not specified, default settings will be used.</param>
		public FlowchartDiagram(FlowDirection direction = FlowDirection.LeftRight, FlowChartConfig config = default) : this(string.Empty, direction, config)
		{

		}

		/// <summary>
		/// Initializes a new instance of the FlowchartDiagram class with the specified title, direction, nodes, links,
		/// and subgraphs.
		/// </summary>
		/// <param name="direction">The direction of the flowchart.</param>
		/// <param name="title">The title of the flowchart diagram</param>
		/// <param name="config">The configuration settings for the flowchart diagram. If not specified, default settings will be used.</param>
		public FlowchartDiagram(string title, FlowDirection direction = FlowDirection.LeftRight, FlowChartConfig config = default) : base(title, config)
		{
			Direction = direction == FlowDirection.None ? FlowDirection.LeftRight : direction;
		}

		/// <summary>
		/// Given a list of nodes and links, calculate the mermaid flowchart
		/// </summary>
		/// <returns>a mermaid graph as a string</returns>
		public override string CalculateDiagram()
		{
			var lines = new List<string>();
			lines.Add(GetHeaderString());
			lines.Add($"{Name} {Direction.PrimaryString()}");

			lines.AddRange(SubGraphs.Select(sg => sg.ToString()).Indent());
			lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
			lines.AddRange(Links.Select(n => n.ToString()).Indent());

			var allNodes = Nodes.OfType<FlowNode>()
				.Concat(SubGraphs.SelectMany(sg => sg.Nodes))
				.ToList();
			var allLinks = Links.OfType<FlowLink>()
				.Concat(SubGraphs.SelectMany(sg => sg.Links))
				.ToList();
			var linkStyles = allLinks.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
			lines.AddRange(linkStyles.Select((n, i) => n.ToStyleString(i)).Indent());
			lines.AddRange(allNodes.Select(n => n.ToClassString()).Indent());
			lines.AddRange(allNodes.Select(n => n.ToClickString()).Indent());

			return string.Join(Environment.NewLine, lines.ClearNewLines());
		}
	}
}
