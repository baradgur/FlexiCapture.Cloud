using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.ServiceSettingsHelpers
{
    /// <summary>
    /// helper for service settings
    /// </summary>
    public static  class ServiceSettingHelper
    {
        /// <summary>
        /// init default service settings
        /// </summary>
        /// <returns></returns>
        public static ServiceSettingsModel InitDefaultServiceSettings(string settingsPath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ServiceSettingsModel));
                ServiceSettingsModel model = new ServiceSettingsModel();
                model.Credentials = new EmailCredentials()
                {
                    UserName = "fccemailagent@netvix.by",
                    Password = "wGbeF6SagD"
                };

                model.AdminCredentials = new EmailCredentials()
                {
                    UserName = "fccemailagent_admin@netvix.by",
                    Password = "LvBEcjN2TcQFuarm"
                };

                model.AgentName = "FlexiCapture.Cloud Email Agent";
                model.AgentEmail = "fccemailagent@netvix.by";

                //receive email params
                model.ImapSettings = new IMAPConnectionProtocolModel() { Server = "mail.netvix.by", Port = 143, DefaultFolder = "INBOX", UseSSL = false};
                model.POP3Settings = new POPConnectionProtocolModel() { Server = "mail.netvix.by", Port = 110, UseSSL = false };

                //send email params
                model.SMTPSettings = new IMAPConnectionProtocolModel() { Server = "mail.netvix.by", Port = 25, UseSSL = false };

                using (FileStream fs = new FileStream(settingsPath, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, model);
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

        /// <summary>
        /// init default service settings
        /// </summary>
        private static ServiceSettingsModel LoadServiceSettings(string settingsPath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ServiceSettingsModel));
                ServiceSettingsModel model = new ServiceSettingsModel();
                using (FileStream fs = new FileStream(settingsPath, FileMode.Open))
                {
                  model =   formatter.Deserialize(fs) as ServiceSettingsModel;
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

        /// <summary>
        /// init service settings
        /// </summary>
        public static ServiceSettingsModel InitServiceSettings(string settingsPath)
        {
            try
            {
                if (!File.Exists(settingsPath))
                {
                   return InitDefaultServiceSettings(settingsPath);
                }
                else
                {
                    return LoadServiceSettings(settingsPath);
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
    }
}
