﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    class SubscriptionPlansHelper
    {
        public static string CheckSubscriptionPlan(int userId, int pagesInTask)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    int parentId = 0;
                    string parentEmail = "";
                    var user = db.Users
                        .Include(x => x.Users2)
                        .FirstOrDefault(x => x.Id == userId);

                    if (user == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    if (user.Users2 == null)
                    {
                        parentId = user.Id;
                        parentEmail = user.Email;
                    }
                    else
                    {
                        parentId = user.Users2.Id;
                        parentEmail = user.Users2.Email;
                    }

                    var dbPlanUse = db.SubscriptionPlanUses
                        .Include(x => x.SubscriptionPlanUseStates)
                        .Include(x => x.SubscriptionPlans)
                        .Include(x => x.SubscriptionPlans1)
                        .FirstOrDefault(x => x.UserId == parentId
                        && x.SubscriptionPlanUseStateId== (int)Models.Enums.SubscriptionPlanUseStates.Active);

                    if (dbPlanUse == null)
                    {
                        string responseText =
                                "DataCapture.Cloud received a conversion request, but no active subscription " +
                                "was found.";
                        EmailHelper.SendEmailResponseFail(parentEmail, responseText, "");
                        return "No active subscription found";
                    }

                    if (dbPlanUse.SubscriptionPlans.PagesInInterval >= dbPlanUse.PagesCount + pagesInTask)
                    {
                        dbPlanUse.PagesCount += pagesInTask;
                        db.SaveChanges();
                    }
                    else
                    {
                        if (dbPlanUse.SubscriptionPlans1 == null)
                        {
                            string responseText =
                                "DataCapture.Cloud received a conversion request, but you subscription doesn't " +
                                "have enough pages in current subscription to perform conversion.";
                            EmailHelper.SendEmailResponseFail(parentEmail, responseText,"");
                            return "Not enough pages in current subscription";
                        }
                        else
                        {
                            DateTime? dateExpiry = null;
                            switch (dbPlanUse.SubscriptionPlans1.SubscriptionPlanTypeId)
                            {
                                case (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase:
                                    dateExpiry = DateTime.Today.AddDays(dbPlanUse.SubscriptionPlans1.Expiration ?? 1);
                                    break;

                                case (int)Models.Enums.SubscriptionPlanTypes.Monthly:
                                    dateExpiry = DateTime.Today.AddMonths(1);
                                    break;

                                case (int)Models.Enums.SubscriptionPlanTypes.Annual:
                                    dateExpiry = DateTime.Today.AddYears(1);
                                    break;
                            }
                            int pagesLeft = dbPlanUse.PagesCount + pagesInTask -
                                            dbPlanUse.SubscriptionPlans.PagesInInterval;
                            dbPlanUse.PagesCount = dbPlanUse.SubscriptionPlans.PagesInInterval;
                            dbPlanUse.SubscriptionPlanUseStateId = (int)Models.Enums.SubscriptionPlanUseStates.Disabled;
                            var addDbPlanUse = new DB.SubscriptionPlanUses()
                            {
                                UserId = parentId,
                                SubscriptionPlanId = dbPlanUse.SubscriptionPlans1.Id,
                                PagesCount = pagesLeft,
                                NextSubscriptionPlanId = null,
                                DateExpiry = dateExpiry,
                                SubscriptionPlanUseStateId = (int)Models.Enums.SubscriptionPlanUseStates.Active,
                            };
                            db.SubscriptionPlanUses.Add(addDbPlanUse);
                            db.SaveChanges();
                        }
                    }
                }

                return "OK";

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return "Exception occured, while checking subscription availability";
            }
        }
    }
}
