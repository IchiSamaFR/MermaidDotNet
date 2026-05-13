using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the type of arrowhead used for links in a Mermaid flowchart diagram.
    /// </summary>
    /// <remarks>Each value corresponds to a different visual representation of an arrowhead in Mermaid
    /// syntax. The selected arrow type determines how the connection between nodes is rendered in the generated
    /// diagram.</remarks>
    public enum FlowLinkArrowType
    {
#pragma warning disable CS1591
        [MermaidEnum("<", ">")]
        Normal,

        [MermaidEnum("o")]
        Circle,

        [MermaidEnum("x")]
        Cross
#pragma warning restore CS1591
    }
}
