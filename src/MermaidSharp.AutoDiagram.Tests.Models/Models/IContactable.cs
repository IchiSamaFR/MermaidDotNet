namespace MermaidSharp.AutoDiagram.Tests.Models
{
    public interface IContactable
    {
        string Email { get; set; }
        void Contact(string message);
    }
}