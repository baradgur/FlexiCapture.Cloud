using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static string GenerateEmailBody(int typeId, string text)
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
                    EmailContentModel content = new EmailContentModel(text);
                    string template = IOHelper.ReadFile(templatePath);

                    foreach (var contentElement in content.Elements)
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
    }
}
