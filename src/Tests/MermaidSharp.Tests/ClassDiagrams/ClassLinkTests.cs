using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.ClassDiagrams
{
    [TestClass]
    public class ClassLinkTests
    {
        #region ClassLink — All types

        [TestMethod]
        public void ClassLink_Inheritance_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Animal", "Dog", ClassLinkType.Inheritance) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Animal<|--Dog";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_Composition_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Car", "Engine", ClassLinkType.Composition) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Car*--Engine";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_Aggregation_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Library", "Book", ClassLinkType.Aggregation) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Libraryo--Book";

            // Act

            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_Association_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Student", "Course", ClassLinkType.Association) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Student-->Course";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_SolidLink_RendersSolidLine()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("A", "B", ClassLinkType.SolidLink) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A--B";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_Dependency_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Client", "Service", ClassLinkType.Dependency) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Client..>Service";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_Realization_RendersCorrectArrow()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("Repository", "IRepository", ClassLinkType.Realization) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Repository..|>IRepository";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_DashedLink_RendersDashedLine()
        {
            // Arrange
            var links = new List<ClassLink> { new ClassLink("A", "B", ClassLinkType.DashedLink) };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A..B";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ClassLink — Labels

        [TestMethod]
        public void ClassLink_WithLabel_RendersLabelAfterColon()
        {
            // Arrange
            var links = new List<ClassLink>
            {
                new ClassLink("Animal", "Dog", ClassLinkType.Inheritance, "extends")
            };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    Animal<|--Dog : extends";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_WithEmptyLabel_RendersWithoutLabel()
        {
            // Arrange
            var links = new List<ClassLink>
            {
                new ClassLink("A", "B", ClassLinkType.Association, string.Empty)
            };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A-->B";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_WithLabel_AllLinkTypes_RendersLabelCorrectly()
        {
            // Arrange
            var links = new List<ClassLink>
            {
                new ClassLink("A", "B", ClassLinkType.Composition, "has"),
                new ClassLink("B", "C", ClassLinkType.Dependency, "uses"),
            };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A*--B : has
    B..>C : uses";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassLink_MultipleLinks_RendersAllInOrder()
        {
            // Arrange
            var links = new List<ClassLink>
            {
                new ClassLink("A", "B", ClassLinkType.Inheritance),
                new ClassLink("B", "C", ClassLinkType.Association, "uses"),
                new ClassLink("C", "D", ClassLinkType.Realization),
            };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A<|--B
    B-->C : uses
    C..|>D";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

    }
}
