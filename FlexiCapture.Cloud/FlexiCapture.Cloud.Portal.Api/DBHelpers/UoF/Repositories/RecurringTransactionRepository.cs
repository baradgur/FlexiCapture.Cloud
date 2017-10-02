using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF.Interfaces;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF.Repositories
{
    public class RecurringTransactionRepository : IRepository<RecurringTransaction>
    {
        private DB_PaymentAgent.PaymentAgentEntities Db;

        public RecurringTransactionRepository(DB_PaymentAgent.PaymentAgentEntities context)
        {
            this.Db = context;
        }

        public IEnumerable<RecurringTransaction> GetAll()
        {
            return Db.RecurringTransaction;
        }

        public RecurringTransaction Get(int id)
        {
            return Db.RecurringTransaction.Find(id);
        }

        public RecurringTransaction Create(RecurringTransaction recurringTransaction)
        {
            Db.RecurringTransaction.Add(recurringTransaction);
            Db.SaveChanges();

            return recurringTransaction;
        }

        public void Update(RecurringTransaction recurringTransaction)
        {
           // Db.Entry(recurringTransaction).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            RecurringTransaction recurringTransaction = Db.RecurringTransaction.Find(id);
            if (recurringTransaction != null)
                Db.RecurringTransaction.Remove(recurringTransaction);
        }
    }
}
