using System;

namespace MermaidDotNet.EntityFramework.Tests.Mock.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime EnrolledAt { get; set; }
        public decimal? Grade { get; set; }
    }
}