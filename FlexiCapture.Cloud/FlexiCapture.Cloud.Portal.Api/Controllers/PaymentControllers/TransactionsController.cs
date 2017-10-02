using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlexiCapture.Cloud.Portal.Api.Controllers.PaymentControllers
{
    public class TransactionsController : ApiController
    {
        

        // GET: api/Transactions
        

        // GET: api/Transactions/5
        public IHttpActionResult Get(int id)
        {
            return Ok();

        }

        // POST: api/Transactions
        public IHttpActionResult Post([FromBody]Models.PaypalModels.Transaction model)
        {
            var response = Helpers.PaymentAndTransactionsHelpers.PaymentAndTransactionsHelper.AddNewTransaction(model);
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        // PUT: api/Transactions/5
        public void Put()
        {
        }

        // DELETE: api/Transactions/5
        public void Delete(int id)
        {
        }
    }
}
