using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.Interfaces;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    public class POPConnectionProtocolModel:IEmailProtocol
    {
        /// <summary>
        /// server
        /// </summary>
        public string Server { get; set; }
        
        /// <summary>
        /// port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// use ssl
        /// </summary>
        public bool UseSSL { get; set; }
    }
}
