using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    public class FlowSubGraph
    {
        public string Name { get; set; }
        //Can be TB, BT, RL, LR (default: LR) (Top/Bottom/Left/Right, respectively)
        public string? Direction { get; set; } = null;
        public List<FlowNode> Nodes { get; set; } = new();
        public List<FlowLink> Links { get; set; } = new();

        public FlowSubGraph(string name, string? direction = null)
        {
            Name = name.Replace(" ", "");
            Direction = direction;
        }
        public FlowSubGraph(string name, List<FlowNode> nodes, List<FlowLink> links, string? direction = null)
        {
            Name = name.Replace(" ", "");
            Nodes = nodes;
            Links = links;
            Direction = direction;
        }

        public string GetSubGraphString()
        {
            var lines = new List<string>();
            lines.Add($"subgraph {Name}");
            if (Direction != null)
            {
                lines.Add($"direction {Direction}");
            }
            lines.AddRange(Nodes.Select(n => n.GetNodeString()).ClearNewLines());
            lines.AddRange(Links.Select(n => n.GetLinkString()).ClearNewLines());
            lines.Add("end");
            return string.Join(Environment.NewLine, lines);
        }
    }
}