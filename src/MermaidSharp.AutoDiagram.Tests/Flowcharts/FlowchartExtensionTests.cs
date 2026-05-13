using MermaidSharp.AutoDiagram.Diagrams;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Tests.Flowcharts
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

#if NET48
			var expected = @"flowchart LR
    subgraph Project
    MermaidSharp[MermaidSharp]
    MermaidSharp.AutoDiagram[MermaidSharp.AutoDiagram]
    end
    MermaidSharp-->mscorlib
    MermaidSharp-->System.Core
    MermaidSharp.AutoDiagram-->MermaidSharp
    MermaidSharp.AutoDiagram-->mscorlib
    MermaidSharp.AutoDiagram-->System.Core";
#else
			var expected = @"flowchart LR
    subgraph Project
    MermaidSharp[MermaidSharp]
    MermaidSharp.AutoDiagram[MermaidSharp.AutoDiagram]
    end
    MermaidSharp-->System.Collections
    MermaidSharp-->System.Collections.Concurrent
    MermaidSharp-->System.Linq
    MermaidSharp-->System.Runtime
    MermaidSharp.AutoDiagram-->MermaidSharp
    MermaidSharp.AutoDiagram-->System.Collections
    MermaidSharp.AutoDiagram-->System.Linq
    MermaidSharp.AutoDiagram-->System.Runtime";

#endif

            // Act
            var diagram = assemblies.ToMermaidFlowchartDiagram();
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.IsNotNull(result);
		    Assert.AreEqual(expected, result);
		}
	}
}
