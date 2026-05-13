using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents common information about a class member, such as its name and
    /// visibility. This abstract class serves as a base for specific member types
    /// like properties and methods, allowing for shared functionality and consistent
    /// handling of member metadata across different member contexts.
    /// </summary>
    public abstract class ClassMemberInfo
    {
        /// <summary>
        /// Gets the name of the member represented by this instance.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the visibility of the member represented by this instance.
        /// </summary>
        public ClassPropertyVisibility Visibility { get; protected set; }
    }
}
