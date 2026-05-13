using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the types of links that can be used between nodes in a Mermaid flowchart.
    /// </summary>
    /// <remarks>Each value corresponds to a different visual style for links in Mermaid diagrams, allowing
    /// customization of the appearance and semantics of connections between nodes.</remarks>
    public enum FlowLinkType
    {
#pragma warning disable CS1591
        [MermaidEnum("--")]
        Normal,

        [MermaidEnum("-.", ".-")]
        Dotted,

        [MermaidEnum("==")]
        Thick,

        [MermaidEnum("~~~")]
        Invisible
#pragma warning restore CS1591
    }
}
