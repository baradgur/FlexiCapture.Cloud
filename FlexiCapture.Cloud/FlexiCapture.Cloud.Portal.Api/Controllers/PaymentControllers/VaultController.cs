using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.Models.PaypalModels;

namespace FlexiCapture.Cloud.Portal.Api.Controllers.PaymentControllers
{
    public class VaultController : ApiController
    {
        //// GET: api/Vault
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Vault/5
        public IHttpActionResult Get(int userId)
        {
            var response = Helpers.PaymentAndTransactionsHelpers
                .PaymentAndTransactionsHelper
                    .CheckVaultIdExistance(userId);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound();
        }

        // POST: api/Vault
        public IHttpActionResult Post([FromBody]VaultUserPair model)
        {
            var response = Helpers.PaymentAndTransactionsHelpers
                .PaymentAndTransactionsHelper
                        .AddNewVaultId(model);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        // PUT: api/Vault/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vault/5
        public void Delete(int id)
        {
        }
    }
}
