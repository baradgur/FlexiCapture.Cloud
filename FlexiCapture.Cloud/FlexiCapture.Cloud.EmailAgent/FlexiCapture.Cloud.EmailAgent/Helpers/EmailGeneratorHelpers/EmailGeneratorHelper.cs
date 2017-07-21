using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.IOHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.EmailGeneratorHelpers
{
    public static class EmailGeneratorHelper
    {
        /// <summary>
        /// generate email body
        /// </summary>
        public static string GenerateEmailBody(int typeId, List<EmailContentElementModel> contentElements)
        {
            try
            {
                string result = "";
                using (var db = new FCCEmailAgentEntities())
                {
                    string templateName =
                        db.EmailTypes.Where(x => x.Id == typeId).Select(x => x.TemplateName).FirstOrDefault();

                    string templatePath = Path.Combine(Program.Agent.SystemSettings.TemplatesPath,
                        SettingsHelper.GetSettingValueByName(templateName));
                    string template = IOHelper.ReadFile(templatePath);

                    foreach (var contentElement in contentElements)
                    {
                        template =template.Replace(contentElement.Name, contentElement.Value);
                    }
                    result = template;
                }
                return result;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return "";
            }

        }

        internal static List<MailAddress> GenerateCcAddresses(string ccResponseTo)
        {
            throw new NotImplementedException();
        }

        public static string GenerateStringAttachments(string textElements)
        {
            try { 
            List<EmailAttachmentModel> models = new List<EmailAttachmentModel>();
            List<string> splitElements = textElements.Split(new string[] { "****" }, StringSplitOptions.None).ToList();

            foreach (var splitElement in splitElements)
            {
                List<string> separateElements = splitElement.Split(new string[] { "***" }, StringSplitOptions.None).ToList();
                if (separateElements.Count == 2)
                {
                    models.Add(new EmailAttachmentModel() { Path = separateElements[0], OriginalName = separateElements[1]});
                }
            }
            var settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            var formatter = new XmlSerializer(typeof(List<EmailAttachmentModel>));
            string response = "";

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    formatter.Serialize(xmlWriter, models);
                }

                response = textWriter.ToString();

            }
            return response;
             }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return "";
            }
        }

        public static System.Net.Mail.Attachment GenerateAttachments(EmailAttachmentModel model)
        {
            try
            {
                FileStream fStream = new FileStream(model.Path,FileMode.Open, FileAccess.Read);
                Attachment response = new Attachment(fStream, model.OriginalName);
                return response;
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
