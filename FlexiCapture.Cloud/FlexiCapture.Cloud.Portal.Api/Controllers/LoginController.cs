using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Helpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.LoginHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.Users;
using FlexiCapture.Cloud.Portal.Api.Users;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }
        // POST api/login
        public AuthUserModel Post([FromBody]UserLoginModel model)
        {
            try
            {
                return LoginHelper.LoginUser(model);
            }
            catch (Exception exception)
            {
                ExceptionHelper.TraceException(MethodBase.GetCurrentMethod().Name, exception);
                return new AuthUserModel()
                {
                    Error = new ErrorModel()
                    {
                        Name = "Error Auth",
                        ShortDescription = exception.Message,
                        FullDescription = (exception.InnerException == null) ? "" : exception.InnerException.Message.ToString()

                    }
                };
            }
        }


        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
