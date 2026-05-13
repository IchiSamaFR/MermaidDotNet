using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mock.Entities
{
	internal class Assignment
	{
		[Description("The unique identifier for the assignment.")]
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public DateOnly DueDate { get; set; }
		public int CourseId { get; set; }
		public required Course Course { get; set; }
		public ICollection<Submission> Submissions { get; set; } = [];
	}
}
