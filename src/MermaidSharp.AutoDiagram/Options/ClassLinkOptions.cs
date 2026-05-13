using MermaidSharp.AutoDiagram.Enums;

namespace MermaidSharp.AutoDiagram.Options
{
    /// <summary>
    /// Provides options to control the content generated for class links in a class diagram from reflection.
    /// </summary>
    public class ClassLinkOptions
    {
        /// <summary>
        /// Gets or sets the option for including class links.
        /// </summary>
        public ClassLinkOption IncludeLinks { get; set; } = ClassLinkOption.All;

        /// <summary>
        /// Gets or sets a value indicating whether to include labels for class links in the diagram.
        /// </summary>
        public bool IncludeLinksLabels { get; set; } = true;
    }
}
