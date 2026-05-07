using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the orientation of the chart.
    /// </summary>
    public enum ChartOrientation
    {
#pragma warning disable CS1591
        [MermaidEnum("vertical")]
        Vertical,

        [MermaidEnum("horizontal")]
        Horizontal
#pragma warning restore CS1591
    }
}
