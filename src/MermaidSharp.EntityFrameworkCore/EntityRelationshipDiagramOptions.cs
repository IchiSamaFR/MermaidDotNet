using MermaidSharp.Enums;

namespace MermaidSharp.EntityFrameworkCore
{
    /// <summary>
    /// Represents configuration options for generating entity-relationship diagrams in Mermaid flowcharts.
    /// </summary>
    /// <remarks>Use this class to customize the inclusion of columns, key types, comments, links, and related
    /// metadata when generating entity-relationship diagrams. Adjust the properties to control the level of detail and
    /// filtering applied to the generated output.</remarks>
    public class EntityRelationshipDiagramOptions
    {
        /// <summary>
        /// Gets or sets the key types used to filter columns in the flowchart generation process.
        /// </summary>
        /// <remarks>Use this property to specify which column key types should be included when
        /// generating flowcharts. Setting this property to a value other than None will restrict the output to columns
        /// matching the selected key types.</remarks>
        public RelationConstraintType FilterColumnByKeyTypes { get; set; } = RelationConstraintType.None;

        /// <summary>
        /// Gets or sets a value indicating whether column information is included in the output.
        /// </summary>
        /// <remarks>Set this property to <see langword="true"/> to include column information, such as column names and types,
        /// in the generated output. Set this property to <see langword="false"/> to omit column details from the diagram.</remarks>
        public bool IncludeColumns { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether column key types are included in the output.
        /// </summary>
        /// <remarks>Set this property to <see langword="true"/> to include key type information for
        /// columns when generating output. This can be useful for scenarios where distinguishing between primary,
        /// foreign, or other key types is required.</remarks>
        public bool IncludeColumnKeyTypes { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether column comments should be included in the generated output.
        /// </summary>
        /// <remarks>Set this property to <see langword="true"/> to include column comments in the output. This can be useful
        /// for providing additional context or documentation about the columns in the generated diagram.</remarks>
        public bool IncludeColumnComments { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether links are included in the generated flowchart output.
        /// </summary>
        /// <remarks>Set this property to <see langword="true"/> to include links in the generated output. This can be useful
        /// for visualizing relationships between entities in the diagram.</remarks>
        public bool IncludeLinks { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether link labels are included in the generated flowchart output.
        /// </summary>
        /// <remarks>When set to <see langword="true"/>, link labels will be rendered alongside
        /// connections in the Mermaid flowchart. Set to <see langword="false"/> to omit labels from links. This
        /// property affects the readability and detail of the visual diagram.</remarks>
        public bool IncludeLinkLabels { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether link delete behaviors are included in the flowchart generation.
        /// </summary>
        /// <remarks>When enabled, the flowchart will display delete behaviors for links, which can be
        /// useful for visualizing relationship constraints or cascade effects. Set to <see langword="false"/> to omit
        /// delete behaviors from the generated diagram.</remarks>
        public bool IncludeLinkDeleteBehaviors { get; set; } = true;
    }
}
