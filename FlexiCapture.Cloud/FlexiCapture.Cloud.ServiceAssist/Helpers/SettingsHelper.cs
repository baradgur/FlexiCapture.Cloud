using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;

namespace FlexiCapture.Cloud.ServiceAssist.Helpers
{
    class SettingsHelper
    {
        /// <summary>
        /// get to settings value
        /// </summary>
        /// <returns></returns>
        public static string GetSettingsValueByName(string name)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    return db.Settings.FirstOrDefault(x => x.SettingName.ToLower().Equals(name.ToLower())).SettingValue;
                }
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
