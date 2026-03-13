using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using MermaidDotNet.EntityFrameworkCore.Enums;




#if NET48
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
#else
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MermaidDotNet.EntityFrameworkCore.Extensions;
#endif

namespace MermaidDotNet.EntityFrameworkCore.Contexts
{
    internal class PropertyTypeContext
    {
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsNullable { get; }
        public bool IsPrimaryKey { get; }
        public bool IsForeignKey { get; }
        public bool IsRequired { get; }
        public bool IsUnique { get; }
        public string Description { get; }

        public Type ForeignParentType { get; set; }
        public string ForeignKeyName { get; set; }
        public Enums.DeleteBehavior DeleteBehavior { get; set; }

#if NET48
        public PropertyTypeContext(EdmProperty property, Type declaringType, bool resolveRelations = true)
        {
            Name = property.Name;

            var primitiveType = property.TypeUsage.EdmType as PrimitiveType;
            ClrType = primitiveType?.ClrEquivalentType ?? typeof(string);

            IsNullable = property.Nullable;

            var declaringEntityType = property.DeclaringType as EntityType;

            IsPrimaryKey = declaringEntityType?.KeyProperties
                .Any(p => p.Name == property.Name) ?? false;

            IsForeignKey = declaringEntityType?.NavigationProperties
                .Any(n => n.GetDependentProperties().Any(p => p.Name == property.Name)) ?? false;

            var propInfo = declaringType?.GetProperty(property.Name);
            if (propInfo != null)
            {
                var indexAttrs = propInfo.GetCustomAttributes(typeof(IndexAttribute), true);
                IsUnique = indexAttrs.Any(attr => ((IndexAttribute)attr).IsUnique);

                var descriptionAttribute = propInfo.GetCustomAttribute<DescriptionAttribute>();
                Description = descriptionAttribute?.Description ?? string.Empty;
            }

            var navigation = declaringEntityType?.NavigationProperties
                .FirstOrDefault(n => n.GetDependentProperties().Any(p => p.Name == property.Name));
            if (navigation != null)
            {
                IsRequired = !property.Nullable;
                var principalEntityType = navigation.TypeUsage.EdmType as EntityType;
                // Recherche dans tous les assemblies chargés
                ForeignParentType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName == principalEntityType.FullName);

                // Fallback si FullName ne correspond pas
                if (ForeignParentType == null)
                {
                    ForeignParentType = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .FirstOrDefault(t => t.Name == principalEntityType.Name);
                }

                ForeignKeyName = property.Name;
                DeleteBehavior = Enums.DeleteBehavior.Cascade; // EF6 ne gère pas explicitement le delete behavior
            }

        }
#else
        public PropertyTypeContext(IProperty property)
        {
            Name = property.Name;
            ClrType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            IsNullable = property.IsNullable;
            IsPrimaryKey = property.IsPrimaryKey();
            IsForeignKey = property.IsForeignKey();
            IsUnique = property.IsUniqueIndex();
            Description = property.GetDescription() ?? string.Empty;

            var foreignKey = property.GetContainingForeignKeys().FirstOrDefault();
            if (foreignKey != null)
            {
                IsRequired = foreignKey.IsRequired;
                ForeignParentType = foreignKey.PrincipalEntityType.ClrType;
                ForeignKeyName = foreignKey.Properties.FirstOrDefault()?.Name ?? string.Empty;
                DeleteBehavior = (Enums.DeleteBehavior)foreignKey.DeleteBehavior;
            }
        }
#endif
    }

}