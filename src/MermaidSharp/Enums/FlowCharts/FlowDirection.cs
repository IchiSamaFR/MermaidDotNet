using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible flow directions for a Mermaid diagram layout.
    /// </summary>
    /// <remarks>Use this enumeration to control the orientation of nodes and edges in generated Mermaid
    /// diagrams. The direction determines how elements are arranged visually, such as top-to-bottom or
    /// left-to-right.</remarks>
    public enum FlowDirection
    {
#pragma warning disable CS1591
        [MermaidEnum]
        None,

        [MermaidEnum("TB")]
        TopBottom,

        [MermaidEnum("TD")]
        TopDown,

        [MermaidEnum("BT")]
        BottomTop,

        [MermaidEnum("RL")]
        RightLeft,

        [MermaidEnum("LR")]
        LeftRight
#pragma warning restore CS1591
    }
}
