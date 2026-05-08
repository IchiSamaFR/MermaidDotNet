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
            var classNamespaces = assemblies.Select(a => BuildClassAssemblyContext(a, options)).ToList();
            diagram.Namespaces.AddRange(classNamespaces.Select(MapToClassNamespace));

            var dictionary = classNamespaces.SelectMany(cn => cn.ClassDiagrams).ToDictionary(c => c.Type.Name, c => c);
            foreach (var nodeContext in dictionary.Values)
            {
                diagram.Links.AddRange(BuildLinks(nodeContext, dictionary, options));
            }
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

			links.AddRange(BuildLinksAssociates(nodeContext, typeNames, options));
			links.AddRange(BuildLinksHerited(nodeContext, typeNames, options));
			links.AddRange(BuildLinksInterfaces(nodeContext, typeNames, options));

            return links;
        }

		private static IEnumerable<ClassLink> BuildLinksAssociates(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
		{
			if (!options.IncludeLinks.HasFlag(ClassLinkOption.Association))
			{
                return Array.Empty<ClassLink>();
			}

			var links = new List<ClassLink>();
			var typeProperties = nodeContext.Properties;
			string linkLabel = options.IncludeLinksLabels ? ClassLinkOption.Association.ToString() : string.Empty;
			foreach (var property in typeProperties)
			{
				foreach (var type in property.Type.Types)
				{
					if (!typeNames.ContainsKey(type.Name))
						continue;

					links.Add(new ClassLink(property.Name, type.Name, ClassLinkType.Association, linkLabel));
				}
			}
            return links;
		}

		private static IEnumerable<ClassLink> BuildLinksHerited(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
		{
			if (!options.IncludeLinks.HasFlag(ClassLinkOption.Inherited))
			{
				return Array.Empty<ClassLink>();
			}

			var links = new List<ClassLink>();
			var baseType = nodeContext.Type.Type.BaseType;
			string linkLabel = options.IncludeLinksLabels ? ClassLinkOption.Inherited.ToString() : string.Empty;
			if (baseType != null && baseType != typeof(object) && typeNames.ContainsKey(baseType.Name))
			{
				links.Add(new ClassLink(baseType.Name, nodeContext.Type.Name, ClassLinkType.Inheritance, linkLabel));
			}
            return links;
		}

		private static IEnumerable<ClassLink> BuildLinksInterfaces(ClassNodeContext nodeContext, Dictionary<string, ClassNodeContext> typeNames, ClassDiagramOptions options)
		{
			if (!options.IncludeLinks.HasFlag(ClassLinkOption.Interface))
			{
				return Array.Empty<ClassLink>();
			}

			var links = new List<ClassLink>();
			string linkLabel = options.IncludeLinksLabels ? ClassLinkOption.Interface.ToString() : string.Empty;
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
