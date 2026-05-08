namespace MermaidSharp.AutoDiagram.Tests.Models
{
    public class Department<T>
    {
        public string Name { get; set; } = string.Empty;
        public List<T> Members { get; set; } = new List<T>();
    }
}