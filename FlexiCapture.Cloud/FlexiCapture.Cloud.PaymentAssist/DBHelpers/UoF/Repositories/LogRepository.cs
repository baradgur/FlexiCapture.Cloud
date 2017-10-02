using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;
using FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Interfaces;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Repositories
{
    public class LogRepository : IRepository<log>
    {
        private DB.PaymentAgentEntities Db;

        public LogRepository(DB.PaymentAgentEntities context)
        {
            this.Db = context;
        }

        public IEnumerable<log> GetAll()
        {
            return Db.log;
        }

        public log Get(int id)
        {
            return Db.log.Find(id);
        }

        public log Create(log log)
        {
            Db.log.Add(log);
            Db.SaveChanges();

            return log;
        }

        public void Update(log log)
        {
           // Db.Entry(log).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            log log = Db.log.Find(id);
            if (log != null)
                Db.log.Remove(log);
        }
    }
}
