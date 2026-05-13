using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    [TestClass]
    public class LinkTypeTests
    {
        [TestMethod]
        public void DottedLinkFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "dotted", linkType: FlowLinkType.Dotted)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1-.dotted.->node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ThickLinkFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "thick", "", false, FlowLinkType.Thick)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1==thick==>node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void InvisibleLinkFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "", "", false, FlowLinkType.Invisible)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1~~~>node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CircleArrowLinkFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", arrowType: FlowLinkArrowType.Circle)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--onode2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CrossArrowLinkFlowchart()
        {
            //Arrange
            FlowDirection direction = FlowDirection.LeftRight;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", arrowType: FlowLinkArrowType.Cross)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--xnode2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BidirectionalCircleArrowLinkFlowchart()
        {
            //Arrange
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Node 1"),
                new FlowNode("node2", "Node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "", "", true, FlowLinkType.Normal, FlowLinkArrowType.Circle)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram();
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1o--onode2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
