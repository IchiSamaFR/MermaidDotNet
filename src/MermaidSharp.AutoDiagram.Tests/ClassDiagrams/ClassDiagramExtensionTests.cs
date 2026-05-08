using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.AutoDiagram.Tests.ClassDiagrams
{
    /// <summary>
    /// Tests for <see cref="ClassDiagramExtension.ToMermaidClassDiagram(IEnumerable{Type}, ClassDiagramOptions)"/>.
    /// </summary>
    [TestClass]
    public class ClassDiagramExtensionTests
    {
        #region Constructors
        [TestMethod]
        public void ToMermaidClassDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
        {
            // Arrange
            var types = new[] { typeof(Person) };
            var expected = @"classDiagram
    class Person {
        +String FirstName
        +String LastName
        +get_FirstName()
        +set_FirstName()
        +get_LastName()
        +set_LastName()
        +SayHello()
        +Equals()
        +GetHashCode()
        +GetType()
        +ToString()
    }";

            // Act
            var diagram = types.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_CalculateDiagram_OutputContainsAllKeyElements()
        {
            // Arrange
            var types = new[] { typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>) };
            var expected = @"classDiagram
    class Employee {
        +String EmployeeId
        +String Email
        +Department~Employee~ Department
        +String FirstName
        +String LastName
        +get_EmployeeId()
        +set_EmployeeId()
        +get_Email()
        +set_Email()
        +Contact()
        +get_Department() Employee
        +set_Department(Employee)
        +get_FirstName()
        +set_FirstName()
        +get_LastName()
        +set_LastName()
        +SayHello()
        +Equals()
        +GetHashCode()
        +GetType()
        +ToString()
    }
    class Manager {
        +Int32 Level
        +String EmployeeId
        +String Email
        +Department~Manager~ Department
        +String FirstName
        +String LastName
        +get_Level()
        +set_Level()
        +Approve()
        +get_EmployeeId()
        +set_EmployeeId()
        +get_Email()
        +set_Email()
        +Contact()
        +get_Department() Employee
        +set_Department(Employee)
        +get_FirstName()
        +set_FirstName()
        +get_LastName()
        +set_LastName()
        +SayHello()
        +Equals()
        +GetHashCode()
        +GetType()
        +ToString()
    }
    class IContactable {
        +String Email
        +get_Email()
        +set_Email()
        +Contact()
    }
    class Department~Employee~ {
        +String Name
        +List~Department~T~~ Members
        +get_Name()
        +set_Name()
        +get_Members() T
        +set_Members(T)
        +Equals()
        +GetHashCode()
        +GetType()
        +ToString()
    }
    Department-->Employee
    IContactable..|>Employee
    Department-->Manager
    Employee<|--Manager
    IContactable..|>Manager";

            // Act
            var diagram = types.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Assembly
        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllTypes()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Tests {
        class Department~T~ {
            +String Name
            +List~Department~T~~ Members
            +get_Name()
            +set_Name()
            +get_Members() T
            +set_Members(T)
            +Equals()
            +GetHashCode()
            +GetType()
            +ToString()
        }
        class Employee {
            +String EmployeeId
            +String Email
            +Department~Employee~ Department
            +String FirstName
            +String LastName
            +get_EmployeeId()
            +set_EmployeeId()
            +get_Email()
            +set_Email()
            +Contact()
            +get_Department() Employee
            +set_Department(Employee)
            +get_FirstName()
            +set_FirstName()
            +get_LastName()
            +set_LastName()
            +SayHello()
            +Equals()
            +GetHashCode()
            +GetType()
            +ToString()
        }
        class IContactable {
            +String Email
            +get_Email()
            +set_Email()
            +Contact()
        }
        class Manager {
            +Int32 Level
            +String EmployeeId
            +String Email
            +Department~Manager~ Department
            +String FirstName
            +String LastName
            +get_Level()
            +set_Level()
            +Approve()
            +get_EmployeeId()
            +set_EmployeeId()
            +get_Email()
            +set_Email()
            +Contact()
            +get_Department() Employee
            +set_Department(Employee)
            +get_FirstName()
            +set_FirstName()
            +get_LastName()
            +set_LastName()
            +SayHello()
            +Equals()
            +GetHashCode()
            +GetType()
            +ToString()
        }
        class Person {
            +String FirstName
            +String LastName
            +get_FirstName()
            +set_FirstName()
            +get_LastName()
            +set_LastName()
            +SayHello()
            +Equals()
            +GetHashCode()
            +GetType()
            +ToString()
        }
        class ClassDiagramExtensionTests {
            +ToMermaidClassDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
            +ToMermaidClassDiagram_CalculateDiagram_OutputContainsAllKeyElements()
            +ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllTypes()
            +ToMermaidClassDiagram_Assembly_GeneratesDiagramForAllProperties()
            +Equals()
            +GetHashCode()
            +GetType()
            +ToString()
        }
    }
    Department-->Employee
    Person<|--Employee
    IContactable..|>Employee
    Department-->Manager
    Employee<|--Manager
    IContactable..|>Manager";

            // Act
            var diagram = assembly.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
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
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Tests {
        class Department~T~ {
            +String Name
            +List~Department~T~~ Members
        }
        class Employee {
            +String EmployeeId
            +String Email
            +Department~Employee~ Department
            +String FirstName
            +String LastName
        }
        class IContactable {
            +String Email
        }
        class Manager {
            +Int32 Level
            +String EmployeeId
            +String Email
            +Department~Manager~ Department
            +String FirstName
            +String LastName
        }
        class Person {
            +String FirstName
            +String LastName
        }
        class ClassDiagramExtensionTests
    }
    Department-->Employee
    Person<|--Employee
    IContactable..|>Employee
    Department-->Manager
    Employee<|--Manager
    IContactable..|>Manager";

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}
