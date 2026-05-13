using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.XYCharts
{
    [TestClass]
    public class XYChartDiagramTests
    {
        [TestMethod]
        public void XYChartDiagram_CalculateDiagram_Comprehensive()
        {
            // Arrange
            var config = new XYChartConfig
            {
                Width = 800,
                Height = 400,
                ShowTitle = true
            };
            var diagram = new XYChartDiagram("Comprehensive XY Chart", "Quarter", "Value", config);
            diagram.XAxis.Labels.AddRange(new[] { "Q1", "Q2", "Q3", "Q4" });
            var series1 = diagram.AddSeries(XYSeriesType.Bar)
                .AddPoints(5, 15, 25, 20);
            var series2 = diagram.AddSeries(XYSeriesType.Line)
                .AddPoints(10, 12, 23, 14);
            var series3 = diagram.AddSeries(XYSeriesType.Line)
                .AddPoints(8, 18, 28, 23);
            var series4 = diagram.AddSeries(XYSeriesType.Line)
                .AddPoints(12, 22, 32, 30);

            var expected =
@"---
title: Comprehensive XY Chart
config:
    xyChart:
        width: 800
        height: 400
        showTitle: true
---
xychart
    x-axis ""Quarter"" [""Q1"", ""Q2"", ""Q3"", ""Q4""]
    y-axis ""Value""
    bar [5, 15, 25, 20]
    line [10, 12, 23, 14]
    line [8, 18, 28, 23]
    line [12, 22, 32, 30]";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
