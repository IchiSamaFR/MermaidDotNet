namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents the base class for actions that can be rendered as Mermaid syntax elements.
    /// </summary>
    /// <remarks>This abstract class provides a contract for derived types to supply a Mermaid-compatible
    /// name, enabling consistent conversion to Mermaid diagram syntax. Inherit from this class to implement specific
    /// Git-related actions for Mermaid diagrams.</remarks>
    public abstract class AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected abstract string Name { get; }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
