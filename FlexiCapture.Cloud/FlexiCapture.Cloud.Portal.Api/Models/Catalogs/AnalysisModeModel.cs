using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.Catalogs
{
    /// <summary>
    /// language VM
    /// </summary>
    public class AnalysisModeModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public AnalysisModeModel()
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