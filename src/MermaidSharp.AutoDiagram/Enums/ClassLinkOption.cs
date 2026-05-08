using System;

namespace MermaidSharp.AutoDiagram.Enums
{
	/// <summary>
	/// Specifies the types of class relationships to include in a class diagram.
	/// </summary>
	[Flags]
	public enum ClassLinkOption
	{
#pragma warning disable CS1591
		None = 0,
		/// <summary>
		/// Indicates a reference from one model to another model (property or field).
		/// </summary>
		Association = 1 << 0,
		/// <summary>
		/// Indicates a reference from a model to an interface it implements.
		/// </summary>
		Interface = 1 << 1,
		/// <summary>
		/// Indicates a reference from a model to an abstract class it inherits.
		/// </summary>
		Inherited = 1 << 2,
		/// <summary>
		/// Indicates all possible class links.
		/// </summary>
		All = Association | Interface | Inherited
#pragma warning restore CS1591
	}
}