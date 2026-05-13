namespace MermaidSharp.AutoDiagram.Core.Tests.Models
{
    public interface IContactable
    {
        string Email { get; set; }
        void Contact(string message);
    }
}