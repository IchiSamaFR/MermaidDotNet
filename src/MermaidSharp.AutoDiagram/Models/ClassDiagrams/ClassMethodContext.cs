using System.Reflection;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents contextual information about a class method, including its metadata and return type.
    /// </summary>
    /// <remarks>Provides access to the method's name, reflection metadata, and a context object describing
    /// the return type. This class is typically used for scenarios involving code analysis, reflection, or dynamic
    /// invocation where method details are required.</remarks>
    public class ClassMethodContext
    {
        /// <summary>
        /// Gets the name of the method represented by this instance.
        /// </summary>
        public string Name => Method.Name;

        /// <summary>
        /// Gets the metadata for the method associated with this instance.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Gets the type context representing the return value of the member.
        /// </summary>
        public ClassTypeContext ReturnType { get; }

        /// <summary>
        /// Initializes a new instance of the ClassMethodContext class using the specified method information.
        /// </summary>
        /// <param name="method">The MethodInfo object that describes the method to be represented by this context. Cannot be null.</param>
        public ClassMethodContext(MethodInfo method)
        {
            Method = method;
            ReturnType = new ClassTypeContext(method);
        }
    }
}
