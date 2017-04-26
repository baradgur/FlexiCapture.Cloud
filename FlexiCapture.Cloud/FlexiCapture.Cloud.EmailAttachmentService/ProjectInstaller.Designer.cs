namespace FlexiCapture.Cloud.EmailAttachmentService
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
            this.FCCEmailAttachmentServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FCCEmailAttachmentServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // FCCEmailAttachmentServiceProcessInstaller
            // 
            this.FCCEmailAttachmentServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.FCCEmailAttachmentServiceProcessInstaller.Password = null;
            this.FCCEmailAttachmentServiceProcessInstaller.Username = null;
            // 
            // FCCEmailAttachmentServiceInstaller
            // 
            this.FCCEmailAttachmentServiceInstaller.Description = "FlexiCapture Cloud Email Attachment Service";
            this.FCCEmailAttachmentServiceInstaller.DisplayName = "FCC Email Attachment Service";
            this.FCCEmailAttachmentServiceInstaller.ServiceName = "FCC_EmailAttachmentService";
            this.FCCEmailAttachmentServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FCCEmailAttachmentServiceProcessInstaller,
            this.FCCEmailAttachmentServiceInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller FCCEmailAttachmentServiceProcessInstaller;
        public System.ServiceProcess.ServiceInstaller FCCEmailAttachmentServiceInstaller;
    }
}