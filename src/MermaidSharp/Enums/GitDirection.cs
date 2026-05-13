using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible directions for arranging nodes in a Mermaid diagram.
    /// </summary>
    /// <remarks>Use this enumeration to control the layout orientation of flowcharts or graphs generated with
    /// Mermaid. The direction determines how nodes are positioned relative to each other in the rendered
    /// diagram.</remarks>
    public enum GitDirection
    {
#pragma warning disable CS1591
        [MermaidEnum]
        None,

        [MermaidEnum("TB")]
        TopBottom,

        [MermaidEnum("BT")]
        BottomTop,

        [MermaidEnum("LR")]
        LeftRight
#pragma warning restore CS1591
    }
}
