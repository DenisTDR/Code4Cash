using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities.Users
{
    public class ProfileEntity : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual AccountEntity Account { get; set; } 
    }
}
