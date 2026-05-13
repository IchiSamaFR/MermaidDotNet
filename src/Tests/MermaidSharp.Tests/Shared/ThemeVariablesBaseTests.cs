using MermaidSharp.Configs.Themes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.Shared
{
	[TestClass]
	public class ThemeVariablesBaseTests
	{
		// Uses FlowChartThemeVariables as a concrete implementation to test ThemeVariables base class behavior

		[TestMethod]
		public void ThemeVariables_DarkMode_True_ReturnsExpectedOutput()
		{
			// Arrange
			var tv = new FlowChartThemeVariables { DarkMode = true };

			string expected = @"---
themeVariables:
    darkMode: true
---";

			// Act
			Assert.AreEqual(expected, tv.ToString());
		}

		[TestMethod]
		public void ThemeVariables_DarkMode_False_ReturnsExpectedOutput()
		{
			// Arrange
			var tv = new FlowChartThemeVariables { DarkMode = false };

			string expected = @"---
themeVariables:
    darkMode: false
---";

			// Act
			Assert.AreEqual(expected, tv.ToString());
		}

		[TestMethod]
		public void ThemeVariables_Background_ReturnsExpectedOutput()
		{
			// Arrange
			var tv = new FlowChartThemeVariables { Background = "#ffffff" };

			string expected = @"---
themeVariables:
    background: ""#ffffff""
---";

			// Act
			Assert.AreEqual(expected, tv.ToString());
		}

		[TestMethod]
		public void ThemeVariables_AllBaseProperties_ReturnsExpectedOutput()
		{
			// Arrange
			var tv = new FlowChartThemeVariables
			{
				DarkMode = false,
				Background = "#ffffff",
				FontFamily = "arial",
				FontSize = "14px",
				PrimaryColor = "#aabbcc",
				PrimaryTextColor = "#111111",
				SecondaryColor = "#ddeeff",
				PrimaryBorderColor = "#334455",
				SecondaryBorderColor = "#667788",
				SecondaryTextColor = "#223344",
				TertiaryColor = "#aabbdd",
				TertiaryBorderColor = "#bbccdd",
				TertiaryTextColor = "#ccddee",
				NoteBkgColor = "#ffffde",
				NoteTextColor = "#333333",
				NoteBorderColor = "#aaaaaa",
				LineColor = "#000000",
				TextColor = "#111111",
				MainBkg = "#f0f0f0",
				ErrorBkgColor = "#ff0000",
				ErrorTextColor = "#ffffff"
			};

			string expected = @"---
themeVariables:
    darkMode: false
    background: ""#ffffff""
    fontFamily: ""arial""
    fontSize: ""14px""
    primaryColor: ""#aabbcc""
    primaryTextColor: ""#111111""
    secondaryColor: ""#ddeeff""
    primaryBorderColor: ""#334455""
    secondaryBorderColor: ""#667788""
    secondaryTextColor: ""#223344""
    tertiaryColor: ""#aabbdd""
    tertiaryBorderColor: ""#bbccdd""
    tertiaryTextColor: ""#ccddee""
    noteBkgColor: ""#ffffde""
    noteTextColor: ""#333333""
    noteBorderColor: ""#aaaaaa""
    lineColor: ""#000000""
    textColor: ""#111111""
    mainBkg: ""#f0f0f0""
    errorBkgColor: ""#ff0000""
    errorTextColor: ""#ffffff""
---";

			// Act
			Assert.AreEqual(expected, tv.ToString());
		}

		[TestMethod]
		public void ThemeVariables_Empty_ReturnsEmpty()
		{
			// Arrange
			var tv = new FlowChartThemeVariables();

			// Act
			Assert.AreEqual(string.Empty, tv.ToString());
		}
	}
}