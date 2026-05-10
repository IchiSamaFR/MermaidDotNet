using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.Diagrams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.AutoDiagram.Tests.Flowchars
{
	[TestClass]
	public class FlowchartExtensionTests
	{
		[TestMethod]
		public void ToMermaidFlowchartDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
		{
			// Arrange
			var assembliesName = new[] { "MermaidSharp", "MermaidSharp.AutoDiagram" };
			var assemblies = assembliesName.Select(Assembly.Load);
			var expected = @"flowchart LR
    subgraph Project
    MermaidSharp[MermaidSharp]
    MermaidSharp.AutoDiagram[MermaidSharp.AutoDiagram]
    end
    MermaidSharp-->System.Runtime
    MermaidSharp-->System.Collections
    MermaidSharp-->System.Collections.Concurrent
    MermaidSharp-->System.Linq
    MermaidSharp.AutoDiagram-->System.Runtime
    MermaidSharp.AutoDiagram-->MermaidSharp
    MermaidSharp.AutoDiagram-->System.Collections
    MermaidSharp.AutoDiagram-->System.Linq";

			// Act
			var diagram = assemblies.ToMermaidFlowchartDiagram();
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.IsNotNull(result);
		    Assert.AreEqual(expected, result);
		}
	}
}
