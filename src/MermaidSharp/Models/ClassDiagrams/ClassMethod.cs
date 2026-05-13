using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a method within a class, including its name, return type, visibility, and parameters.
    /// </summary>
    /// <remarks>Use this class to describe the signature and characteristics of a class method, such as when
    /// generating code or analyzing class structures. The parameters collection is initialized to an empty list if not
    /// provided.</remarks>
    public class ClassMethod
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the return type of the method.
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// Gets or sets the visibility level of the class property.
        /// </summary>
        public ClassPropertyVisibility Visibility { get; set; }
        /// <summary>
        /// Gets the collection of parameters associated with the method or class.
        /// </summary>
        public List<ClassMethodParam> Parameters { get; }

        /// <summary>
        /// Initializes a new instance of the ClassMethod class with the specified method name, return type, visibility,
        /// and parameters.
        /// </summary>
        /// <param name="name">The name of the method to be represented.</param>
        /// <param name="returnType">The return type of the method. If not specified, an empty string is used.</param>
        /// <param name="visibility">The visibility level of the method, such as public, private, or protected.</param>
        /// <param name="parameters">A list of parameters for the method. If null, an empty list is used.</param>
        public ClassMethod(string name, string returnType = "", ClassPropertyVisibility visibility = ClassPropertyVisibility.Public, List<ClassMethodParam> parameters = null)
        {
            Name = name;
            ReturnType = returnType;
            Visibility = visibility;
            Parameters = parameters ?? new List<ClassMethodParam>();
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var visibilitySymbol = Visibility.PrimaryString();
            var returnTypeString = ReturnType.FormatAngleBracket();
            var parametersString = string.Join(", ", Parameters.Select(p => p.ToString()));

            var returnedParts = new string[]
            {
                $"{visibilitySymbol}{Name}({parametersString})",
                returnTypeString
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
