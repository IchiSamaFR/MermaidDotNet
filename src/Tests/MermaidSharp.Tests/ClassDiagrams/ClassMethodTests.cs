using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.ClassDiagrams
{
    [TestClass]
    public class ClassMethodTests
    {
        #region ClassMethod — Visibility

        [TestMethod]
        public void ClassMethod_PublicVisibility_RendersWithPlusAndSpace()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Execute", visibility: ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        +Execute()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_PrivateVisibility_RendersWithMinusAndSpace()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Init", visibility: ClassPropertyVisibility.Private)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        -Init()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_ProtectedVisibility_RendersWithHashAndSpace()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "OnUpdate", visibility: ClassPropertyVisibility.Protected)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        #OnUpdate()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_InternalVisibility_RendersWithTildeAndSpace()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Reset", visibility: ClassPropertyVisibility.Internal)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        ~Reset()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_NoneVisibility_RendersWithoutSymbol()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Process", visibility: ClassPropertyVisibility.None)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        Process()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion


        #region ClassMethod — Return types

        [TestMethod]
        public void ClassMethod_WithReturnType_RendersReturnTypeAfterParens()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Repository", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetId", returnType: "int", visibility: ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Repository {
        +GetId() int
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithStringReturnType_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("User", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetFullName", returnType: "string", visibility: ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class User {
        +GetFullName() string
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithGenericReturnType_RendersWithTilde()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Repository", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetAll", returnType: "List<string>", visibility: ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Repository {
        +GetAll() List~string~
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_NoReturnType_RendersWithoutReturnType()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Run", visibility: ClassPropertyVisibility.Public)
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        +Run()
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion



        #region ClassMethod — Parameters

        [TestMethod]
        public void ClassMethod_WithSingleTypedParameter_RendersParameter()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Service", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "SetName", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("name", "string")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Service {
        +SetName(string name)
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithMultipleParameters_RendersAllSeparatedByComma()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Math", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Add", returnType: "int", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("x", "int"),
                        new ClassMethodParam("y", "int")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Math {
        +Add(int x, int y) int
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithReturnTypeAndParameters_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Calculator", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Divide", returnType: "double", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("dividend", "double"),
                        new ClassMethodParam("divisor", "double")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Calculator {
        +Divide(double dividend, double divisor) double
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithParameterNoType_RendersNameOnly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Handler", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Handle", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("value")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Handler {
        +Handle(value)
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithGenericParameter_RendersWithTilde()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Processor", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Process", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("items", "List<int>")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Processor {
        +Process(List~int~ items)
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClassMethod_WithThreeParameters_RendersAllParameters()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Builder", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Build", returnType: "string", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("firstName", "string"),
                        new ClassMethodParam("lastName", "string"),
                        new ClassMethodParam("age", "int")
                    })
                })
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"classDiagram
    class Builder {
        +Build(string firstName, string lastName, int age) string
    }";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}
