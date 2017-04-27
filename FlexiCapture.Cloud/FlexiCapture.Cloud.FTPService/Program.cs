using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.FTPService
{
    static class Program
    {
        public static FCCFtpService Service;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service = new FCCFtpService();
            FlexiCapture.Cloud.FTPService.Helpers.ProcessorHelper.MakeProcessing();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                Service
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
