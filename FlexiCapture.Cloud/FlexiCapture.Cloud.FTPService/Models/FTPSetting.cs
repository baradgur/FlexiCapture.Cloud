using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.FTPService.Models
{
    public class FTPSetting
    {
        #region fields
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
        public bool UseSsl { get; set; }
        public bool DeleteFile { get; set; }
        public bool Enabled { get; set; }
        public int ServiceType { get; set; }
        #endregion

        public FTPSetting(int id, int userId, string userName, string host, int port, string password,
            string path, bool useSsl, bool deleteFile, bool enabled, int serviceType)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            Host = host;
            Port = port;
            Password = password;
            Path = path;
            UseSsl = useSsl;
            DeleteFile = deleteFile;
            Enabled = enabled;
            ServiceType = serviceType;
        }

    }
}
