using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    /// <summary>
    /// queue model
    /// </summary>
    public class QueueModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public QueueModel()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// queue id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// content of queue
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// State Id
        /// </summary>
        public int StateId { get; set; }

        
        #endregion
    }
}
