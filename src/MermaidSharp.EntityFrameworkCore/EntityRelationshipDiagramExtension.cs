using MermaidSharp.Diagrams;
using MermaidSharp.EntityFrameworkCore.Contexts;
using MermaidSharp.EntityFrameworkCore.Models;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using System.Collections.Generic;
using System.Linq;


#if NET48
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
#else
using Microsoft.EntityFrameworkCore;
#endif

namespace MermaidSharp.EntityFrameworkCore
{
    /// <summary>
    /// Provides extension methods for generating Mermaid entity-relationship diagrams from Entity Framework database
    /// contexts.
    /// </summary>
    /// <remarks>These extension methods enable visualization of the entities and relationships defined in a
    /// DbContext using Mermaid syntax. The generated diagrams reflect the current model configuration of the context
    /// and can be customized with various options.</remarks>
    public static class EntityRelationshipDiagramExtension
    {
        /// <summary>
        /// Generates a Mermaid entity-relationship diagram representing the structure of the specified Entity Framework
        /// database context.
        /// </summary>
        /// <remarks>This extension method provides a convenient way to visualize the entities and
        /// relationships defined in a DbContext using Mermaid syntax. The diagram reflects the current model
        /// configuration of the context.</remarks>
        /// <param name="dbContext">The Entity Framework database context to analyze. Cannot be null.</param>
        /// <returns>An EntityRelationshipDiagram object containing the Mermaid syntax for the entity-relationship diagram of the
        /// database context.</returns>
        public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext)
        {
            return dbContext.ToMermaidEntityDiagram(new EntityRelationshipDiagramOptions());
        }

        /// <summary>
        /// Generates a Mermaid entity-relationship diagram representing the structure of the specified Entity Framework
        /// database context.
        /// </summary>
        /// <remarks>This extension method provides a convenient way to visualize the entities and
        /// relationships defined in a DbContext using Mermaid syntax. The diagram reflects the current model
        /// configuration of the context.</remarks>
        /// <param name="dbContext">The Entity Framework database context to analyze. Cannot be null.</param>
        /// <param name="options">Options to customize the generation of the entity-relationship diagram, such as whether to include column details, relationships, and other metadata.</param>
        /// <returns>An EntityRelationshipDiagram object containing the Mermaid syntax for the entity-relationship diagram of the
        /// database context.</returns>
        public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext, EntityRelationshipDiagramOptions options)
        {
            var entityTypes = GetEntityTypes(dbContext);
            var schema = BuildSchema(entityTypes);
            return BuildEntityRelationshipDiagram(schema, options);
        }

#if NET48
        private static List<EntityTypeContext> GetEntityTypes(DbContext dbContext)
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);

            return container.BaseEntitySets
                .OfType<EntitySet>()
                .Select(set =>
                {
                    var entityType = set.ElementType;
                    var clrType = objectContext.GetType().Assembly.GetTypes()
                        .FirstOrDefault(t => t.Name == entityType.Name) ?? typeof(object);
                    return new EntityTypeContext(entityType, clrType);
                })
                .OrderBy(e => e.Name)
                .ToList();
        }
#else
        private static List<EntityTypeContext> GetEntityTypes(DbContext dbContext)
        {
            return dbContext.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .Select(et => new EntityTypeContext(et))
                .OrderBy(e => e.Name)
                .ToList();
        }
#endif

        private static RelationLinkType GetRelationType(PropertyTypeContext foreignProperty)
        {
            if (foreignProperty.IsUnique)
            {
                return foreignProperty.IsRequired ? RelationLinkType.ExactlyOne : RelationLinkType.ZeroOrOne;
            }
            else
            {
                return foreignProperty.IsRequired ? RelationLinkType.OneOrMore : RelationLinkType.ZeroOrMore;
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
                        TargetType = RelationLinkType.ExactlyOne,
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
                var referenceType = property.IsPrimaryKey ? RelationConstraintType.PrimaryKey : RelationConstraintType.None;
                referenceType |= property.IsForeignKey ? RelationConstraintType.ForeignKey : RelationConstraintType.None;
                referenceType |= property.IsUnique ? RelationConstraintType.UniqueKey : RelationConstraintType.None;

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
                    var referenceType = property.IsUnique ? RelationConstraintType.UniqueKey : RelationConstraintType.None;
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

            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);
            return diagram;
        }

        private static List<EntityRelationNode> BuildEntityRelationNodes(DiagramSchema schema, EntityRelationshipDiagramOptions options)
        {
            var nodes = new List<EntityRelationNode>();
            foreach (var table in schema.Tables)
            {
                nodes.Add(new EntityRelationNode(
                    table.Name,
                    columns: BuildEntityRelationColumns(table, options)
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

            var filteredColumns = options.FilterColumnByKeyTypes != RelationConstraintType.None
                ? table.Columns.Where(c => c.ColumnKeyType != RelationConstraintType.None && options.FilterColumnByKeyTypes.HasFlag(c.ColumnKeyType))
                : table.Columns;

            foreach (var column in filteredColumns)
            {
                var erColumn = new EntityRelationColumn(
                    column.Name,
                    column.Type.Name,
                    options.IncludeColumnKeyTypes ? column.ColumnKeyType : RelationConstraintType.None,
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
                    link.SourceType,
                    link.TargetType,
                    relationLabel
                ));
            }
            return links;
        }
    }
}
