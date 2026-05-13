#if NET8_0_OR_GREATER
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.Reflection;

namespace MermaidSharp.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// Provides extension methods for retrieving property metadata and descriptions.
    /// </summary>
    /// <remarks>This static class contains helper methods that extend the functionality of property metadata
    /// interfaces, enabling convenient access to property descriptions and related information in a type-safe
    /// manner.</remarks>
    public static class PropertyExtensions
    {
        /// <summary>
        /// Retrieves the description specified by the <see cref="DescriptionAttribute"/> for the given property, if
        /// present.
        /// </summary>
        /// <remarks>This method inspects the CLR property corresponding to the provided metadata and
        /// returns the value of its <see cref="DescriptionAttribute"/>. If the attribute is not present, the method
        /// returns an empty string.</remarks>
        /// <param name="property">The property metadata from which to obtain the description.</param>
        /// <returns>The description text if a <see cref="DescriptionAttribute"/> is applied to the property; otherwise, an empty
        /// string.</returns>
        public static string GetDescription(this IProperty property)
        {
            var clrProperty = property.DeclaringType.ClrType
                .GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);

            if (clrProperty == null)
                return string.Empty;

            var descriptionAttribute = clrProperty
                .GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute?.Description ?? string.Empty;
        }
    }
}
#endif
