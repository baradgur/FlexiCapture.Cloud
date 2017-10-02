using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;
using FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF;
using Microsoft.Ajax.Utilities;

namespace FlexiCapture.Cloud.Portal.Api.Controllers.PaymentControllers
{
    public class CardsController : ApiController
    {
        private PaymentRepoUoF unitOfWork;

        public CardsController()
        {
            unitOfWork = new PaymentRepoUoF();
        }

        // GET: api/Cards
        public IEnumerable<Card> Get()
        {
            List<FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent.Card> cards = unitOfWork.Cards.GetAll().ToList();

            if (cards == null)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                //{
                //    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                //    ReasonPhrase = "Product ID Not Found"
                //};
                //throw new HttpResponseException(resp);
            }
            return null;
        }

        // GET: api/Cards/5
        public Card Get(int id)
        {
            return unitOfWork.Cards.Get(id);
        }

        // POST: api/Cards
        public void Post([FromBody]Card model)
        {
            unitOfWork.Cards.Create(model);
        }

        // PUT: api/Cards/5
        public void Put(int id, [FromBody]Card model)
        {
            unitOfWork.Cards.Update(model);
        }

        // DELETE: api/Cards/5
        public void Delete(int id)
        {
            unitOfWork.Cards.Delete(id);
        }
    }
}
