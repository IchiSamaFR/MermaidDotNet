using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.ClassDiagrams
{
	[TestClass]
	public class ClassNamespaceTests
	{
		[TestMethod]
		public void ClassDiagram_WithSingleNamespace_ShouldRenderNamespaceBlock()
		{
			// Arrange
			var ns = new ClassNamespace("MyNamespace");
			ns.Classes.Add(new ClassNode("MyClass"));
			var diagram = new ClassDiagram();
			diagram.Namespaces.Add(ns);

			var expected = @"classDiagram
    namespace MyNamespace {
        class MyClass
    }";

			// Act
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassDiagram_WithMultipleNamespaces_ShouldRenderAllNamespaces()
		{
			// Arrange
			var ns1 = new ClassNamespace("NS1");
			ns1.Classes.Add(new ClassNode("ClassA"));
			var ns2 = new ClassNamespace("NS2");
			ns2.Classes.Add(new ClassNode("ClassB"));
			var diagram = new ClassDiagram();
			diagram.Namespaces.Add(ns1);
			diagram.Namespaces.Add(ns2);

			var expected = @"classDiagram
    namespace NS1 {
        class ClassA
    }
    namespace NS2 {
        class ClassB
    }";

			// Act
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void ClassDiagram_WithLinksAcrossNamespaces_ShouldRenderCorrectly()
		{
			// Arrange
			var ns1 = new ClassNamespace("NS1");
			var classA = new ClassNode("ClassA");
			var classB = new ClassNode("ClassB");
			ns1.Classes.Add(classA);
			ns1.Classes.Add(classB);

			var ns2 = new ClassNamespace("NS2");
			var classC = new ClassNode("ClassC");
			ns2.Classes.Add(classC);

			var linkAB = new ClassLink("ClassA", "ClassB", ClassLinkType.Association);
			var linkBC = new ClassLink("ClassB", "ClassC", ClassLinkType.Inheritance);

			var diagram = new ClassDiagram();
			diagram.Namespaces.Add(ns1);
			diagram.Namespaces.Add(ns2);
			diagram.Links.Add(linkAB);
			diagram.Links.Add(linkBC);

			var expected = @"classDiagram
    namespace NS1 {
        class ClassA
        class ClassB
    }
    namespace NS2 {
        class ClassC
    }
    ClassA-->ClassB
    ClassB<|--ClassC";

			// Act
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.AreEqual(expected, result);
		}
	}
}
