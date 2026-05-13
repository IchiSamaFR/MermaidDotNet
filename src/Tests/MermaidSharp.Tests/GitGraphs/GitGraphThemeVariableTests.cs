using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class GitGraphThemeVariableTests
	{
		#region ThemeVariables inherited properties

		[TestMethod]
		public void GitGraphThemeVariables_PrimaryColor_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables
			{
				PrimaryColor = "#aabbcc"
			};

			string expected = @"---
themeVariables:
    primaryColor: ""#aabbcc""
---";

			// Act
			string result = themeVariables.ToString();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphThemeVariables_DarkMode_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables
			{
				DarkMode = true
			};

			string expected = @"---
themeVariables:
    darkMode: true
---";

			// Act
			string result = themeVariables.ToString();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphThemeVariables_Empty_ReturnsEmpty()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables();

			// Act
			string result = themeVariables.ToString();

			// Assert
			Assert.AreEqual(string.Empty, result);
		}

		#endregion

		#region GitGraphConfig with ThemeVariables

		[TestMethod]
		public void GitGraphConfig_WithThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables
			{
				PrimaryColor = "#aabbcc",
				FontSize = "14px"
			};
			var config = new GitGraphConfig(themeVariables: themeVariables);

			string expected = @"---
config:
    themeVariables:
        fontSize: ""14px""
        primaryColor: ""#aabbcc""
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_ThemeAndThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables
			{
				PrimaryColor = "#aabbcc"
			};
			var config = new GitGraphConfig(ConfigTheme.Dark, themeVariables: themeVariables);

			string expected = @"---
config:
    theme: dark
    themeVariables:
        primaryColor: ""#aabbcc""
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraphConfig_ThemeVariablesAndGitGraphParams_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new GitGraphThemeVariables
			{
				PrimaryColor = "#aabbcc"
			};
			var config = new GitGraphConfig(ConfigTheme.Forest, showCommitLabel: true, mainBranchName: "main", themeVariables: themeVariables);

			string expected = @"---
config:
    theme: forest
    gitGraph:
        showCommitLabel: true
        mainBranchName: ""main""
    themeVariables:
        primaryColor: ""#aabbcc""
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion
	}
}