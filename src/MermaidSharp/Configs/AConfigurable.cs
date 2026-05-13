using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the abstract base type for Mermaid configuration objects in the configuration hierarchy.
    /// </summary>
    public abstract class AConfigurable
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> PropertiesCache
            = new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var lines = GetConfigLines();
            if (lines.Count == 0)
                return string.Empty;

            lines.Insert(0, $"---");
            lines.Add("---");

            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// Returns the configuration lines for the current instance as a list of strings.
        /// </summary>
        /// <returns>A list of strings representing the configuration lines. The list is empty if there are no parameters.</returns>
        public abstract List<string> GetConfigLines();

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current settings.
        /// Handles string, double, bool, and List&lt;string&gt; properties decorated with <see cref="ThemeVariableAttribute"/>.
        /// </summary>
        /// <returns>A list of strings representing the configuration parameters.</returns>
        protected List<string> GetThemeVariableParams()
        {
            var lst = new List<string>();
            var props = PropertiesCache.GetOrAdd(
                GetType(),
                t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                      .Where(p => p.GetCustomAttribute<ThemeVariableAttribute>() != null)
                      .ToArray()
            );

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<ThemeVariableAttribute>();
                var value = prop.GetValue(this);
                if (value == null)
                    continue;

                lst.AddRange(GetProperty(attr, value));
            }

            return lst;
        }

        private IEnumerable<string> GetProperty(ThemeVariableAttribute attr, object value)
        {
            var lst = new List<string>();

            if (value is IThemeVariables themeVars)
            {
                if (!string.IsNullOrWhiteSpace(attr.Name))
                    throw new InvalidOperationException("Theme variable name must be empty for properties implementing IThemeVariables.");

                lst.AddRange(themeVars.GetConfigLines());
                return lst;
            }

            if (string.IsNullOrWhiteSpace(attr.Name))
                throw new InvalidOperationException("Theme variable name is required for scalar theme variable properties.");

            // Handle IEnumerable<string>
            if (value is IEnumerable<string> strList)
            {
                var items = strList.ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (string.IsNullOrEmpty(item))
                        continue;

                    lst.Add($"{attr.Name.Replace("{index}", (i + 1).ToString())}: \"{item}\"");
                }
            }
            // Handle string
            else if (value is string strVal && !string.IsNullOrEmpty(strVal))
            {
                lst.Add($"{attr.Name}: \"{strVal}\"");
            }
            // Handle double
            else if (value is double dblVal)
            {
                lst.Add($"{attr.Name}: {dblVal.ToString("G", CultureInfo.InvariantCulture)}");
            }
            // Handle bool
            else if (value is bool boolVal)
            {
                lst.Add($"{attr.Name}: {(boolVal ? "true" : "false")}");
            }

            return lst;
        }
    }
}
