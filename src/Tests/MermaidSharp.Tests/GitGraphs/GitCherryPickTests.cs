using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class GitCherryPickTests
	{
		[TestMethod]
		public void GitCherryPick()
		{
			//Arrange
			var cherryPick = new GitCherryPick("abc123");
			string expected = @"cherry-pick id:""abc123""";

			//Act
			string result = cherryPick.ToString();

			//Assert
			Assert.IsNotNull(cherryPick);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitCherryPick_WithParent()
		{
			//Arrange
			var cherryPick = new GitCherryPick("abc123", "myParent");
			string expected = @"cherry-pick id:""abc123"" parent: ""myParent""";

			//Act
			string result = cherryPick.ToString();

			//Assert
			Assert.IsNotNull(cherryPick);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitCherryPick_WithParentAndTag()
		{
			//Arrange
			var cherryPick = new GitCherryPick("abc123", "myParent", "myTag");
			string expected = @"cherry-pick id:""abc123"" parent: ""myParent"" tag: ""myTag""";
			//Act
			string result = cherryPick.ToString();

			//Assert
			Assert.IsNotNull(cherryPick);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}
	}
}