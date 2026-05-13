using MermaidSharp.Attributes;
using System;

namespace MermaidSharp.Extensions
{
    internal static class EnumExtension
    {
        [Obsolete("Use PrimaryString instead.")]
        public static string StartString(this Enum value)
        {
            return PrimaryString(value);
        }

        [Obsolete("Use SecondaryString instead.")]
        public static string EndString(this Enum value)
        {
            return SecondaryString(value);
        }

        /// <summary>
        /// Retrieves the primary string value associated with the specified enumeration value, as defined by the
        /// MermaidEnumAttribute.
        /// </summary>
        /// <remarks>Use this method to obtain a custom string representation for an enum value when the
        /// MermaidEnumAttribute is applied. If the attribute is not present, the method returns an empty
        /// string.</remarks>
        /// <param name="value">The enumeration value for which to obtain the primary string.</param>
        /// <returns>The primary string defined by the MermaidEnumAttribute for the specified enumeration value, or an empty
        /// string if no attribute is found.</returns>
        public static string PrimaryString(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var attr = (MermaidEnumAttribute)Attribute.GetCustomAttribute(field, typeof(MermaidEnumAttribute));
            if (attr == null)
            {
                return string.Empty;
            }

            return attr.Primary;
        }

        /// <summary>
        /// Retrieves the secondary string value associated with the specified enumeration value, as defined by the
        /// MermaidEnumAttribute.
        /// </summary>
        /// <remarks>Use this method to access additional descriptive information for enum values when
        /// decorated with MermaidEnumAttribute. If the attribute is not present, the method returns an empty
        /// string.</remarks>
        /// <param name="value">The enumeration value for which to obtain the secondary string.</param>
        /// <returns>The secondary string defined by the MermaidEnumAttribute for the specified enumeration value; or an empty
        /// string if no attribute is present or the value is null.</returns>
        public static string SecondaryString(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var attr = (MermaidEnumAttribute)Attribute.GetCustomAttribute(field, typeof(MermaidEnumAttribute));
            if (attr == null)
            {
                return string.Empty;
            }

            return attr.Secondary;
        }
    }
}
