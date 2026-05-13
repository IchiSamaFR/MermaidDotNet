using MermaidSharp.AutoDiagram.Constants;
using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Models.ClassDiagrams;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Diagrams
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

            var allContexts = classNamespaces.SelectMany(c => c.ClassDiagrams).ToList();
            var nameMap = BuildDisambiguatedNames(allContexts);

            diagram.Namespaces
                .AddRange(classNamespaces.Select(c => MapToClassNamespace(c, options, nameMap)));

            var nodesContext = classNamespaces.SelectMany(cn => cn.ClassDiagrams);
            diagram.Links.AddRange(BuildLinks(nodesContext, options, nameMap));

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

            var nameMap = BuildDisambiguatedNames(contexts);

            diagram.Nodes.AddRange(contexts.Select(c => MapToClassNode(c, options, nameMap)));
            diagram.Links.AddRange(BuildLinks(contexts, options, nameMap));

            return diagram;
        }


        #region Context Builders
        private static ClassAssemblyContext BuildClassAssemblyContext(Assembly assembly, ClassDiagramOptions options)
        {
            var assemblyContext = new ClassAssemblyContext(assembly);
            var types = assembly.GetTypes()
                .Where(cd => options.AssemblyClassFilter == null || options.AssemblyClassFilter(cd.DeclaringType ?? cd))
                .Where(cd => options.IncludeClassesVisibility.Contains(GetClassVisibility(cd.DeclaringType ?? cd)))
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
                .Where(p => options.PropertyOptions.IncludeVisibility.Contains(p.Visibility))
                .OrderBy(p => Array.IndexOf(ClassDiagramConstants.PropertiesSortOrder, p.Visibility))
                .ThenBy(p => p.Name));
            nodeContext.Methods.AddRange(nodeContext.Type.Type.GetRuntimeMethods()
                .Select(BuildClassMethodContext)
                .Where(m => options.MethodOptions.IncludeVisibility.Contains(m.Visibility))
                .OrderBy(m => Array.IndexOf(ClassDiagramConstants.PropertiesSortOrder, m.Visibility))
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
        private static ClassNamespace MapToClassNamespace(ClassAssemblyContext assemblyContext, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            var classNamespace = new ClassNamespace(assemblyContext.Name);
            classNamespace.Classes.AddRange(assemblyContext.ClassDiagrams.Select(c => MapToClassNode(c, options, nameMap)));
            return classNamespace;
        }

        private static ClassNode MapToClassNode(ClassNodeContext classNodeContext, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            string name = ResolveName(classNodeContext.Type.Type, classNodeContext.Type, nameMap);
            var properties = classNodeContext.Properties.Select(MapToClassProperty).ToList();
            var methods = classNodeContext.Methods.Select(m => MapToClassMethod(m, options)).ToList();
            return new ClassNode(name, string.Empty, string.Empty, properties, methods);
        }

        private static ClassProperty MapToClassProperty(ClassPropertyContext propertyContext)
        {
            return new ClassProperty(propertyContext.Name, propertyContext.Type.FullName, propertyContext.Visibility);
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

        private static IEnumerable<ClassLink> BuildLinks(IEnumerable<ClassNodeContext> nodesContext, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            var dictionary = nodesContext.ToDictionary(c => GetTypeLookupKey(c.Type.Type), c => c);
            var links = new List<ClassLink>();
            foreach (var node in nodesContext)
            {
                links.AddRange(BuildLinksAssociates(node, dictionary, options, nameMap));
                links.AddRange(BuildLinksHerited(node, dictionary, options, nameMap));
                links.AddRange(BuildLinksInterfaces(node, dictionary, options, nameMap));
            }
            return links.OrderBy(l => l.SourceNode).ThenBy(l => l.DestinationNode);
        }

        private static IEnumerable<ClassLink> BuildLinksAssociates(ClassNodeContext nodeContext, Dictionary<Type, ClassNodeContext> typeNames, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Association))
                return Array.Empty<ClassLink>();

            var links = new List<ClassLink>();
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Association.ToString() : string.Empty;
            foreach (var property in nodeContext.Properties)
            {
                if (!typeNames.TryGetValue(GetTypeLookupKey(property.Type.Type), out var propertyNodeContext))
                    continue;

                var sourceName = ResolveName(nodeContext.Type.Type, nodeContext.Type, nameMap);
                var destName = ResolveName(propertyNodeContext.Type.Type, propertyNodeContext.Type, nameMap);
                links.Add(new ClassLink(sourceName, destName, ClassLinkType.Association, linkLabel));
            }
            return links;
        }

        private static IEnumerable<ClassLink> BuildLinksHerited(ClassNodeContext nodeContext, Dictionary<Type, ClassNodeContext> typeNames, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Inherited))
                return Array.Empty<ClassLink>();

            var links = new List<ClassLink>();
            var baseType = nodeContext.Type.Type.BaseType;
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Inherited.ToString() : string.Empty;
            if (baseType != null
                && baseType != typeof(object)
                && typeNames.TryGetValue(GetTypeLookupKey(baseType), out var baseNodeContext))
            {
                var sourceName = ResolveName(baseNodeContext.Type.Type, baseNodeContext.Type, nameMap);
                var destName = ResolveName(nodeContext.Type.Type, nodeContext.Type, nameMap);
                links.Add(new ClassLink(sourceName, destName, ClassLinkType.Inheritance, linkLabel));
            }
            return links;
        }

        private static IEnumerable<ClassLink> BuildLinksInterfaces(ClassNodeContext nodeContext, Dictionary<Type, ClassNodeContext> typeNames, ClassDiagramOptions options, Dictionary<Type, string> nameMap)
        {
            if (!options.LinkOptions.IncludeLinks.HasFlag(ClassLinkOption.Interface))
                return Array.Empty<ClassLink>();

            var links = new List<ClassLink>();
            string linkLabel = options.LinkOptions.IncludeLinksLabels ? ClassLinkOption.Interface.ToString() : string.Empty;
            foreach (var iface in nodeContext.Type.Type.GetInterfaces())
            {
                if (typeNames.TryGetValue(GetTypeLookupKey(iface), out var interfaceNodeContext))
                {
                    var sourceName = ResolveName(interfaceNodeContext.Type.Type, interfaceNodeContext.Type, nameMap);
                    var destName = ResolveName(nodeContext.Type.Type, nodeContext.Type, nameMap);
                    links.Add(new ClassLink(sourceName, destName, ClassLinkType.Realization, linkLabel));
                }
            }
            return links;
        }

        private static Type GetTypeLookupKey(Type type)
        {
            return type.IsGenericType
                ? type.GetGenericTypeDefinition()
                : type;
        }
        #endregion

        #region Disambiguation
        private static Dictionary<Type, string> BuildDisambiguatedNames(IEnumerable<ClassNodeContext> contexts)
        {
            var result = new Dictionary<Type, string>();
            var groups = contexts.GroupBy(c => c.Type.Name).ToList();

            foreach (var group in groups)
            {
                var items = group.ToList();
                if (items.Count == 1)
                {
                    result[items[0].Type.Type] = items[0].Type.FullName;
                    continue;
                }

                // Find the minimal number of namespace segments to disambiguate all types in the group
                int segments = 1;
                while (segments <= 20)
                {
                    var names = items.Select(i => BuildPrefixedName(i.Type, segments)).ToList();
                    if (names.Distinct().Count() == items.Count)
                        break;
                    segments++;
                }

                foreach (var item in items)
                    result[item.Type.Type] = BuildPrefixedName(item.Type, segments);
            }

            return result;
        }

        private static string BuildPrefixedName(ClassTypeContext typeContext, int namespaceSegments)
        {
            if (typeContext.Type.Namespace == null || namespaceSegments == 0)
                return typeContext.FullName;

            var parts = typeContext.Type.Namespace.Split('.');
            var prefix = string.Join(".", parts.Skip(Math.Max(0, parts.Length - namespaceSegments)));
            return prefix + "." + typeContext.FullName;
        }

        private static string ResolveName(Type type, ClassTypeContext typeContext, Dictionary<Type, string> nameMap)
        {
            var lookupKey = GetTypeLookupKey(type);
            return nameMap.TryGetValue(lookupKey, out var name) ? name : typeContext.FullName;
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
