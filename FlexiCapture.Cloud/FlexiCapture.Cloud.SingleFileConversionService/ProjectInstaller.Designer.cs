namespace FlexiCapture.Cloud.SingleFileConversionService
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
            this.SFCProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SFCInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SFCProcessInstaller
            // 
            this.SFCProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SFCProcessInstaller.Password = null;
            this.SFCProcessInstaller.Username = null;
            // 
            // SFCInstaller
            // 
            this.SFCInstaller.Description = "FlexiCapture Cloud File Single Conversion Service";
            this.SFCInstaller.DisplayName = "FCC Single File Conversion Service";
            this.SFCInstaller.ServiceName = "FCC_SingleFileConversionService";
            this.SFCInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SFCProcessInstaller,
            this.SFCInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SFCProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SFCInstaller;
    }
}