﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAttachmentService.Models
{
    /// <summary>
    /// IMAP Protocol
    /// </summary>
    public class IMAPConnectionProtocolModel
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
        /// <summary>
        /// default folder for read
        /// </summary>
        public string DefaultFolder { get; set; }
    }
}
