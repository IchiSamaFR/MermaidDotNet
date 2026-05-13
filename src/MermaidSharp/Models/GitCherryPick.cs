namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a Git cherry-pick action that applies the changes from a specific commit onto the current branch.
    /// </summary>
    /// <remarks>Use this class to encapsulate the parameters required to perform a cherry-pick operation in a
    /// Git workflow. The commit identifier, optional parent, and tag information are provided to control the
    /// cherry-pick behavior.</remarks>
    public class GitCherryPick : AGitAction
    {
        /// <summary>
        /// Gets the command name used to invoke the cherry-pick operation.
        /// </summary>
        protected override string Name => "cherry-pick";

        /// <summary>
        /// Gets the unique identifier of the commit associated with this instance.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the identifier of the parent element.
        /// </summary>
        public string Parent { get; }

        /// <summary>
        /// Gets the tag associated with the current instance.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Initializes a new instance of the GitCherryPick class with the specified commit identifier, parent, and tag.
        /// </summary>
        /// <param name="id">The unique identifier of the commit to cherry-pick. Cannot be null or empty.</param>
        /// <param name="parent">The parent commit identifier. If not specified, defaults to an empty string.</param>
        /// <param name="tag">The tag associated with the cherry-pick operation. If not specified, defaults to an empty string.</param>
        public GitCherryPick(string id, string parent = "", string tag = "")
        {
            Id = id;
            Parent = parent;
            Tag = tag;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var returned = $"{Name} id:\"{Id}\"";

            if (!string.IsNullOrWhiteSpace(Parent))
                returned += $" parent: \"{Parent}\"";

            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";

            return returned;
        }
    }
}
