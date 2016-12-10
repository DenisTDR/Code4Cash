using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Data.Models.ViewModels.Users;
using Code4Cash.Data.Users;

namespace Code4Cash.Controllers.Users
{
    public class UserController:ApiController
    {

        public async Task<object> Register()
        {

            return null;
        }

        [ResponseType(typeof(SessionViewModel))]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var authLayer = new AuthLayer())
            {
                if (!await authLayer.CheckCredentials(model))
                {
                    return BadRequest("invalid_credentials");
                }
                var sessionEntity = await authLayer.CreateSession(model);
                var sessionViewModel = Mapper.Map<SessionViewModel>(sessionEntity);
                return Ok(sessionViewModel);
            }
        }
    }
}