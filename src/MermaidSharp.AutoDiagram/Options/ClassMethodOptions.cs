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
    /// Provides options to control the content generated for class methods in a class diagram from reflection.
    /// </summary>
    public class ClassMethodOptions
    {
        /// <summary>
        /// Gets or sets the visibility level of class methods to include.
        /// </summary>
        public List<ClassPropertyVisibility> IncludeVisibility { get; set; } = new List<ClassPropertyVisibility>
        {
            ClassPropertyVisibility.Public,
            ClassPropertyVisibility.Protected,
            ClassPropertyVisibility.Internal,
            ClassPropertyVisibility.Private
        };

        /// <summary>
        /// Gets or sets a value indicating whether method or function parameters are included in the output.
        /// </summary>
        public bool IncludeParameters { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the return type of methods or functions is included in the output.
        /// </summary>
        public bool IncludeReturnType { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to include methods that return void in the output.
        /// </summary>
        public bool IncludeReturnVoid { get; set; } = false;
    }
}
