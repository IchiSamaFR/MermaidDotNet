using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a Git merge action for merging a specified branch into the current branch within a Mermaid diagram context.
    /// </summary>
    /// <remarks>Use this class to represent a merge operation in a Git graph diagram. The resulting graph can be used to
    /// generate Mermaid diagrams for documentation or visualization purposes. This class does not perform validation of branch
    /// existence or naming; such validation should be handled by the caller or a higher-level component.</remarks>
    public class GitMerge : AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "merge";

        /// <summary>
        /// Gets the name of the branch to be merged into the current branch.
        /// </summary>
        public string Branch { get; }

        /// <summary>
        /// Gets the unique identifier for this instance.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the tag associated with the merge action.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Gets the type of the Git commit represented by this instance, which can be used to specify different commit types in Mermaid diagrams.
        /// </summary>
        public GitCommitType CommitType { get; }

        /// <summary>
        /// Initializes a new instance with the specified branch and optional tag.
        /// </summary>
        /// <param name="branch">The name of the branch to merge.</param>
        /// <param name="id">The unique identifier of the merge. If not specified, an empty string is used.</param>
        /// <param name="tag">An optional tag associated with the merge. If not specified, no tag is used.</param>
        /// <param name="commitType">The type of the Git commit. If not specified, defaults to GitCommitType.None.</param>
        public GitMerge(string branch, string id = "", string tag = "", GitCommitType commitType = GitCommitType.None)
        {
            Branch = branch;
            Id = id;
            Tag = tag;
            CommitType = commitType;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var returned = $"{Name} {Branch}";

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
