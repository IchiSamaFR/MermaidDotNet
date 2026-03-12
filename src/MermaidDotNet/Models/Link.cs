using MermaidDotNet.Enums;
using System.Text;

namespace MermaidDotNet.Models
{
    public class Link
    {
        public Link(string sourceNode, string destinationNode)
        {
            SourceNode = sourceNode.Replace(" ", "");
            DestinationNode = destinationNode.Replace(" ", "");
        }

        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }

        public virtual string GetLinkString()
        {
            return $"{SourceNode}{GetLink()}{DestinationNode}";
        }
        protected virtual string GetLink()
        {
            return "-->";
        }
    }
}
