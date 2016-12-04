using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.ModelMappings.Base;

namespace Code4Cash.Data.Models.ModelMappings.Users
{
    public class AccountMap:EntityMap<AccountEntity>
    {
        public AccountMap()
        {
//            HasOptional(a => a.Profile).WithOptionalPrincipal();
        }
    }
}