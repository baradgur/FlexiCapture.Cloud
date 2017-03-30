using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Models
{
    public class QueuePackageTaskModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public QueuePackageTaskModel()
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
        /// send date time
        /// </summary>
        public DateTime? DeliveryDateTime { get; set; }
        #endregion
    }
}
