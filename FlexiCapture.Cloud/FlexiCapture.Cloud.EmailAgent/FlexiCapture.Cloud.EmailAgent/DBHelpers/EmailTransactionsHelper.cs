using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    public static class EmailTransactionsHelper
    {
        /// <summary>
        /// add email transaction
        /// </summary>
        public static void AddEmailTransaction(int emailId)
        {
            try
            {
                using (var db =new FCCEmailAgentEntities())
                {
                    db.EmailTransactions.Add(new EmailTransactions() {EmailId = emailId, State = 2, ExecutionDate = DateTime.Now});
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }

        }

        /// <summary>
        /// update transactions states
        /// </summary>
        public static void UpdateTransactionStateByEmailState(int emailId, int emailStateId)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    EmailTransactions transaction = db.EmailTransactions.FirstOrDefault(x => x.Id == emailId);
                    if (transaction != null)
                    {
                        switch (emailStateId)
                        {
                            case 2:
                                transaction.State = 3;
                                break;
                            case 3:
                                transaction.State = 4;
                                break;
                        }
                        transaction.ExecutionDate = DateTime.Now;
                        
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }

        }
    }
}
