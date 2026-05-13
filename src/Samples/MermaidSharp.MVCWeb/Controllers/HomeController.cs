using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using MermaidSharp.MVCWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MermaidSharp.MVCWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<FlowNode> nodes = new()
            {
                new("node1", "This is node 1"),
                new("node2", "This is node 2", FlowNodeShapeType.Hexagon),
                new("node3", "This is node 3", FlowNodeShapeType.Rounded)
            };
            List<FlowLink> links = new()
            {
                new FlowLink("node1", "node2", "12s"),
                new FlowLink("node1", "node3", "3mins")
            };
            FlowchartDiagram flowchart = new();
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);
            string graph = flowchart.CalculateDiagram();

            return View(model: graph);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
