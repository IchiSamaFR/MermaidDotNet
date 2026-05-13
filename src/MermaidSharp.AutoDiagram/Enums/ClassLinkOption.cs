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

		Association = 1 << 0,

		Interface = 1 << 1,

		Inherited = 1 << 2,

		All = Association | Interface | Inherited
#pragma warning restore CS1591
	}
}