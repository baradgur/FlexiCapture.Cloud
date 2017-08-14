using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    class SubscriptionsHelper
    {
        public static void CheckSubscribes()
        {
            try
            {
                List<DB.SubscriptionPlanUses> uses = new List<SubscriptionPlanUses>();
                using (var db = new FCCPortalEntities2())
                {
                    // checking active subscribes
                    var dbSubscribes = db.SubscriptionPlanUses
                        .Include(x=>x.SubscriptionPlans)
                        .Include(x => x.SubscriptionPlans1)
                        .Where(x => x.SubscriptionPlanUseStateId == (int)Models.Enums.SubscriptionPlanUseStates.Active);

                    foreach (var dbPlanUse in dbSubscribes)
                    {
                        bool isExpired = dbPlanUse.DateExpiry.HasValue && dbPlanUse.DateExpiry.Value < DateTime.Today;
                        bool isNewPlanNeeded = false;
                        switch (dbPlanUse.SubscriptionPlans.SubscriptionPlanTypeId)
                        {
                            case (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase:
                                if (isExpired)
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                    else
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Disabled;
                                    }
                                }
                                else
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                    else
                                    {
                                        if (dbPlanUse.SubscriptionPlans.PagesInInterval == dbPlanUse.PagesCount)
                                        {
                                            dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Disabled;
                                        }
                                    }
                                }

                                break;

                            case (int)Models.Enums.SubscriptionPlanTypes.Monthly:
                                if (isExpired)
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                    else
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Disabled;
                                        isNewPlanNeeded = true;
                                    }
                                }
                                else
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        //check payments here if can't pay block use of the plan
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                }

                                break;

                            case (int)Models.Enums.SubscriptionPlanTypes.Annual:
                                if (isExpired)
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                    else
                                    {
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Disabled;
                                        isNewPlanNeeded = true;
                                    }
                                }
                                else
                                {
                                    if (dbPlanUse.AltPagesCount.HasValue && dbPlanUse.AltPagesCount.Value > 0)
                                    {
                                        //check payments here if can't pay block use of the plan
                                        dbPlanUse.SubscriptionPlanUseStateId =
                                            (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                                    }
                                }

                                break;
                        }
                        if (isNewPlanNeeded)
                        {
                            // add another plan
                            uses.Add(dbPlanUse);
                        }
                    }
                    db.SaveChanges();
                    foreach (var use in uses)
                    {
                        SubscriptionsHelper.AddPlan(use);
                    }
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
            }
        }

        private static void AddPlan(SubscriptionPlanUses dbPlanUse)
        {
            try
            {
                DateTime? dateExpiry = null;
                using (var db = new FCCPortalEntities2())
                {
                    var currentSubscription = dbPlanUse.SubscriptionPlans1 != null
                        ? dbPlanUse.SubscriptionPlans1
                        : dbPlanUse.SubscriptionPlans;

                    switch (currentSubscription.SubscriptionPlanTypeId)
                    {
                        case (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase:
                            dateExpiry = DateTime.Today.AddDays(currentSubscription.Expiration ?? 1);
                            break;

                        case (int)Models.Enums.SubscriptionPlanTypes.Monthly:
                            dateExpiry = DateTime.Today.AddMonths(1);
                            break;

                        case (int)Models.Enums.SubscriptionPlanTypes.Annual:
                            dateExpiry = DateTime.Today.AddYears(1);
                            break;
                    }

                    var dbPlan = new DB.SubscriptionPlanUses()
                    {
                        UserId = dbPlanUse.UserId,
                        SubscriptionPlanId = currentSubscription.Id,
                        PagesCount = 0,
                        NextSubscriptionPlanId = null,
                        DateExpiry = dateExpiry,
                        SubscriptionPlanUseStateId = (int)Models.Enums.SubscriptionPlanUseStates.Active,
                        AltPagesCount = null,
                        PaidAltPagesCount = null
                    };

                    db.SubscriptionPlanUses.Add(dbPlan);
                    db.SaveChanges();
                }
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
