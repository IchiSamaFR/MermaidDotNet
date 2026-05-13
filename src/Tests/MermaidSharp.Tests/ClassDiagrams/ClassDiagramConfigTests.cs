using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.ClassDiagrams
{
	[TestClass]
	public class ClassDiagramConfigTests
	{
		#region Theme Only

		[TestMethod]
		public void ClassDiagramConfig_ThemeDark_ReturnsExpectedOutput()
		{
			// Arrange
			var config = new ClassDiagramConfig(ConfigTheme.Dark);

			string expected = @"---
config:
    theme: dark
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassDiagramConfig_NoTheme_ReturnsEmpty()
		{
			// Arrange
			var config = new ClassDiagramConfig();

			// Act
			string result = config.ToString();

			// Assert
			Assert.AreEqual(string.Empty, result);
		}

		#endregion

		#region ThemeVariables

		[TestMethod]
		public void ClassDiagramConfig_ThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new ClassDiagramThemeVariables
			{
				PrimaryColor = "#aabbcc",
				FontSize = "14px"
			};
			var config = new ClassDiagramConfig(themeVariables: themeVariables);

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
		public void ClassDiagramConfig_ThemeAndThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new ClassDiagramThemeVariables
			{
				PrimaryColor = "#aabbcc"
			};
			var config = new ClassDiagramConfig(ConfigTheme.Dark, themeVariables);

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

		#endregion
	}
}