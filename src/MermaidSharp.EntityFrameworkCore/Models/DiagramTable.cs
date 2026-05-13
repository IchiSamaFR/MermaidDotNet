using MermaidSharp.EntityFrameworkCore.Contexts;
using System.Collections.Generic;

namespace MermaidSharp.EntityFrameworkCore.Models
{
    internal class DiagramTable
    {
        public EntityTypeContext EntityType { get; set; }
        public string Name { get; set; }
        public List<DiagramColumn> Columns { get; set; } = new List<DiagramColumn>();
    }
}
