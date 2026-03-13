using MermaidDotNet.EntityFrameworkCore.Enums;
using MermaidDotNet.Enums;

namespace MermaidDotNet.EntityFrameworkCore.Models
{
    internal class DiagramLink
    {
        public DiagramTable Source { get; set; }
        public DiagramTable Target { get; set; }
        public string Label { get; set; }
        public RelationType SourceType { get; set; }
        public RelationType TargetType { get; set; }
        public DeleteBehavior DeleteBehavior { get; set; }
    }
}
