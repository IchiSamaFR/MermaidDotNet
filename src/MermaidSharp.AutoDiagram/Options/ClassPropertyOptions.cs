using MermaidSharp.Enums;
using System.Collections.Generic;

namespace MermaidSharp.AutoDiagram.Options
{
    /// <summary>
    /// Provides options to control the content generated for class properties in a class diagram from reflection.
    /// </summary>
    public class ClassPropertyOptions
    {
        /// <summary>
        /// Gets or sets the visibility level of class properties to include.
        /// </summary>
        public List<ClassPropertyVisibility> IncludeVisibility { get; set; } = new List<ClassPropertyVisibility>
        {
            ClassPropertyVisibility.Public,
            ClassPropertyVisibility.Protected,
            ClassPropertyVisibility.Internal,
            ClassPropertyVisibility.Private
        };
    }
}
