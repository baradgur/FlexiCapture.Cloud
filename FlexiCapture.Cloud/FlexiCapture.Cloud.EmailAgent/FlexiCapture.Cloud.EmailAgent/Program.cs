using System.ServiceProcess;

namespace FlexiCapture.Cloud.EmailAgent
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>

        public static FCCEmailAgent Agent;
        private static void Main()
        {
            Agent = new FCCEmailAgent();
//#if DEBUG
//          //execute operations in debug mode
            //Agent = new FCCEmailAgent();
//            Agent.OnDebug();
//#else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    Agent 
                };
                ServiceBase.Run(ServicesToRun);
//#endif
        }
    }
}