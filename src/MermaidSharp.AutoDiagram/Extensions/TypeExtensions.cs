using System;
using System.Linq;

namespace MermaidSharp.AutoDiagram.Extensions
{
    /// <summary>
    /// Provides extension methods for obtaining user-friendly names and representations of .NET types, including
    /// support for generic types.
    /// </summary>
    /// <remarks>These methods are intended to simplify the display or logging of type names, especially for
    /// generic types, by removing technical details such as backtick notation and presenting type arguments in a
    /// readable format.</remarks>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns a human-readable name for the specified type, including generic type arguments if present.
        /// </summary>
        /// <remarks>For generic types, the returned name includes the generic type name followed by angle
        /// brackets containing the friendly names of the generic arguments. For non-generic types, the type's name is
        /// returned as is.</remarks>
        /// <param name="type">The type for which to obtain a friendly display name.</param>
        /// <returns>A string representing the friendly name of the type, including generic arguments if applicable.</returns>
        public static string GetFriendlyTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericName = type.GetGenericTypeDefinition().Name;
                // Remove the `N suffix (e.g., "List`1" -> "List")
                var backtickIndex = genericName.IndexOf('`');
                if (backtickIndex >= 0)
                    genericName = genericName.Substring(0, backtickIndex);

                var args = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
                return $"{genericName}<{args}>";
            }

            return type.Name;
        }

        /// <summary>
        /// Returns a user-friendly string representation of a generic type's arguments.
        /// </summary>
        /// <remarks>This method is intended for use with generic types. For non-generic types, the result
        /// is an empty string.</remarks>
        /// <param name="type">The type for which to obtain a friendly argument list representation.</param>
        /// <returns>A comma-separated string of the generic argument type names if the type is generic; otherwise, an empty
        /// string.</returns>
        public static string GetFriendlyType(this Type type)
        {
            if (type.IsGenericType)
            {
                var args = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
                return args;
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns a user-friendly name for the specified type, omitting generic arity information if present.
        /// </summary>
        /// <remarks>This method is useful for displaying type names in logs, user interfaces, or
        /// diagnostics where generic arity is not desired.</remarks>
        /// <param name="type">The type for which to retrieve a friendly display name.</param>
        /// <returns>A string containing the type's name without generic arity suffix; for generic types, the base type name is
        /// returned.</returns>
        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericName = type.GetGenericTypeDefinition().Name;
                // Remove the `N suffix (e.g., "List`1" -> "List")
                var backtickIndex = genericName.IndexOf('`');
                if (backtickIndex >= 0)
                    genericName = genericName.Substring(0, backtickIndex);

                return genericName;
            }
            return type.Name;
        }
    }
}
