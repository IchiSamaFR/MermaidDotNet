using MermaidDotNet.Enums;

namespace MermaidDotNet.EntityFrameworkCore
{
    public class EntityRelationshipDiagramOptions
    {
        public ColumnKeyType FilterColumnByKeyTypes { get; set; } = ColumnKeyType.None;

        public bool IncludeColumns { get; set; } = true;
        public bool IncludeColumnKeyTypes { get; set; } = true;
        public bool IncludeColumnComments { get; set; } = true;
        public bool IncludeLinks { get; set; } = true;
        public bool IncludeLinkLabels { get; set; } = true;
        public bool IncludeLinkDeleteBehaviors { get; set; } = true;
    }
}
