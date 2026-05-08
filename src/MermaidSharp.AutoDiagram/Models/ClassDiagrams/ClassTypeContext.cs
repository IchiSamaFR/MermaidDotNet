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
        public ClassTypeContext ReflectedType { get; }

        /// <summary>
        /// Gets the collection of types associated with the current instance.
        /// </summary>
        public List<Type> Types { get; }

        /// <summary>
        /// Gets the simple name of the type (e.g. "List" for List&lt;string&gt;).
        /// </summary>
        public string Name => Type.IsGenericType
            ? Type.GetGenericTypeDefinition().Name.Split('`')[0]
            : Type.Name;

        /// <summary>
        /// Gets the fully qualified name, including the reflected type if available.
        /// </summary>
        public string FullName => Name + (ReflectedType != null ? $"~{ReflectedType.FullName}~" : string.Empty);

        /// <summary>
        /// Initializes a new instance from a Type.
        /// </summary>
        /// <param name="type">The type.</param>
        public ClassTypeContext(Type type, Type reflectedType = null)
        {
            if (type.IsGenericType)
            {
                Type = type.GetGenericTypeDefinition(); // List<>
                if (reflectedType == null)
                {
                    var genericArg = type.GetGenericArguments().FirstOrDefault();
                    ReflectedType = genericArg != null ? new ClassTypeContext(genericArg) : null;
                }
                else
                {
                    ReflectedType = new ClassTypeContext(reflectedType);
                }
            }
            else
            {
                Type = type;
                ReflectedType = null;
            }

            Types = new List<Type> { Type };
            if (ReflectedType != null)
                Types.AddRange(ReflectedType.Types);
        }

        /// <summary>
        /// Initializes a new instance from a PropertyInfo.
        /// </summary>
        /// <param name="property">The property info.</param>
        public ClassTypeContext(PropertyInfo property)
            : this(property.PropertyType, property.ReflectedType)
        {
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