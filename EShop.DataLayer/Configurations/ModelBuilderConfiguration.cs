using Microsoft.EntityFrameworkCore;
using Pluralize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EShop.DataLayer.Configurations
{
    public static class ModelBuilderConfiguration
    {
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
            {
                Pluralizer pluralize = new Pluralizer();
                modelBuilder.Entity(type).ToTable(pluralize.Pluralize(type.Name, 100));
            }
        }
        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
         
}
}
