using MermaidSharp.Configs;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for all Mermaid graph types, providing common configuration and title support.
    /// </summary>
    /// <remarks>This abstract class serves as the foundation for specific graph implementations within the
    /// MermaidSharp library. It encapsulates shared configuration logic and ensures consistent handling of graph titles
    /// and settings across derived types.</remarks>
    public abstract class AGraph<TConfig> : AMermaid<TConfig>
        where TConfig : IConfig, new()
    {
        /// <summary>
        /// Initializes a new instance of the AGraph class with the specified title and configuration.
        /// </summary>
        /// <param name="title">The title to assign to the graph. If not specified, an empty string is used.</param>
        /// <param name="config">The configuration settings to apply to the graph. If null, default configuration is used.</param>
        protected AGraph(string title = "", TConfig config = default) : base(title, config)
        {
        }
    }
}
