using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.ClassDiagrams
{
    [TestClass]
    public class ClassNodeTests
    {
        [TestMethod]
        public void SingleNode_NoPropertiesNoMethods_ReturnsSimpleClass()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Animal", properties: new List<ClassProperty>())
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Animal";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MultipleNodes_NoLinks_ReturnsAllClasses()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Animal", properties: new List<ClassProperty>()),
                new ClassNode("Dog", properties: new List<ClassProperty>()),
                new ClassNode("Cat", properties: new List<ClassProperty>()),
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Animal
    class Dog
    class Cat";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassNode_WithType_RendersAngleBracketsAsTilde()
        {
            // Arrange
            var node = new ClassNode("Animal", properties: new List<ClassProperty>())
            {
                Type = "Interface"
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.Add(node);

            var expected = @"classDiagram
    class Animal ~Interface~";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassNode_WithSimpleType_RendersType()
        {
            // Arrange
            var node = new ClassNode("Container", properties: new List<ClassProperty>())
            {
                Type = "Abstract"
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.Add(node);

            var expected = @"classDiagram
    class Container ~Abstract~";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassNode_NameWithSpaces_RemovesSpaces()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("My Class", properties: new List<ClassProperty>())
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class MyClass";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }



        #region Mixed nodes — properties and methods

        [TestMethod]
        public void ClassNode_WithOnlyMethods_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod("Start", visibility: ClassPropertyVisibility.Public),
                    new ClassMethod("Stop", visibility: ClassPropertyVisibility.Public),
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        +Start()
        +Stop()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassNode_WithOnlyProperties_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Config", properties: new List<ClassProperty>
                {
                    new ClassProperty("Host", "string", ClassPropertyVisibility.Public),
                    new ClassProperty("Port", "int", ClassPropertyVisibility.Public),
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Config {
        +string Host
        +int Port
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassNode_PropertiesRenderedBeforeMethods()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Entity", properties: new List<ClassProperty>
                {
                    new ClassProperty("Id", "int", ClassPropertyVisibility.Public)
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod("GetId", "int", ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Entity {
        +int Id
        +GetId() int
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

    }
}
