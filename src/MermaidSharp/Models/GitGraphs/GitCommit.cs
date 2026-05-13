using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a Git commit action with an associated identifier and tag for use in Mermaid diagrams.
    /// </summary>
    /// <remarks>Use this class to model a commit node in a Mermaid flowchart or sequence diagram, specifying
    /// the commit's unique identifier and an optional tag. The class provides a Mermaid-compatible string
    /// representation for integration with diagram generation workflows.</remarks>
    public class GitCommit : AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "commit";

        /// <summary>
        /// Gets the unique identifier for this instance.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Gets the tag associated with the current object.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Gets the type of the Git commit represented by this instance, which can be used to specify different commit types in Mermaid diagrams.
        /// </summary>
        public GitCommitType CommitType { get; }

        /// <summary>
        /// Initializes a new instance of the GitCommit class with the specified commit identifier and tag.
        /// </summary>
        /// <param name="id">The unique identifier of the commit. If not specified, an empty string is used.</param>
        /// <param name="tag">The tag associated with the commit. If not specified, an empty string is used.</param>
        /// <param name="commitType">The type of the Git commit. If not specified, defaults to GitCommitType.None.</param>
        public GitCommit(string id = "", string tag = "", GitCommitType commitType = GitCommitType.None)
        {
            Id = id;
            Tag = tag;
            CommitType = commitType;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var returned = Name;

            if (!string.IsNullOrWhiteSpace(Id))
                returned += $" id: \"{Id}\"";

            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";

            if (CommitType != GitCommitType.None)
                returned += $" type: {CommitType.PrimaryString()}";

            return returned;
        }
    }
}
