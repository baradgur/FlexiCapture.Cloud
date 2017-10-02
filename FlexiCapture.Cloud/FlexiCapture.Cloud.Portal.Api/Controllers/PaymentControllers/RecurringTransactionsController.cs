using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;


namespace FlexiCapture.Cloud.Portal.Api.Controllers.PaymentControllers
{
    public class RecurringTransactionsController : ApiController
    {
        private PaymentRepoUoF unitOfWork;

        public RecurringTransactionsController()
        {
            unitOfWork = new PaymentRepoUoF();
        }

        // GET: api/RecurringTransactions
        public IEnumerable<RecurringTransaction> Get()
        {
            return unitOfWork.RecurringTransactions.GetAll();
        }

        // GET: api/RecurringTransactions/5
        public RecurringTransaction Get(int id)
        {
            return unitOfWork.RecurringTransactions.Get(id);
        }

        // POST: api/RecurringTransactions
        public void Post([FromBody]RecurringTransaction model)
        {
            unitOfWork.RecurringTransactions.Create(model);
        }

        // PUT: api/RecurringTransactions/5
        public void Put(int id, [FromBody]RecurringTransaction model)
        {
            unitOfWork.RecurringTransactions.Update(model);
        }

        // DELETE: api/RecurringTransactions/5
        public void Delete(int id)
        {
            unitOfWork.RecurringTransactions.Delete(id);
        }
    }
}
