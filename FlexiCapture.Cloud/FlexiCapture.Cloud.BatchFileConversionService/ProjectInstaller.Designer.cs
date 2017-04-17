namespace FlexiCapture.Cloud.BatchFileConversionService
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
            this.batchFileConversionServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.batchFileConversionServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // batchFileConversionServiceProcessInstaller
            // 
            this.batchFileConversionServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.batchFileConversionServiceProcessInstaller.Password = null;
            this.batchFileConversionServiceProcessInstaller.Username = null;
            // 
            // batchFileConversionServiceInstaller
            // 
            this.batchFileConversionServiceInstaller.Description = "FlexiCapture Cloud Batch File Conversion Service";
            this.batchFileConversionServiceInstaller.DisplayName = "FCC Batch File Conversion Service";
            this.batchFileConversionServiceInstaller.ServiceName = "FCC_BatchFileConversionService";
            this.batchFileConversionServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.batchFileConversionServiceProcessInstaller,
            this.batchFileConversionServiceInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller batchFileConversionServiceProcessInstaller;
        public System.ServiceProcess.ServiceInstaller batchFileConversionServiceInstaller;
    }
}