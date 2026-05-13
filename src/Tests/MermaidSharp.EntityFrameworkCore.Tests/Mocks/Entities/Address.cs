using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mock.Entities
{
	internal class Address
	{
		public string? Street { get; set; }
		public required string City { get; set; }
		[DescriptionAttribute("37000, 63400, ...")]
		public required string PostalCode { get; set; }
	}
}
