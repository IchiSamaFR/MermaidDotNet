using MermaidSharp.AutoDiagram.Diagrams;
using MermaidSharp.AutoDiagram.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.AutoDiagram.Tests.Flowcharts
{
	[TestClass]
	public class FlowchartExtensionTests
	{
		[TestMethod]
		public void ToMermaidFlowchartDiagram_VisibilityOptions_RespectsPropertyAndMethodVisibility()
		{
			// Arrange
			var assemblies = new[] { typeof(FlowchartExtensionTests).Assembly, typeof(Person).Assembly };

            // Act
            var diagram = assemblies.ToMermaidFlowchartDiagram();
			var result = diagram.CalculateDiagram();

			// Assert
			Assert.IsNotNull(result);
			StringAssert.Contains(result, "flowchart LR");
			StringAssert.Contains(result, "subgraph Project");
			StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests[MermaidSharp.AutoDiagram.Tests]");
			StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests.Models[MermaidSharp.AutoDiagram.Tests.Models]");
			StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests-->MermaidSharp.AutoDiagram");
			StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests-->MermaidSharp.AutoDiagram.Tests.Models");
			StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests.Models-->");
		}
	}
}
