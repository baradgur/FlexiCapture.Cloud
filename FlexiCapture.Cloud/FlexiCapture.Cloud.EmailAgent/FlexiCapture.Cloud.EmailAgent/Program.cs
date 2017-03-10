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
            #if DEBUG
            //execute operations in debug mode
            Agent = new FCCEmailAgent();
            Agent.OnDebug();
            #else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new FCCEmailAgent() 
                };
                ServiceBase.Run(ServicesToRun);
            #endif
        }
    }
}