using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.Helpers.EmailHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.ServiceSettingsHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgent
{
    public partial class FCCEmailAgent : ServiceBase
    {
        #region vars
        /// <summary>
        /// system settings
        /// </summary>
        public SystemSettingsModel SystemSettings;

        /// <summary>
        /// service settings from settings.xml
        /// </summary>
        public ServiceSettingsModel ServiceSettings { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public FCCEmailAgent()
        {
            InitializeComponent();
        }
        #endregion

        #region debug method
        /// <summary>
        /// method for debug mode start operations
        /// </summary>
        public void OnDebug()
        {
            OnStart(null);
        }
        #endregion
        
        #region methods
        /// <summary>
        /// start service method
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                SystemSettings = new SystemSettingsModel();
                ServiceSettings = ServiceSettingHelper.InitServiceSettings(SystemSettings.SettingsPath);
                EmailHelper.TestConnectionParams();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// stop service method
        /// </summary>
        protected override void OnStop()
        {
        }
        #endregion
    }
}
