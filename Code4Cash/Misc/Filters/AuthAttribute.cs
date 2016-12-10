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
            this.Role = role;
        }
        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
            var token = GetToken(httpActionContext);
            return CheckToken(token, httpActionContext);
        }

        private string GetToken(HttpActionContext httpActionContext)
        {
            if (httpActionContext.Request.Headers.Contains(AuthHeaderName))
            {
                var authToken = httpActionContext.Request.Headers.GetValues(AuthHeaderName).First();
                if (!string.IsNullOrEmpty(authToken))
                {
                    return authToken;
                }
            }
            var requestModel =
                JsonConvert.DeserializeObject<RequestModel<EmptyViewModel>>(
                    httpActionContext.Request.Content.ReadAsStringAsync().Result);
            var token = requestModel?.Token;
            return !string.IsNullOrEmpty(token) ? token : null;
        }

        private bool CheckToken(string token, HttpActionContext httpActionContext)
        {

            using (var db = new DatabaseUnit())
            {
                var repo = db.Repo<SessionEntity>();
                var session = repo.GetOne(ses => ses.Token == token).Result;
                if (session == null)
                {
                    SetResponse("Invalid Token (not registered!)", httpActionContext);
                    return false;
                }
                if ((session.Account.Role.Functions & Role) == RoleFunction.None && Role != RoleFunction.None)
                {
                    SetResponse("You don't have the required Role(" + Role + ") for this action", httpActionContext);
                    return false;
                }
                httpActionContext.Request.Headers.Add("Role", session.Account.Role.ToString());
            }

            httpActionContext.Request.Headers.Add("AuthOk", "yes");
            return true;
        }

        private void SetResponse(string message, HttpActionContext httpActionContext)
        {
            httpActionContext.Request.Headers.Add("AuthOk", "no");
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            msg.Headers.Add("Reason", message);

            throw new HttpResponseException(msg);
        }

        private const string AuthHeaderName = "Authorization";
    }
}