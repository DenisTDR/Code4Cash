using AutoMapper;

namespace Code4Cash.Data.Models.ModelMappings.Base
{
    public interface IEntityViewModelMap
    {
        void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression);
        void ConfigureViewModelToEntityMapper(IMapperConfigurationExpression configurationExpression);
    }
}