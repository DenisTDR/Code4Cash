using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels.Users;

namespace Code4Cash.Data.Models.ModelMappings.Users
{
    public class SessionMap : EntityViewModelMap<SessionEntity, SessionViewModel>
    {
        public override void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<SessionEntity, SessionViewModel>()
                .ForMember(te => te.Id, opt => opt.Ignore())
                .PreserveReferences();
        }
    }
}