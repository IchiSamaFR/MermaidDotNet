using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the types of relationships that can be represented between classes in a Mermaid class diagram.
    /// </summary>
    /// <remarks>Each value corresponds to a specific UML relationship, such as inheritance, composition,
    /// aggregation, association, dependency, or realization. These values are used to generate the appropriate link
    /// syntax in Mermaid diagrams.</remarks>
    public enum ClassLinkType
    {
#pragma warning disable CS1591
        [MermaidEnum("<|--")]
        Inheritance,

        [MermaidEnum("*--")]
        Composition,

        [MermaidEnum("o--")]
        Aggregation,

        [MermaidEnum("-->")]
        Association,

        [MermaidEnum("--")]
        SolidLink,

        [MermaidEnum("..>")]
        Dependency,

        [MermaidEnum("..|>")]
        Realization,

        [MermaidEnum("..")]
        DashedLink
#pragma warning restore CS1591
    }
}
