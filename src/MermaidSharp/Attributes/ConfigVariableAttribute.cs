using System;

namespace MermaidSharp.Attributes
{
    /// <summary>
    /// Specifies Mermaid config variable metadata for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigVariableAttribute : Attribute
    {
        /// <summary>
        /// Gets the Mermaid config variable name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigVariableAttribute"/> class.
        /// </summary>
        /// <param name="name">Mermaid config variable name.</param>
        public ConfigVariableAttribute(string name)
        {
            Name = name;
        }
    }
}