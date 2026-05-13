using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Provides a base class for configuration objects that generate Mermaid configuration blocks with optional theming support.
    /// </summary>
    /// <remarks>
    /// This abstract class is intended to be inherited by specific configuration types that require Mermaid-compatible configuration output.
    /// It manages the formatting of configuration parameters and supports theming through the Theme property.
    /// Derived classes extend configuration output by overriding <see cref="SectionName"/> and decorating properties
    /// with <see cref="MermaidSharp.Attributes.ConfigVariableAttribute"/> for automatic serialization.
    /// </remarks>
    public abstract class AConfig<TThemeVariables> : IConfig
        where TThemeVariables : IThemeVariables, new()
    {
        private const string Name = "config";

        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
        protected virtual string SectionName { get; }

        private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> ConfigPropertiesCache
            = new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Gets the theme variables used to customize the appearance of the component.
        /// </summary>
        public TThemeVariables ThemeVariables { get; }

        /// <summary>
        /// Gets or sets the theme configuration for the Mermaid diagram rendering.
        /// </summary>
        public ConfigTheme Theme { get; set; }

        /// <summary>
        /// Initializes a new instance of the AConfig class with the specified theme.
        /// </summary>
        /// <param name="theme">The theme to apply to the configuration.</param>
        /// <param name="themeVariables">The theme variables to apply to the configuration.</param>
        public AConfig(ConfigTheme theme = ConfigTheme.None, TThemeVariables themeVariables = default)
        {
            Theme = theme;

            if (themeVariables == null)
                themeVariables = new TThemeVariables();
            ThemeVariables = themeVariables;
        }

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
        public List<string> GetConfigLines()
        {
            var paramsList = GetParams();
            if (paramsList.Count == 0)
                return new List<string>();

            paramsList.Insert(0, $"{Name}:");

            return paramsList;
        }

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current theme settings and config variables.
        /// </summary>
        /// <remarks>
        /// Override this method in a derived class to include additional configuration parameters as needed.
        /// </remarks>
        /// <returns>
        /// A list of strings representing the configuration parameters. The list is empty if no parameters are set.
        /// </returns>
        protected List<string> GetParams()
        {
            var lst = new List<string>();

            // Add theme and theme variables
            if (Theme != ConfigTheme.None)
                lst.Add($"theme: {Theme.PrimaryString()}");

            // Add config variables decorated with [ConfigVariable]
            lst.AddRange(GetSectionConfigVariableParams());

            lst.AddRange(ThemeVariables.GetConfigLines());

            return lst.Indent();
        }

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on properties decorated with <see cref="ConfigVariableAttribute"/>.
        /// </summary>
        /// <returns>A list of strings representing the configuration parameters.</returns>
        protected List<string> GetSectionConfigVariableParams()
        {
            var lst = new List<string>();
            var props = ConfigPropertiesCache.GetOrAdd(
                GetType(),
                t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                      .Where(p => p.GetCustomAttribute<ConfigVariableAttribute>() != null)
                      .ToArray()
            );

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<ConfigVariableAttribute>();
                var value = prop.GetValue(this);
                if (value == null)
                    continue;

                lst.AddRange(GetConfigProperty(attr, value));
            }

            if (lst.Count == 0)
                return lst;

            if (string.IsNullOrEmpty(SectionName))
                throw new InvalidOperationException("SectionName must be defined in the derived class when using ConfigVariable attributes.");

            lst = lst.Indent();
            lst.Insert(0, $"{SectionName}:");
            return lst;
        }

        private IEnumerable<string> GetConfigProperty(ConfigVariableAttribute attr, object value)
        {
            var lst = new List<string>();

            if (value is IEnumerable enumerableVal && !(value is string))
            {
                var enumerableCastedVal = enumerableVal.Cast<object>().Select(o => o?.ToString()).ToList();
                foreach (var item in enumerableCastedVal)
                {
                    lst.AddRange(GetConfigProperty(attr, item));
                }
            }
            else if (value is double dblVal)
            {
                lst.Add($"{attr.Name}: {dblVal.ToString("G", CultureInfo.InvariantCulture)}");
            }
            else if (value is float floatVal)
            {
                lst.Add($"{attr.Name}: {floatVal.ToString("G", CultureInfo.InvariantCulture)}");
            }
            else if (value is decimal decVal)
            {
                lst.Add($"{attr.Name}: {decVal.ToString("G", CultureInfo.InvariantCulture)}");
            }
            else if (value is bool boolVal)
            {
                lst.Add($"{attr.Name}: {boolVal.ToString().ToLowerInvariant()}");
            }
            else if (value is string strVal && !string.IsNullOrEmpty(strVal))
            {
                lst.Add($"{attr.Name}: \"{strVal}\"");
            }
            else if (value is Enum enumVal)
            {
                lst.Add($"{attr.Name}: {enumVal.PrimaryString()}");
            }
            else
            {
                lst.Add($"{attr.Name}: {value.ToString()}");
            }

            return lst;
        }
    }
}