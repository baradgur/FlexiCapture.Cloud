namespace FlexiCapture.Cloud.ZipService
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
            this.FCCZipServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FCCZipServiceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // FCCZipServiceProcessInstaller
            // 
            this.FCCZipServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.FCCZipServiceProcessInstaller.Password = null;
            this.FCCZipServiceProcessInstaller.Username = null;
            // 
            // FCCZipServiceInstaller1
            // 
            this.FCCZipServiceInstaller1.Description = "FCC Zip Service";
            this.FCCZipServiceInstaller1.DisplayName = "FCC Zip Service";
            this.FCCZipServiceInstaller1.ServiceName = "FCCZipService";
            this.FCCZipServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FCCZipServiceProcessInstaller,
            this.FCCZipServiceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller FCCZipServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller FCCZipServiceInstaller1;
    }
}