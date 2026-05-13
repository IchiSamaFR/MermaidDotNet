using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Diagrams;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MermaidSharp.Tests.PieCharts
{
    [TestClass]
    public class PieChartDiagramTests
    {
        #region Empty Diagram

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));

            string expected = @"pie
    ""Dogs"" : 386
    ""Cats"" : 85";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_EmptyDiagram()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            string expected = "pie";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ShowData

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithShowData()
        {
            // Arrange
            var diagram = new PieChartDiagram(showData: true);
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));

            string expected = @"pie showData
    ""Dogs"" : 386
    ""Cats"" : 85";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Title

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithTitle()
        {
            // Arrange
            var diagram = new PieChartDiagram(title: "Pets adopted by volunteers");
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));

            string expected = @"pie
    title Pets adopted by volunteers
    ""Dogs"" : 386
    ""Cats"" : 85";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Slices

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithSlices()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));
            diagram.Slices.Add(new PieSlice("Rats", 15));

            string expected = @"pie
    ""Dogs"" : 386
    ""Cats"" : 85
    ""Rats"" : 15";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithDecimalSliceValues()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            diagram.Slices.Add(new PieSlice("Cats", 42.96));
            diagram.Slices.Add(new PieSlice("Dogs", 50.05));
            diagram.Slices.Add(new PieSlice("Rats", 10.01));

            string expected = @"pie
    ""Cats"" : 42.96
    ""Dogs"" : 50.05
    ""Rats"" : 10.01";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region All Options

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithAllOptions()
        {
            // Arrange
            var diagram = new PieChartDiagram(title: "Pets adopted by volunteers", showData: true);
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));
            diagram.Slices.Add(new PieSlice("Rats", 15));

            string expected = @"pie showData
    title Pets adopted by volunteers
    ""Dogs"" : 386
    ""Cats"" : 85
    ""Rats"" : 15";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region AddSlice Fluent API

        [TestMethod]
        public void PieChartDiagram_AddSlice_EnablesMethodChaining()
        {
            // Arrange
            var diagram = new PieChartDiagram();

            string expected = @"pie
    ""Dogs"" : 386
    ""Cats"" : 85
    ""Rats"" : 15";

            // Act
            var result = diagram
                .AddSlice("Dogs", 386)
                .AddSlice("Cats", 85)
                .AddSlice("Rats", 15)
                .CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartDiagram_AddSlice_ThrowsArgumentOutOfRangeException_WhenValueIsZero()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            try
            {
                // Act
                diagram.AddSlice("Dogs", 0);

                // Assert
                Assert.Fail("Expected ArgumentOutOfRangeException was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void PieChartDiagram_AddSlice_ThrowsArgumentOutOfRangeException_WhenValueIsNegative()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            try
            {
                // Act
                diagram.AddSlice("Dogs", -10);

                // Assert
                Assert.Fail("Expected ArgumentOutOfRangeException was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void PieChartDiagram_AddSlice_ThrowsArgumentNullException_WhenLabelIsNull()
        {
            // Arrange
            var diagram = new PieChartDiagram();
            try
            {
                // Act
                diagram.AddSlice(null, 42);

                // Assert
                Assert.Fail("Expected ArgumentNullException was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        #endregion

        #region Config

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithTextPosition()
        {
            // Arrange
            var config = new PieChartConfig(textPosition: 0.5);
            var diagram = new PieChartDiagram(config: config);
            diagram.Slices.Add(new PieSlice("Dogs", 386));
            diagram.Slices.Add(new PieSlice("Cats", 85));

            string expected = @"---
config:
    pie:
        textPosition: 0.5
---
pie
    ""Dogs"" : 386
    ""Cats"" : 85";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithAllOptions_AndConfig()
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
            var config = new PieChartConfig(textPosition: 0.5, themeVariables: themeVariables);
            var diagram = new PieChartDiagram(title: "Key elements in Product X", showData: true, config: config);
            diagram.Slices.Add(new PieSlice("Calcium", 42.96));
            diagram.Slices.Add(new PieSlice("Potassium", 50.05));
            diagram.Slices.Add(new PieSlice("Magnesium", 10.01));
            diagram.Slices.Add(new PieSlice("Iron", 5));

            string expected = @"---
config:
    pie:
        textPosition: 0.5
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
---
pie showData
    title Key elements in Product X
    ""Calcium"" : 42.96
    ""Potassium"" : 50.05
    ""Magnesium"" : 10.01
    ""Iron"" : 5";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PieChartDiagram_CalculateDiagram_WithColors()
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
            var config = new PieChartConfig(textPosition: 0.5, themeVariables: themeVariables);
            var diagram = new PieChartDiagram(title: "Key elements in Product X", showData: true, config: config);
            diagram.Slices.Add(new PieSlice("Calcium", 42.96, "red"));
            diagram.Slices.Add(new PieSlice("Potassium", 50.05));
            diagram.Slices.Add(new PieSlice("Magnesium", 10.01, "green"));
            diagram.Slices.Add(new PieSlice("Iron", 5, "yellow"));

            string expected = @"---
config:
    pie:
        textPosition: 0.5
    themeVariables:
        pie1: ""red""
        pie3: ""green""
        pie4: ""yellow""
        pieTitleTextSize: ""18px""
        pieTitleTextColor: ""#333333""
        pieSectionTextSize: ""14px""
        pieSectionTextColor: ""#666666""
        pieLegendTextSize: ""12px""
        pieLegendTextColor: ""#999999""
        pieStrokeColor: ""#000000""
        pieOuterStrokeWidth: ""2px""
        pieOpacity: 0.8
---
pie showData
    title Key elements in Product X
    ""Calcium"" : 42.96
    ""Potassium"" : 50.05
    ""Magnesium"" : 10.01
    ""Iron"" : 5";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}
