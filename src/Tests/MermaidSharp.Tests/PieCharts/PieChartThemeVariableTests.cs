using MermaidSharp.Configs.Themes;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MermaidSharp.Tests.PieCharts
{
	[TestClass]
	public class PieChartThemeVariableTests
	{
		[TestMethod]
		public void PieChartThemeVariableTests_CalculateConfig()
		{
			// Arrange
			var themeVariables = new PieChartThemeVariables
			{
				PieTitleTextSize = "18px",
				PieTitleTextColor = "#333333",
				PieSectionTextSize = "14px",
				PieSectionTextColor = "#666666",
				PieLegendTextSize = "12px",
				PieLegendTextColor = "#999999",
				PieStrokeColor = "#000000",
				PieOuterStrokeWidth = "2px",
				PieOpacity = 0.8
			};
			string expected = @"---
themeVariables:
    pieTitleTextSize: ""18px""
    pieTitleTextColor: ""#333333""
    pieSectionTextSize: ""14px""
    pieSectionTextColor: ""#666666""
    pieLegendTextSize: ""12px""
    pieLegendTextColor: ""#999999""
    pieStrokeColor: ""#000000""
    pieOuterStrokeWidth: ""2px""
    pieOpacity: 0.8
---";

			// Act
			var result = themeVariables.ToString();

			// Assert
			Assert.AreEqual(expected, result);
		}


		[TestMethod]
		public void PieChartThemeVariableTests_CalculatePartConfig()
		{
			// Arrange
			var themeVariables = new PieChartThemeVariables
			{
				PieTitleTextSize = "18px",
				PieTitleTextColor = "#333333",
				PieSectionTextSize = "14px",
				PieSectionTextColor = "#666666",
				PieLegendTextSize = "12px",
			};
			string expected = @"---
themeVariables:
    pieTitleTextSize: ""18px""
    pieTitleTextColor: ""#333333""
    pieSectionTextSize: ""14px""
    pieSectionTextColor: ""#666666""
    pieLegendTextSize: ""12px""
---";

			// Act
			var result = themeVariables.ToString();

			// Assert
			Assert.AreEqual(expected, result);
		}
	}
}
