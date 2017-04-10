using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.CatalogsHelpers;
using FlexiCapture.Cloud.Portal.Api.Helpers.ManageUserHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.Catalogs;

namespace FlexiCapture.Cloud.Portal.Api.Models.UserProfiles
{
    /// <summary>
    /// user profile model for manipulating
    /// </summary>
    public class ManageUserProfileModel
    {
        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public ManageUserProfileModel()
        {
            try
            {
                AvailableAnalysisModes =
                    CatalogHelper.ConvertToAnalysisModeCollection(
                        DBHelpers.ManageUserProfileHelper.GetToAllAnalysisModes());

                AvailableExportFormats =
                    CatalogHelper.ConvertToExportFormatCollection(
                        DBHelpers.ManageUserProfileHelper.GetToAllExportFormats());

                AvailableLanguages =
                    CatalogHelper.ConvertToLanguageCollection(DBHelpers.ManageUserProfileHelper.GetToAllLanguages());

                AvailablePrintTypes =
                    CatalogHelper.ConvertToPrintTypeCollection(DBHelpers.ManageUserProfileHelper.GetToAllPrintTypes());

                AvailableRotationTypes =
                    CatalogHelper.ConvertToRotationTypeCollection(
                        DBHelpers.ManageUserProfileHelper.GetToAllRotationTypes());

                Deskew = true;
                RemoveGarbage = true;
                RemoveTexture = true;
                SplitDualPage = true;
                Resolution = "";
                JpegQuality = "";
                SpeedOcr = false;
                LookForBarCodes = true;

                SelectedExportFormats = new List<ExportFormatModel>();
                SelectedLanguages = new List<LanguageModel>();
                SelectedPrintTypes = new List<PrintTypeModel>();

                SelectedRotationType = CatalogHelper.ConvertToRotationTypeSingle(FlexiCapture.Cloud.Portal.Api.DBHelpers.ManageUserProfileHelper.GetToDefaultRotationType());
                SelectedAnalysisModel= CatalogHelper.ConvertToAnalysisModelSingle(FlexiCapture.Cloud.Portal.Api.DBHelpers.ManageUserProfileHelper.GetToDefaultAnalysisModel());

                SelectedAnalysisModelId = SelectedAnalysisModel.Id;
                SelectedRotationTypeId = SelectedRotationType.Id;


                Name = "Profile " + DateTime.Now.ToShortDateString();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region fields
        /// <summary>
        /// int Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// profile name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// deskew
        /// </summary>
        public bool Deskew { get; set; }

        /// <summary>
        /// removee garbage
        /// </summary>
        public bool RemoveGarbage { get; set; }

        /// <summary>
        /// remove texture
        /// </summary>
        public bool RemoveTexture { get; set; }


        /// <summary>
        /// selected rotation type Id
        /// </summary>
        public int SelectedRotationTypeId { get; set; }

        /// <summary>
        /// selected rotation type
        /// </summary>
        public RotationTypeModel SelectedRotationType { get; set; }

        /// <summary>
        /// current analysis model
        /// </summary>
        public AnalysisModeModel SelectedAnalysisModel { get; set; }

        /// <summary>
        /// current analysis model id
        /// </summary>
        public int SelectedAnalysisModelId { get; set; }
        /// <summary>
        /// split pages
        /// </summary>
        public bool SplitDualPage { get; set; }

        /// <summary>
        /// output format
        /// </summary>
        public string OutputFormat { get; set; }
        /// <summary>
        /// resolution
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// jpeg quality
        /// </summary>
        public string JpegQuality { get; set; }

        /// <summary>
        /// speed ocr
        /// </summary>
        public bool SpeedOcr { get; set; }

        /// <summary>
        /// search bar codes or not
        /// </summary>
        public bool LookForBarCodes { get; set; }

        #region availiable lists
        /// <summary>
        /// availiable langs
        /// </summary>
        public List<LanguageModel> AvailableLanguages { get; set; }
        
        /// <summary>
        /// availiable print types
        /// </summary>
        public List<PrintTypeModel> AvailablePrintTypes { get; set; }

        /// <summary>
        /// availiable export types
        /// </summary>
        public List<ExportFormatModel> AvailableExportFormats { get; set; }

        /// <summary>
        /// availiable rotation types
        /// </summary>
        public List<RotationTypeModel> AvailableRotationTypes { get; set; }

        /// <summary>
        /// availiable analysis models
        /// </summary>
        public List<AnalysisModeModel> AvailableAnalysisModes { get; set; }
        #endregion 

        #region selected lists
        /// <summary>
        /// selected langs
        /// </summary>
        public List<LanguageModel> SelectedLanguages { get; set; }

        /// <summary>
        /// selected print types
        /// </summary>
        public List<PrintTypeModel> SelectedPrintTypes { get; set; }

        /// <summary>
        /// selected export types
        /// </summary>
        public List<ExportFormatModel> SelectedExportFormats { get; set; }

        #endregion
        
        #endregion
    }
}