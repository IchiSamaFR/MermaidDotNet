namespace MermaidSharp.AutoDiagram.Core.Tests.Models
{
    public class Employee : Person, IContactable
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public void Contact(string message) { }
        public Department<Employee> Department { get; set; } = new Department<Employee>();
    }
}