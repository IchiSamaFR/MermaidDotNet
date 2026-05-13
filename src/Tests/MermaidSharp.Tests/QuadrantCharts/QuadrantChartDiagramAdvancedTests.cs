using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.QuadrantCharts
{
    /// <summary>
    /// Contains advanced and comprehensive unit tests for <see cref="QuadrantChartDiagram"/>.
    /// </summary>
    [TestClass]
    public class QuadrantChartDiagramAdvancedTests
    {
        /// <summary>
        /// Verifies that CalculateDiagram returns a complex diagram with multiple points, styles, and axis/labels.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ComplexScenario_ReturnsExpectedDiagram()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram
            {
                XAxisLeft = "Low",
                XAxisRight = "High",
                YAxisBottom = "Bottom",
                YAxisTop = "Top",
                Quadrant1 = "Q1",
                Quadrant2 = "Q2",
                Quadrant3 = "Q3",
                Quadrant4 = "Q4"
            };

            diagram.Points.Add(new QuadrantChartPoint { Label = "A", X = 0.1, Y = 0.9, Color = "#ff0000", Radius = 10 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "B", X = 0.5, Y = 0.5, Color = "#00ff00", Radius = 8 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "C", X = 0.9, Y = 0.1, Color = "#0000ff", Radius = 12 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "D", X = 0.3, Y = 0.7 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "E", X = 0.7, Y = 0.3, Color = "#aaaaaa" });

            string expected = @"quadrantChart
    x-axis Low --> High
    y-axis Bottom --> Top
    quadrant-1 Q1
    quadrant-2 Q2
    quadrant-3 Q3
    quadrant-4 Q4
    A: [0.1, 0.9] color: #ff0000, radius: 10
    B: [0.5, 0.5] color: #00ff00, radius: 8
    C: [0.9, 0.1] color: #0000ff, radius: 12
    D: [0.3, 0.7]
    E: [0.7, 0.3] color: #aaaaaa";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns the expected diagram with custom config and theme variables.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_WithConfigAndThemeVariables_ReturnsExpectedDiagram()
        {
            // Arrange
            var themeVariables = new QuadrantChartThemeVariables
            {
                DarkMode = true,
                Background = "#222222",
                FontFamily = "Verdana"
            };

            var config = new QuadrantChartConfig(ConfigTheme.None, themeVariables)
            {
                ChartWidth = 900,
                ChartHeight = 700,
                TitleFontSize = 20
            };

            var diagram = new QuadrantChartDiagram("Advanced Quadrant", config)
            {
                XAxisLeft = "Min",
                XAxisRight = "Max",
                YAxisBottom = "Start",
                YAxisTop = "End",
                Quadrant1 = "Alpha",
                Quadrant2 = "Beta",
                Quadrant3 = "Gamma",
                Quadrant4 = "Delta"
            };

            diagram.Points.Add(new QuadrantChartPoint { Label = "P1", X = 0.2, Y = 0.8, Color = "#123456", Radius = 14 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "P2", X = 0.8, Y = 0.2, Color = "#654321", Radius = 7 });

            string expected = @"---
title: Advanced Quadrant
config:
    quadrantChart:
        chartWidth: 900
        chartHeight: 700
        titleFontSize: 20
    themeVariables:
        darkMode: true
        background: ""#222222""
        fontFamily: ""Verdana""
---
quadrantChart
    x-axis Min --> Max
    y-axis Start --> End
    quadrant-1 Alpha
    quadrant-2 Beta
    quadrant-3 Gamma
    quadrant-4 Delta
    P1: [0.2, 0.8] color: #123456, radius: 14
    P2: [0.8, 0.2] color: #654321, radius: 7";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram renders x-axis correctly when only the right label is set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsXAxisRightOnly_WhenOnlyRightSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram { XAxisRight = "High" };
            string expected = @"quadrantChart
    x-axis --> High";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram renders y-axis correctly when only the top label is set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsYAxisTopOnly_WhenOnlyTopSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram { YAxisTop = "High" };
            string expected = @"quadrantChart
    y-axis --> High";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}