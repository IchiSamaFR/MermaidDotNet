using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a column in an entity relationship, including its name, data type, key type, and optional comment.
    /// </summary>
    /// <remarks>Use this class to describe the properties of a column when modeling relationships between
    /// entities, such as in database schema definitions or entity-relationship diagrams. The column's key type
    /// indicates its role in the relationship, such as primary key, foreign key, or none.</remarks>
    public class EntityRelationColumn
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the data type of the column.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the key type of the column, indicating its role in the entity relation.
        /// </summary>
        public RelationConstraintType ColumnKeyType { get; set; }
        /// <summary>
        /// Gets or sets the comment associated with the column.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Initializes a new instance of the EntityRelationColumn class with the specified column name, type, key type,
        /// and comment.
        /// </summary>
        /// <param name="name">The name of the column. Cannot be null.</param>
        /// <param name="type">The data type of the column. If not specified, an empty string is used.</param>
        /// <param name="columnKeyType">The key type of the column, indicating its role in the entity relation. Defaults to RelationContraintType.None.</param>
        /// <param name="comment">An optional comment describing the column. If not specified, an empty string is used.</param>
        public EntityRelationColumn(string name, string type = "", RelationConstraintType columnKeyType = RelationConstraintType.None, string comment = "")
        {
            Name = name;
            Type = type;
            ColumnKeyType = columnKeyType;
            Comment = comment;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var comment = !string.IsNullOrEmpty(Comment) ? $"\"{Comment}\"" : string.Empty;

            var returnedParts = new string[]
            {
                Type,
                Name,
                ColumnKeyType.PrimaryString(),
                comment
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
