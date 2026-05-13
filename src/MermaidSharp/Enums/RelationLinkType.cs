using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the cardinality of a relationship between nodes in a Mermaid entity-relationship diagram.
    /// </summary>
    /// <remarks>Use this enumeration to define how many instances of an entity can participate in a
    /// relationship when generating Mermaid diagrams. The values correspond to standard entity-relationship
    /// cardinalities, such as zero or one, exactly one, zero or more, and one or more.</remarks>
    public enum RelationLinkType
    {
#pragma warning disable CS1591
        [MermaidEnum("|o", "o|")]
        ZeroOrOne,

        [MermaidEnum("||")]
        ExactlyOne,

        [MermaidEnum("}o", "o{")]
        ZeroOrMore,

        [MermaidEnum("}|", "|{")]
        OneOrMore
#pragma warning restore CS1591
    }
}
