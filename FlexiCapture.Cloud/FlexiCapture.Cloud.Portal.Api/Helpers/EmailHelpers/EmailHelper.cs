﻿using System;
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
                    EmailContentLine = "#username#="+userName+";#password#="+password+";#buttontitle#=Go to site;#sendername#=FlexiCapture cloud;#linkto#=http://flexicapture.cloud;#linktitle#=Go To Flexi Capture Cloud",
                    ToEmails = email,
                    FromEmail = "fccemailagent@netvix.by",
                    Subject = "FlexiCapture.cloud Recover Password",
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
               // Console.WriteLine(exception.Message);
            }
        }
    }
}