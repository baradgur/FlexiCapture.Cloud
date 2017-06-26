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
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.ZipService.Helpers.ProcessorHelper;

namespace FlexiCapture.Cloud.ZipService
{
    public partial class FccZipService : ServiceBase
    {
        public Timer Timer = new Timer();

        public FccZipService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// debug mode method
        /// </summary>
        public void OnDebug()
        {
            ProcessorHelper.MakeProcessing();
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
                FlexiCapture.Cloud.ZipService.Helpers.ProcessorHelper.ProcessorHelper.MakeProcessing();
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
        /// start service in release
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.AddLog("Start ZIP service");
                Timer = new Timer {Interval = 4000};
                Timer.Elapsed += Timer_Elapsed;
                LogHelper.AddLog("Start Timer");
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
        /// stop service
        /// </summary>

        protected override void OnStop()
        {
            try
            {
                Timer.Stop();
            }
            catch (Exception)
            {

            }
        }
    }
}
