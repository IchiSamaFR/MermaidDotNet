using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible positions for the X axis in a Mermaid quadrant chart.
    /// </summary>
    public enum XAxisPosition
    {
#pragma warning disable CS1591
        [MermaidEnum("top")]
        Top,

        [MermaidEnum("bottom")]
        Bottom
#pragma warning restore CS1591
    }
}