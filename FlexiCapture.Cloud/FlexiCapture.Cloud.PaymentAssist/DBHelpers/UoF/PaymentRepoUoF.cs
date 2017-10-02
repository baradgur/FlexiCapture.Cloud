using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;
using FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Repositories;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF
{
    public class PaymentRepoUoF : IDisposable
    {
        private PaymentAgentEntities db = new PaymentAgentEntities();

        private CardRepository cardRepository;
        private MerchantDetailRepository merchantDetailRepository;
        private LogRepository logRepository;
        private RecurringTransactionRepository recurringTransactionRepository;
        private TransactionRepository transactionRepository;


        public CardRepository Cards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(db);
                return cardRepository;
            }
        }

        public MerchantDetailRepository MerchantDetails
        {
            get
            {
                if (merchantDetailRepository == null)
                    merchantDetailRepository = new MerchantDetailRepository(db);
                return merchantDetailRepository;
            }
        }

        public LogRepository LogDetails
        {
            get
            {
                if (logRepository == null)
                    logRepository = new LogRepository(db);
                return logRepository;
            }
        }

        public RecurringTransactionRepository RecurringTransactions
        {
            get
            {
                if (recurringTransactionRepository == null)
                    recurringTransactionRepository = 
                        new RecurringTransactionRepository(db);
                return recurringTransactionRepository;
            }
        }

        public TransactionRepository Transactions
        {
            get
            {
                if (transactionRepository == null)
                    transactionRepository = new TransactionRepository(db);
                return transactionRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
