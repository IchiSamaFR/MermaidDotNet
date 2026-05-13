using MermaidSharp.Configs;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.QuadrantCharts
{
    /// <summary>
    /// Contains unit tests for <see cref="QuadrantChartConfig"/>.
    /// </summary>
    [TestClass]
    public class QuadrantChartConfigTests
    {
        /// <summary>
        /// Verifies that the dark theme is correctly serialized.
        /// </summary>
        [TestMethod]
        public void QuadrantChartConfig_ThemeDark_ToString_ReturnsExpectedYaml()
        {
            // Arrange
            var config = new QuadrantChartConfig(ConfigTheme.Dark);

            string expected = @"---
config:
    theme: dark
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Verifies that ToString returns the expected YAML for all properties.
        /// </summary>
        [TestMethod]
        public void QuadrantChartConfig_AllProperties_ToString_ReturnsExpectedYaml()
        {
            // Arrange
            var config = new QuadrantChartConfig
            {
                ChartWidth = 800,
                ChartHeight = 600,
                TitlePadding = 15,
                TitleFontSize = 22,
                QuadrantPadding = 10,
                QuadrantTextTopPadding = 7,
                QuadrantLabelFontSize = 18,
                QuadrantInternalBorderStrokeWidth = 2,
                QuadrantExternalBorderStrokeWidth = 3,
                XAxisLabelPadding = 8,
                XAxisLabelFontSize = 14,
                XAxisPosition = XAxisPosition.Bottom,
                YAxisLabelPadding = 9,
                YAxisLabelFontSize = 13,
                YAxisPosition = YAxisPosition.Left,
                PointTextPadding = 6,
                PointLabelFontSize = 11,
                PointRadius = 9
            };

            string expected = @"---
config:
    quadrantChart:
        chartWidth: 800
        chartHeight: 600
        titlePadding: 15
        titleFontSize: 22
        quadrantPadding: 10
        quadrantTextTopPadding: 7
        quadrantLabelFontSize: 18
        quadrantInternalBorderStrokeWidth: 2
        quadrantExternalBorderStrokeWidth: 3
        xAxisLabelPadding: 8
        xAxisLabelFontSize: 14
        xAxisPosition: bottom
        yAxisLabelPadding: 9
        yAxisLabelFontSize: 13
        yAxisPosition: left
        pointTextPadding: 6
        pointLabelFontSize: 11
        pointRadius: 9
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}