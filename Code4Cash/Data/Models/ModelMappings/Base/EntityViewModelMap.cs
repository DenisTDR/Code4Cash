using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc;

namespace Code4Cash.Data.Models.ModelMappings.Base
{
    public abstract class EntityViewModelMap<TE, TVm> : EntityMap<TE>, IEntityViewModelMap where TE : Entity
        where TVm : ViewModel
    {
        public virtual void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<TE, TVm>().AfterMap((entity, viewModel) =>
            {
                viewModel.Id = entity.Selector;
            }).PreserveReferences();
        }
        public virtual void ConfigureViewModelToEntityMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<TVm, TE>()
                .ForMember(te => te.Id, opt => opt.Ignore())
                .AfterMap((viewModel, entity) =>
                {
                    entity.Selector = viewModel.Id;
                });
        }


    }
}