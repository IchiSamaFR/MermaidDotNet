using MermaidSharp.AutoDiagram.Extensions;
using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ClassPropertyVisibility IncludeVisibility { get; set; } = ClassPropertyVisibilityExtensions.All;
    }
}
