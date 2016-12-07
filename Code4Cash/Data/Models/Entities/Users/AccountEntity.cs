using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities.Users
{
    public class AccountEntity : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual RoleEntity Role { get; set; }
        public virtual ProfileEntity Profile { get; set; }
        public virtual IList<SessionEntity> Sessions { get; set; }
        public bool Active { get; set; }
    }
}