using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ModelMappings.Base
{
    public abstract class EntityMap<TE, TVm> : EntityTypeConfiguration<TE>, IEntityMap where TE : Entity
        where TVm : ViewModel
    {
        public void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<TE, TVm>().AfterMap((entity, viewModel) =>
            {
                viewModel.Id = entity.Selector;
            });
        }
        public void ConfigureViewModelToEntityMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<TVm, TE>().AfterMap((viewModel, entity) =>
            {
                entity.Selector = viewModel.Id;
            });
        }

        public static IEnumerable<Type> GetAllEntityMaps()
        {
            var list =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.IsSubclassOfGenericType(typeof(EntityMap<,>)));
            return list;
        }
    }
}