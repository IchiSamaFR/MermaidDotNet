using System.Collections.Generic;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents the context for a .NET assembly used in class diagram generation.
    /// </summary>
    /// <remarks>This context provides access to the assembly metadata and the collection of class diagrams
    /// associated with the assembly. It is typically used as the root context when analyzing or visualizing the
    /// structure of an assembly's types.</remarks>
    public class ClassAssemblyContext
    {
        /// <summary>
        /// Gets the simple name of the current assembly.
        /// </summary>
        public string Name => Assembly.GetName().Name ?? "UnknownAssembly";

        /// <summary>
        /// Gets the assembly that defines the current type or member.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the collection of class diagram contexts associated with the current instance.
        /// </summary>
        public List<ClassNodeContext> ClassDiagrams { get; } = new List<ClassNodeContext>();

        /// <summary>
        /// Initializes a new instance of the ClassAssemblyContext class using the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly that contains the types to be used in the context. Cannot be null.</param>
        public ClassAssemblyContext(Assembly assembly)
        {
            Assembly = assembly;
        }
    }
}
