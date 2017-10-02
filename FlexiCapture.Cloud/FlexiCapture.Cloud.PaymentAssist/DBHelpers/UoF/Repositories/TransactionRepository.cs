using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;
using FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Interfaces;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private DB.PaymentAgentEntities Db;

        public TransactionRepository(DB.PaymentAgentEntities context)
        {
            this.Db = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return Db.Transaction;
        }

        public Transaction Get(int id)
        {
            return Db.Transaction
                .Where(x => x.TransactionID == id)
                    .DefaultIfEmpty(null)
                        .Single();
        }

        public Transaction Create(Transaction transaction)
        {
            Db.Transaction.Add(transaction);
            Db.SaveChanges();

            return transaction;
        }

        public void Update(Transaction transaction)
        {
           // Db.Entry(transaction).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Transaction transaction = Db.Transaction.Find(id);
            if (transaction != null)
                Db.Transaction.Remove(transaction);
        }
    }
}
