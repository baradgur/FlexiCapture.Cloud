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
using FlexiCapture.Cloud.EmailAttachmentService.Helpers.ProcessorHelper;

namespace FlexiCapture.Cloud.EmailAttachmentService
{
    public partial class FCC_EmailAttachmentService : ServiceBase
    {
        public Timer Timer = new Timer();

        public FCC_EmailAttachmentService()
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

                ProcessorHelper.MakeProcessing();
                Timer.Start();
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

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
                Timer = new Timer();
                Timer.Interval = 5000;
                Timer.Elapsed += Timer_Elapsed;
                Timer.Start();
            }
            catch (Exception)
            {
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
