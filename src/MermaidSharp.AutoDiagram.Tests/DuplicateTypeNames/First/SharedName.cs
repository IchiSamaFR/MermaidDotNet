namespace MermaidSharp.AutoDiagram.Tests.DuplicateTypeNames.First
{
    internal class SharedName
    {
        public MermaidSharp.AutoDiagram.Tests.DuplicateTypeNames.Second.SharedName Dependency { get; set; }
            = new MermaidSharp.AutoDiagram.Tests.DuplicateTypeNames.Second.SharedName();
    }
}
