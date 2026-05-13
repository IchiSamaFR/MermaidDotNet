using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a class node in a Mermaid diagram, including its type, properties, and methods.
    /// </summary>
    /// <remarks>Use this class to model object-oriented class structures within Mermaid diagrams. The class
    /// node can include a type annotation, a collection of properties, and a collection of methods, allowing for
    /// detailed representation of class definitions in UML or class diagrams.</remarks>
    public class ClassNode : ANode
    {
        /// <summary>
        /// Gets or sets the type or category of the class.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets the collection of properties defined for the class.
        /// </summary>
        public List<ClassProperty> Properties { get; }
        /// <summary>
        /// Gets the collection of methods defined for the class.
        /// </summary>
        public List<ClassMethod> Methods { get; }

        /// <summary>
        /// Initializes a new instance of the ClassNode class with the specified name, type, display text, properties,
        /// and methods.
        /// </summary>
        /// <param name="name">The unique identifier for the class node.</param>
        /// <param name="type">The type or category of the class. This parameter is optional.</param>
        /// <param name="text">The display text or label for the class node. This parameter is optional.</param>
        /// <param name="properties">A list of properties associated with the class. If null, an empty list is used.</param>
        /// <param name="methods">A list of methods associated with the class. If null, an empty list is used.</param>
        /// <param name="cssClass">The CSS class to apply to the class node for styling purposes. This parameter is optional.</param>
        public ClassNode(string name, string type = "", string text = "", List<ClassProperty> properties = null, List<ClassMethod> methods = null, string cssClass = "") : base(name, text, cssClass)
        {
            Type = type;
            Properties = properties ?? new List<ClassProperty>();
            Methods = methods ?? new List<ClassMethod>();
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var lines = new List<string>();
            var nameString = new string[] {
                "class",
                Name,
                string.IsNullOrEmpty(Type) ? string.Empty : $"~{Type.FormatAngleBracket()}~"
            }.JoinNonEmpty(" ");

            if (Properties.Count == 0 && Methods.Count == 0)
            {
                return nameString;
            }
            lines.Add(string.Join(" ", nameString, "{"));
            lines.AddRange(Properties.Select(p => p.ToString()).Indent());
            lines.AddRange(Methods.Select(m => m.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
