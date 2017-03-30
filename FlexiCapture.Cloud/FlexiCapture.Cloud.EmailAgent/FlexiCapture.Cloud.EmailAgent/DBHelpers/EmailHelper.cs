using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.Helpers.EmailGeneratorHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    public static class EmailHelper
    {
        /// <summary>
        /// add email to database
        /// </summary>
        /// <returns></returns>
        public static int AddEmailToDbByEmailModel(int taskId, EmailModel emailModel)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    Emails email = new Emails();
                    email.Subject = emailModel.Subject;
                    email.FromEmail = emailModel.FromEmail;
                    email.ReceiverUserId = UsersHelper.CheckExistsUserByEmail(emailModel.ToEmails);
                    email.StateId = 1;
                    email.TaskId = taskId;
                    email.TypeId = emailModel.TypeId;
                    email.Host = Program.Agent.ServiceSettings.SMTPSettings.Server;
                    email.Port = Program.Agent.ServiceSettings.SMTPSettings.Port;
                    email.Body = EmailGeneratorHelper.GenerateEmailBody(emailModel.TypeId, emailModel.EmailContentLine);

                    db.Emails.Add(email);
                    db.SaveChanges();

                    EmailTransactionsHelper.AddEmailTransaction(email.Id);
                    return email.Id;
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return -1;
            }

        }

        /// <summary>
        /// select  all not executed emails
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static List<Emails> GetToNotExecutedEmailsByTaskId(int taskId)
        {
            try
            {
                using (var db= new FCCEmailAgentEntities())
                {
                    return db.Emails.Where(x => x.TaskId == taskId && x.StateId == 1).ToList();
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }

        }

        /// <summary>
        /// update email state
        /// </summary>
        public static void UpdateEmailState(int emailId, int stateId)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    Emails email = db.Emails.FirstOrDefault(x => x.Id == emailId);
                    if (email!=null)
                    {
                        email.StateId = stateId;
                        db.SaveChanges();
                        EmailTransactionsHelper.UpdateTransactionStateByEmailState(emailId,stateId);
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
