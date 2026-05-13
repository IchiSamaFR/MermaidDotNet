namespace MermaidSharp.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Address? Address { get; set; }
        public int SchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        public ICollection<Submission> Submissions { get; set; } = [];
    }
}
