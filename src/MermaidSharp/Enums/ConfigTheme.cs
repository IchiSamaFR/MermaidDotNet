using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the available themes for Mermaid diagram configuration.
    /// </summary>
    /// <remarks>Use this enumeration to select a visual theme when rendering Mermaid diagrams. The selected
    /// theme determines the color scheme and overall appearance of the generated diagrams.</remarks>
    public enum ConfigTheme
    {
#pragma warning disable CS1591

        [MermaidEnum("")]
        None,

        [MermaidEnum("base")]
        Base,

        [MermaidEnum("forest")]
        Forest,

        [MermaidEnum("dark")]
        Dark,

        [MermaidEnum("default")]
        Default,

        [MermaidEnum("neutral")]
        Neutral
#pragma warning restore CS1591
    }
}
