using MermaidSharp.Enums;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents contextual information about a property within a class, including its metadata and associated type
    /// context.
    /// </summary>
    /// <remarks>This class provides access to the property's name, reflection metadata, and a type context
    /// for advanced analysis or code generation scenarios. It is typically used in scenarios where property-level
    /// inspection or manipulation is required, such as serialization, mapping, or code analysis tools.</remarks>
    public class ClassPropertyContext : ClassMemberInfo
    {
        /// <summary>
        /// Gets the metadata for the property represented by this instance.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets the context information for the class type associated with this instance.
        /// </summary>
        public ClassTypeContext Type { get; }

        /// <summary>
        /// Initializes a new instance of the ClassPropertyContext class for the specified property.
        /// </summary>
        /// <param name="property">The property metadata to associate with this context. Cannot be null.</param>
        /// <param name="visibility">The visibility of the property. Cannot be null.</param>
        public ClassPropertyContext(PropertyInfo property, ClassPropertyVisibility visibility)
        {
            Property = property;
            Name = property.Name;
            Visibility = visibility;
            Type = new ClassTypeContext(property.PropertyType);
        }
    }
}
