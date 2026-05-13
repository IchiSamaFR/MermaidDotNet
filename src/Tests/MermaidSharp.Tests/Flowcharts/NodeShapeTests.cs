using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    [TestClass]
    public class NodeShapeTests
    {
        [TestMethod]
        public void ParallelogramNodeShapeFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is a parallelogram", FlowNodeShapeType.Parallelogram)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[/This is a parallelogram/]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TrapezoidNodeShapeFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is a trapezoid", FlowNodeShapeType.Trapezoid)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[\This is a trapezoid\]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TrapezoidAltNodeShapeFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is a trapezoid alt", FlowNodeShapeType.TrapezoidAlt)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[/This is a trapezoid alt\]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SubroutineNodeShapeFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is a subroutine", FlowNodeShapeType.Subroutine)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[[This is a subroutine]]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
