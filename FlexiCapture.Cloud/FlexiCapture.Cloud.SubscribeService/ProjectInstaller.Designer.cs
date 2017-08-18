namespace FlexiCapture.Cloud.SubscribeService
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
            this.SBSProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SBSInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SBSProcessInstaller
            // 
            this.SBSProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SBSProcessInstaller.Password = null;
            this.SBSProcessInstaller.Username = null;
            // 
            // SBSInstaller
            // 
            this.SBSInstaller.Description = "FlexiCapture Cloud Subscribe Service";
            this.SBSInstaller.DisplayName = "FCC Subscribe Service";
            this.SBSInstaller.ServiceName = "FCC_SubscribeService";
            this.SBSInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SBSProcessInstaller,
            this.SBSInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SBSProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SBSInstaller;
    }
}
