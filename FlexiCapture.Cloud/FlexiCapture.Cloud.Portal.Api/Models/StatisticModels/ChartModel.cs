using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Helpers.StatisticHelpers;

namespace FlexiCapture.Cloud.Portal.Api.Models.StatisticModels
{
    public class ChartModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ChartModel(List<object> axis, List<DateValueModel> model)
        {
            try
            {
                ChartData = new List<List<object>>();
                ChartData.Add(axis);
                foreach (var value in model)
                {
                    ChartData.Add(new List<object>() {value.Date.ToShortDateString(),value.Count});
                }
            }
            catch (Exception e)
            {
            }
        }
        public List<List<object>> ChartData { get; set; }
    }
}