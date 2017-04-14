using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.SingleFileConversionService
{
    static class Program
    {
        public static SingleFileConversionService Service;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service = new SingleFileConversionService();

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
