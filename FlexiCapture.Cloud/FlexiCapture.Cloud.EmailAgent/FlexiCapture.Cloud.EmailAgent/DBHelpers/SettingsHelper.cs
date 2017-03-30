using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    /// <summary>
    /// setting helper
    /// </summary>
    public static class SettingsHelper
    {
        /// <summary>
        /// get to setting value by name
        /// </summary>
        /// <returns></returns>
        public static string GetSettingValueByName(string settingName)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    var setting = db.Settings.FirstOrDefault(x => x.SettingName.ToLower().Equals(settingName.ToLower()));

                    return setting != null ? setting.SettingValue : "";
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
