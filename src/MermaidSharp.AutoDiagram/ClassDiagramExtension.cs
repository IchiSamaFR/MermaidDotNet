using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Extensions;
using MermaidSharp.AutoDiagram.Models.ClassDiagrams;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public static ClassDiagram ToMermaidClassDiagram(this Assembly assembly, ClassDiagramOptions options = null)
        {
            return new[] { assembly }.ToMermaidClassDiagram(options);
        }

        /// <summary>
        /// Generates a Mermaid class diagram from all public types in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to inspect. Cannot be null.</param>
        /// <param name="options">Options controlling what is included in the diagram. If null, default options are used.</param>
        /// <returns>A <see cref="ClassDiagram"/> representing the types and relationships found in the assemblies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="assemblies"/> is null.</exception>
        public static ClassDiagram ToMermaidClassDiagram(this IEnumerable<Assembly> assemblies, ClassDiagramOptions options = null)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            if (options == null)
                options = new ClassDiagramOptions();

            var diagram = new ClassDiagram();
            var classNamespaces = assemblies
                .Select(a => BuildClassAssemblyContext(a, options))
                .OrderBy(c => c.Name)
                .ToList();
            diagram.Namespaces
                .AddRange(classNamespaces.Select(c => MapToClassNamespace(c, options)));

            var nodesContext = classNamespaces
                .SelectMany(cn => cn.ClassDiagrams);
            diagram.Links.AddRange(BuildLinks(nodesContext, options));

            return diagram;
        }

        /// <summary>
        /// Generates a Mermaid class diagram from the specified type.
        /// </summary>
        /// <param name="type">The type to include in the diagram. Cannot be null.</param>
        /// <param name="options">Options controlling what is included in the diagram. If null, default options are used.</param>
        /// <returns>A <see cref="ClassDiagram"/> representing the provided type and its relationships.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static ClassDiagram ToMermaidClassDiagram(this Type type, ClassDiagramOptions options = null)
        {
            return new[] { type }.ToMermaidClassDiagram(options);
        }

        /// <summary>
        /// Generates a Mermaid class diagram from the specified collection of types.
        /// </summary>
        /// <param name="types">The collection of types to include in the diagram. Cannot be null.</param>
        /// <param name="options">Options controlling what is included in the diagram. If null, default options are used.</param>
        /// <returns>A <see cref="ClassDiagram"/> representing the provided types and their relationships.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="types"/> is null.</exception>
        public static ClassDiagram ToMermaidClassDiagram(this IEnumerable<Type> types, ClassDiagramOptions options = null)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            if (options == null)
                options = new ClassDiagramOptions();

            var diagram = new ClassDiagram();
            var contexts = types
                .Select(t => BuildClassNodeContext(t, options))
                .ToList();

            diagram.Nodes.AddRange(contexts.Select(c => MapToClassNode(c, options)));
            diagram.Links.AddRange(BuildLinks(contexts, options));

            return diagram;
        }


        #region Context Builders
        private static ClassAssemblyContext BuildClassAssemblyContext(Assembly assembly, ClassDiagramOptions options)
        {
            var assemblyContext = new ClassAssemblyContext(assembly);
            var types = assembly.GetTypes()
                .Where(cd => options.AssemblyClassFilter == null || options.AssemblyClassFilter(cd.DeclaringType ?? cd))
                .Where(cd => options.IncludeClassesVisibility.HasFlag(GetClassVisibility(cd.DeclaringType ?? cd)))
                .Where(cd => cd.DeclaringType == null)
                .Distinct()
                .ToList();
            assemblyContext.ClassDiagrams
                .AddRange(types
                    .Select(t => BuildClassNodeContext(t, options))
                    .OrderBy(t => t.Properties.Count + t.Methods.Count == 0)
                    .ThenBy(t => t.Type.Name));
            return assemblyContext;
        }

        private static ClassNodeContext BuildClassNodeContext(Type type, ClassDiagramOptions options)
        {
            var nodeContext = new ClassNodeContext(type.DeclaringType ?? type);

            nodeContext.Properties.AddRange(nodeContext.Type.Type.GetRuntimeProperties()
                .Select(BuildClassPropertyContext)
                .Where(p => options.PropertyOptions.IncludeVisibility.HasFlag(p.Visibility))
                .OrderBy(p => (int)p.Visibility)
                .ThenBy(p => p.Name));
            nodeContext.Methods.AddRange(nodeContext.Type.Type.GetRuntimeMethods()
                .Select(BuildClassMethodContext)
                .Where(m => options.MethodOptions.IncludeVisibility.HasFlag(m.Visibility))
                .OrderBy(m => (int)m.Visibility)
                .ThenBy(m => m.Name));
            return nodeContext;
        }

        private static ClassPropertyContext BuildClassPropertyContext(PropertyInfo property)
        {
            return new ClassPropertyContext(property, GetPropertyVisibility(property));
        }

        private static ClassMethodContext BuildClassMethodContext(MethodInfo method)
        {
            return new ClassMethodContext(method, GetMethodVisibility(method));
        }
        #endregion

        #region Diagram Models
        private static ClassNamespace MapToClassNamespace(ClassAssemblyContext assemblyContext, ClassDiagramOptions options)
        {
            var classNamespace = new ClassNamespace(assemblyContext.Name);
            var filteredClasses = assemblyContext.ClassDiagrams;
            classNamespace.Classes.AddRange(filteredClasses.Select(c => MapToClassNode(c, options)));
            return classNamespace;
        }

        private static ClassNode MapToClassNode(ClassNodeContext classNodeContext, ClassDiagramOptions options)
        {
            string name = classNodeContext.Type.FullName;
            var properties = classNodeContext.Properties
                .Select(p => MapToClassProperty(p, options)).ToList();
            var methods = classNodeContext.Methods
                .Select(m => MapToClassMethod(m, options)).ToList();

            return new ClassNode(name, string.Empty, string.Empty, properties, methods);
        }

        private static ClassProperty MapToClassProperty(ClassPropertyContext propertyContext, ClassDiagramOptions options)
        {
            return new ClassProperty(propertyContext.Name, propertyContext.Type.FullName, GetPropertyVisibility(propertyContext.Property));
        }

        private static ClassMethod MapToClassMethod(ClassMethodContext methodContext, ClassDiagramOptions options)
        {
            var parameters = new List<ClassMethodParam>();
            if (options.MethodOptions.IncludeParameters)
            {
                parameters.AddRange(methodContext.ParameterTypes.Select(p => new ClassMethodParam(p.FullName)));
            }

            var returnType = string.Empty;
            if (options.MethodOptions.IncludeReturnType)
            {
                if (options.MethodOptions.IncludeReturnVoid || methodContext.ReturnType.Type != typeof(void))
                {
                    returnType = methodContext.ReturnType.FullName;
                }
            }
            return new ClassMethod(methodContext.Name, returnType, methodContext.Visibility, parameters);
        }

        private static IEnumerable<ClassLink> BuildLinks(IEnumerable<ClassNodeContext> nodesContext, ClassDiagramOptions options)
        {
            var dictionary = nodesContext.ToDictionary(c => c.Type.Name, c => c);

            var links = new List<ClassLink>();
            foreach (var node in nodesContext)
            {
                links.AddRange(BuildLinksAssociates(node, dictionary, options));
                links.AddRange(BuildLinksHerited(node, dictionary, options));
                links.AddRange(BuildLinksInterfaces(node, dictionary, options));
            }
            return links
                .OrderBy(l => l.SourceNode)
                .ThenBy(l => l.DestinationNode);
        }

        private static IEnumerable<ClassLink> BuildLinksAssociates(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Association))
            {
                return Array.Empty<ClassLink>();
            }

            var links = new List<ClassLink>();
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Association.ToString() : string.Empty;
            foreach (var property in nodeContext.Properties)
            {
                if (!typeNames.ContainsKey(property.Type.Name))
                    continue;

                links.Add(new ClassLink(nodeContext.Type.Name, property.Type.Name, ClassLinkType.Association, linkLabel));
            }
            return links;
        }

        private static IEnumerable<ClassLink> BuildLinksHerited(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Inherited))
            {
                return Array.Empty<ClassLink>();
            }

            var links = new List<ClassLink>();
            var baseType = nodeContext.Type.Type.BaseType;
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Inherited.ToString() : string.Empty;
            if (baseType != null && baseType != typeof(object) && typeNames.ContainsKey(baseType.Name))
            {
                links.Add(new ClassLink(baseType.Name, nodeContext.Type.Name, ClassLinkType.Inheritance, linkLabel));
            }
            return links;
        }

        private static IEnumerable<ClassLink> BuildLinksInterfaces(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Interface))
            {
                return Array.Empty<ClassLink>();
            }

            var links = new List<ClassLink>();
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Interface.ToString() : string.Empty;
            foreach (var iface in nodeContext.Type.Type.GetInterfaces())
            {
                if (typeNames.ContainsKey(iface.Name))
                {
                    links.Add(new ClassLink(iface.Name, nodeContext.Type.Name, ClassLinkType.Realization, linkLabel));
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
