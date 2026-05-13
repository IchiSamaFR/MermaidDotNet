using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a namespace containing a collection of class definitions.
    /// </summary>
    /// <remarks>Use this class to group related classes under a single namespace. The namespace name and its
    /// contained classes can be accessed and modified as needed.</remarks>
    public class ClassNamespace
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the collection of class nodes associated with the namespace.
        /// </summary>
        public List<ClassNode> Classes { get; }

        /// <summary>
        /// Initializes a new instance of the ClassNamespace class with the specified namespace name and an optional
        /// list of class nodes.
        /// </summary>
        /// <param name="name">The name of the namespace to associate with this instance. Cannot be null.</param>
        /// <param name="classes">An optional list of ClassNode objects to include in the namespace. If null, an empty list is used.</param>
        public ClassNamespace(string name, List<ClassNode> classes = null)
        {
            Name = name;
            Classes = classes ?? new List<ClassNode>();
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var lines = new List<string>();
            var nameString = new string[] {
                "namespace",
                Name
            }.JoinNonEmpty(" ");
            lines.Add(string.Join(" ", nameString, "{"));
            lines.AddRange(Classes.Select(c => c.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
