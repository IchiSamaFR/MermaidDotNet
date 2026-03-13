using System;
using System.Collections.Generic;
using System.Linq;

#if NET48
using System.Data.Entity.Core.Metadata.Edm;
#else
using Microsoft.EntityFrameworkCore.Metadata;
#endif

namespace MermaidDotNet.EntityFrameworkCore.Contexts
{
    internal class EntityTypeContext
    {
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsOwned { get; }
        public List<PropertyTypeContext> Properties { get; } = new List<PropertyTypeContext>();

#if NET48
        private readonly EntityType _entityType;

        public EntityTypeContext(EntityType entityType, Type clrType)
        {
            _entityType = entityType;
            ClrType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == entityType.FullName);

            if (ClrType == null)
            {
                ClrType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.Name == entityType.Name);
            }
            Name = entityType.Name;
            IsOwned = false;
            Properties = _entityType.Properties
                .Where(p => p.TypeUsage.EdmType is PrimitiveType)
                .Select(p => new PropertyTypeContext(p, ClrType))
                .OrderByDescending(p => p.IsPrimaryKey).ThenBy(p => p.Name)
                .ToList();
        }
#else
        private readonly IEntityType _entityType;

        public EntityTypeContext(IEntityType entityType)
        {
            _entityType = entityType;
            ClrType = _entityType.ClrType;
            Name = _entityType.ClrType.Name;
            IsOwned = _entityType.IsOwned();
            Properties = _entityType.GetProperties()
                .Select(p => new PropertyTypeContext(p))
                .OrderByDescending(p => p.IsPrimaryKey).ThenBy(p => p.Name)
                .ToList();
        }
#endif

        public IEnumerable<NavigationContext> GetNavigations()
        {
#if NET48
            return _entityType.NavigationProperties
                .Select(n => new NavigationContext(n));
#else
            return _entityType.GetNavigations()
                .Select(n => new NavigationContext(n));
#endif
        }

        public bool Equals(EntityTypeContext other)
        {
            if (other == null) return false;
            return ClrType == other.ClrType && Name == other.Name;
        }
    }

    internal class NavigationContext
    {
#if NET48
        private readonly NavigationProperty _navigation;

        public NavigationContext(NavigationProperty navigation)
        {
            _navigation = navigation;
        }

        public string Name => _navigation.Name;

        public EntityTypeContext TargetEntityType
        {
            get
            {
                var targetType = _navigation.ToEndMember.GetEntityType();
                var entitesNamespace = $"{targetType.NamespaceName}.Entities";
                var clrType = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t =>
                        t.Name == targetType.Name &&
                        t.Namespace == entitesNamespace);
                return new EntityTypeContext(targetType, clrType);
            }
        }
#else
        private readonly INavigation _navigation;

        public NavigationContext(INavigation navigation)
        {
            _navigation = navigation;
        }

        public string Name => _navigation.Name;
        public EntityTypeContext TargetEntityType => new EntityTypeContext(_navigation.TargetEntityType);
#endif
    }
}