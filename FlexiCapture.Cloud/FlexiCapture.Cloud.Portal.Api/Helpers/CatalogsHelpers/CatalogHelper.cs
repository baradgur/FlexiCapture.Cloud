using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Catalogs;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.CatalogsHelpers
{
    /// <summary>
    /// catalog helper
    /// </summary>
    public class CatalogHelper
    {
        /// <summary>
        /// convert analysis db model to analysis mode model
        /// </summary>
        /// <returns></returns>
        public static List<AnalysisModeModel> ConvertToAnalysisModeCollection(List<AnalysisModeCatalog> catalogs)
        {
            try
            {
                List<AnalysisModeModel> models = new List<AnalysisModeModel>();
                foreach (var analysisModeCatalog in catalogs)
                {
                    models.Add(new AnalysisModeModel() {Id = analysisModeCatalog.Id, Name = analysisModeCatalog.Name});
                }
                return models;
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

        /// <summary>
        /// convert export format db model to export format model
        /// </summary>
        /// <returns></returns>
        public static List<ExportFormatModel> ConvertToExportFormatCollection(List<ExportFormatsCatalog> catalogs)
        {
            try
            {
                List<ExportFormatModel> models = new List<ExportFormatModel>();
                foreach (var exportFormatsCatalog in catalogs)
                {
                    models.Add(new ExportFormatModel() { Id = exportFormatsCatalog.Id, Name = exportFormatsCatalog.Name });
                }
                return models;
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

        /// <summary>
        /// convert language db model to export language model
        /// </summary>
        /// <returns></returns>
        public static List<LanguageModel> ConvertToLanguageCollection(List<LanguagesCatalog> catalogs)
        {
            try
            {
                List<LanguageModel> models = new List<LanguageModel>();
                foreach (var languagesCatalog in catalogs)
                {
                    models.Add(new LanguageModel() { Id = languagesCatalog.Id, Name = languagesCatalog.Name });
                }
                return models;
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

        /// <summary>
        /// convert language db model to export language model
        /// </summary>
        /// <returns></returns>
        public static List<PrintTypeModel> ConvertToPrintTypeCollection(List<PrintTypeCatalog> catalogs)
        {
            try
            {
                List<PrintTypeModel> models = new List<PrintTypeModel>();
                foreach (var printTypeCatalog in catalogs)
                {
                    models.Add(new PrintTypeModel() { Id = printTypeCatalog.Id, Name = printTypeCatalog.Name });
                }
                return models;
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

        /// <summary>
        /// convert rotation db model to export rotation model
        /// </summary>
        /// <returns></returns>
        public static List<RotationTypeModel> ConvertToRotationTypeCollection(List<RotationTypeCatalog> catalogs)
        {
            try
            {
                List<RotationTypeModel> models = new List<RotationTypeModel>();
                foreach (var rotationTypeCatalog in catalogs)
                {
                    models.Add(new RotationTypeModel() { Id = rotationTypeCatalog.Id, Name = rotationTypeCatalog.Name });
                }
                return models;
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

        /// <summary>
        /// convert rotation db model to export rotation model
        /// </summary>
        /// <returns></returns>
        public static RotationTypeModel ConvertToRotationTypeSingle(RotationTypeCatalog catalog)
        {
            try
            {
                
                    return (new RotationTypeModel() { Id = catalog.Id, Name = catalog.Name });
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

        /// <summary>
        /// convert rotation db model to export rotation model
        /// </summary>
        /// <returns></returns>
        public static AnalysisModeModel ConvertToAnalysisModelSingle(AnalysisModeCatalog catalog)
        {
            try
            {

                return (new AnalysisModeModel() { Id = catalog.Id, Name = catalog.Name });
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