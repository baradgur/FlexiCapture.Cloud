using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    /// <summary>
    /// user model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// user email
        /// </summary>
        public string UserEmail { get; set; }
    }
}
