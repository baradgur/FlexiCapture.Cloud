using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;
using FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Interfaces;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Repositories
{
    public class MerchantDetailRepository : IRepository<MerchantDetail>
    {
        private DB.PaymentAgentEntities Db;

        public MerchantDetailRepository(DB.PaymentAgentEntities context)
        {
            this.Db = context;
        }

        public IEnumerable<MerchantDetail> GetAll()
        {
            return Db.MerchantDetail;
        }

        public MerchantDetail Get(int id)
        {
            return Db.MerchantDetail.Find(id);
        }

        public MerchantDetail Create(MerchantDetail merchantDetail)
        {
            Db.MerchantDetail.Add(merchantDetail);
            Db.SaveChanges();

            return merchantDetail;
        }

        public void Update(MerchantDetail merchantDetail)
        {
           // Db.Entry(merchantDetail).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            MerchantDetail merchantDetail = Db.MerchantDetail.Find(id);
            if (merchantDetail != null)
                Db.MerchantDetail.Remove(merchantDetail);
        }
    }
}
