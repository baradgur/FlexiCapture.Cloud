using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using System.Data.Entity;
namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public static class StatisticHelper
    {
        public static List<DB.UserLogins> GetToUserRegistrationStatistics(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var db =new FCCPortalEntities())
                {
                    return db.UserLogins
                        .Where(x => x.RegistrationDate >= startDate && x.RegistrationDate <= endDate)
                        .ToList();
                    
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<DB.Documents> GetToDocumentsStatistic(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.Documents
                        .Where(x => x.Date >= startDate && x.Date <= endDate)
                        .ToList();

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}