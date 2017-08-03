using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers
{
    public static class EmailHelper
    {
        /// <summary>
        /// add queue to database
        /// </summary>
        public static void AddQueueToDb(QueueModel model)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    Queue queue = new Queue()
                    {
                        EmailContent = model.EmailContent,
                        StateId = 1
                    };
                    db.Queue.Add(queue);
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


        public static void AddQueueToDb(List<QueueModel> queueModels)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    foreach (var model in queueModels)
                    {
                    Queue queue = new Queue()
                    {
                        EmailContent = model.EmailContent,
                        StateId = 1
                    };
                    db.Queue.Add(queue);
                    }
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
        ///     test method to send single emails
        /// </summary>
        public static void SendNewPasswordToEmail(string userName, string email, string password)
        {
            try
            {
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#username#="+userName+";#password#="+password+ ";#buttontitle#=Login at DataCapture.cloud;#sendername#=Your DataCapture.cloud Support Team;#linkto#=http://datacapture.cloud;#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = email,
                    FromEmail = "support@datacapture.cloud",
                    Subject = "Action Required - DataCapture.cloud Password Reset",
                    TypeId = 10,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now }
                });
                
                package.Emails.AddRange(models);

                var model = new QueueModel();
                model.StateId = 1;

                var settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                var formatter = new XmlSerializer(typeof(QueuePackageModel));
                string content = "";

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        formatter.Serialize(xmlWriter, package);
                    }
                    content = textWriter.ToString();

                }

                model.EmailContent = content;
                AddQueueToDb(model);

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                // Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        ///     test method to send single emails
        /// </summary>
        public static void SendConfirmationEmail(string email, string url)
        {
            try
            {
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#link#="+url+ ";#buttontitle#=Confirm Registration;#sendername#=Your DataCapture.cloud Support Team;#linkto#=http://datacapture.cloud;#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = email,
                    FromEmail = "support@datacapture.cloud",
                    Subject = "Action Required - DataCapture.cloud Registration Confirmation",
                    TypeId = 11,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now }
                });

                package.Emails.AddRange(models);

                var model = new QueueModel();
                model.StateId = 1;

                var settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                var formatter = new XmlSerializer(typeof(QueuePackageModel));
                string content = "";

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        formatter.Serialize(xmlWriter, package);
                    }
                    content = textWriter.ToString();

                }

                model.EmailContent = content;
                AddQueueToDb(model);

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                // Console.WriteLine(exception.Message);
            }
        }
        
        /// <summary>
        ///     test method to send single emails
        /// </summary>
        public static void SendNewUserInfoEmail(string firstName, string lastName, string email, string companyName,
            string phoneNumber, string userRoleName)
        {
            try
            {
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#fullname#=" + firstName + " "+lastName+ ";" +
                    "#email#=" + email + ";" +
                    "#company#=" + companyName + ";" +
                    "#phone#=" + phoneNumber + ";" +
                    "#userrole#=" + userRoleName + ";" +
                    "#sendername#=Your DataCapture.cloud Support Team;" +
                    "#link#=http://datacapture.cloud;#linkto#=http://datacapture.cloud;" +
                    "#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = "NewUserRegistration@DataCapture.cloud",
                    FromEmail = "support@datacapture.cloud",
                    Subject = "New user registered",
                    TypeId = 15,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now }
                });

                package.Emails.AddRange(models);

                var model = new QueueModel();
                model.StateId = 1;

                var settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                var formatter = new XmlSerializer(typeof(QueuePackageModel));
                string content = "";

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        formatter.Serialize(xmlWriter, package);
                    }
                    content = textWriter.ToString();

                }

                model.EmailContent = content;
                AddQueueToDb(model);

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                // Console.WriteLine(exception.Message);
            }
        }
    }
}