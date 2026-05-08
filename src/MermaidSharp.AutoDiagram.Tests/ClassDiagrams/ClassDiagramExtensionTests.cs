using System;
using System.Collections.Generic;
using System.Linq;
using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.AutoDiagram.Tests.ClassDiagrams
{
    /// <summary>
    /// Tests for <see cref="ClassDiagramExtension.ToMermaidClassDiagram(IEnumerable{Type}, ClassDiagramOptions)"/>.
    /// </summary>
    [TestClass]
    public class ClassDiagramExtensionTests
    {
        #region Classes
        [TestMethod]
        public void ToMermaidClassDiagram_ComplexModel_GeneratesCorrectNodesAndLinks()
        {
            // Arrange
            var types = new[]
            {
                typeof(Person), typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>)
            };

            // Act
            var diagram = types.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            // Vérifie la présence des classes principales
            var nodeNames = diagram.Nodes.Select(n => n.Name).ToList();
            CollectionAssert.Contains(nodeNames, "Person");
            CollectionAssert.Contains(nodeNames, "Employee");
            CollectionAssert.Contains(nodeNames, "Manager");
            CollectionAssert.Contains(nodeNames, "Department");
            CollectionAssert.Contains(nodeNames, "IContactable");

            // Vérifie héritage et implémentation d'interface
            Assert.IsTrue(diagram.Links.Any(l => l.SourceNode == "Person" && l.DestinationNode == "Employee"));
            Assert.IsTrue(diagram.Links.Any(l => l.SourceNode == "Employee" && l.DestinationNode == "Manager"));
            Assert.IsTrue(diagram.Links.Any(l => l.SourceNode == "IContactable" && l.DestinationNode == "Employee"));

            // Vérifie propriétés et méthodes publiques sur Employee
            var employeeNode = diagram.Nodes.FirstOrDefault(n => n.Name == "Employee");
            var employeePropertyNames = employeeNode.Properties.Select(p => p.Name).ToList();
            CollectionAssert.Contains(employeePropertyNames, "EmployeeId");
            CollectionAssert.Contains(employeePropertyNames, "Email");
            CollectionAssert.Contains(employeePropertyNames, "Department");


            var employeeMethodNames = employeeNode.Methods.Select(m => m.Name).ToList();
            CollectionAssert.Contains(employeeMethodNames, "Contact");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
        {
            // Arrange
            var types = new[] { typeof(Person) };
            var options = new ClassDiagramOptions();

            // Act
            var diagram = types.ToMermaidClassDiagram(options);

            // Assert
            var personNode = diagram.Nodes.FirstOrDefault(n => n.Name == "Person");
            Assert.IsTrue(personNode.Properties.Any(p => p.Name == "FirstName")); // public
            Assert.IsTrue(personNode.Properties.Any(p => p.Name == "InternalId")); // internal
            Assert.IsTrue(personNode.Properties.Any(p => p.Name == "Age")); // protected
            Assert.IsTrue(personNode.Properties.Any(p => p.Name == "SecretCode")); // private
            Assert.IsTrue(personNode.Methods.Any(m => m.Name == "SayHello")); // public
            Assert.IsTrue(personNode.Methods.Any(m => m.Name == "DoWork")); // protected
            Assert.IsTrue(personNode.Methods.Any(m => m.Name == "InternalMethod")); // internal
            Assert.IsTrue(personNode.Methods.Any(m => m.Name == "Hide")); // private
        }

        [TestMethod]
        public void ToMermaidClassDiagram_CalculateDiagram_OutputContainsAllKeyElements()
        {
            // Arrange
            var types = new[] { typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>) };
            var diagram = types.ToMermaidClassDiagram();

            // Act
            var output = diagram.CalculateDiagram();

            // Assert
            StringAssert.Contains(output, "classDiagram");
            StringAssert.Contains(output, "Employee");
            StringAssert.Contains(output, "Manager");
            StringAssert.Contains(output, "Department");
            StringAssert.Contains(output, "IContactable");
            StringAssert.Contains(output, "EmployeeId");
            StringAssert.Contains(output, "Contact");
            StringAssert.Contains(output, "Approve");
        }
        #endregion

        #region Assembly
        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllTypes()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;

            // Act
            var diagram = assembly.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            var nodeNames = diagram.Nodes.Select(n => n.Name).ToList();
            CollectionAssert.Contains(nodeNames, "Person");
            CollectionAssert.Contains(nodeNames, "Employee");
            CollectionAssert.Contains(nodeNames, "Manager");
            CollectionAssert.Contains(nodeNames, "Department");
            CollectionAssert.Contains(nodeNames, "IContactable");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllProperties()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;
            var options = new ClassDiagramOptions()
            {
                IncludeMethodsVisibility = ClassPropertyVisibility.None
            };

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            var nodeNames = diagram.Nodes.Select(n => n.Name).ToList();
            CollectionAssert.Contains(nodeNames, "Person");
            CollectionAssert.Contains(nodeNames, "Employee");
            CollectionAssert.Contains(nodeNames, "Manager");
            CollectionAssert.Contains(nodeNames, "Department");
            CollectionAssert.Contains(nodeNames, "IContactable");
        }

        #endregion
    }
}
