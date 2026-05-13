using System.Collections.Generic;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Defines operations for retrieving configuration data as text lines.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Returns the configuration lines for the current instance as a list of strings.
        /// </summary>
        /// <returns>A list of strings representing the configuration lines. The list is empty if there are no parameters.</returns>
        List<string> GetConfigLines();
    }
}
