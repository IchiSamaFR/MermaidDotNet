using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a property within a class, including its name, type, and visibility.
    /// </summary>
    /// <remarks>Use this class to model and describe properties when generating or analyzing class structures
    /// programmatically. The visibility is represented by the ClassPropertyVisibility enumeration, which determines how
    /// the property is exposed in the class definition.</remarks>
    public class ClassProperty
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the visibility level of the class property.
        /// </summary>
        public ClassPropertyVisibility Visibility { get; set; }

        /// <summary>
        /// Initializes a new instance of the ClassProperty class with the specified property name, type, and
        /// visibility.
        /// </summary>
        /// <param name="name">The name of the property to be represented.</param>
        /// <param name="type">The data type of the property.</param>
        /// <param name="visibility">The visibility level of the property, such as public, private, or protected.</param>
        public ClassProperty(string name, string type, ClassPropertyVisibility visibility)
        {
            Name = name;
            Type = type;
            Visibility = visibility;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var visibilitySymbol = Visibility.PrimaryString();
            return $"{visibilitySymbol}{Type.FormatAngleBracket()} {Name}";
        }
    }
}
