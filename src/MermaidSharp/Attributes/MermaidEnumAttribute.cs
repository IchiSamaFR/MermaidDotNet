using System;

namespace MermaidSharp.Attributes
{
    /// <summary>
    /// Specifies the primary and secondary string representations for an enumeration value when generating Mermaid diagrams.
    /// </summary>
    /// <remarks>Apply this attribute to enumeration members to control how their values are represented in
    /// Mermaid syntax. The primary string is used as the default representation, and the secondary string is used
    /// in contexts where a reversed or alternative representation is required (e.g., for directional relationship
    /// markers).</remarks>
    public class MermaidEnumAttribute : Attribute
    {
        /// <summary>
        /// Gets the primary value associated with this instance.
        /// </summary>
        public string Primary { get; }
        /// <summary>
        /// Gets the secondary value associated with this instance.
        /// </summary>
        public string Secondary { get; }

        /// <summary>
        /// Initializes a new instance of the MermaidEnumAttribute class with optional primary and secondary string
        /// representations for the enumeration value.
        /// </summary>
        /// <param name="start">The primary string representation for the enumeration value. If not specified, an empty string is used.</param>
        /// <param name="end">The secondary string representation for the enumeration value. If not specified, the primary value is used.</param>
        public MermaidEnumAttribute(string start = "", string end = "")
        {
            Primary = start;
            Secondary = string.IsNullOrEmpty(end) ? start : end;
        }
    }
}
