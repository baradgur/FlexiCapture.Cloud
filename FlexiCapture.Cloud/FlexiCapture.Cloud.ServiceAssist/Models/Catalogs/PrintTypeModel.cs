using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.ServiceAssist.Models.Catalogs
{
    /// <summary>
    /// print type VM
    /// </summary>
    public class PrintTypeModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public PrintTypeModel()
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
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}