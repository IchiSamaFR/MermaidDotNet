using MermaidDotNet.Diagrams;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.Flowcharts;

[TestClass]
public class FlowchartTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        FlowchartDiagram flowchart = new("TD", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidLRFlowchart()
    {
        //Arrange
        FlowchartDiagram flowchart = new("LR", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidBTFlowchart()
    {
        //Arrange
        FlowchartDiagram flowchart = new("BT", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidRLFlowchart()
    {
        //Arrange
        FlowchartDiagram flowchart = new("RL", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidTBFlowchart()
    {
        //Arrange
        FlowchartDiagram flowchart = new("TB", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void InvalidNoDirectionFlowchart()
    {
        //Arrange
        try
        {
            FlowchartDiagram flowchart = new("none", new(), new());

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Direction none is currently unsupported", ex.Message);
        }
    }



    [TestMethod]
    public void NodesAddedIncorrectlyFlowchart()
    {
        //Arrange
        try
        {
            FlowchartDiagram flowchart = new("LR", new(), new());
            flowchart.Nodes.Add(new("node1", "node1"));

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("The NavigationNodes collection is empty, but Nodes collection is not empty. This is likely an issue because Nodes were added manually instead of as a collection in the FlowChart constructor", ex.Message);
        }
    }

    [TestMethod]
    public void SourceNodeDoesNotExistInNodesFlowchart()
    {
        //Arrange
        try
        {
            List<Models.FlowNode> nodes = [new("node2", "node2")];
            FlowchartDiagram flowchart = new("LR", nodes, new());
            flowchart.Links.Add(new FlowLink("node1", "node2", "1"));

            //Act
            flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Source node (node1) in link connection (node1-->node2) not found", ex.Message);
        }
    }

    [TestMethod]
    public void DestinationNodeDoesNotExistInNodesFlowchart()
    {
        //Arrange
        try
        {
            List<Models.FlowNode> nodes = [new("node1", "node1")];
            FlowchartDiagram flowchart = new("LR", nodes, new());
            flowchart.Links.Add(new FlowLink("node1", "node2", "1"));

            //Act
            flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Destination node (node2) in link connection (node1-->node2) not found", ex.Message);
        }
    }
}