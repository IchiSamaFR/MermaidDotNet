using MermaidSharp.Configs;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.PieCharts
{
    [TestClass]
    public class PieChartConfigTests
    {
        #region Empty Config

        [TestMethod]
        public void PieChartConfig_Empty_ReturnsEmptyString()
        {
            // Arrange
            var config = new PieChartConfig();
            string expected = "";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Theme Only

        [TestMethod]
        public void PieChartConfig_ThemeDark()
        {
            // Arrange
            var config = new PieChartConfig(ConfigTheme.Dark);

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

        [TestMethod]
        public void PieChartConfig_ThemeForest()
        {
            // Arrange
            var config = new PieChartConfig(ConfigTheme.Forest);

            string expected = @"---
config:
    theme: forest
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region TextPosition Only

        [TestMethod]
        public void PieChartConfig_TextPosition_Half()
        {
            // Arrange
            var config = new PieChartConfig(textPosition: 0.5);

            string expected = @"---
config:
    pie:
        textPosition: 0.5
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartConfig_TextPosition_Center()
        {
            // Arrange
            var config = new PieChartConfig(textPosition: 0.5d);

            string expected = @"---
config:
    pie:
        textPosition: 0.5
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartConfig_TextPosition_Edge()
        {
            // Arrange
            var config = new PieChartConfig(textPosition: 1);

            string expected = @"---
config:
    pie:
        textPosition: 1
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartConfig_TextPosition_PropertySetter()
        {
            // Arrange
            var config = new PieChartConfig { TextPosition = 0.75 };

            string expected = @"---
config:
    pie:
        textPosition: 0.75
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Theme and TextPosition

        [TestMethod]
        public void PieChartConfig_ThemeAndTextPosition()
        {
            // Arrange
            var config = new PieChartConfig(ConfigTheme.Dark, textPosition: 0.5);

            string expected = @"---
config:
    theme: dark
    pie:
        textPosition: 0.5
---";

            // Act
            string result = config.ToString();

            // Assert
            Assert.IsNotNull(config);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
        #endregion
    }
}
