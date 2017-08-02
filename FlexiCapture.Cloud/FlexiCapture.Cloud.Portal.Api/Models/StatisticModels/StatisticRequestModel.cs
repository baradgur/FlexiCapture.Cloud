using System;
using System.Collections.Generic;

namespace FlexiCapture.Cloud.Portal.Api.Models.StatisticModels
{
   
    /// <summary>
    ///     model
    /// </summary>
    public class StatisticRequestModel
    {
        #region constructor
        /// <summary>
        /// constructor without params
        /// </summary>
        public StatisticRequestModel()
        {
            try
            {
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        ///     constructor
        /// </summary>
        public StatisticRequestModel(int param)
        {
            try
            {
                StatisticDateRanges = new List<StatisticDateRangeModel>
                {
                    new StatisticDateRangeModel {Id = 1, Name = "Day"},
                    new StatisticDateRangeModel {Id = 2, Name = "Week"},
                    new StatisticDateRangeModel {Id = 3, Name = "Month"},
                    new StatisticDateRangeModel {Id = 4, Name = "Date Range"}
                };

                StatisticTypes = new List<StatisticTypeModel>
                {
                    new StatisticTypeModel {Id = 1, Name = "New registered users"},
                    new StatisticTypeModel {Id = 2, Name = "OCR processing statistics"},
                    new StatisticTypeModel {Id = 3, Name = "Dollar amount received"}
                };

                DateRange = StatisticDateRanges[0];
                StatisticType = StatisticTypes[0];
                StartDate = DateTime.Now.ToString("yyyy-MM-dd");
                EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception e)
            {
            }
        }

        #endregion

        #region fields

        /// <summary>
        ///     statistic types
        /// </summary>
        public List<StatisticTypeModel> StatisticTypes { get; set; }

        /// <summary>
        ///     statistic types
        /// </summary>
        public List<StatisticDateRangeModel> StatisticDateRanges { get; set; }


        /// <summary>
        ///     type range
        /// </summary>
        public StatisticDateRangeModel DateRange { get; set; }

        /// <summary>
        ///     statistic type
        /// </summary>
        public StatisticTypeModel StatisticType { get; set; }

        /// <summary>
        ///     start date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        ///     end date
        /// </summary>
        public string EndDate { get; set; }

        #endregion
    }
}