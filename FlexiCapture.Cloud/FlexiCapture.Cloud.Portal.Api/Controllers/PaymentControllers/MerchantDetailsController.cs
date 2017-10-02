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
    public class MerchantDetailsController : ApiController
    {
        private PaymentRepoUoF unitOfWork;

        public MerchantDetailsController()
        {
            unitOfWork = new PaymentRepoUoF();
        }

        // GET: api/MerchantDetail
        public IEnumerable<MerchantDetail> Get()
        {
            return unitOfWork.MerchantDetails.GetAll();
        }

        // GET: api/MerchantDetail/5
        public MerchantDetail Get(int id)
        {
            return unitOfWork.MerchantDetails.Get(id);
        }

        // POST: api/MerchantDetail
        public void Post([FromBody]MerchantDetail model)
        {
            unitOfWork.MerchantDetails.Create(model);
        }

        // PUT: api/MerchantDetail/5
        public void Put(int id, [FromBody]MerchantDetail model)
        {
            unitOfWork.MerchantDetails.Update(model);
        }

        // DELETE: api/MerchantDetail/5
        public void Delete(int id)
        {
            unitOfWork.MerchantDetails.Delete(id);
        }
    }
}
