using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities.Users
{
    public class SessionEntity : Entity
    {
        public virtual AccountEntity Account { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}