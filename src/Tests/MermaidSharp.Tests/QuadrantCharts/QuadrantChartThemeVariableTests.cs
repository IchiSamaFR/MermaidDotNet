using MermaidSharp.Configs.Themes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.QuadrantCharts
{
    /// <summary>
    /// Contains unit tests for <see cref="QuadrantChartThemeVariables"/> properties serialization.
    /// </summary>
    [TestClass]
    public class QuadrantChartThemeVariableTests
    {
        /// <summary>
        /// Verifies that all AThemeVariables base properties are serialized as expected via QuadrantChartThemeVariables.
        /// </summary>
        [TestMethod]
        public void QuadrantChartThemeVariables_AllBaseProperties_ToString_ReturnsExpectedYaml()
        {
            // Arrange
            var theme = new QuadrantChartThemeVariables
            {
                DarkMode = true,
                Background = "#111111",
                FontFamily = "Arial",
                FontSize = "16px",
                PrimaryColor = "#222222",
                PrimaryTextColor = "#ffffff",
                SecondaryColor = "#333333",
                PrimaryBorderColor = "#444444",
                SecondaryBorderColor = "#555555",
                SecondaryTextColor = "#666666",
                TertiaryColor = "#777777",
                TertiaryBorderColor = "#888888",
                TertiaryTextColor = "#999999",
                NoteBkgColor = "#aaaaaa",
                NoteTextColor = "#bbbbbb",
                NoteBorderColor = "#cccccc",
                LineColor = "#dddddd",
                TextColor = "#eeeeee",
                MainBkg = "#fafafa",
                ErrorBkgColor = "#ff0000",
                ErrorTextColor = "#00ff00"
            };

            string expected = @"---
themeVariables:
    darkMode: true
    background: ""#111111""
    fontFamily: ""Arial""
    fontSize: ""16px""
    primaryColor: ""#222222""
    primaryTextColor: ""#ffffff""
    secondaryColor: ""#333333""
    primaryBorderColor: ""#444444""
    secondaryBorderColor: ""#555555""
    secondaryTextColor: ""#666666""
    tertiaryColor: ""#777777""
    tertiaryBorderColor: ""#888888""
    tertiaryTextColor: ""#999999""
    noteBkgColor: ""#aaaaaa""
    noteTextColor: ""#bbbbbb""
    noteBorderColor: ""#cccccc""
    lineColor: ""#dddddd""
    textColor: ""#eeeeee""
    mainBkg: ""#fafafa""
    errorBkgColor: ""#ff0000""
    errorTextColor: ""#00ff00""
---";

            // Act
            string result = theme.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}