using System;
using System.Collections.Generic;
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
        [TestMethod]
        public void ToMermaidClassDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
        {
            // Arrange
            var type = typeof(Person);
#if NET48
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
#elif NET8_0_OR_GREATER
            var expected = @"classDiagram
    class Person {
        +String FirstName
        +String LastName
        +get_FirstName()
        +set_FirstName()
        +get_LastName()
        +set_LastName()
        +SayHello()
        +GetType()
        +ToString()
        +Equals()
        +GetHashCode()
    }";
#endif

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
            var types = new[] { typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>) };

#if NET48
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
    Department-->Employee : Association
    IContactable..|>Employee : Interface
    Department-->Manager : Association
    Employee<|--Manager : Inherited
    IContactable..|>Manager : Interface";
#elif NET8_0_OR_GREATER
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
        +GetType()
        +ToString()
        +Equals()
        +GetHashCode()
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
        +GetType()
        +ToString()
        +Equals()
        +GetHashCode()
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
        +GetType()
        +ToString()
        +Equals()
        +GetHashCode()
    }
    Department-->Employee : Association
    IContactable..|>Employee : Interface
    Department-->Manager : Association
    Employee<|--Manager : Inherited
    IContactable..|>Manager : Interface";
#endif

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
            var whiteListe = new List<Type> { typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<Employee>).GetGenericTypeDefinition(), typeof(Person) };
			var options = new ClassDiagramOptions()
			{
				IncludeLinksLabels = false,
                TypeFilter = (type) => whiteListe.Contains(type)
			};
#if NET48
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
    }
    Department-->Employee
    Person<|--Employee
    IContactable..|>Employee
    Department-->Manager
    Employee<|--Manager
    IContactable..|>Manager";
#elif NET8_0_OR_GREATER
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Tests {
        class Department~T~ {
            +String Name
            +List~Department~T~~ Members
            +get_Name()
            +set_Name()
            +get_Members() T
            +set_Members(T)
            +GetType()
            +ToString()
            +Equals()
            +GetHashCode()
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
            +GetType()
            +ToString()
            +Equals()
            +GetHashCode()
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
            +GetType()
            +ToString()
            +Equals()
            +GetHashCode()
        }
        class Person {
            +String FirstName
            +String LastName
            +get_FirstName()
            +set_FirstName()
            +get_LastName()
            +set_LastName()
            +SayHello()
            +GetType()
            +ToString()
            +Equals()
            +GetHashCode()
        }
    }
    Department-->Employee
    Person<|--Employee
    IContactable..|>Employee
    Department-->Manager
    Employee<|--Manager
    IContactable..|>Manager";
#endif

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
                IncludeMethodsVisibility = ClassPropertyVisibility.None
            };

#if NET48
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
    Department-->Employee : Association
    Person<|--Employee : Inherited
    IContactable..|>Employee : Interface
    Department-->Manager : Association
    Employee<|--Manager : Inherited
    IContactable..|>Manager : Interface";
#elif NET8_0_OR_GREATER
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Tests {
        class AutoGeneratedProgram
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
    Department-->Employee : Association
    Person<|--Employee : Inherited
    IContactable..|>Employee : Interface
    Department-->Manager : Association
    Employee<|--Manager : Inherited
    IContactable..|>Manager : Interface";
#endif

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
            var assemblies = new[] { typeof(Employee).Assembly, typeof(ClassDiagram).Assembly };
            var options = new ClassDiagramOptions()
			{
				IncludeMethodsVisibility = ClassPropertyVisibility.None,
                IncludePropertiesVisibility = ClassPropertyVisibility.None,
                IncludeClassesVisibility = ClassPropertyVisibility.Public
			};
            var expected = @"classDiagram
    namespace MermaidSharp.AutoDiagram.Tests {
        class Department~T~
        class Employee
        class IContactable
        class Manager
        class Person
        class ClassDiagramExtensionTests
    }
    namespace MermaidSharp {
        class AGitAction
        class ALink
        class ANode
        class ChartXAxis
        class ChartYAxis
        class ClassLink
        class ClassMethod
        class ClassMethodParam
        class ClassNamespace
        class ClassNode
        class ClassProperty
        class EntityRelationColumn
        class EntityRelationLink
        class EntityRelationNode
        class FlowLink
        class FlowNode
        class FlowSubGraph
        class GitBranch
        class GitCheckout
        class GitCherryPick
        class GitCommit
        class GitMerge
        class PieSlice
        class QuadrantChartPoint
        class XYSeries
        class ChartOrientation
        class ClassLinkType
        class ClassPropertyVisibility
        class ConfigTheme
        class FlowDirection
        class FlowLinkArrowType
        class FlowLinkType
        class FlowNodeShapeType
        class GitCommitType
        class GitDirection
        class RelationConstraintType
        class RelationLinkType
        class XAxisPosition
        class XYSeriesType
        class YAxisPosition
        class ADiagram~TConfig~
        class AGraph~TConfig~
        class AMermaid~TConfig~
        class ClassDiagram
        class EntityRelationshipDiagram
        class FlowchartDiagram
        class GitGraph
        class PieChartDiagram
        class QuadrantChartDiagram
        class XYChartDiagram
        class AConfig~TThemeVariables~
        class AConfigurable
        class ClassDiagramConfig
        class EntityRelationshipConfig
        class FlowChartConfig
        class GitGraphConfig
        class IConfig
        class PieChartConfig
        class QuadrantChartConfig
        class XYChartConfig
        class AThemeVariables
        class ClassDiagramThemeVariables
        class EntityRelationshipThemeVariables
        class FlowChartThemeVariables
        class GitGraphThemeVariables
        class IThemeVariables
        class PieChartThemeVariables
        class QuadrantChartThemeVariables
        class XYChartThemeVariables
        class XYChartThemeChildConfig
        class ConfigVariableAttribute
        class MermaidEnumAttribute
        class ThemeVariableAttribute
    }
    Person<|--Employee : Inherited
    IContactable..|>Employee : Interface
    Employee<|--Manager : Inherited
    IContactable..|>Manager : Interface
    ALink<|--ClassLink : Inherited
    ANode<|--ClassNode : Inherited
    ALink<|--EntityRelationLink : Inherited
    ANode<|--EntityRelationNode : Inherited
    ALink<|--FlowLink : Inherited
    ANode<|--FlowNode : Inherited
    AGitAction<|--GitBranch : Inherited
    AGitAction<|--GitCheckout : Inherited
    AGitAction<|--GitCherryPick : Inherited
    AGitAction<|--GitCommit : Inherited
    AGitAction<|--GitMerge : Inherited
    IConfig..|>AConfig : Interface
    IConfig..|>ClassDiagramConfig : Interface
    IConfig..|>EntityRelationshipConfig : Interface
    IConfig..|>FlowChartConfig : Interface
    IConfig..|>GitGraphConfig : Interface
    IConfig..|>PieChartConfig : Interface
    IConfig..|>QuadrantChartConfig : Interface
    IConfig..|>XYChartConfig : Interface
    AConfigurable<|--AThemeVariables : Inherited
    IThemeVariables..|>AThemeVariables : Interface
    AThemeVariables<|--ClassDiagramThemeVariables : Inherited
    IThemeVariables..|>ClassDiagramThemeVariables : Interface
    AThemeVariables<|--EntityRelationshipThemeVariables : Inherited
    IThemeVariables..|>EntityRelationshipThemeVariables : Interface
    AThemeVariables<|--FlowChartThemeVariables : Inherited
    IThemeVariables..|>FlowChartThemeVariables : Interface
    AThemeVariables<|--GitGraphThemeVariables : Inherited
    IThemeVariables..|>GitGraphThemeVariables : Interface
    AThemeVariables<|--PieChartThemeVariables : Inherited
    IThemeVariables..|>PieChartThemeVariables : Interface
    AThemeVariables<|--QuadrantChartThemeVariables : Inherited
    IThemeVariables..|>QuadrantChartThemeVariables : Interface
    AThemeVariables<|--XYChartThemeVariables : Inherited
    IThemeVariables..|>XYChartThemeVariables : Interface
    AConfigurable<|--XYChartThemeChildConfig : Inherited
    IThemeVariables..|>XYChartThemeChildConfig : Interface";

			// Act
			var diagram = assemblies.ToMermaidClassDiagram(options);
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
		}
    }
}
