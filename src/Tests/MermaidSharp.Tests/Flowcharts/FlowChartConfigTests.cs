using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.Flowcharts
{
	[TestClass]
	public class FlowChartConfigTests
	{
		#region Theme Only

		[TestMethod]
		public void FlowChartConfig_ThemeDark_ReturnsExpectedOutput()
		{
			// Arrange
			var config = new FlowChartConfig(ConfigTheme.Dark);

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
		public void FlowChartConfig_ThemeForest_ReturnsExpectedOutput()
		{
			// Arrange
			var config = new FlowChartConfig(ConfigTheme.Forest);

			string expected = @"---
config:
    theme: forest
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void FlowChartConfig_NoTheme_ReturnsEmpty()
		{
			// Arrange
			var config = new FlowChartConfig();

			// Act
			string result = config.ToString();

			// Assert
			Assert.AreEqual(string.Empty, result);
		}

		#endregion

		#region ThemeVariables Only

		[TestMethod]
		public void FlowChartConfig_ThemeVariables_AllProperties_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new FlowChartThemeVariables
			{
				NodeBorder = "#ff0000",
				ClusterBkg = "#eeeeee",
				ClusterBorder = "#aaaaaa",
				DefaultLinkColor = "#333333",
				TitleColor = "#111111",
				EdgeLabelBackground = "#ffffff",
				NodeTextColor = "#000000"
			};
			var config = new FlowChartConfig(themeVariables: themeVariables);

			string expected = @"---
config:
    themeVariables:
        nodeBorder: ""#ff0000""
        clusterBkg: ""#eeeeee""
        clusterBorder: ""#aaaaaa""
        defaultLinkColor: ""#333333""
        titleColor: ""#111111""
        edgeLabelBackground: ""#ffffff""
        nodeTextColor: ""#000000""
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void FlowChartConfig_ThemeVariables_PartialProperties_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new FlowChartThemeVariables
			{
				NodeBorder = "#ff0000",
				TitleColor = "#111111"
			};
			var config = new FlowChartConfig(themeVariables: themeVariables);

			string expected = @"---
config:
    themeVariables:
        nodeBorder: ""#ff0000""
        titleColor: ""#111111""
---";

			// Act
			string result = config.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void FlowChartConfig_EmptyThemeVariables_ReturnsEmpty()
		{
			// Arrange
			var config = new FlowChartConfig(themeVariables: new FlowChartThemeVariables());

			// Act
			string result = config.ToString();

			// Assert
			Assert.AreEqual(string.Empty, result);
		}

		#endregion

		#region Theme And ThemeVariables Combined

		[TestMethod]
		public void FlowChartConfig_ThemeAndThemeVariables_ReturnsExpectedOutput()
		{
			// Arrange
			var themeVariables = new FlowChartThemeVariables
			{
				NodeBorder = "#ff0000",
				NodeTextColor = "#000000"
			};
			var config = new FlowChartConfig(ConfigTheme.Dark, themeVariables);

			string expected = @"---
config:
    theme: dark
    themeVariables:
        nodeBorder: ""#ff0000""
        nodeTextColor: ""#000000""
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