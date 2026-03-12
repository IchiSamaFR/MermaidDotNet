using System;
using System.Collections.Generic;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidDotNet.EntityFramework.Tests.Mock.Entities
{
    public class Assignment
    {
        [Description("The unique identifier for the assignment.")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}