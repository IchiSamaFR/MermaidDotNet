using MermaidSharp.Attributes;
using System;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the type of key constraint applied to a database column, such as primary key, foreign key, or unique
    /// key. Supports bitwise combination of its member values.
    /// </summary>
    /// <remarks>Use this enumeration to indicate one or more key constraints for a column in a database
    /// schema. Multiple values can be combined using a bitwise OR operation to represent columns that participate in
    /// multiple key constraints.</remarks>
    [Flags]
    public enum RelationConstraintType
    {
#pragma warning disable CS1591
        None = 0,

        [MermaidEnum("PK")]
        PrimaryKey = 1,

        [MermaidEnum("FK")]
        ForeignKey = 2,

        [MermaidEnum("UK")]
        UniqueKey = 4,

        [MermaidEnum("PK, FK")]
        PrimaryKeyForeignKey = PrimaryKey | ForeignKey,

        [MermaidEnum("PK, UK")]
        PrimaryKeyUniqueKey = PrimaryKey | UniqueKey,

        [MermaidEnum("FK, UK")]
        ForeignKeyUniqueKey = ForeignKey | UniqueKey,

        [MermaidEnum("PK, FK, UK")]
        PrimaryKeyForeignKeyUniqueKey = PrimaryKey | ForeignKey | UniqueKey,
#pragma warning restore CS1591
    }
}
