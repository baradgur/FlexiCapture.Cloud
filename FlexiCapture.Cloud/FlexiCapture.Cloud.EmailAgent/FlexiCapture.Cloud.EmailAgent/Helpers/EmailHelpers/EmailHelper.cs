using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;
using ImapX;
using ImapX.Enums;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.EmailHelpers
{
    public static class EmailHelper
    {
        #region SMTP helpers

        /// <summary>
        /// send email
        /// </summary>
        /// <param name="email"></param>
        public static string SendMessage(Emails email)
        {
            try
            {
                Users user = UsersHelper.GetToUserInfo(email.ReceiverUserId);
                if (user==null) return "ERROR";
                
                var settings = Program.Agent.ServiceSettings;
                MailMessage mail = new MailMessage(email.FromEmail, user.UserEmail);
                SmtpClient client = new SmtpClient();
                client.Port = settings.SMTPSettings.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                client.Credentials= new System.Net.NetworkCredential(settings.Credentials.UserName, settings.Credentials.Password);
                client.Host = settings.SMTPSettings.Server;
                mail.Subject = email.Subject;
                mail.IsBodyHtml = true;
                mail.Body = email.Body;
                client.Send(mail);
                LogHelper.AddLog("Email to: "+mail.To+" succesfully sent");
                return "OK";
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return "ERROR";
            }

        }
        
        #endregion
        #region imap helpers
        /// <summary>
        /// receive emails from IMAP
        /// </summary>
        public static List<EmailMessageModel> ReceiveEmailsFromIMAP(ServiceSettingsModel model)
        {
            try
            {
                List<EmailMessageModel> models = new List<EmailMessageModel>();
                var client = new ImapClient(model.ImapSettings.Server,model.ImapSettings.Port,model.ImapSettings.UseSSL);
                if (client.Connect())
                {

                    if (client.Login(model.Credentials.UserName, model.Credentials.Password))
                    {
                        // login successful
                        
                        List<string> lst = new List<string>();

                        foreach (var folder in client.Folders)
                        {
                            lst.Add(folder.Name);

                            if (folder.Name.ToLower().Equals(model.ImapSettings.DefaultFolder.ToLower()))
                            {
                                folder.Messages.Download("ALL", MessageFetchMode.Full, Int32.MaxValue);

                                foreach (var message in folder.Messages)
                                {
                                    if (!message.Seen)
                                    {
                                        models.Add(new EmailMessageModel()
                                        {
                                            Body = message.Body.Text,
                                            DateTime =message.Date,
                                            ReceiverEmail = model.Credentials.UserName,
                                            SenderEmail =  message.From.Address,
                                            Subject = message.Subject
                                        });

                                        //message.Seen = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
                return models;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// test connections
        /// </summary>
        public static bool TestConnectionParams()
        {
            try
            {
                ServiceSettingsModel model = Program.Agent.ServiceSettings;

                switch (model.ReceiveType)
                {
                        case ReceiveType.IMAP:
                        return ReceiveEmailsFromIMAP(model)!=null;
                        
                }
                return false;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return false;
            }

        }

        /// <summary>
        /// execute all not executed emails
        /// </summary>
        public static void ProcessNotExecutedTasks()
        {
            try
            {
                List<Tasks> overdueTasks = TaskHelper.GetToOverdueTasks();
                
                    foreach (var overdueTask in overdueTasks)
                    {
                        List<Emails> emails = DBHelpers.EmailHelper.GetToNotExecutedEmailsByTaskId(overdueTask.Id);

                        foreach (var email in emails)
                        {
                            //send email
                            string sendResult =SendMessage(email);
                            //update email state
                            int state = 2;
                            switch (sendResult)
                            {
                                case "OK":
                                    state = 2;
                                    break;

                                case "ERROR":
                                    state = 3;
                                    break;
                            }
                            
                             DBHelpers.EmailHelper.UpdateEmailState(email.Id,state);

                        }
                        LogHelper.AddLog("Succesfully completed task #"+overdueTask.Id+". Total processed emails: "+emails.Count);

                        DBHelpers.TaskHelper.UpdateTaskState(overdueTask.Id,2);
                    }
                
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: "+methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
        }
        #endregion

       
    }
}
