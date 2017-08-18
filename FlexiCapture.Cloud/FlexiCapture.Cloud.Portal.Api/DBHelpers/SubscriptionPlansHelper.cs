using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.Models.SubscriptionPlanModels;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    public class SubscriptionPlansHelper
    {
        public static SubscriptionPlansViewModel GetSubscriptionPlansView()
        {
            try
            {
                SubscriptionPlansViewModel model = new SubscriptionPlansViewModel();

                model.Plans = GetSubsriptionPlans(true);
                model.States = GetSubscriptionStates();
                model.Types = GetSubscriptionTypes();

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static SubscriptionPlanUsesViewModel GetSubscriptionPlanUsesView(int userId)
        {
            try
            {
                SubscriptionPlanUsesViewModel model = new SubscriptionPlanUsesViewModel();

                model.Plans = GetSubsriptionPlans(false);
                model.PlanUses = GetSubscriptionPlanUses(userId, true);

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        private static List<SubscriptionPlanUseModel> GetSubscriptionPlanUses(int userId, bool IsActiveOrBlocked)
        {
            try
            {
                List<SubscriptionPlanUseModel> model = new List<SubscriptionPlanUseModel>();

                using (var db = new FCCPortalEntities())
                {
                    var plans = db.SubscriptionPlanUses
                        .Include(x => x.SubscriptionPlanUseStates)
                        .Include(x => x.SubscriptionPlans)
                        .Include(x => x.SubscriptionPlans1)
                        .Where(x => x.UserId == userId && (!IsActiveOrBlocked || x.SubscriptionPlanUseStateId == (int)Models.Enums.SubscriptionPlanUseStates.Active
                                                            || x.SubscriptionPlanUseStateId == (int)Models.Enums.SubscriptionPlanUseStates.Blocked));

                    foreach (var plan in plans)
                    {
                        SubscriptionPlanUseModel mPlan = new SubscriptionPlanUseModel()
                        {
                            Id = plan.Id,
                            DateExpiry = plan.DateExpiry,
                            NextSubscriptionPlanId = plan.NextSubscriptionPlanId,
                            NextSubscriptionPlanName = plan.SubscriptionPlans1 != null ? plan.SubscriptionPlans1.Name : "",
                            PagesCount = plan.PagesCount,
                            SubscriptionPlanId = plan.SubscriptionPlanId,
                            SubscriptionPlanName = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.Name : "",
                            SubscriptionPlanUseStateId = plan.SubscriptionPlanUseStateId,
                            SubscriptionPlanUseStateName = plan.SubscriptionPlanUseStates.Name,
                            CostPerPage = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.CostPerPage : 0,
                            PagesMax = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.PagesInInterval : 0,
                            SubscriptionPlanTypeId = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.SubscriptionPlanTypeId : 0,
                            IsDefault = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.IsDefault : false,
                            UserId = plan.UserId,
                            AltPagesCount = plan.AltPagesCount,
                            PaidAltPagesCount = plan.PaidAltPagesCount,
                            AltCostPerPage = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.AltCostPerPage: null,
                        };
                        model.Add(mPlan);
                    }
                }

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static List<SubscriptionPlanTypeModel> GetSubscriptionTypes()
        {
            try
            {
                List<SubscriptionPlanTypeModel> model = new List<SubscriptionPlanTypeModel>();

                using (var db = new FCCPortalEntities())
                {
                    var types = db.SubscriptionPlanTypes;

                    foreach (var type in types)
                    {
                        SubscriptionPlanTypeModel mType = new SubscriptionPlanTypeModel()
                        {
                            Id = type.Id,
                            Name = type.Name
                        };
                        model.Add(mType);
                    }
                }

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static List<SubscriptionPlanStateModel> GetSubscriptionStates()
        {
            try
            {
                List<SubscriptionPlanStateModel> model = new List<SubscriptionPlanStateModel>();

                using (var db = new FCCPortalEntities())
                {
                    var states = db.SubscriptionPlanStates;

                    foreach (var state in states)
                    {
                        SubscriptionPlanStateModel mState = new SubscriptionPlanStateModel()
                        {
                            Id = state.Id,
                            Name = state.Name
                        };
                        model.Add(mState);
                    }
                }

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static List<SubscriptionPlanModel> GetSubsriptionPlans(bool getAll)
        {
            try
            {
                List<SubscriptionPlanModel> model = new List<SubscriptionPlanModel>();

                using (var db = new FCCPortalEntities())
                {
                    var plans = db.SubscriptionPlans
                        .Include(x => x.SubscriptionPlanStates)
                        .Include(x => x.SubscriptionPlanTypes)
                        .Where(x => (getAll || x.SubscriptionPlanStateId == (int)Models.Enums.SubscriptionPlanStates.Active));

                    foreach (var plan in plans)
                    {
                        SubscriptionPlanModel mPlan = new SubscriptionPlanModel()
                        {
                            Id = plan.Id,
                            Name = plan.Name,
                            CostPerPage = plan.CostPerPage,
                            Expiration = plan.Expiration,
                            IsDefault = plan.IsDefault,
                            PagesInInterval = plan.PagesInInterval,
                            SubscriptionPlanStateId = plan.SubscriptionPlanStateId,
                            SubscriptionPlanTypeId = plan.SubscriptionPlanTypeId,
                            SubscriptionPlanStateName = plan.SubscriptionPlanStates.Name,
                            SubscriptionPlanTypeName = plan.SubscriptionPlanTypes.Name,
                            AltCostPerPage = plan.AltCostPerPage
                        };
                        model.Add(mPlan);
                    }
                }

                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static SubscriptionPlanModel AddPlan(SubscriptionPlanModel model)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    int? expiration =
                    model.SubscriptionPlanTypeId == (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase
                        ? model.Expiration
                        : null;

                    int? altCost =
                    model.SubscriptionPlanTypeId != (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase
                        ? model.AltCostPerPage
                        : null;

                    var dbPlan = new DB.SubscriptionPlans()
                    {
                        PagesInInterval = model.PagesInInterval,
                        SubscriptionPlanStateId = model.SubscriptionPlanStateId,
                        SubscriptionPlanTypeId = model.SubscriptionPlanTypeId,
                        CostPerPage = model.CostPerPage,
                        Expiration = expiration,
                        Name = model.Name,
                        AltCostPerPage = altCost
                    };

                    db.SubscriptionPlans.Add(dbPlan);
                    db.SaveChanges();

                    model = GetSubsriptionPlan(dbPlan.Id);
                }
                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }

        public static SubscriptionPlanModel UpdatePlan(SubscriptionPlanModel model)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    int? expiration =
                    model.SubscriptionPlanTypeId == (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase
                        ? model.Expiration
                        : null;

                    int? altCost =
                    model.SubscriptionPlanTypeId != (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase
                        ? model.AltCostPerPage
                        : null;

                    int state = model.IsDefault ? 1 : model.SubscriptionPlanStateId;

                    var dbPlan = db.SubscriptionPlans.FirstOrDefault(x => x.Id == model.Id);

                    if (dbPlan == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    dbPlan.PagesInInterval = model.PagesInInterval;
                    dbPlan.SubscriptionPlanStateId = state;
                    dbPlan.SubscriptionPlanTypeId = model.SubscriptionPlanTypeId;
                    dbPlan.CostPerPage = model.CostPerPage;
                    dbPlan.Expiration = expiration;
                    dbPlan.Name = model.Name;
                    dbPlan.AltCostPerPage = altCost;

                    db.SaveChanges();

                    model = GetSubsriptionPlan(dbPlan.Id);
                }
                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }

        public static SubscriptionPlanModel GetSubsriptionPlan(int id)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var plan = db.SubscriptionPlans
                        .Include(x => x.SubscriptionPlanStates)
                        .Include(x => x.SubscriptionPlanTypes)
                        .FirstOrDefault(x => x.Id == id);

                    if (plan == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    SubscriptionPlanModel model = new SubscriptionPlanModel()
                    {
                        Id = plan.Id,
                        Name = plan.Name,
                        CostPerPage = plan.CostPerPage,
                        Expiration = plan.Expiration,
                        IsDefault = plan.IsDefault,
                        PagesInInterval = plan.PagesInInterval,
                        SubscriptionPlanStateId = plan.SubscriptionPlanStateId,
                        SubscriptionPlanTypeId = plan.SubscriptionPlanTypeId,
                        SubscriptionPlanStateName = plan.SubscriptionPlanStates.Name,
                        SubscriptionPlanTypeName = plan.SubscriptionPlanTypes.Name,
                        AltCostPerPage = plan.AltCostPerPage,
                    };
                    return model;
                }


            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }


        public static SubscriptionPlanUseModel AddPlanUse(SubscriptionPlanUseModel model)
        {
            try
            {
                var inUsePlans = GetSubscriptionPlanUses(model.UserId, true);
                //blocked and active plans can't go together
                var currentPlan = inUsePlans.FirstOrDefault();
                DateTime? dateExpiry = null;
                using (var db = new FCCPortalEntities())
                {
                    var dbPlan = db.SubscriptionPlans.FirstOrDefault(x => x.Id == model.SubscriptionPlanId);

                    if (dbPlan == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    switch (dbPlan.SubscriptionPlanTypeId)
                    {
                        case (int)Models.Enums.SubscriptionPlanTypes.OneTimePurchase:
                            dateExpiry = DateTime.Today.AddDays(dbPlan.Expiration ?? 1);
                            break;

                        case (int)Models.Enums.SubscriptionPlanTypes.Monthly:
                            dateExpiry = DateTime.Today.AddMonths(1);
                            break;

                        case (int)Models.Enums.SubscriptionPlanTypes.Annual:
                            dateExpiry = DateTime.Today.AddYears(1);
                            break;
                    }

                    int pagesCount = currentPlan != null ? currentPlan.AltPagesCount ?? 0 : 0;
                    int? altPagesCount = null;
                    int planUseState = (int)Models.Enums.SubscriptionPlanUseStates.Active;

                    if (pagesCount > dbPlan.PagesInInterval)
                    {
                        altPagesCount = pagesCount - dbPlan.PagesInInterval;
                        pagesCount = dbPlan.PagesInInterval;
                        planUseState = (int)Models.Enums.SubscriptionPlanUseStates.Blocked;
                    }

                    var dbPlanUse = new DB.SubscriptionPlanUses()
                    {
                        UserId = model.UserId,
                        SubscriptionPlanId = dbPlan.Id,
                        PagesCount = pagesCount,
                        NextSubscriptionPlanId = null,
                        DateExpiry = dateExpiry,
                        SubscriptionPlanUseStateId = planUseState,
                        AltPagesCount = altPagesCount,
                        PaidAltPagesCount = null
                    };

                    db.SubscriptionPlanUses.Add(dbPlanUse);
                    db.SaveChanges();
                    if (currentPlan != null) { 
                    currentPlan.AltPagesCount = null;
                    currentPlan.SubscriptionPlanUseStateId = (int) Models.Enums.SubscriptionPlanUseStates.Disabled;
                        UpdatePlanUse(currentPlan);
                    }


                    model = GetSubscriptionPlanUse(dbPlanUse.Id);
                }
                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }

        public static SubscriptionPlanUseModel UpdatePlanUse(SubscriptionPlanUseModel model)
        {
            try
            {

                using (var db = new FCCPortalEntities())
                {
                    var dbPlanUse = db.SubscriptionPlanUses.FirstOrDefault(x => x.Id == model.Id);

                    if (dbPlanUse == null)
                    {
                        throw new ObjectNotFoundException();
                    }

                    dbPlanUse.NextSubscriptionPlanId = model.NextSubscriptionPlanId;
                    dbPlanUse.PagesCount = model.PagesCount >= dbPlanUse.PagesCount ? model.PagesCount : dbPlanUse.PagesCount;
                    dbPlanUse.SubscriptionPlanUseStateId = model.SubscriptionPlanUseStateId;
                    dbPlanUse.AltPagesCount = model.AltPagesCount;

                    db.SaveChanges();

                    model = GetSubscriptionPlanUse(dbPlanUse.Id);
                }
                return model;
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }
        }

        private static SubscriptionPlanUseModel GetSubscriptionPlanUse(int useId)
        {
            try
            {
               using (var db = new FCCPortalEntities())
                {
                    var plan = db.SubscriptionPlanUses
                        .Include(x => x.SubscriptionPlanUseStates)
                        .Include(x => x.SubscriptionPlans)
                        .Include(x => x.SubscriptionPlans1)
                        .FirstOrDefault(x => x.Id == useId);

                    if (plan == null)
                    {
                        throw new ObjectNotFoundException();
                    }
                        SubscriptionPlanUseModel mPlan = new SubscriptionPlanUseModel()
                        {
                            Id = plan.Id,
                            DateExpiry = plan.DateExpiry,
                            NextSubscriptionPlanId = plan.NextSubscriptionPlanId,
                            NextSubscriptionPlanName = plan.SubscriptionPlans1 != null ? plan.SubscriptionPlans1.Name : "",
                            PagesCount = plan.PagesCount,
                            SubscriptionPlanId = plan.SubscriptionPlanId,
                            SubscriptionPlanName = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.Name : "",
                            SubscriptionPlanUseStateId = plan.SubscriptionPlanUseStateId,
                            SubscriptionPlanUseStateName = plan.SubscriptionPlanUseStates.Name,
                            CostPerPage = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.CostPerPage : 0,
                            PagesMax = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.PagesInInterval : 0,
                            SubscriptionPlanTypeId = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.SubscriptionPlanTypeId : 0,
                            IsDefault = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.IsDefault : false,
                            UserId = plan.UserId,
                            AltPagesCount = plan.AltPagesCount,
                            PaidAltPagesCount = plan.PaidAltPagesCount,
                            AltCostPerPage = plan.SubscriptionPlans != null ? plan.SubscriptionPlans.AltCostPerPage : null,
                        };
                    return mPlan;
                }
                
        
                
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }
    }
}