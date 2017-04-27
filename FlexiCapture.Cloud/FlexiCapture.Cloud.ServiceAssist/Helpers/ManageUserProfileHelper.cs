using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Models.Catalogs;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;

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
    }
}