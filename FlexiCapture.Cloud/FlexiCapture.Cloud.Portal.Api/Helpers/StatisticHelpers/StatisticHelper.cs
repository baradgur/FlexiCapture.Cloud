using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.StatisticModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.StatisticHelpers
{
    public static class StatisticHelper
    {
        /// <summary>
        /// get to default settings
        /// </summary>
        /// <returns></returns>
        public static string GetToDefaultSettings()
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(new StatisticRequestModel(1));
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }

        /// <summary>
        /// show statistic
        /// </summary>
        /// <returns></returns>
        public static string ShowStatistic(StatisticRequestModel model)
        {
            try
            {
                string data = "";
                switch (model.StatisticType.Id)
                {
                    case 1:
                        data = NewUsersStatisticHelper.GenerateStatistics(model);
                        break;

                    case 2:
                        data = DocumentsStatisticHelper.GenerateStatistics(model);
                        break;
                }

                return data;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }
        }
    }
}