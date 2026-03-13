using MermaidDotNet.EntityFrameworkCore.Contexts;
using MermaidDotNet.Enums;
using System;

namespace MermaidDotNet.EntityFrameworkCore.Models
{
    internal class DiagramColumn
    {
        public PropertyTypeContext Property { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool IsNullable { get; set; }
        public ColumnKeyType ColumnKeyType { get; set; }
    }
}
