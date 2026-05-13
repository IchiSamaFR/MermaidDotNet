using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Options;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MermaidSharp.AutoDiagram.Core.Tests.ClassDiagrams
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

            var expected = @"classDiagram
    class Person {
        -String SecretCode
        #Int32 Age
        ~Int32 InternalId
        +String FirstName
        +String LastName
        -get_SecretCode() String
        -Hide()
        -set_SecretCode(String)
        #DoWork()
        #Finalize()
        #get_Age() Int32
        #MemberwiseClone() Object
        #set_Age(Int32)
        ~get_InternalId() Int32
        ~InternalMethod()
        ~set_InternalId(Int32)
        +Equals(Object) Boolean
        +get_FirstName() String
        +get_LastName() String
        +GetHashCode() Int32
        +GetType() Type
        +SayHello()
        +set_FirstName(String)
        +set_LastName(String)
        +ToString() String
    }";

            // Act
            var diagram = type.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_CalculateDiagram_OutputContainsAllKeyElements()
        {
            // Arrange
            var types = new[] { typeof(Person), typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>) };

            var expected = @"classDiagram
    class Person {
        -String SecretCode
        #Int32 Age
        ~Int32 InternalId
        +String FirstName
        +String LastName
        -get_SecretCode() String
        -Hide()
        -set_SecretCode(String)
        #DoWork()
        #Finalize()
        #get_Age() Int32
        #MemberwiseClone() Object
        #set_Age(Int32)
        ~get_InternalId() Int32
        ~InternalMethod()
        ~set_InternalId(Int32)
        +Equals(Object) Boolean
        +get_FirstName() String
        +get_LastName() String
        +GetHashCode() Int32
        +GetType() Type
        +SayHello()
        +set_FirstName(String)
        +set_LastName(String)
        +ToString() String
    }
    class Employee {
        #Int32 Age
        ~Int32 InternalId
        +Department~Employee~ Department
        +String Email
        +String EmployeeId
        +String FirstName
        +String LastName
        #DoWork()
        #Finalize()
        #get_Age() Int32
        #MemberwiseClone() Object
        #set_Age(Int32)
        ~get_InternalId() Int32
        ~InternalMethod()
        ~set_InternalId(Int32)
        +Contact(String)
        +Equals(Object) Boolean
        +get_Department() Department~Employee~
        +get_Email() String
        +get_EmployeeId() String
        +get_FirstName() String
        +get_LastName() String
        +GetHashCode() Int32
        +GetType() Type
        +SayHello()
        +set_Department(Department~Employee~)
        +set_Email(String)
        +set_EmployeeId(String)
        +set_FirstName(String)
        +set_LastName(String)
        +ToString() String
    }
    class Manager {
        #Int32 Age
        ~Int32 InternalId
        +Department~Employee~ Department
        +String Email
        +String EmployeeId
        +String FirstName
        +String LastName
        +Int32 Level
        #DoWork()
        #Finalize()
        #get_Age() Int32
        #MemberwiseClone() Object
        #set_Age(Int32)
        ~get_InternalId() Int32
        ~InternalMethod()
        ~set_InternalId(Int32)
        +Approve()
        +Contact(String)
        +Equals(Object) Boolean
        +get_Department() Department~Employee~
        +get_Email() String
        +get_EmployeeId() String
        +get_FirstName() String
        +get_LastName() String
        +get_Level() Int32
        +GetHashCode() Int32
        +GetType() Type
        +SayHello()
        +set_Department(Department~Employee~)
        +set_Email(String)
        +set_EmployeeId(String)
        +set_FirstName(String)
        +set_LastName(String)
        +set_Level(Int32)
        +ToString() String
    }
    class IContactable {
        +String Email
        +Contact(String)
        +get_Email() String
        +set_Email(String)
    }
    class Department~Employee~ {
        +List~Employee~ Members
        +String Name
        #Finalize()
        #MemberwiseClone() Object
        +Equals(Object) Boolean
        +get_Members() List~Employee~
        +get_Name() String
        +GetHashCode() Int32
        +GetType() Type
        +set_Members(List~Employee~)
        +set_Name(String)
        +ToString() String
    }
    Employee-->Department : Association
    Employee<|--Manager : Inherited
    IContactable..|>Employee : Interface
    IContactable..|>Manager : Interface
    Manager-->Department : Association
    Person<|--Employee : Inherited";

            // Act
            var diagram = types.ToMermaidClassDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
        
        
        [TestMethod]
        public void ToMermaidClassDiagram_Assembly_GeneratesDiagramForFilteredTypes()
        {
            // Arrange
            var assembly = typeof(Employee).Assembly;
            var whiteListe = new List<Type> { typeof(Employee), typeof(Manager), typeof(Person) };
			var options = new ClassDiagramOptions()
			{
				LinkOptions = new ClassLinkOptions()
				{
					IncludeLinks = ClassLinkOption.All
                },
                AssemblyClassFilter = (type) => whiteListe.Contains(type)
			};

            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Core.Domain {
        class Employee {
            #Int32 Age
            ~Int32 InternalId
            +Department~Employee~ Department
            +String Email
            +String EmployeeId
            +String FirstName
            +String LastName
            #DoWork()
            #Finalize()
            #get_Age() Int32
            #MemberwiseClone() Object
            #set_Age(Int32)
            ~get_InternalId() Int32
            ~InternalMethod()
            ~set_InternalId(Int32)
            +Contact(String)
            +Equals(Object) Boolean
            +get_Department() Department~Employee~
            +get_Email() String
            +get_EmployeeId() String
            +get_FirstName() String
            +get_LastName() String
            +GetHashCode() Int32
            +GetType() Type
            +SayHello()
            +set_Department(Department~Employee~)
            +set_Email(String)
            +set_EmployeeId(String)
            +set_FirstName(String)
            +set_LastName(String)
            +ToString() String
        }
        class Manager {
            #Int32 Age
            ~Int32 InternalId
            +Department~Employee~ Department
            +String Email
            +String EmployeeId
            +String FirstName
            +String LastName
            +Int32 Level
            #DoWork()
            #Finalize()
            #get_Age() Int32
            #MemberwiseClone() Object
            #set_Age(Int32)
            ~get_InternalId() Int32
            ~InternalMethod()
            ~set_InternalId(Int32)
            +Approve()
            +Contact(String)
            +Equals(Object) Boolean
            +get_Department() Department~Employee~
            +get_Email() String
            +get_EmployeeId() String
            +get_FirstName() String
            +get_LastName() String
            +get_Level() Int32
            +GetHashCode() Int32
            +GetType() Type
            +SayHello()
            +set_Department(Department~Employee~)
            +set_Email(String)
            +set_EmployeeId(String)
            +set_FirstName(String)
            +set_LastName(String)
            +set_Level(Int32)
            +ToString() String
        }
        class Person {
            -String SecretCode
            #Int32 Age
            ~Int32 InternalId
            +String FirstName
            +String LastName
            -get_SecretCode() String
            -Hide()
            -set_SecretCode(String)
            #DoWork()
            #Finalize()
            #get_Age() Int32
            #MemberwiseClone() Object
            #set_Age(Int32)
            ~get_InternalId() Int32
            ~InternalMethod()
            ~set_InternalId(Int32)
            +Equals(Object) Boolean
            +get_FirstName() String
            +get_LastName() String
            +GetHashCode() Int32
            +GetType() Type
            +SayHello()
            +set_FirstName(String)
            +set_LastName(String)
            +ToString() String
        }
    }
    Employee<|--Manager : Inherited
    Person<|--Employee : Inherited";

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
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
                MethodOptions = new ClassMethodOptions()
                {
                    IncludeVisibility = ClassPropertyVisibility.None
                }
            };

            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Core.Domain {
        class Department~T~ {
            +List~T~ Members
            +String Name
        }
        class Employee {
            #Int32 Age
            ~Int32 InternalId
            +Department~Employee~ Department
            +String Email
            +String EmployeeId
            +String FirstName
            +String LastName
        }
        class IContactable {
            +String Email
        }
        class Manager {
            #Int32 Age
            ~Int32 InternalId
            +Department~Employee~ Department
            +String Email
            +String EmployeeId
            +String FirstName
            +String LastName
            +Int32 Level
        }
        class Person {
            -String SecretCode
            #Int32 Age
            ~Int32 InternalId
            +String FirstName
            +String LastName
        }
    }
    Employee-->Department : Association
    Employee<|--Manager : Inherited
    IContactable..|>Employee : Interface
    IContactable..|>Manager : Interface
    Manager-->Department : Association
    Person<|--Employee : Inherited";

            // Act
            var diagram = assembly.ToMermaidClassDiagram(options);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
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
					IncludeVisibility = ClassPropertyVisibility.None
				},
                PropertyOptions = new ClassPropertyOptions()
                {
                    IncludeVisibility = ClassPropertyVisibility.None
                },
                IncludeClassesVisibility = ClassPropertyVisibility.Public
			};
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Core.Domain {
        class Department~T~
        class Employee
        class IContactable
        class Manager
        class Person
    }
    Employee<|--Manager : Inherited
    IContactable..|>Employee : Interface
    IContactable..|>Manager : Interface
    Person<|--Employee : Inherited";

			// Act
			var diagram = assemblies.ToMermaidClassDiagram(options);
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
		}
    }
}
