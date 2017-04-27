using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.ServiceAssist.DB;
using System.Data.Entity;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
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
                using (var db = new FCCPortalEntities2())
                {
                    UserProfiles profile = db.UserProfiles
                        .Include(x=>x.AnalysisModeCatalog)
                        .Include(x=>x.RotationTypeCatalog)
                        .Include(x => x.UserProfileExportFormats.Select(y=>y.ExportFormatsCatalog))
                        .Include(x => x.UserProfileLanguages.Select(y=>y.LanguagesCatalog))
                        .Include(x => x.UserProfilePrintTypes.Select(y=>y.PrintTypeCatalog))
                        .Include(x=>x.UserProfileServiceDefault)
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
                using (var db = new FCCPortalEntities2())
                {
                    return db.UserProfiles.Where(x => x.UserId == userId).Select(x => x.Id).ToList();
                }
            }
            catch (Exception)
            {
                return null;
                
            }
        }
        #endregion
    }
}