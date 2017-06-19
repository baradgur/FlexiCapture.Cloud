﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.ZipService
{
    static class Program
    {
        public static FCCZipService Service;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service = new FCCZipService();
//#if DEBUG
//            Service.OnDebug();
//#else
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FCCZipService()
            };
            ServiceBase.Run(ServicesToRun);

//#endif
        }
    }
}
