using MermaidSharp.Diagrams;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    [TestClass]
    public class FlowchartTests
    {
        [TestMethod]
        public void ValidTDFlowchart()
        {
            //Arrange
            FlowchartDiagram flowchart = new FlowchartDiagram("TD");

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }

        [TestMethod]
        public void ValidLRFlowchart()
        {
            //Arrange
            FlowchartDiagram flowchart = new FlowchartDiagram("LR");

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }

        [TestMethod]
        public void ValidBTFlowchart()
        {
            //Arrange
            FlowchartDiagram flowchart = new FlowchartDiagram("BT");

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }

        [TestMethod]
        public void ValidRLFlowchart()
        {
            //Arrange
            FlowchartDiagram flowchart = new FlowchartDiagram("RL");

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }

        [TestMethod]
        public void ValidTBFlowchart()
        {
            //Arrange
            FlowchartDiagram flowchart = new FlowchartDiagram("TB");

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
                FlowchartDiagram flowchart = new FlowchartDiagram("none");

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
                FlowchartDiagram flowchart = new FlowchartDiagram("LR");
                flowchart.Nodes.Add(new FlowNode("node1", "node1"));

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
                List<FlowNode> nodes = new List<FlowNode> { new FlowNode("node2", "node2") };
                FlowchartDiagram flowchart = new FlowchartDiagram("LR");
                flowchart.Nodes.AddRange(nodes);
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
                List<FlowNode> nodes = new List<FlowNode> { new FlowNode("node1", "node1") };
                FlowchartDiagram flowchart = new FlowchartDiagram("LR");
                flowchart.Nodes.AddRange(nodes);
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
}
