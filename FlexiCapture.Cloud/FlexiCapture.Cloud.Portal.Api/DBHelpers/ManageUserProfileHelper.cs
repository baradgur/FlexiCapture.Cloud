using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using System.Data.Entity;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// class for load all catalogs 
    /// </summary>
    public static class ManageUserProfileHelper
    {
        #region languages
        /// <summary>
        /// get to all languages
        /// </summary>
        /// <returns></returns>
        public static List<LanguagesCatalog> GetToAllLanguages()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.LanguagesCatalog.Select(x=>x).ToList();    
                }
                
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region print types
        /// <summary>
        /// get to all print types
        /// </summary>
        /// <returns></returns>
        public static List<PrintTypeCatalog> GetToAllPrintTypes()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.PrintTypeCatalog.Select(x => x).ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region export formats
        /// <summary>
        /// get to all print types
        /// </summary>
        /// <returns></returns>
        public static List<ExportFormatsCatalog> GetToAllExportFormats()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.ExportFormatsCatalog.Select(x => x).ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region rotation types
        /// <summary>
        /// get to all rotation types
        /// </summary>
        /// <returns></returns>
        public static List<RotationTypeCatalog> GetToAllRotationTypes()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.RotationTypeCatalog.Select(x => x).ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// get to default model
        /// </summary>
        /// <returns></returns>
        public static RotationTypeCatalog GetToDefaultRotationType()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.RotationTypeCatalog.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion

        #region analysis models
        /// <summary>
        /// get to all analyze modes
        /// </summary>
        /// <returns></returns>
        public static List<AnalysisModeCatalog> GetToAllAnalysisModes()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.AnalysisModeCatalog.Select(x => x).ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// get to default model
        /// </summary>
        /// <returns></returns>
        public static  AnalysisModeCatalog GetToDefaultAnalysisModel()
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.AnalysisModeCatalog.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion

        #region user profile
        /// <summary>
        /// get to user profile
        /// </summary>
        /// <returns></returns>
        public static UserProfiles GetToUserProfileById(int profileId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    UserProfiles profile = db.UserProfiles
                        .Include(x=>x.AnalysisModeCatalog)
                        .Include(x=>x.RotationTypeCatalog)
                        .Include(x => x.UserProfileExportFormats.Select(y=>y.ExportFormatsCatalog))
                        .Include(x => x.UserProfileLanguages.Select(y=>y.LanguagesCatalog))
                        .Include(x => x.UserProfilePrintTypes.Select(y=>y.PrintTypeCatalog))
                        .FirstOrDefault(x => x.Id == profileId);

                    return profile;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<int> GetToUserProfiles(int userId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.UserProfiles.Where(x => x.UserId == userId).Select(x => x.Id).ToList();
                }
            }
            catch (Exception)
            {
                return null;
                
            }
        }

        /// <summary>
        /// update user profile
        /// </summary>
        public static void UpdateUserProfile(ManageUserProfileModel model)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    int profileId = model.Id;

                    UserProfiles profile = db.UserProfiles
                       .Include(x => x.AnalysisModeCatalog)
                       .Include(x => x.RotationTypeCatalog)
                       .Include(x => x.UserProfileExportFormats.Select(y => y.ExportFormatsCatalog))
                       .Include(x => x.UserProfileLanguages.Select(y => y.LanguagesCatalog))
                       .Include(x => x.UserProfilePrintTypes.Select(y => y.PrintTypeCatalog))
                        .FirstOrDefault(x => x.Id == profileId);

                    if (profile != null)
                    {
                        profile.OutputFormat = model.OutputFormat;
                        profile.SpeedOcr = model.SpeedOcr;
                        profile.SplitDualPage = model.SplitDualPage;
                        profile.AnalysisModeId = model.SelectedAnalysisModel.Id;
                        profile.CreationDateTime = DateTime.Now;
                        profile.Deskew = model.Deskew;
                        profile.JpegQuality = model.JpegQuality;
                        profile.LookForBarcodes = model.LookForBarCodes;
                        profile.Name = model.Name;
                        profile.RemoveGarbage = model.RemoveGarbage;
                        profile.RemoveTexture = model.RemoveTexture;
                        profile.RotationTypeId = model.SelectedRotationType.Id;
                        
                        #region user profile print types checker

                        foreach (var sType in model.SelectedPrintTypes)
                        {
                            UserProfilePrintTypes type =
                                db.UserProfilePrintTypes.FirstOrDefault(
                                    x => x.UserProfileId == model.Id && x.PrintTypeId == sType.Id);
                            if (type == null)
                                db.UserProfilePrintTypes.Add(new UserProfilePrintTypes()
                                {
                                    PrintTypeId = sType.Id,
                                    UserProfileId = model.Id
                                });
                            
                        }
                        db.SaveChanges();

                        List<UserProfilePrintTypes> types =
                            db.UserProfilePrintTypes.Where(x => x.UserProfileId == model.Id).ToList();

                        foreach (UserProfilePrintTypes type in types)
                        {
                            var element = model.SelectedPrintTypes.FirstOrDefault(x => x.Id == type.PrintTypeId);
                            if (element == null)
                                db.UserProfilePrintTypes.Remove(type);
                        }
                        db.SaveChanges();

                        #endregion

                        #region user profile language checker

                        foreach (var language in model.SelectedLanguages)
                        {
                            UserProfileLanguages lang =
                                db.UserProfileLanguages.FirstOrDefault(
                                    x => x.UserProfileId == model.Id && x.LanguageId == language.Id);
                            if (lang == null)
                                db.UserProfileLanguages.Add(new UserProfileLanguages()
                                {
                                    LanguageId = language.Id,
                                    UserProfileId = model.Id
                                });

                        }
                        db.SaveChanges();

                        List<UserProfileLanguages> languages =
                            db.UserProfileLanguages.Where(x => x.UserProfileId == model.Id).ToList();

                        foreach (UserProfileLanguages lang in languages)
                        {
                            var element = model.SelectedLanguages.FirstOrDefault(x => x.Id == lang.LanguageId);
                            if (element == null)
                                db.UserProfileLanguages.Remove(lang);
                        }
                        db.SaveChanges();

                        #endregion

                        #region user profile export formats

                        foreach (var exportFormat in model.SelectedExportFormats)
                        {
                            UserProfileExportFormats format =
                                db.UserProfileExportFormats.FirstOrDefault(
                                    x => x.UserProfileId == model.Id && x.ExportFormatId == exportFormat.Id);
                            if (format == null)
                                db.UserProfileExportFormats.Add(new UserProfileExportFormats()
                                {
                                    ExportFormatId = exportFormat.Id,
                                    UserProfileId = model.Id
                                });

                        }
                        db.SaveChanges();

                        List<UserProfileExportFormats> formats =
                            db.UserProfileExportFormats.Where(x => x.UserProfileId == model.Id).ToList();

                        foreach (UserProfileExportFormats format in formats)
                        {
                            var element = model.SelectedExportFormats.FirstOrDefault(x => x.Id == format.ExportFormatId);
                            if (element == null)
                                db.UserProfileExportFormats.Remove(format);
                        }
                        db.SaveChanges();

                        #endregion
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        #endregion
    }
}