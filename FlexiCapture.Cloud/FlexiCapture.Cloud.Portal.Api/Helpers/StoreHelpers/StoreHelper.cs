using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.StoreModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.StoreHelpers
{
    public static  class StoreHelper
    {
        /// <summary>
        /// set service state
        /// </summary>
        public static void SetServiceState(StoreModel model)
        {
            try
            {
                DBHelpers.StoreHelper.SetStoreState(model);
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
            }
        }
    }
}