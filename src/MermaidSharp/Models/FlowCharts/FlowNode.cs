using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a flowchart node with a specific shape and optional click action for use in Mermaid diagrams.
    /// </summary>
    /// <remarks>A FlowNode extends the base Node class by allowing the specification of a node shape and an
    /// optional click action. The click action can be used to define interactive behavior in rendered Mermaid diagrams,
    /// such as navigation or custom JavaScript execution. This class is typically used when constructing flowcharts
    /// that require nodes with distinct shapes or interactive capabilities.</remarks>
    public class FlowNode : ANode
    {
        /// <summary>
        /// Gets or sets the shape type used to render the flow node.
        /// </summary>
        public FlowNodeShapeType Shape { get; set; }
        /// <summary>
        /// Gets or sets the click action associated with the flow node.
        /// </summary>
        public string ClickAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the FlowNode class with the specified name, display text, shape, CSS class,
        /// and click action.
        /// </summary>
        /// <param name="name">The unique identifier for the node within the flowchart.</param>
        /// <param name="text">The display text shown inside the node.</param>
        /// <param name="shape">The shape of the node. Defaults to ShapeType.Rectangle.</param>
        /// <param name="cssClass">An optional CSS class to apply custom styling to the node. If not specified, no additional CSS class is
        /// applied.</param>
        /// <param name="clickAction">An optional action or URL to be triggered when the node is clicked. If not specified, the node will not have
        /// a click action.</param>
        public FlowNode(string name, string text, FlowNodeShapeType shape = FlowNodeShapeType.Rectangle, string cssClass = "", string clickAction = "")
            : base(name, text, cssClass)
        {
            Shape = shape;
            ClickAction = clickAction;
        }

        /// <summary>
        /// Generates a Mermaid click directive string for the current node if a click action is defined.
        /// </summary>
        /// <remarks>Use this method to produce a valid Mermaid syntax for node click actions. If no click
        /// action is set, the method returns an empty string, indicating that no click directive should be
        /// rendered.</remarks>
        /// <returns>A Mermaid click directive string if a click action is specified; otherwise, an empty string.</returns>
        public string ToClickString()
        {
            if (string.IsNullOrEmpty(ClickAction))
            {
                return string.Empty;
            }
            return $"click {Name} \"{ClickAction}\"";
        }

        /// <summary>
        /// Generates the text surrounded by the primary and secondary shape delimiters.
        /// </summary>
        /// <returns>A string consisting of the primary shape delimiter, the text, and the secondary shape delimiter.</returns>
        protected override string GetSurroundedText()
        {
            return $"{Shape.PrimaryString()}{Text}{Shape.SecondaryString()}";
        }
    }
}
