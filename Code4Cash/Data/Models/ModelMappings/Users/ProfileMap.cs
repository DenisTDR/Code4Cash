﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels.Users;

namespace Code4Cash.Data.Models.ModelMappings.Users
{
    public class ProfileMap:EntityViewModelMap<ProfileEntity, ProfileViewModel>
    {
        public ProfileMap()
        {
            HasRequired(p => p.Account).WithOptional(a => a.Profile).WillCascadeOnDelete();
            
        }
    }
}