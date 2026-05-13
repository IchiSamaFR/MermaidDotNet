using MermaidSharp.Enums;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents contextual information about a class method, including its metadata and return type.
    /// </summary>
    /// <remarks>Provides access to the method's name, reflection metadata, and a context object describing
    /// the return type. This class is typically used for scenarios involving code analysis, reflection, or dynamic
    /// invocation where method details are required.</remarks>
    public class ClassMethodContext : ClassMemberInfo
    {
        /// <summary>
        /// Gets the metadata for the method associated with this instance.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Gets the collection of type contexts representing the parameter types of the method.
        /// </summary>
        public ClassTypeContext[] ParameterTypes { get; }

        /// <summary>
        /// Gets the type context representing the return value of the member.
        /// </summary>
        public ClassTypeContext ReturnType { get; }

        /// <summary>
        /// Initializes a new instance of the ClassMethodContext class using the specified method information.
        /// </summary>
        /// <param name="method">The MethodInfo object that describes the method to be represented by this context. Cannot be null.</param>
        /// <param name="visibility">The visibility of the method. Cannot be null.</param>
        public ClassMethodContext(MethodInfo method, ClassPropertyVisibility visibility)
        {
            Method = method;
            Name = method.Name;
            Visibility = visibility;
            ParameterTypes = method.GetParameters()
                .Select(p => new ClassTypeContext(p.ParameterType))
                .ToArray();
            ReturnType = new ClassTypeContext(method.ReturnType);
        }
    }
}
