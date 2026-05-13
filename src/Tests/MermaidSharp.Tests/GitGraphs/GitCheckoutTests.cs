using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class GitCheckoutTests
	{
		[TestMethod]
		public void GitCheckout()
		{
			//Arrange
			var checkout = new GitCheckout("myBranch");
			string expected = @"checkout myBranch";

			//Act
			string result = checkout.ToString();

			//Assert
			Assert.IsNotNull(checkout);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}
	}
}
