using System;
using System.Linq;
using AutoMapper;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                var mappingTypes = EntityViewModelMap<Entity, ViewModel>.GetAllEntityMaps();

                mappingTypes.ToList().ForEach(mappingType =>
                {
                    var entityMap = (IEntityViewModelMap)Activator.CreateInstance(mappingType);
                    entityMap.ConfigureEntityToViewModelMapper(cfg);
                    entityMap.ConfigureViewModelToEntityMapper(cfg);
                });


            });
        } 
    }
}