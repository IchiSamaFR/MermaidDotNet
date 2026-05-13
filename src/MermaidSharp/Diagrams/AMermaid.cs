using MermaidSharp.Configs;
using MermaidSharp.Constants;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for Mermaid diagram generators, providing common properties and methods for creating
    /// and formatting diagram output.
    /// </summary>
    /// <remarks>Derive from this class to implement specific types of Mermaid diagrams. This class defines
    /// the contract for generating diagram content and managing diagram titles.</remarks>
    public abstract class AMermaid<TConfig>
        where TConfig : IConfig, new()
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        protected abstract string Name { get; }

        /// <summary>
        /// Gets the Mermaid diagram type name for the current instance.
        /// </summary>
        /// <remarks>
        /// This property is provided for backward compatibility and may be removed in a future version.
        /// </remarks>
        [Obsolete("Access to the diagram type name is deprecated and will be removed in a future version.")]
        public string DiagramName => Name;

        /// <summary>
        /// Gets or sets the configuration settings for the current instance.
        /// </summary>
        protected TConfig Config { get; }

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the AMermaid class with the specified title.
        /// </summary>
        /// <param name="title">The title to assign to the diagram. If not specified, the title is set to an empty string.</param>
        /// <param name="config">The configuration settings for the diagram. If not specified, default settings will be used.</param>
        protected AMermaid(string title = "", TConfig config = default)
        {
            Title = title;

            if (config == null)
                config = new TConfig();
            Config = config;
        }

        /// <summary>
        /// Generates a formatted header string for the flowchart title, including separators and the title text, if a
        /// title is specified.
        /// </summary>
        /// <remarks>Override this method to customize the formatting of the flowchart header in derived
        /// classes.</remarks>
        /// <returns>A formatted string containing the flowchart title and separators if the title is not null or empty;
        /// otherwise, an empty string.</returns>
        protected virtual string GetHeaderString()
        {
            var configLines = GetConfigLines();
            if (configLines.Count == 0)
            {
                return string.Empty;
            }

            var lines = new List<string>();
            lines.Add(FormattingConstants.TitleSeparator);
            lines.AddRange(configLines);
            lines.Add(FormattingConstants.TitleSeparator);
            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// Creates a list of configuration lines representing the current object's settings.
        /// </summary>
        /// <returns>A list of strings containing configuration lines.</returns>
        protected virtual List<string> GetConfigLines()
        {
            var lines = new List<string>();

            if (!string.IsNullOrWhiteSpace(Title))
                lines.Add($"title: {Title}");

            lines.AddRange(Config?.GetConfigLines() ?? new List<string>());

            return lines;
        }

        /// <summary>
        /// Generates the complete Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid diagram.</returns>
        public abstract string CalculateDiagram();
    }
}
