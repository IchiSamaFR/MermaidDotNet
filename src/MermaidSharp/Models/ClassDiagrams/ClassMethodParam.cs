using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a parameter of a class method, including its name and type.
    /// </summary>
    /// <remarks>This class is typically used to describe method parameters when generating code or
    /// documentation. The type information may be empty if not specified.</remarks>
    public class ClassMethodParam
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the type of the method parameter.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the ClassMethodParam class with the specified parameter name and type.
        /// </summary>
        /// <param name="name">The name of the method parameter.</param>
        /// <param name="type">The type of the method parameter. If not specified, an empty string is used.</param>
        public ClassMethodParam(string name, string type = "")
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var returnedParts = new string[]
            {
                Type.FormatAngleBracket(),
                Name
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
