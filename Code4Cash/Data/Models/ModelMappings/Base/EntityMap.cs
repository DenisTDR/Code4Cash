﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Misc;

namespace Code4Cash.Data.Models.ModelMappings.Base
{
    public abstract class EntityMap<TE>: EntityTypeConfiguration<TE> where TE:Entity
    {
        protected EntityMap()
        {
            var entityName = typeof(TE).Name.Replace("Entity", "");
            Map(emc =>
            {
                emc.ToTable(entityName);
            });
        }
        public static IEnumerable<Type> GetAllEntityMaps()
        {
            var list =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.IsSubclassOfGenericType(typeof(EntityMap<>)) && !type.IsGenericType);
            return list;
        }
    }
}