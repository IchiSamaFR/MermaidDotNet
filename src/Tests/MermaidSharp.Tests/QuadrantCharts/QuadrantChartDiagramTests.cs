using MermaidSharp.Diagrams;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.QuadrantCharts
{
    /// <summary>
    /// Contains unit tests for <see cref="QuadrantChartDiagram"/>.
    /// </summary>
    [TestClass]
    public class QuadrantChartDiagramTests
    {
        /// <summary>
        /// Verifies that CalculateDiagram returns the diagram name.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ContainsDiagramName_WhenCalled()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram();
            var expected = "quadrantChart";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.Contains(result, expected);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns the correct point syntax.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsCorrectPointSyntax_WhenPointAdded()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram();
            diagram.Points.Add(new QuadrantChartPoint { Label = "Point 1", X = 0.75, Y = 0.80 });
            var expected = @"quadrantChart
    Point 1: [0.75, 0.8]";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns x-axis syntax with both labels.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsXAxisBothLabels_WhenBothSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram { XAxisLeft = "Low", XAxisRight = "High" };
            var expected = @"quadrantChart
    x-axis Low --> High";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns y-axis syntax with both labels.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsYAxisBothLabels_WhenBothSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram { YAxisBottom = "Low", YAxisTop = "High" };
            var expected = @"quadrantChart
    y-axis Low --> High";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns quadrant labels when set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsQuadrantLabels_WhenSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram
            {
                Quadrant1 = "Top Right",
                Quadrant2 = "Top Left",
                Quadrant3 = "Bottom Left",
                Quadrant4 = "Bottom Right"
            };
            var expected = @"quadrantChart
    quadrant-1 Top Right
    quadrant-2 Top Left
    quadrant-3 Bottom Left
    quadrant-4 Bottom Right";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that CalculateDiagram returns point with inline style when set.
        /// </summary>
        [TestMethod]
        public void CalculateDiagram_ReturnsPointWithStyle_WhenStyleSet()
        {
            // Arrange
            var diagram = new QuadrantChartDiagram();
            diagram.Points.Add(new QuadrantChartPoint
            {
                Label = "Point A",
                X = 0.9345,
                Y = 0.0,
                Color = "#ff3300",
                Radius = 12
            });
            var expected = @"quadrantChart
    Point A: [0.9345, 0] color: #ff3300, radius: 12";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}