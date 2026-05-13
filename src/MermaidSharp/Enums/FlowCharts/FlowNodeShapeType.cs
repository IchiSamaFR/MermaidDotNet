using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the available node shapes for flowchart elements in Mermaid diagrams.
    /// </summary>
    /// <remarks>Use this enumeration to select the visual representation of nodes when generating Mermaid
    /// flowcharts. Each value corresponds to a specific Mermaid syntax for node shapes, such as rectangles, circles, or
    /// hexagons. The selected shape determines how the node appears in the rendered diagram.</remarks>
    public enum FlowNodeShapeType
    {
#pragma warning disable CS1591
        [MermaidEnum("[", "]")]
        Rectangle,

        [MermaidEnum("(", ")")]
        Rounded,

        [MermaidEnum("([", "])")]
        Stadium,

        [MermaidEnum("[(", ")]")]
        Cylinder,

        [MermaidEnum("((", "))")]
        Circle,

        [MermaidEnum("{", "}")]
        Rhombus,

        [MermaidEnum("{{", "}}")]
        Hexagon,

        [MermaidEnum("[/", "/]")]
        Parallelogram,

        [MermaidEnum("[\\", "\\]")]
        Trapezoid,

        [MermaidEnum("[/", "\\]")]
        TrapezoidAlt,

        [MermaidEnum("[[", "]]")]
        Subroutine
#pragma warning restore CS1591
    }
}
