using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Models.ViewModels.Users
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RoleFunction> Roles { get; set; }
    }
}