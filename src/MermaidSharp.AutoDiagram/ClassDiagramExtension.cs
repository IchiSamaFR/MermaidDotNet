using MermaidSharp.AutoDiagram.Extensions;
using MermaidSharp.AutoDiagram.Models.ClassDiagrams;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MermaidSharp.AutoDiagram
{
    /// <summary>
    /// Provides extension methods for generating Mermaid class diagrams from .NET types using reflection.
    /// </summary>
    public static class ClassDiagramExtension
    {
        /// <summary>
        /// Generates a Mermaid class diagram from all public types in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect. Cannot be null.</param>
        /// <param name="options">Options controlling what is included in the diagram. If null, default options are used.</param>
        /// <returns>A <see cref="ClassDiagram"/> representing the types and relationships found in the assembly.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="assembly"/> is null.</exception>
        public static ClassDiagram ToMermaidClassDiagram(this Assembly assembly, ClassDiagramOptions? options = null)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            options ??= new ClassDiagramOptions();

            var diagram = new ClassDiagram();
            var classNamespace = BuildClassAssemblyContext(assembly, options);

            diagram.Namespaces.Add(MapToClassNamespace(classNamespace));

            var dictionary = classNamespace.ClassDiagrams.ToDictionary(c => c.Type.Name, c => c);
            foreach (var nodeContext in classNamespace.ClassDiagrams)
            {
                diagram.Links.AddRange(BuildLinks(nodeContext, dictionary, options));
            }
            return diagram;
        }

        /// <summary>
        /// Generates a Mermaid class diagram from the specified collection of types.
        /// </summary>
        /// <param name="types">The collection of types to include in the diagram. Cannot be null.</param>
        /// <param name="options">Options controlling what is included in the diagram. If null, default options are used.</param>
        /// <returns>A <see cref="ClassDiagram"/> representing the provided types and their relationships.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="types"/> is null.</exception>
        public static ClassDiagram ToMermaidClassDiagram(this IEnumerable<Type> types, ClassDiagramOptions? options = null)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            options ??= new ClassDiagramOptions();

            var diagram = new ClassDiagram();
            var contexts = types.Select(t => BuildClassNodeContext(t, options)).ToList();

            diagram.Nodes.AddRange(contexts.Select(MapToClassNode));

            var dictionary = contexts.ToDictionary(c => c.Type.Name, c => c);
            foreach (var nodeContext in contexts)
            {
                diagram.Links.AddRange(BuildLinks(nodeContext, dictionary, options));
            }

            return diagram;
        }


        #region Context Builders
        private static ClassAssemblyContext BuildClassAssemblyContext(Assembly assembly, ClassDiagramOptions options)
        {
            var assemblyContext = new ClassAssemblyContext(assembly);
            var types = assembly.GetTypes()
                .Where(cd => options.TypeFilter == null || options.TypeFilter(cd.DeclaringType ?? cd))
                .Where(cd => options.IncludeClassesVisibility.HasFlag(GetClassVisibility(cd.DeclaringType ?? cd)))
                .Where(cd => cd.DeclaringType == null)
                .Distinct()
                .ToList();
            assemblyContext.ClassDiagrams.AddRange(types.Select(t => BuildClassNodeContext(t, options)));
            return assemblyContext;
        }

        private static ClassNodeContext BuildClassNodeContext(Type type, ClassDiagramOptions options)
        {
            var nodeContext = new ClassNodeContext(type.DeclaringType ?? type);
            nodeContext.Properties.AddRange(nodeContext.Type.Type.GetProperties()
                .Where(p => options.IncludePropertiesVisibility.HasFlag(GetPropertyVisibility(p)))
                .Select(BuildClassPropertyContext));
            nodeContext.Methods.AddRange(nodeContext.Type.Type.GetMethods()
                .Where(m => options.IncludeMethodsVisibility.HasFlag(GetMethodVisibility(m)))
                .Select(BuildClassMethodContext));
            return nodeContext;
        }

        private static ClassPropertyContext BuildClassPropertyContext(PropertyInfo property)
        {
            return new ClassPropertyContext(property);
        }

        private static ClassMethodContext BuildClassMethodContext(MethodInfo method)
        {
            return new ClassMethodContext(method);
        }
        #endregion

        #region Diagram Models
        private static ClassNamespace MapToClassNamespace(ClassAssemblyContext assemblyContext)
        {
            var classNamespace = new ClassNamespace(assemblyContext.Name);
            var filteredClasses = assemblyContext.ClassDiagrams;
            classNamespace.Classes.AddRange(filteredClasses.Select(MapToClassNode));
            return classNamespace;
        }

        private static ClassNode MapToClassNode(ClassNodeContext classNodeContext)
        {
            string name = classNodeContext.Type.FullName;
            var properties = classNodeContext.Properties
                .Select(MapToClassProperty).ToList();
            var methods = classNodeContext.Methods
                .Select(MapToClassMethod).ToList();

            return new ClassNode(name, string.Empty, string.Empty, properties, methods);
        }

        private static ClassProperty MapToClassProperty(ClassPropertyContext propertyContext)
        {
            return new ClassProperty(propertyContext.Name, propertyContext.Type.FullName, GetPropertyVisibility(propertyContext.Property));
        }

        private static ClassMethod MapToClassMethod(ClassMethodContext methodContext)
        {
            var method = methodContext.Method;
            var parameters = method.GetParameters()
                .Select(p => new ClassMethodParam(string.Empty, p.ParameterType.GetFriendlyType()))
                .ToList();
            var returnType = method.ReturnType == typeof(void) ? string.Empty : method.ReturnType.GetFriendlyType();
            return new ClassMethod(method.Name, returnType, GetMethodVisibility(method), parameters);
        }

        private static IEnumerable<ClassLink> BuildLinks(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
        {
            var links = new List<ClassLink>();

            var typeProperties = nodeContext.Properties;
            foreach (var property in typeProperties)
            {
                foreach (var type in property.Type.Types)
                {
                    if (!typeNames.ContainsKey(type.Name))
                        continue;

                    links.Add(new ClassLink(property.Name, type.Name, ClassLinkType.Association));
                }
            }

            return links;
        }
        #endregion

        #region Visibility Helpers
        private static ClassPropertyVisibility GetClassVisibility(Type type)
        {
            if (type.IsPublic || type.IsNestedPublic)
                return ClassPropertyVisibility.Public;
            if (type.IsNestedFamily)
                return ClassPropertyVisibility.Protected;
            if (type.IsNotPublic || type.IsNestedAssembly)
                return ClassPropertyVisibility.Internal;
            return ClassPropertyVisibility.Private;
        }

        private static ClassPropertyVisibility GetPropertyVisibility(PropertyInfo prop)
        {
            var getter = prop.GetGetMethod(nonPublic: true);
            if (getter == null)
                return ClassPropertyVisibility.Private;

            if (getter.IsPublic)
                return ClassPropertyVisibility.Public;
            if (getter.IsFamily)
                return ClassPropertyVisibility.Protected;
            if (getter.IsAssembly)
                return ClassPropertyVisibility.Internal;
            return ClassPropertyVisibility.Private;
        }

        private static ClassPropertyVisibility GetMethodVisibility(MethodInfo method)
        {
            if (method.IsPublic)
                return ClassPropertyVisibility.Public;
            if (method.IsFamily)
                return ClassPropertyVisibility.Protected;
            if (method.IsAssembly)
                return ClassPropertyVisibility.Internal;
            return ClassPropertyVisibility.Private;
        }
        #endregion
    }
}
