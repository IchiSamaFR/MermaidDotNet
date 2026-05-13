using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    [TestClass]
    public class AdvancedFeaturesTests
    {
        [TestMethod]
        public void NodeWithCssClassFlowchart()
        {
            //Arrange
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Styled Node", FlowNodeShapeType.Rectangle, "myClass")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram();
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[Styled Node]
    class node1 myClass";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NodeWithClickActionFlowchart()
        {
            //Arrange
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Clickable Node", FlowNodeShapeType.Rectangle, "", "https://example.com")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram();
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[Clickable Node]
    click node1 ""https://example.com""";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NodeWithBothClassAndClickFlowchart()
        {
            //Arrange
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "Styled Clickable Node", FlowNodeShapeType.Rectangle, "highlightClass", "alert('Hello!')")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram();
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[Styled Clickable Node]
    class node1 highlightClass
    click node1 ""alert('Hello!')""";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ComplexFlowchartWithAllFeatures()
        {
            //Arrange
            FlowDirection direction = FlowDirection.TopDown;
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("start", "Start", FlowNodeShapeType.Circle, "startClass", "console.log('Start clicked')"),
                new FlowNode("process", "Process Data", FlowNodeShapeType.Rectangle),
                new FlowNode("decision", "Is Valid?", FlowNodeShapeType.Rhombus, "decisionClass"),
                new FlowNode("end", "End", FlowNodeShapeType.Circle)
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("start", "process", linkType: FlowLinkType.Normal),
                new FlowLink("process", "decision", "validate", linkType: FlowLinkType.Dotted),
                new FlowLink("decision", "end", "yes", "stroke:green,stroke-width:3px", linkType: FlowLinkType.Thick),
                new FlowLink("decision", "process", linkType: FlowLinkType.Normal, arrowType: FlowLinkArrowType.Circle)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart TD
    start((Start))
    process[Process Data]
    decision{Is Valid?}
    end((End))
    start-->process
    process-.validate.->decision
    decision==yes==>end
    decision--oprocess
    linkStyle 0 stroke:green,stroke-width:3px
    class start startClass
    class decision decisionClass
    click start ""console.log('Start clicked')""";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
