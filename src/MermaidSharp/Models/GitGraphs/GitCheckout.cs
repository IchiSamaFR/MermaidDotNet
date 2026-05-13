namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a Git checkout action for switching to a specified branch within a Mermaid diagram context.
    /// </summary>
    public class GitCheckout : AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "checkout";

        /// <summary>
        /// Gets the name of the branch to switch.
        /// </summary>
        public string Branch { get; }

        /// <summary>
        /// Initializes a new instance with the specified branch name.
        /// </summary>
        /// <param name="branch">The name of the Git branch to associate with this instance. Cannot be empty or null.</param>
        public GitCheckout(string branch)
        {
            Branch = branch;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return $"{Name} {Branch}";
        }
    }
}
