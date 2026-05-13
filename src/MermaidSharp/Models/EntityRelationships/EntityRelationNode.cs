using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents an entity node for use in entity-relationship diagrams, containing a collection of columns and
    /// associated metadata.
    /// </summary>
    /// <remarks>Use this class to model entities with columns when generating entity-relationship diagrams
    /// using Mermaid syntax. The columns define the structure and attributes of the entity. Inherits from Node to
    /// provide common node functionality.</remarks>
    public class EntityRelationNode : ANode
    {
        /// <summary>
        /// Gets or sets the collection of columns that define the relationship between entities.
        /// </summary>
        public List<EntityRelationColumn> Columns { get; set; }

        /// <summary>
        /// Initializes a new instance of the EntityRelationNode class with the specified name, display text, columns,
        /// and optional CSS class.
        /// </summary>
        /// <param name="name">The unique identifier for the node.</param>
        /// <param name="text">The display text associated with the node. This parameter is optional.</param>
        /// <param name="columns">A list of columns that define the structure of the entity relation. If null, an empty list is used.</param>
        /// <param name="cssClass">An optional CSS class to apply custom styling to the node.</param>
        public EntityRelationNode(string name, string text = "", List<EntityRelationColumn> columns = null, string cssClass = "")
            : base(name, text, cssClass)
        {
            Columns = columns ?? new List<EntityRelationColumn>();
        }


        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var lines = new List<string>();
            if (Columns.Count == 0)
            {
                return base.ToString();
            }
            lines.Add(string.Join(" ", base.ToString(), "{"));
            lines.AddRange(Columns.Select(c => c.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
