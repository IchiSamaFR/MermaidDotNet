using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class GitBranchTests
	{
		[TestMethod]
		public void GitBranch()
		{
			//Arrange
			var branch = new GitBranch("myBranch");
			string expected = @"branch myBranch";

			//Act
			string result = branch.ToString();

			//Assert
			Assert.IsNotNull(branch);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}
	}
}
