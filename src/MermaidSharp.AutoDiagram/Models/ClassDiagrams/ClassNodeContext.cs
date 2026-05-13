using System;
using System.Collections.Generic;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents the context information for a class node, including its type, methods, and properties.
    /// </summary>
    /// <remarks>This context is typically used for analyzing or generating metadata about a class, such as in
    /// code analysis or diagram generation scenarios. The class provides access to the class type information as well
    /// as collections of its methods and properties.</remarks>
    public class ClassNodeContext
    {
        /// <summary>
        /// Gets the context information for the class type associated with this instance.
        /// </summary>
        public ClassTypeContext Type { get; }

        /// <summary>
        /// Gets the collection of method contexts defined for the class.
        /// </summary>
        public List<ClassMethodContext> Methods { get; } = new List<ClassMethodContext>();

        /// <summary>
        /// Gets the collection of property contexts associated with the class.
        /// </summary>
        public List<ClassPropertyContext> Properties { get; } = new List<ClassPropertyContext>();

        /// <summary>
        /// Initializes a new instance of the ClassNodeContext class for the specified type.
        /// </summary>
        /// <param name="type">The type to be represented by this context. Cannot be null.</param>
        public ClassNodeContext(Type type)
        {
            Type = new ClassTypeContext(type);
        }
    }
}
