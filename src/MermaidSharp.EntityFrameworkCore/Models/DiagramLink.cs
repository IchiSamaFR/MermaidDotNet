using MermaidSharp.EntityFrameworkCore.Enums;
using MermaidSharp.Enums;

namespace MermaidSharp.EntityFrameworkCore.Models
{
    internal class DiagramLink
    {
        public DiagramTable Source { get; set; }
        public DiagramTable Target { get; set; }
        public string Label { get; set; }
        public RelationLinkType SourceType { get; set; }
        public RelationLinkType TargetType { get; set; }
        public DeleteBehavior DeleteBehavior { get; set; }
    }
}
