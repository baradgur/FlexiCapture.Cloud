using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.FtpConversionSettingsHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.FTPAccessTestHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ServiceSettingsHelper;
using FlexiCapture.Cloud.Portal.Api.Models.Errors;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers
{
    public class FTPAccessTestController : ApiController
    {
        // POST: api/FTPAccessTest
        public IHttpActionResult Post([FromBody]FTPSettingsAggregateModel model)
        {
            try
            {
                int flag = FTPAccessTestHelper.TestFtpAccess(model);

                if (flag == 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(flag.ToString());
                }
                
            }
            catch (Exception exception)
            {
                return BadRequest();    
            }
        }


    }
}
