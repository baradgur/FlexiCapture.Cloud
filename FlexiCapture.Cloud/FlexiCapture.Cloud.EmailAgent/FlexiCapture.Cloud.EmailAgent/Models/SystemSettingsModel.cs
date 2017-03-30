using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    public class SystemSettingsModel
    {

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public SystemSettingsModel()
        {
            try
            {
                InitSystemSettings();
            }
            catch (Exception)
            {
            }
        }
        #endregion
 
        #region methods
        /// <summary>
        /// init system settings method
        /// </summary>
        public  void InitSystemSettings()
        {
            try
            {
                MainPath = SettingsHelper.GetSettingValueByName("MainPath");
                DataPath = Path.Combine(MainPath, SettingsHelper.GetSettingValueByName("DataPath"));
                TemplatesPath = Path.Combine(MainPath, SettingsHelper.GetSettingValueByName("TemplatesPath"));
                SettingsPath = Path.Combine(MainPath, SettingsHelper.GetSettingValueByName("SettingsPath"));
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }
        }

        
        #endregion
        
        #region fields
        /// <summary>
        /// main path
        /// </summary>
        public string MainPath { get; set; }

        /// <summary>
        /// path to data folder
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// path to email templates folder
        /// </summary>
        public string TemplatesPath { get; set; }

        /// <summary>
        /// path to settings.xml
        /// </summary>
        public string SettingsPath { get; set; }
        #endregion
    }
}
