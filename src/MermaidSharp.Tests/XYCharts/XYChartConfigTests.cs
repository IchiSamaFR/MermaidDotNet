using MermaidSharp.Configs;
using MermaidSharp.Configs.Enums;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.XYCharts
{
    /// <summary>
    /// Contains unit tests for <see cref="XYChartConfig"/>.
    /// </summary>
    [TestClass]
    public class XYChartConfigTests
    {
        /// <summary>
        /// Verifies that the dark theme is correctly serialized.
        /// </summary>
        [TestMethod]
        public void XYChartConfig_ThemeDark_ToString_ReturnsExpectedYaml()
        {
            // Arrange
            var config = new XYChartConfig(ConfigTheme.Dark);

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
        public void XYChartConfig_AllProperties_ToString_ReturnsExpectedYaml()
        {
            // Arrange
            var config = new XYChartConfig
            {
                Width = 900,
                Height = 500,
                ShowTitle = true,
                ChartOrientationValue = ChartOrientation.Horizontal
            };

            string expected = @"---
config:
    xyChart:
        width: 900
        height: 500
        showTitle: true
        chartOrientation: horizontal
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
