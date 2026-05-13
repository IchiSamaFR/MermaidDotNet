using System.Text;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a node in a Mermaid flowchart, containing a unique name, display text, and optional CSS class for
    /// styling.
    /// </summary>
    public abstract class ANode
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the text content associated with this instance.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the CSS class or classes to apply to the element.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the Node class with the specified name, display text, and optional CSS class.
        /// </summary>
        /// <param name="name">The unique identifier for the node. Spaces will be removed from this value.</param>
        /// <param name="text">The display text associated with the node.</param>
        /// <param name="cssClass">An optional CSS class to apply to the node for custom styling. If not specified, no CSS class is applied.</param>
        protected ANode(string name, string text, string cssClass = "")
        {
            Name = name.Replace(" ", "");
            Text = text;
            CssClass = cssClass;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(GetSurroundedText());
            }

            return sb.ToString();
        }
        /// <summary>
        /// Generates a Mermaid class diagram string representation for the current class, including its CSS class if
        /// specified.
        /// </summary>
        /// <returns>A string representing the class in Mermaid syntax with its CSS class, or an empty string if no CSS class is
        /// defined.</returns>
        public virtual string ToClassString()
        {
            if (string.IsNullOrEmpty(CssClass))
            {
                return string.Empty;
            }

            return $"class {Name} {CssClass}";
        }
        /// <summary>
        /// Returns the text value surrounded by square brackets.
        /// </summary>
        /// <returns>A string containing the current text value enclosed in square brackets.</returns>
        protected virtual string GetSurroundedText()
        {
            return $"[{Text}]";
        }
    }
}
