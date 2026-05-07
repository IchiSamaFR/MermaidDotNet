using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the available series types for XY charts.
    /// </summary>
    /// <remarks>Use this enumeration to select the visual representation of data in an XY chart, such as a
    /// line or bar series.</remarks>
    public enum XYSeriesType
    {
#pragma warning disable CS1591
        [MermaidEnum("line")]
        Line,

        [MermaidEnum("bar")]
        Bar
#pragma warning restore CS1591
    }
}
