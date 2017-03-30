using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Interfaces
{
    /// <summary>
    /// interface for email connections
    /// </summary>
    public interface IEmailProtocol
    {
        /// <summary>
        /// port address 
        /// </summary>
        string Server { get; set; }
        /// <summary>
        /// port for connect
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// use ssl connections
        /// </summary>
        bool UseSSL { get; set; }

    }
}
