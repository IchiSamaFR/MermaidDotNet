using MermaidDotNet.Diagrams;
using MermaidDotNet.EntityFrameworkCore.Contexts;
using MermaidDotNet.EntityFrameworkCore.Models;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;


#if NET48
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
#else
using Microsoft.EntityFrameworkCore;
#endif

namespace MermaidDotNet.EntityFrameworkCore
{
	public static class EntityRelationshipDiagramExtension
	{
		public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext)
		{
			return dbContext.ToMermaidEntityDiagram(new EntityRelationshipDiagramOptions());
		}

		public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext, EntityRelationshipDiagramOptions options)
		{
			var entityTypes = GetEntityTypes(dbContext);
			var schema = BuildSchema(entityTypes);
			return BuildEntityRelationshipDiagram(schema, options);
		}

		private static List<EntityTypeContext> GetEntityTypes(DbContext dbContext)
		{
			return dbContext.Model.GetEntityTypes()
				.Where(e => !e.IsOwned())
				.Select(et => new EntityTypeContext(et))
				.OrderBy(e => e.Name)
				.ToList();
		}

		private static RelationType GetRelationType(PropertyTypeContext foreignProperty)
		{
			if (foreignProperty.IsUnique)
			{
				return foreignProperty.IsRequired ? RelationType.ExactlyOne : RelationType.ZeroOrOne;
			}
			else
			{
				return foreignProperty.IsRequired ? RelationType.OneOrMore : RelationType.ZeroOrMore;
			}
		}

		private static DiagramSchema BuildSchema(List<EntityTypeContext> entityTypes)
		{
			var schema = new DiagramSchema();
			schema.Tables = entityTypes
				.Select(BuildTable)
				.ToList();

			var entityTables = schema.Tables.ToDictionary(t => t.EntityType.ClrType, t => t);
			foreach (var table in schema.Tables)
			{
				var foreignColumns = table.Columns
					.Where(c => c.Property.IsForeignKey)
					.ToList();

				foreach (var column in foreignColumns)
				{
					var foreignKey = column.Property;
					if (foreignKey == null)
						continue;

					if (!entityTables.TryGetValue(foreignKey.ForeignParentType, out var targetTable))
						continue;

					schema.Links.Add(new DiagramLink
					{
						Source = table,
						Target = targetTable,
						Label = foreignKey.Name,
						SourceType = GetRelationType(foreignKey),
						TargetType = RelationType.ExactlyOne,
						DeleteBehavior = (Enums.DeleteBehavior)foreignKey.DeleteBehavior
					});
				}
			}

			return schema;
		}

		private static DiagramTable BuildTable(EntityTypeContext entityType)
		{
			var table = new DiagramTable
			{
				Name = entityType.Name,
				EntityType = entityType
			};

			foreach (var property in entityType.Properties)
			{
				var referenceType = property.IsPrimaryKey ? ColumnKeyType.PrimaryKey : ColumnKeyType.None;
				referenceType |= property.IsForeignKey ? ColumnKeyType.ForeignKey : ColumnKeyType.None;
				referenceType |= property.IsUnique ? ColumnKeyType.UniqueKey : ColumnKeyType.None;

				table.Columns.Add(new DiagramColumn
				{
					Property = property,
					Name = property.Name,
					Type = property.ClrType,
					IsNullable = property.IsNullable,
					ColumnKeyType = referenceType
				});
			}

			// Include owned entity properties inline (EF Core only)
			foreach (var navigation in entityType.GetNavigations()
				.Where(n => n.TargetEntityType.IsOwned))
			{
				var owned = navigation.TargetEntityType;
				foreach (var property in owned.Properties
					.Where(p => !p.IsForeignKey && !p.IsPrimaryKey))
				{
					var referenceType = property.IsUnique ? ColumnKeyType.UniqueKey : ColumnKeyType.None;
					table.Columns.Add(new DiagramColumn
					{
						Property = property,
						Name = string.Format("{0}_{1}", navigation.Name, property.Name),
						Type = property.ClrType,
						IsNullable = property.IsNullable,
						ColumnKeyType = referenceType
					});
				}
			}

			return table;
		}

		private static EntityRelationshipDiagram BuildEntityRelationshipDiagram(DiagramSchema schema, EntityRelationshipDiagramOptions options)
		{
			var nodes = BuildEntityRelationNodes(schema, options);
			var links = BuildEntityRelationLinks(schema, options);
			return new EntityRelationshipDiagram(nodes, links);
		}

		private static List<EntityRelationNode> BuildEntityRelationNodes(DiagramSchema schema, EntityRelationshipDiagramOptions options)
		{
			var nodes = new List<EntityRelationNode>();
			foreach (var table in schema.Tables)
			{
				nodes.Add(new EntityRelationNode(
					table.Name,
					BuildEntityRelationColumns(table, options)
				));
			}
			return nodes;
		}

		private static List<EntityRelationColumn> BuildEntityRelationColumns(DiagramTable table, EntityRelationshipDiagramOptions options)
		{
			var columns = new List<EntityRelationColumn>();

			if (!options.IncludeColumns)
			{
				return columns;
			}

			var filteredColumns = options.FilterColumnByKeyTypes != ColumnKeyType.None
				? table.Columns.Where(c => c.ColumnKeyType != ColumnKeyType.None && options.FilterColumnByKeyTypes.HasFlag(c.ColumnKeyType))
				: table.Columns;

			foreach (var column in filteredColumns)
			{
				var erColumn = new EntityRelationColumn(
					column.Name,
					column.Type.Name,
					options.IncludeColumnKeyTypes ? column.ColumnKeyType : ColumnKeyType.None,
					options.IncludeColumnComments ? column.Property.Description : string.Empty
				);
				columns.Add(erColumn);
			}

			return columns;
		}

		private static List<EntityRelationLink> BuildEntityRelationLinks(DiagramSchema schema, EntityRelationshipDiagramOptions options)
		{
			var links = new List<EntityRelationLink>();

			if (!options.IncludeLinks)
			{
				return links;
			}

			foreach (var link in schema.Links)
			{
				var relationLabel = string.Empty;

				if (options.IncludeLinkLabels)
				{
					relationLabel = link.Label;
				}

				if (options.IncludeLinkDeleteBehaviors)
				{
					relationLabel = string.Join(" ", relationLabel, string.Format("({0})", link.DeleteBehavior.ToString()));
				}

				links.Add(new EntityRelationLink(
					link.Source.Name,
					link.Target.Name,
					relationLabel,
					link.SourceType,
					link.TargetType
				));
			}
			return links;
		}
	}
}