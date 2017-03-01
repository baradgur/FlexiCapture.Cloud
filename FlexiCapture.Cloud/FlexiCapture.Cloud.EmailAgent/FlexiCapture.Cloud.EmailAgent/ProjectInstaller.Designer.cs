namespace FlexiCapture.Cloud.EmailAgent
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FCCEmailAgentProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FCCEmailAgentInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // FCCEmailAgentProcessInstaller
            // 
            this.FCCEmailAgentProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.FCCEmailAgentProcessInstaller.Password = null;
            this.FCCEmailAgentProcessInstaller.Username = null;
            // 
            // FCCEmailAgentInstaller
            // 
            this.FCCEmailAgentInstaller.Description = "Flexi Capture Email Agent";
            this.FCCEmailAgentInstaller.DisplayName = "FCC Email Agent";
            this.FCCEmailAgentInstaller.ServiceName = "FCC Email Agent";
            this.FCCEmailAgentInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FCCEmailAgentProcessInstaller,
            this.FCCEmailAgentInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller FCCEmailAgentProcessInstaller;
        public System.ServiceProcess.ServiceInstaller FCCEmailAgentInstaller;
    }
}