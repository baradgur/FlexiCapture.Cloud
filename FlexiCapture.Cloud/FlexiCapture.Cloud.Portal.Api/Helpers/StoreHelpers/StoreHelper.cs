using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            catch (Exception)
            {
            }
        }
    }
}