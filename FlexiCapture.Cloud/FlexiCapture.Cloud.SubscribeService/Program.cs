using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace FlexiCapture.Cloud.SubscribeService
{
    static class Program
    {
        public static SubscribeService Service;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service = new SubscribeService();

            //#if DEBUG
            //            Service.OnDebug();
            //#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                Service
            };
            ServiceBase.Run(ServicesToRun);
            //#endif
        }
    }
}