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
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
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
        /// <summary>
        ///     test method to send single emails
        /// </summary>
        public static void SendEmailResponseFail(string email, string text, string ccAddresses)
        {
            try
            {
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#responsetext#=" + text + ";#sendername#=Your DataCapture.cloud Support Team;#link#=http://datacapture.cloud;#linkto#=http://datacapture.cloud;#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = email,
                    FromEmail = "support@datacapture.cloud",
                    Subject = "Conversion Request Response",
                    TypeId = 13,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now },
                    CcResponseTo = ccAddresses
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
               // Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        ///     test method to send single emails
        /// </summary>
        public static void SendEmailResponseFail(int userId, string text, string ccAddresses)
        {
            try
            {
                string email = "";
                using(var db = new FCCPortalEntities2())
                {
                    string userEmail = (from s in db.Users
                        where s.Id == userId
                        select s.Email).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(userEmail))
                    {
                        email = userEmail;
                    }

                }
                if (string.IsNullOrWhiteSpace(email))
                {
                    return;
                }
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#responsetext#=" + text + ";#sendername#=Your DataCapture.cloud Support Team;#link#=http://datacapture.cloud;#linkto#=http://datacapture.cloud;#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = email,
                    FromEmail = "support@datacapture.cloud",
                    Subject = "Conversion Request Response",
                    TypeId = 13,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now },
                    CcResponseTo = ccAddresses
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
                // Console.WriteLine(exception.Message);
            }
        }

        public static void SendEmailResponse(int userId, List<Tuple<int, string>> downloadIds, List<Tuple<string, string>> attachmentsLinks, string ccAddresses, string text)
        {
            try
            {
                string email = "";
                string listDowloadLinks = "";
                string attachmentsList = "";

                string portalApiUrl = SettingsHelper.GetSettingValueByName("PortalApiUrl");

                using (var db = new FCCPortalEntities2())
                {
                    string userEmail = (from s in db.Users
                                        where s.Id == userId
                                        select s.Email).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(userEmail))
                    {
                        email = userEmail;
                    }

                }
                if (string.IsNullOrWhiteSpace(email))
                {
                    return;
                }

                foreach (var item in downloadIds)
                {
                    listDowloadLinks += "<li><a href*~*'"+portalApiUrl+"/api/downloadFile/"+item.Item1+"'>"+item.Item2+"</a></li>";
                }

                foreach (var item in attachmentsLinks)
                {
                    attachmentsList += item.Item1 + "***" + item.Item2 + "****";
                }

                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                    EmailContentLine = "#attachmentslist#=" + attachmentsList + ";#listdownloadlinks#=" + listDowloadLinks+ ";#sendername#=Your DataCapture.cloud Support Team;#responsetext#=" + text + ";#link#=http://datacapture.cloud;#linkto#=http://datacapture.cloud;#linktitle#=This is a one-time automatically-generated e-mail.Visit http://DataCapture.cloud",
                    ToEmails = email,
                    FromEmail = "support@datacapture.cloud",
                    Subject = "Conversion Request Response",
                    TypeId = 12,
                    Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now },
                    CcResponseTo = ccAddresses
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
                // Console.WriteLine(exception.Message);
            }
        }
    }
}