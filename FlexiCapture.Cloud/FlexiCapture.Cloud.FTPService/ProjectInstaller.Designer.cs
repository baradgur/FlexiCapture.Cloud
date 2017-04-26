namespace FlexiCapture.Cloud.FTPService
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
            this.FTPProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FTPServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // FTPProcessInstaller
            // 
            this.FTPProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.FTPProcessInstaller.Password = null;
            this.FTPProcessInstaller.Username = null;
            // 
            // FTPServiceInstaller
            // 
            this.FTPServiceInstaller.Description = "FlexiCapture Cloud FTP Service";
            this.FTPServiceInstaller.DisplayName = "FCC FTP Service";
            this.FTPServiceInstaller.ServiceName = "FCC_FtpService";
            this.FTPServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FTPProcessInstaller,
            this.FTPServiceInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller FTPProcessInstaller;
        public System.ServiceProcess.ServiceInstaller FTPServiceInstaller;
    }
}