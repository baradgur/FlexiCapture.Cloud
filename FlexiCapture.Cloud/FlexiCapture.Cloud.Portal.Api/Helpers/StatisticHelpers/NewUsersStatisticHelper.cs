using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FlexiCapture.Cloud.Portal.Api.Models.StatisticModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.StatisticHelpers
{
    public static class NewUsersStatisticHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateStatistics(StatisticRequestModel model)
        {
            try
            {
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;
                switch (model.DateRange.Id)
                {
                    //day
                    case 1:
                        startDate = startDate.AddDays(-1);
                        break;

                    //week
                    case 2:
                        startDate = startDate.AddDays(-7);
                        break;
                    //month
                    case 3:
                        startDate = startDate.AddMonths(-1);
                        break;
                    //range    
                    case 4:
                        startDate = DateTime.Parse(model.StartDate);
                        endDate = DateTime.Parse(model.EndDate);
                        break;
                }

                if (startDate > endDate) return "";
                List<DB.UserLogins> logins = DBHelpers.StatisticHelper.GetToUserRegistrationStatistics(startDate, endDate);

                List<DateValueModel> vals = new List<DateValueModel>();

                DateTime cTime = startDate;
                while (cTime <= endDate)
                {
                    vals.Add(new DateValueModel() { Count = 0, Date = cTime });
                    cTime = cTime.AddDays(1);
                }

                foreach (var login in logins)
                {
                    for (int i = 0; i < vals.Count; i++)
                    {
                        if (login.RegistrationDate!=null)
                        if (login.RegistrationDate.Value.ToShortDateString().Equals( vals[i].Date.ToShortDateString()))
                        {
                            vals[i].Count++;
                        }
                    }
                }

                List<object> axis = new List<object>()
                {
                    "Date",
                    "Count"
                };

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return serializer.Serialize(new ChartModel(axis, vals));
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}