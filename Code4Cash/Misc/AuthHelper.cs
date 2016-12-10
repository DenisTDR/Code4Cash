using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Data.Models.ViewModels.Base;
using Newtonsoft.Json;
using static System.String;

namespace Code4Cash.Misc
{
    public class AuthHelper
    {

        private AuthHelper()
        {

        }

        private static AuthHelper _instance = null;
        public static AuthHelper GetInstance()
        {
            return _instance ?? (_instance = new AuthHelper());
        }

        public string GetToken(HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage == null)
            {
                throw new Exception("httpRequestMessage is nullll");
            }
            if (httpRequestMessage.Headers == null)
            {
                throw new Exception("httpRequestMessage.Headers is nullll");
            }

            if (httpRequestMessage.Headers.Contains(AuthHeaderName))
            {
                var authToken = httpRequestMessage.Headers.GetValues(AuthHeaderName).First();
                if (!IsNullOrEmpty(authToken))
                {
                    return authToken;
                }
            }
            var requestModel =
                JsonConvert.DeserializeObject<RequestModel<EmptyViewModel>>(
                    httpRequestMessage.Content.ReadAsStringAsync().Result);
            var token = requestModel?.Token;
            return !IsNullOrEmpty(token) ? token : null;
        }

        public bool CheckToken(string token, HttpRequestMessage httpRequestMessage, RoleFunction role, bool throwResponseException = false)
        {

            using (var db = new DatabaseLayer())
            {
                var repo = db.Repo<SessionEntity>();
                var session = repo.GetOne(ses => ses.Token == token).Result;
                if (session == null)
                {
                    if (throwResponseException)
                    {
                        SetResponse("Invalid Token (not registered!)", httpRequestMessage);
                    }
                    return false;
                }
                if ((session.Account.Role.Functions & role) == RoleFunction.None && role != RoleFunction.None)
                {
                    if (throwResponseException)
                    {
                        SetResponse("You don't have the required Role(" + role + ") for this action", httpRequestMessage);
                    }
                    return false;
                }
            }
            return true;
        }

        public int GetAccountId(HttpRequestMessage httpRequestMessage)
        {
            var token = GetToken(httpRequestMessage);
            if (token == null)
            {
                throw new UnauthorizedAccessException();
            }
            using (var db = new DatabaseLayer())
            {
                var repo = db.Repo<SessionEntity>();
                var session = repo.GetOne(ses => ses.Token == token).Result;
                if(session == null)
                {
                    throw new UnauthorizedAccessException();
                }
                return session.Account.Id;
            }
        }
        private void SetResponse(string message, HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Add("AuthOk", "no");
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            msg.Headers.Add("Reason", message);

            throw new HttpResponseException(msg);
        }

        private const string AuthHeaderName = "Authorization";
    }
}