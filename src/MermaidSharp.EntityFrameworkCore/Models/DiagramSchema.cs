using System.Collections.Generic;

namespace MermaidSharp.EntityFrameworkCore.Models
{
    internal class DiagramSchema
    {
        public List<DiagramTable> Tables { get; set; } = new List<DiagramTable>();
        public List<DiagramLink> Links { get; set; } = new List<DiagramLink>();
    }
}
