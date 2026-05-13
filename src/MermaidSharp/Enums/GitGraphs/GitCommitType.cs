using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the type of a Git commit for diagram rendering.
    /// </summary>
    /// <remarks>Use this enumeration to indicate how a commit should be visually represented or handled in
    /// Mermaid diagrams. The values correspond to standard, reversed, or highlighted commit types, which may affect the
    /// appearance or semantics in generated diagrams.</remarks>
    public enum GitCommitType
    {
        /// <summary>
        /// Specifies that no value is set.
        /// </summary>
        [MermaidEnum("")]
        None,

        /// <summary>
        /// Default commit type. Represented by a solid circle in the diagram
        /// </summary>
        [MermaidEnum("NORMAL")]
        Normal,

        /// <summary>
        /// To emphasize a commit as a reverse commit. Represented by a crossed solid circle in the diagram.
        /// </summary>
        [MermaidEnum("REVERSE")]
        Reverse,

        /// <summary>
        /// To highlight a particular commit in the diagram. Represented by a filled rectangle in the diagram.
        /// </summary>
        [MermaidEnum("HIGHLIGHT")]
        Highlight
    }
}
