namespace MermaidSharp.AutoDiagram.Tests.Models.DuplicateTypeNames.First
{
    internal class SharedName
    {
        public Second.SharedName Dependency { get; set; }
            = new Second.SharedName();
    }
}
