using MermaidSharp.Configs;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class GitGraphConfigTests
	{
		#region Theme Only

		[TestMethod]
		public void GitGraphConfig_ThemeDark()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Dark);

			string expected = @"---
config:
    theme: dark
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_ThemeForest()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Forest);

			string expected = @"---
config:
    theme: forest
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region GitGraph Config Only

		[TestMethod]
		public void GitGraphConfig_ShowCommitLabelTrue()
		{
			//Arrange
			var config = new GitGraphConfig { ShowCommitLabel = true };

			string expected = @"---
config:
    gitGraph:
        showCommitLabel: true
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_ShowBranchesFalse()
		{
			//Arrange
			var config = new GitGraphConfig { ShowBranches = false };

			string expected = @"---
config:
    gitGraph:
        showBranches: false
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_RotateCommitLabelTrue()
		{
			//Arrange
			var config = new GitGraphConfig { RotateCommitLabel = true };

			string expected = @"---
config:
    gitGraph:
        rotateCommitLabel: true
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_MainBranchName()
		{
			//Arrange
			var config = new GitGraphConfig { MainBranchName = "master" };

			string expected = @"---
config:
    gitGraph:
        mainBranchName: ""master""
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Combined Config

		[TestMethod]
		public void GitGraphConfig_AllGitGraphParams()
		{
			//Arrange
			var config = new GitGraphConfig
			{
				ShowCommitLabel = false,
				ShowBranches = true,
				RotateCommitLabel = true,
				MainBranchName = "master"
			};

			string expected = @"---
config:
    gitGraph:
        showCommitLabel: false
        showBranches: true
        rotateCommitLabel: true
        mainBranchName: ""master""
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_ThemeAndGitGraphParams()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Forest)
			{
				ShowCommitLabel = true,
				MainBranchName = "master"
			};

			string expected = @"---
config:
    theme: forest
    gitGraph:
        showCommitLabel: true
        mainBranchName: ""master""
---";

			//Act
			string result = config.ToString();

			//Assert
			Assert.IsNotNull(config);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion
	}
}