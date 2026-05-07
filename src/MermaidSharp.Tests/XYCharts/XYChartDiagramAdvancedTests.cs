using MermaidSharp.Configs;
using MermaidSharp.Configs.Enums;
using MermaidSharp.Configs.Themes.Children;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.XYCharts
{
    /// <summary>
    /// Contains advanced and comprehensive unit tests for <see cref="XYChartDiagram"/>.
    /// </summary>
    [TestClass]
    public class XYChartDiagramAdvancedTests
    {
        /// <summary>
        /// Verifies that CalculateDiagram returns a complex diagram with multiple series, labels, and axes.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ComplexScenario_ReturnsExpectedDiagram()
        {
            // Arrange
            var themeVariables = new MermaidSharp.Configs.Themes.XYChartThemeVariables
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
                ErrorTextColor = "#00ff00",
                XYChartConfig = new XYChartThemeChildConfig
                {
                    BackgroundColor = "#f0f0f0",
                    TitleColor = "#123456",
                    DataLabelColor = "#654321",
                    XAxisLabelColor = "#abcdef",
                    XAxisTitleColor = "#fedcba",
                    XAxisTickColor = "#112233",
                    XAxisLineColor = "#334455",
                    YAxisLabelColor = "#223344",
                    YAxisTitleColor = "#556677",
                    YAxisTickColor = "#778899",
                    YAxisLineColor = "#8899aa",
                    PlotColorPalette = "#aabbcc,#ddeeff"
                }
            };

            var config = new XYChartConfig(themeVariables: themeVariables)
            {
                Width = 1000,
                Height = 600,
                TitlePadding = 20,
                TitleFontSize = 28,
                ShowTitle = true,
                XAxis = XAxisPosition.Bottom,
                YAxis = YAxisPosition.Left,
                ChartOrientationValue = ChartOrientation.Horizontal,
                PlotReservedSpacePercent = 15,
                ShowDataLabel = true,
                ShowDataLabelOutsideBar = false
            };
            var diagram = new XYChartDiagram("Advanced XY Chart", "Month", "Revenue", config);
            diagram.XAxis.Labels.AddRange(new[] { "Jan", "Feb", "Mar", "Apr" });
            diagram.AddSeries(XYSeriesType.Bar).AddPoints(20, 30, 40, 50);
            diagram.AddSeries(XYSeriesType.Line).AddPoints(25, 35, 45, 55);
            diagram.AddSeries(XYSeriesType.Line).AddPoints(22, 32, 42, 52);

            string expected =
@"---
title: Advanced XY Chart
config:
    xyChart:
        width: 1000
        height: 600
        titlePadding: 20
        titleFontSize: 28
        showTitle: true
        xAxis: bottom
        yAxis: left
        chartOrientation: horizontal
        plotReservedSpacePercent: 15
        showDataLabel: true
        showDataLabelOutsideBar: false
    themeVariables:
        xyChart:
            backgroundColor: ""#f0f0f0""
            titleColor: ""#123456""
            dataLabelColor: ""#654321""
            xAxisLabelColor: ""#abcdef""
            xAxisTitleColor: ""#fedcba""
            xAxisTickColor: ""#112233""
            xAxisLineColor: ""#334455""
            yAxisLabelColor: ""#223344""
            yAxisTitleColor: ""#556677""
            yAxisTickColor: ""#778899""
            yAxisLineColor: ""#8899aa""
            plotColorPalette: ""#aabbcc,#ddeeff""
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
---
xychart
x-axis ""Month"" [""Jan"", ""Feb"", ""Mar"", ""Apr""]
y-axis ""Revenue""
bar [20, 30, 40, 50]
line [25, 35, 45, 55]
line [22, 32, 42, 52]";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns the expected diagram with only X axis labels set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_XAxisLabelsOnly_ReturnsExpectedDiagram()
        {
            // Arrange
            var diagram = new XYChartDiagram("Labels Only", "Quarter", "", null);
            diagram.XAxis.Labels.AddRange(new[] { "Q1", "Q2", "Q3" });
            string expected =
@"---
title: Labels Only
---
xychart
x-axis ""Quarter"" [""Q1"", ""Q2"", ""Q3""]
y-axis ""yAxis""";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns the expected diagram with only Y axis label set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_YAxisLabelOnly_ReturnsExpectedDiagram()
        {
            // Arrange
            var diagram = new XYChartDiagram("Y Axis Only", "", "Amount", null);
            string expected =
@"---
title: Y Axis Only
---
xychart
x-axis ""xAxis"" []
y-axis ""Amount""";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
