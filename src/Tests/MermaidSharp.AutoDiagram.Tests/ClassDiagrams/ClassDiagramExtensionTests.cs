using MermaidSharp.AutoDiagram.Diagrams;
using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Options;
using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MermaidSharp.AutoDiagram.Tests.ClassDiagrams
{
    /// <summary>
    /// Tests for <see cref="ClassDiagramExtension.ToMermaidClassDiagram(IEnumerable{Type}, ClassDiagramOptions)"/>.
    /// </summary>
    [TestClass]
    public class ClassDiagramExtensionTests
    {
        [TestMethod]
        public void ToMermaidClassDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
        {
            // Arrange
            var type = typeof(Person);

            // Act
            var diagram = type.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "class Person {");
            StringAssert.Contains(result, "-String SecretCode");
            StringAssert.Contains(result, "#Int32 Age");
            StringAssert.Contains(result, "~Int32 InternalId");
            StringAssert.Contains(result, "+String FirstName");
            StringAssert.Contains(result, "+String LastName");
            StringAssert.Contains(result, "-Hide()");
            StringAssert.Contains(result, "#DoWork()");
            StringAssert.Contains(result, "~InternalMethod()");
            StringAssert.Contains(result, "+SayHello()");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_CalculateDiagram_OutputContainsAllKeyElements()
        {
            // Arrange
            var types = new[] { typeof(Person), typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<>) };

            // Act
            var diagram = types.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "class Person {");
            StringAssert.Contains(result, "class Employee {");
            StringAssert.Contains(result, "class Manager {");
            StringAssert.Contains(result, "class IContactable {");
            StringAssert.Contains(result, "class Department~T~ {");
            StringAssert.Contains(result, "+Department~Employee~ Department");
            StringAssert.Contains(result, "+Contact(String)");
            StringAssert.Contains(result, "+Approve()");
            StringAssert.Contains(result, "Employee-->Department~T~ : Association");
            StringAssert.Contains(result, "Employee<|--Manager : Inherited");
            StringAssert.Contains(result, "IContactable..|>Employee : Interface");
            StringAssert.Contains(result, "IContactable..|>Manager : Interface");
            StringAssert.Contains(result, "Manager-->Department~T~ : Association");
            StringAssert.Contains(result, "Person<|--Employee : Inherited");
        }
        
        
        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForFilteredTypes()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;
            var whiteList = new List<Type> { typeof(Employee), typeof(Manager), typeof(Person) };
			var options = new ClassDiagramOptions()
			{
				LinkOptions = new ClassLinkOptions()
				{
					IncludeLinks = ClassLinkOption.All
                },
                AssemblyClassFilter = (type) => whiteList.Contains(type)
			};

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "namespace MermaidSharp.AutoDiagram.Tests.Models {");
            StringAssert.Contains(result, "class Employee {");
            StringAssert.Contains(result, "class Manager {");
            StringAssert.Contains(result, "class Person {");
            StringAssert.Contains(result, "Employee<|--Manager : Inherited");
            StringAssert.Contains(result, "Person<|--Employee : Inherited");
            StringAssert.DoesNotMatch(result, new Regex("class Department"));
            StringAssert.DoesNotMatch(result, new Regex("class IContactable"));
            StringAssert.DoesNotMatch(result, new Regex("Association"));
            StringAssert.DoesNotMatch(result, new Regex("Interface"));
        }

        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_DoesNotThrowWhenTypeNamesCollideAcrossNamespaces()
        {
            // Arrange
            var assembly = typeof(DuplicateTypeNames.First.SharedName).Assembly;
            var includedTypes = new List<Type>
            {
                typeof(DuplicateTypeNames.First.SharedName),
                typeof(DuplicateTypeNames.Second.SharedName)
            };
            var options = new ClassDiagramOptions()
            {
                AssemblyClassFilter = type => includedTypes.Contains(type),
                MethodOptions = new ClassMethodOptions()
                {
                    IncludeVisibility = new List<ClassPropertyVisibility>()
                }
            };

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "namespace MermaidSharp.AutoDiagram.Tests {");
            StringAssert.Contains(result, "class SharedName {");
            Assert.AreEqual(2, result.Split(new[] { "class SharedName" }, StringSplitOptions.None).Length - 1);
            StringAssert.Contains(result, "SharedName-->SharedName : Association");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllProperties()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;
            var options = new ClassDiagramOptions()
            {
                MethodOptions = new ClassMethodOptions()
                {
                    IncludeVisibility = new List<ClassPropertyVisibility>()
                }
            };

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "namespace MermaidSharp.AutoDiagram.Tests.Models {");
            StringAssert.Contains(result, "class Department~T~ {");
            StringAssert.Contains(result, "+List~T~ Members");
            StringAssert.Contains(result, "+String Name");
            StringAssert.Contains(result, "class Employee {");
            StringAssert.Contains(result, "#Int32 Age");
            StringAssert.Contains(result, "~Int32 InternalId");
            StringAssert.Contains(result, "+Department~Employee~ Department");
            StringAssert.Contains(result, "+String Email");
            StringAssert.Contains(result, "+String EmployeeId");
            StringAssert.Contains(result, "+String FirstName");
            StringAssert.Contains(result, "+String LastName");
            StringAssert.Contains(result, "class IContactable {");
            StringAssert.Contains(result, "class Manager {");
            StringAssert.Contains(result, "+Int32 Level");
            StringAssert.Contains(result, "class Person {");
            StringAssert.Contains(result, "-String SecretCode");
            StringAssert.Contains(result, "Employee-->Department~T~ : Association");
            StringAssert.Contains(result, "Employee<|--Manager : Inherited");
            StringAssert.Contains(result, "IContactable..|>Employee : Interface");
            StringAssert.Contains(result, "IContactable..|>Manager : Interface");
            StringAssert.Contains(result, "Manager-->Department~T~ : Association");
            StringAssert.Contains(result, "Person<|--Employee : Inherited");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_Assemblies_GeneratesDiagramForAllTypes()
        {
            // Arrange
            var assemblies = new[] { typeof(Employee).Assembly };
            var options = new ClassDiagramOptions()
            {
                MethodOptions = new ClassMethodOptions()
                {
                    IncludeVisibility = new List<ClassPropertyVisibility> { ClassPropertyVisibility.None }
                },
                PropertyOptions = new ClassPropertyOptions()
                {
                    IncludeVisibility = new List<ClassPropertyVisibility> { ClassPropertyVisibility.None }
                },
                IncludeClassesVisibility = new List<ClassPropertyVisibility> { ClassPropertyVisibility.Public }
            };

            // Act
            var diagram = assemblies.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "namespace MermaidSharp.AutoDiagram.Tests.Models {");
            StringAssert.Contains(result, "class Department~T~");
            StringAssert.Contains(result, "class Employee");
            StringAssert.Contains(result, "class IContactable");
            StringAssert.Contains(result, "class Manager");
            StringAssert.Contains(result, "class Person");
            StringAssert.Contains(result, "Employee<|--Manager : Inherited");
            StringAssert.Contains(result, "IContactable..|>Employee : Interface");
            StringAssert.Contains(result, "IContactable..|>Manager : Interface");
            StringAssert.Contains(result, "Person<|--Employee : Inherited");
        }
    }
}
