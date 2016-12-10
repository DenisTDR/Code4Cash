using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Data.Models.ViewModels.Base;
using Newtonsoft.Json;

namespace Code4Cash.Misc.Filters
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public RoleFunction Role { get; private set; }
        public AuthAttribute(RoleFunction role)
        {
            Role = role;
        }
        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
            var token = AuthHelper.GetInstance().GetToken(httpActionContext.Request);
            return AuthHelper.GetInstance().CheckToken(token, httpActionContext.Request, Role, true);
        }

      

 
    }
}