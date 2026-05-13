using MermaidSharp.EntityFrameworkCore.Contexts;
using MermaidSharp.Enums;
using System;

namespace MermaidSharp.EntityFrameworkCore.Models
{
	internal class DiagramColumn
	{
		public PropertyTypeContext Property { get; set; }
		public string Name { get; set; }
		public Type Type { get; set; }
		public bool IsNullable { get; set; }
		public RelationConstraintType ColumnKeyType { get; set; }
	}
}
