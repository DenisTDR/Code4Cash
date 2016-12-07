using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities.Users
{
    public class RoleEntity:Entity
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public RoleFunction Functions { get; set; }
    }
}