using MermaidSharp.Configs;
using MermaidSharp.Models;

namespace MermaidSharp.Diagrams
{
	/// <summary>
	/// Represents an entity-relationship diagram that models entities and their relationships using Mermaid syntax.
	/// </summary>
	/// <remarks>Use this class to define and generate entity-relationship diagrams for visualization or
	/// documentation purposes. The diagram is constructed from a collection of entity nodes and relationship links, and
	/// supports Mermaid's ER diagram features.</remarks>
	public class EntityRelationshipDiagram : ADiagram<EntityRelationshipConfig, EntityRelationNode, EntityRelationLink>
	{
		/// <summary>
		/// Gets the Mermaid name associated with the current instance.
		/// </summary>
		protected override string Name => "erDiagram";

		/// <summary>
		/// Initializes a new instance of the EntityRelationshipDiagram class with the specified title.
		/// </summary>
		/// <param name="title">The title of the entity-relationship diagram. If not specified, the diagram will have an empty title.</param>
		/// <param name="config">The configuration settings for the entity-relationship diagram. If not specified, default settings will be used.</param>
		public EntityRelationshipDiagram(string title = "", EntityRelationshipConfig config = default) : base(title, config)
		{
		}
	}
}
