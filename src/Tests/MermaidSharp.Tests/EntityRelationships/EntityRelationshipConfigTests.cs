using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.EntityRelationships
{
	[TestClass]
	public class EntityRelationshipConfigTests
	{
		#region Theme Only

		[TestMethod]
		public void EntityRelationshipConfig_ThemeDark_ReturnsExpectedOutput()
		{
			// Arrange
			var config = new EntityRelationshipConfig(ConfigTheme.Dark);

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
		public void EntityRelationshipConfig_NoTheme_ReturnsEmpty()
		{
			// Arrange
			var config = new EntityRelationshipConfig();

			// Act
			string result = config.ToString();

			// Assert
			Assert.AreEqual(string.Empty, result);
		}

		#endregion

		#region ThemeVariables

		[TestMethod]
		public void EntityRelationshipConfig_ThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new EntityRelationshipThemeVariables
			{
				PrimaryColor = "#aabbcc",
				FontSize = "14px"
			};
			var config = new EntityRelationshipConfig(themeVariables: themeVariables);

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
		public void EntityRelationshipConfig_ThemeAndThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new EntityRelationshipThemeVariables
			{
				PrimaryColor = "#aabbcc"
			};
			var config = new EntityRelationshipConfig(ConfigTheme.Dark, themeVariables);

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