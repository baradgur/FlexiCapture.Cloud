using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.CommunicationModels;
using FlexiCapture.Cloud.Portal.Api.Models.Enums;
using System.Data.Entity;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.Models;
using FlexiCapture.Cloud.Portal.Api.Helpers.EmailHelpers;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class CommunicationHelper
    {
        public static List<CommunicationModel> GetCommunications()
        {
            try
            {
                List<CommunicationModel> models = new List<CommunicationModel>();
                using (var db = new FCCPortalEntities())
                {
                    var dbModels = db.Notifications
                        .Include(x => x.NotificationTypes)
                        .Include(x => x.Users)
                        .Include(x => x.Users1)
                        .Include(x => x.UserRoleTypes);

                    foreach (var dbModel in dbModels)
                    {
                        CommunicationModel model = new CommunicationModel()
                        {
                            Id = dbModel.Id,
                            NotificationTypeId = dbModel.NotificationTypeId,
                            Message = dbModel.Message,
                            Subject = dbModel.Subject,
                            Sender = UsersHelper.GetToUserData(dbModel.Users),
                            Date = dbModel.Date,
                        };
                        if (dbModel.UserRoleId.HasValue)
                        {
                            model.UserRoleId = dbModel.UserRoleId.Value;
                        }
                        else if (dbModel.Users1 != null)
                        {
                            model.User = UsersHelper.GetToUserData(dbModel.Users1);
                            model.UserRoleId = -1;
                        }
                        else
                        {
                            model.UserRoleId = 0;
                        }
                        models.Add(model);
                    }
                }
                return models;
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
        /// 
        /// </summary>
        public static CommunicationModel SendCommunication(CommunicationModel model)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    if (model.UserRoleId == 0)
                    {
                        var users = db.Users
                            .Where(x => model.NotificationTypeId == (int)CommunicationTypes.Important
                                        ||
                                        model.NotificationTypeId == (int)CommunicationTypes.MonthlyUsePayment &&
                                        x.GetUsePaymentNotif
                                        ||
                                        model.NotificationTypeId == (int)CommunicationTypes.PortalUpdatesReleases &&
                                        x.GetReleaseUpdateNotif);
                        SendEmails(users, model);
                    }
                    else if (model.UserRoleId == -1)
                    {
                        var users = db.Users.Where(x => x.Id == model.User.Id);
                        SendEmails(users, model);
                    }
                    else
                    {
                        var users = db.Users
                            .Include(x => x.UserLogins)
                            .Where(x => (model.NotificationTypeId == (int)CommunicationTypes.Important
                                        ||
                                        model.NotificationTypeId == (int)CommunicationTypes.MonthlyUsePayment &&
                                        x.GetUsePaymentNotif
                                        ||
                                        model.NotificationTypeId == (int)CommunicationTypes.PortalUpdatesReleases &&
                                        x.GetReleaseUpdateNotif)
                                        && x.UserLogins.FirstOrDefault() != null && x.UserLogins.FirstOrDefault().UserRoleId == model.UserRoleId);
                        SendEmails(users, model);
                    }

                    var dbCommunication = new DB.Notifications()
                    {
                        NotificationTypeId = model.NotificationTypeId,
                        Message = model.Message,
                        SenderId = model.Sender.Id,
                        Subject = model.Subject,
                        Date = System.DateTime.UtcNow
                    };
                    if (model.UserRoleId > 0)
                    {
                        dbCommunication.UserRoleId = model.UserRoleId;
                    }
                    else if (model.UserRoleId == -1)
                    {
                        dbCommunication.UserId = model.User.Id;
                    }

                    db.Notifications.Add(dbCommunication);
                    db.SaveChanges();
                    model.Id = dbCommunication.Id;
                }
                return model;
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

        private static void SendEmails(IQueryable<DB.Users> users, CommunicationModel email)
        {
            try
            {
                List<QueueModel> queueModels = new List<QueueModel>();
                foreach (var user in users)
                {
                    QueuePackageModel package = new QueuePackageModel();
                    var models = new List<EmailModel>();
                    models.Add(new EmailModel
                    {
                        EmailContentLine = "#emailtitle#=" + email.Subject + ";" +
                                           "#username#=" + (user != null ? user.FirstName + " " + user.LastName : "") + ";" +
                                           "#notification#=" + email.Message + ";" +
                                           "#sendername#=" + (email.Sender != null ? email.Sender.FirstName + " " + email.Sender.LastName : "") + ", Your DataCapture.cloud Support Team;" +
                                           "#linkto#=http://datacapture.cloud",
                        ToEmails = user.Email,
                        FromEmail = "support@datacapture.cloud",
                        Subject = email.Subject,
                        TypeId = 2,
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
                    queueModels.Add(model);
                }
                EmailHelper.AddQueueToDb(queueModels);


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