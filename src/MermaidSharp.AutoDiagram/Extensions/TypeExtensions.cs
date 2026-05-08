using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Extensions
{
    public static class TypeExtensions
    {
        public static string GetFriendlyTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericName = type.GetGenericTypeDefinition().Name;
                // Remove the `N suffix (e.g., "List`1" -> "List")
                var backtickIndex = genericName.IndexOf('`');
                if (backtickIndex >= 0)
                    genericName = genericName[..backtickIndex];

                var args = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
                return $"{genericName}<{args}>";
            }

            return type.Name;
        }
        public static string GetFriendlyType(this Type type)
        {
            if (type.IsGenericType)
            {
                var args = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
                return args;
            }

            return string.Empty;
        }
        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericName = type.GetGenericTypeDefinition().Name;
                // Remove the `N suffix (e.g., "List`1" -> "List")
                var backtickIndex = genericName.IndexOf('`');
                if (backtickIndex >= 0)
                    genericName = genericName[..backtickIndex];

                return genericName;
            }
            return type.Name;
        }
    }
}
