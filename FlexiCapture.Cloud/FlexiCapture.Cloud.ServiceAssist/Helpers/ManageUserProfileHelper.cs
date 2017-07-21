using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Models.Catalogs;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;
using System.Data.Entity;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;

namespace FlexiCapture.Cloud.ServiceAssist.Helpers
{
    public static class ManageUserProfileHelper
    {
        /// <summary>
        /// get to profile by id
        /// </summary>
        /// <returns></returns>
        public static ManageUserProfileModel GetToUserProfileById(int profileId, int serviceId=-1)
        {
            try
            {
                ManageUserProfileModel model = new ManageUserProfileModel();

                UserProfiles profile = DBHelpers.ManageUserProfileHelper.GetToUserProfileById(profileId);

                model.Name = profile.Name;
                model.Id = profileId;
                model.Deskew = profile.Deskew;
                model.JpegQuality = profile.JpegQuality ?? "";
                model.LookForBarCodes = profile.LookForBarcodes;
                model.RemoveGarbage = profile.RemoveGarbage;
                model.RemoveTexture = profile.RemoveTexture;
                model.Resolution = profile.Resolution ?? "";
                model.OutputFormat = profile.OutputFormat ?? "" ;
                model.SelectedAnalysisModel = new AnalysisModeModel()
                {
                    Id = profile.AnalysisModeCatalog.Id,
                    Name = profile.AnalysisModeCatalog.Name
                };

                model.SelectedAnalysisModelId = model.SelectedAnalysisModel.Id;
                model.SelectedRotationType = new RotationTypeModel()
                {
                    Id = profile.RotationTypeCatalog.Id,
                    Name = profile.RotationTypeCatalog.Name
                };

                model.SpeedOcr = profile.SpeedOcr;
                model.SplitDualPage = profile.SplitDualPage;
                model.UserId = profile.UserId;
                var defaultServiceId = profile.UserProfileServiceDefault.FirstOrDefault(x => x.ServiceTypeId == serviceId && x.UserProfileId == profileId);

                model.DefaultServiceId = -1;

                if (defaultServiceId != null)
                {
                    model.DefaultServiceId = defaultServiceId.ServiceTypeId;
                }
                foreach (var format in profile.UserProfileExportFormats)
                {
                    model.SelectedExportFormats.Add(new ExportFormatModel() {Id =format.ExportFormatsCatalog.Id, Name = format.ExportFormatsCatalog.Name});
                }

                foreach (var language in profile.UserProfileLanguages)
                {
                    model.SelectedLanguages.Add(new LanguageModel() { Id = language.LanguagesCatalog.Id, Name = language.LanguagesCatalog.Name});
                }

                foreach (var printType in profile.UserProfilePrintTypes)
                {
                    model.SelectedPrintTypes.Add(new PrintTypeModel() { Id = printType.PrintTypeCatalog.Id, Name = printType.PrintTypeCatalog.Name });
                }

                return model;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public static ManageUserProfileModel GetToUserProfile(int objUserId, int i)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {// ServiceId == i means it's ftp service
                    var subscribe = db.UserServiceSubscribes
                            .Include(x => x.Users)
                            .Include(x => x.Users.UserProfiles.Select(xx => xx.UserProfileServiceDefault))
                            .Where(x => x.ServiceId == i && x.SubscribeStateId == 1 && x.UserId == objUserId)
                            .Select(x => x).FirstOrDefault();
                    int profileId = 0;
                    foreach (var userProfile in subscribe.Users.UserProfiles)
                    {
                        foreach (var defaultService in userProfile.UserProfileServiceDefault)
                        {
                            if (defaultService.ServiceTypeId == i)
                            {
                                profileId = userProfile.Id;
                            }
                        }
                    }
                    if (profileId != 0)
                    {
                        return Helpers.ManageUserProfileHelper.GetToUserProfileById(profileId, i);
                    }
                    return null;
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
        public static ManageUserProfileModel GetToUserProfileForZipConversion(int objUserId, int i)
        {
            try
            {
                using (var db = new FCCPortalEntities2())
                {
                    var userProfiles = db.UserProfiles
                            .Include(x => x.UserProfileServiceDefault)
                            .Where(x => x.UserId == objUserId);
                    int profileId = 0;
                    foreach (var userProfile in userProfiles)
                    {
                        foreach (var defaultService in userProfile.UserProfileServiceDefault)
                        {
                            if (defaultService.ServiceTypeId == i)
                            {
                                profileId = userProfile.Id;
                            }
                        }
                    }
                    if (profileId != 0)
                    {
                        return Helpers.ManageUserProfileHelper.GetToUserProfileById(profileId, i);
                    }
                    return null;
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