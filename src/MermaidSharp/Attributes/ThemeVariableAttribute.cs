using System;

namespace MermaidSharp.Attributes
{
	/// <summary>
	/// Specifies Mermaid theme variable metadata for a property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ThemeVariableAttribute : Attribute
	{
		/// <summary>
		/// Gets the Mermaid variable name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ThemeVariableAttribute"/> class.
		/// </summary>
		/// <param name="name">Mermaid variable name.</param>
		public ThemeVariableAttribute(string name = null)
		{
			Name = name;
		}
	}
}