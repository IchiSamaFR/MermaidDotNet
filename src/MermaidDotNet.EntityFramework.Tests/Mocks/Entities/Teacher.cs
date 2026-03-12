using System.Collections.Generic;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidDotNet.EntityFramework.Tests.Mock.Entities
{
    public class Teacher
    {
        [Description("The unique identifier for the teacher.")]
        public int Id { get; set; }
        [Description("The first name of the teacher.")]
        public string FirstName { get; set; }
        [Description("The last name of the teacher.")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
    }
}