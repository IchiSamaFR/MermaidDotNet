using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    /// <summary>
    /// Comprehensive test to demonstrate all the new flowchart functionality added
    /// </summary>
    [TestClass]
    public class ComprehensiveFeatureTests
    {
        [TestMethod]
        public void AllNewFeaturesShowcaseFlowchart()
        {
            //Arrange - Create a flowchart that uses every new feature
            FlowDirection direction = FlowDirection.BottomTop; // New direction support
            List<FlowNode> nodes = new List<FlowNode>
            {
                // Various new node shapes with styling and click actions
                new FlowNode("start", "Start Process", FlowNodeShapeType.Circle, "startNode", "startFlow()"),
                new FlowNode("input", "Input Data", FlowNodeShapeType.Parallelogram, "inputClass"),
                new FlowNode("validate", "Validate?", FlowNodeShapeType.Rhombus),
                new FlowNode("process", "Process", FlowNodeShapeType.Subroutine, clickAction: "processData()"),
                new FlowNode("store", "Store Result", FlowNodeShapeType.Cylinder),
                new FlowNode("finish", "Complete", FlowNodeShapeType.Stadium)
            };

            List<FlowLink> links = new List<FlowLink>
            {
                // Various new link types and arrow types
                new FlowLink("start", "input", "begin", linkType: FlowLinkType.Normal),
                new FlowLink("input", "validate", "check", linkType: FlowLinkType.Dotted),
                new FlowLink("validate", "process", "valid", "stroke:green,stroke-width:3px", linkType: FlowLinkType.Thick),
                new FlowLink("process", "store", "", linkType: FlowLinkType.Normal, arrowType: FlowLinkArrowType.Circle),
                new FlowLink("store", "finish", "", linkType: FlowLinkType.Invisible),
                new FlowLink("validate", "input", "invalid", isBidirectional: true, linkType: FlowLinkType.Normal, arrowType: FlowLinkArrowType.Cross)
            };

            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart BT
    start((Start Process))
    input[/Input Data/]
    validate{Validate?}
    process[[Process]]
    store[(Store Result)]
    finish([Complete])
    start--begin-->input
    input-.check.->validate
    validate==valid==>process
    process--ostore
    store~~~>finish
    validatex--invalid--xinput
    linkStyle 0 stroke:green,stroke-width:3px
    class start startNode
    class input inputClass
    click start ""startFlow()""
    click process ""processData()""";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
