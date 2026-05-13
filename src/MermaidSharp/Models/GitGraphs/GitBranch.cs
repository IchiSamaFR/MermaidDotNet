namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a git commit that creates a new branch in a repository.
    /// </summary>
    public class GitBranch : AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "branch";

        /// <summary>
        /// Gets the name of the branch to be created.
        /// </summary>
        public string Branch { get; }

        /// <summary>
        /// Initializes a new instance with the specified branch name.
        /// </summary>
        /// <param name="branch">The name of the Git branch to associate with this instance. Cannot be empty or null.</param>
        public GitBranch(string branch)
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
