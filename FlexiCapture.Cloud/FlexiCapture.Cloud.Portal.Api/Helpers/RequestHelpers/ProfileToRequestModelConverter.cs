using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.Portal.Api.Models.UserProfiles;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.RequestHelpers
{
    public static class ProfileToRequestModelConverter
    {
        /// <summary>
        /// convert file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertFileToBase64(string path)
        {
            try
            {
                Byte[] bytes = File.ReadAllBytes(path);
                String file = Convert.ToBase64String(bytes);
                return file;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// convert profile to request model
        /// </summary>
        /// <returns></returns>
        public static string ConvertProfileToRequestModel(List<Documents> documents, ManageUserProfileModel profile)
        {
            try
            {
                OcrRequestModel requestModel = new OcrRequestModel();
                requestModel.ApiKey = SettingsHelper.GetSettingsValueByName("ApiKey");
                requestModel.CleanupSettings = new CleanupSettingsModel()
                {
                    Deskew = profile.Deskew,
                    RemoveGarbage = profile.RemoveGarbage,
                    RemoveTexture = profile.RemoveTexture,
                    SplitDualPage = profile.SplitDualPage,
                    RotationType = profile.SelectedRotationType.Name,
                    JpegQuality = profile.JpegQuality,
                    OutputFormat = profile.OutputFormat,
                    Resolution = profile.Resolution
                };

                string printTypes = "";
                foreach (var selectedPrintType in profile.SelectedPrintTypes)
                {
                    printTypes += selectedPrintType.Name + ";";
                }

                string languages = "";
                foreach (var selectedLanguages in profile.SelectedLanguages)
                {
                    languages += selectedLanguages.Name + ";";
                }

                requestModel.OcrSettings = new OcrSettingsModel()
                {
                    SpeedOcr = profile.SpeedOcr,
                    LookForBarcodes = profile.LookForBarCodes,
                    AnalysisMode = profile.SelectedAnalysisModel.Name,
                    PrintType = printTypes,
                    OcrLanguage = languages
                };

                string exportFormats = "";
                foreach (var exportFormat in profile.SelectedExportFormats)
                {
                    exportFormats += exportFormat.Name + ";";
                }
                requestModel.OutputSettings = new OutputSettingsModel()
                {
                    ExportFormat = exportFormats
                };

                string serverPath = HostingEnvironment.MapPath("~/");

                foreach (var document in documents)
                {
                    string filePath = Path.Combine(serverPath, document.Path);
 
                    InputFileModel model = new InputFileModel()
                    {
                        Name = document.OriginalFileName,
                        Password = "",
                        InputUrl = "",
                        InputBlob = ConvertFileToBase64(filePath),
                        InputType = document.DocumentTypes.Name,
                        PostFix = ""
                    };
                    requestModel.InputFiles.Add(model);

                }

                var json = JsonConvert.SerializeObject(requestModel);
                
                return json;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}