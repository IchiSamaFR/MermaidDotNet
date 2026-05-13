using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    /// <summary>
    /// Represents a type context for a property, including generic arguments.
    /// </summary>
    public class ClassTypeContext
    {
        /// <summary>
        /// Gets the .NET type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the generic argument contexts.
        /// </summary>
        public ClassTypeContext ArgumentType { get; }

        /// <summary>
        /// Gets the simple name of the type (e.g. "List" for List&lt;string&gt;).
        /// </summary>
        public string Name => Type.IsGenericType
            ? Type.GetGenericTypeDefinition().Name.Split('`')[0]
            : Type.Name;

        /// <summary>
        /// Gets the fully qualified name, including the reflected type if available.
        /// </summary>
        public string FullName => Name + (ArgumentType != null ? $"~{ArgumentType.FullName}~" : string.Empty);

        /// <summary>
        /// Initializes a new instance from a Type.
        /// </summary>
        /// <param name="type">The type.</param>
        public ClassTypeContext(Type type)
        {
            Type = type;

            if (!Type.IsGenericType)
            {
                return;
            }

            var argument = Type.GetGenericArguments().FirstOrDefault();
            if (argument == null)
            {
                return;
            }

            ArgumentType = new ClassTypeContext(argument);
        }

        /// <summary>
        /// Initializes a new instance of the ClassTypeContext class using the return type of the specified method.
        /// </summary>
        /// <param name="method">The method whose return type is used to initialize the context.</param>
        public ClassTypeContext(MethodInfo method)
            : this(method.ReturnType)
        {
        }
    }
}