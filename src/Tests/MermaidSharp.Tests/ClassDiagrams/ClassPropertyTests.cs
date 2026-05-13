using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.ClassDiagrams
{
	[TestClass]
	public class ClassPropertyTests
	{
		[TestMethod]
		public void ClassProperty_PublicVisibility_RendersWithPlusSign()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("User", properties: new List<ClassProperty>
				{
					new ClassProperty("Id", "int", ClassPropertyVisibility.Public)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class User {
        +int Id
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_PrivateVisibility_RendersWithMinusSign()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("User", properties: new List<ClassProperty>
				{
					new ClassProperty("Password", "string", ClassPropertyVisibility.Private)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class User {
        -string Password
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_ProtectedVisibility_RendersWithHashSign()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Entity", properties: new List<ClassProperty>
				{
					new ClassProperty("InternalId", "int", ClassPropertyVisibility.Protected)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Entity {
        #int InternalId
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_InternalVisibility_RendersWithTildeSign()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Entity", properties: new List<ClassProperty>
				{
					new ClassProperty("Flag", "bool", ClassPropertyVisibility.Internal)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Entity {
        ~bool Flag
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_NoneVisibility_RendersWithoutSymbol()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Entity", properties: new List<ClassProperty>
				{
					new ClassProperty("Status", "string", ClassPropertyVisibility.None)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Entity {
        string Status
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_AllVisibilities_RendersAllSymbols()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Sample", properties: new List<ClassProperty>
				{
					new ClassProperty("PubField", "int", ClassPropertyVisibility.Public),
					new ClassProperty("PrivField", "string", ClassPropertyVisibility.Private),
					new ClassProperty("ProtField", "bool", ClassPropertyVisibility.Protected),
					new ClassProperty("IntField", "double", ClassPropertyVisibility.Internal),
					new ClassProperty("NoneField", "DateTime", ClassPropertyVisibility.None),
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Sample {
        +int PubField
        -string PrivField
        #bool ProtField
        ~double IntField
        DateTime NoneField
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_GenericType_RendersWithTilde()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Container", properties: new List<ClassProperty>
				{
					new ClassProperty("Items", "List<string>", ClassPropertyVisibility.Public)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Container {
        +List~string~ Items
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassProperty_NestedGenericType_RendersWithTilde()
		{
			// Arrange
			var nodes = new List<ClassNode>
			{
				new ClassNode("Cache", properties: new List<ClassProperty>
				{
					new ClassProperty("Data", "Dictionary<string, int>", ClassPropertyVisibility.Private)
				})
			};
			var diagram = new ClassDiagram();
			diagram.Nodes.AddRange(nodes);

			var expected = @"classDiagram
    class Cache {
        -Dictionary~string, int~ Data
    }";

			// Act
			string result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

	}
}
