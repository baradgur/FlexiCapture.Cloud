using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.EmailHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.ProcessorHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.ServiceSettingsHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;
using EmailHelper = FlexiCapture.Cloud.EmailAgent.Helpers.EmailHelpers.EmailHelper;

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

        public Timer Timer = new Timer();
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
                Timer = new Timer();
                Timer.Interval = 5000;
                Timer.Elapsed += Timer_Elapsed;

                SystemSettings = new SystemSettingsModel();
                ServiceSettings = ServiceSettingHelper.InitServiceSettings(SystemSettings.SettingsPath);
                
                LogHelper.AddLog("Settings Path "+SystemSettings.SettingsPath);
                //Timer.Start();
                 ProcessorHelper.MakeProcessing();

                LogHelper.AddLog("FCC Email Agent Service started");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// make timer procedure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Timer.Stop();
                if (ServiceSettings == null)
                {
                    SystemSettings = new SystemSettingsModel();
                    ServiceSettings = ServiceSettingHelper.InitServiceSettings(SystemSettings.SettingsPath);
                }
                ProcessorHelper.MakeProcessing();
                Timer.Start();
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
        /// stop service method
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                Timer.Stop();
                LogHelper.AddLog("FCC Email Agent Service stopped");

                Emails email = new Emails()
                {
                    FromEmail = ServiceSettings.AgentEmail,
                    Subject = "Service Email Agent has been stopped",
                    ReceiverUserId = UsersHelper.CheckExistsUserByEmail(ServiceSettings.AdminCredentials.UserName),
                    TypeId = 4,
                    Host = Program.Agent.ServiceSettings.SMTPSettings.Server,
                    Port = Program.Agent.ServiceSettings.SMTPSettings.Port,
                    Body = "Service Stopped!",
                };

                EmailHelper.SendMessage(email);
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
    }
}
