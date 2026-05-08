using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Extensions;
using MermaidSharp.Enums;
using System;

namespace MermaidSharp.AutoDiagram
{
    /// <summary>
    /// Provides options to control the content generated in a class diagram from reflection.
    /// </summary>
    public class ClassDiagramOptions
    {
        /// <summary>
        /// Gets or sets the visibility level of class properties to include.
        /// </summary>
        /// <remarks>Use this property to control which class property visibilities are included when
        /// generating diagrams or processing class metadata. The default value includes all visibility
        /// levels.</remarks>
        public ClassPropertyVisibility IncludeClassesVisibility { get; set; } = ClassPropertyVisibilityExtensions.All;

        /// <summary>
        /// Gets or sets a value indicating the visibility of properties to include in the diagram.
        /// </summary>
        public ClassPropertyVisibility IncludePropertiesVisibility { get; set; } = ClassPropertyVisibilityExtensions.All;

        /// <summary>
        /// Gets or sets a value indicating the visibility of methods to include in the diagram.
        /// </summary>
        public ClassPropertyVisibility IncludeMethodsVisibility { get; set; } = ClassPropertyVisibilityExtensions.All;

        /// <summary>
        /// Gets or sets the option for including class links.
        /// </summary>
        public ClassLinkOption IncludeLinks { get; set; } = ClassLinkOption.All;
        
        /// <summary>
        /// Gets or sets a value indicating whether to include labels for class links in the diagram.
        /// </summary>
        public bool IncludeLinksLabels { get; set; } = true;

		/// <summary>
		/// Gets or sets a predicate used to filter which types are included in the diagram.
		/// When null, all types are included.
		/// </summary>
		public Func<Type, bool> TypeFilter { get; set; } = null;
    }
}
