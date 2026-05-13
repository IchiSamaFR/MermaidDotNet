namespace MermaidSharp.AutoDiagram.Core.Tests.Models
{
    /// <summary>
    /// Represents a person with various properties and methods for testing.
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        internal int InternalId { get; set; }
        protected int Age { get; set; }
        private string SecretCode { get; set; } = "";

        public void SayHello() { }
        protected void DoWork() { }
        private void Hide() { }
        internal void InternalMethod() { }
    }
}