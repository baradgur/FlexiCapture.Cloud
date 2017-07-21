using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist.Models.UserProfiles;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.ServiceAssist.Helpers
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
            catch (Exception exception)
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
                requestModel.ApiKey = SettingsHelper.GetSettingValueByName("ApiKey");
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
                int counter = 0;
                foreach (var selectedPrintType in profile.SelectedPrintTypes)
                {
                    printTypes += selectedPrintType.Name ;
                    counter++;
                    if (counter < profile.SelectedPrintTypes.Count)
                    {
                        printTypes += ";";
                    }
                }
                
                string languages = "";
                counter = 0;
                foreach (var selectedLanguages in profile.SelectedLanguages)
                {
                    languages += selectedLanguages.Name ;
                    counter++;
                    if (counter < profile.SelectedLanguages.Count)
                    {
                        languages += ";";
                    }
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
                counter = 0;
                foreach (var exportFormat in profile.SelectedExportFormats)
                {
                    exportFormats += exportFormat.Name;
                    counter++;
                    if (counter < profile.SelectedExportFormats.Count)
                    {
                        exportFormats += ";";
                    }
                }

                requestModel.OutputSettings = new OutputSettingsModel()
                {
                    ExportFormat = exportFormats
                };

                string serverPath = SettingsHelper.GetSettingValueByName("MainPath");
                string url = SettingsHelper.GetSettingValueByName("ServerPathApi");
                foreach (var document in documents)
                {
                   string filePath = Path.Combine(serverPath, document.Path);
                    string dUrl = Path.Combine(url, document.Path);
                    dUrl = dUrl.Replace("\\", "/");
                //LogHelper.AddLog("DownloadUrl: "+dUrl);
                    InputFileModel model = new InputFileModel()
                    {
                        Name = document.OriginalFileName,
                        Password = "",
                        //InputUrl = dUrl,
                        //InputBlob = "",
                        InputUrl = "",
                        InputBlob = ConvertFileToBase64(filePath),
                        InputType = document.DocumentTypes.Name,
                        PostFix = ""
                    };
                    requestModel.InputFiles.Add(model);

                }

                var json = JsonConvert.SerializeObject(requestModel);
                LogHelper.AddLog("JSON: " + json);
                LogHelper.AddLog("JSON: " + json);
                return json;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public static string ConvertProfileToRequestModel(List<ZipDocuments> documents, ManageUserProfileModel profile)
        {
            try
            {
                OcrRequestModel requestModel = new OcrRequestModel();
                requestModel.ApiKey = SettingsHelper.GetSettingValueByName("ApiKey");
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
                int counter = 0;
                foreach (var selectedPrintType in profile.SelectedPrintTypes)
                {
                    printTypes += selectedPrintType.Name;
                    counter++;
                    if (counter < profile.SelectedPrintTypes.Count)
                    {
                        printTypes += ";";
                    }
                }

                string languages = "";
                counter = 0;
                foreach (var selectedLanguages in profile.SelectedLanguages)
                {
                    languages += selectedLanguages.Name;
                    counter++;
                    if (counter < profile.SelectedLanguages.Count)
                    {
                        languages += ";";
                    }
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
                counter = 0;
                foreach (var exportFormat in profile.SelectedExportFormats)
                {
                    exportFormats += exportFormat.Name;
                    counter++;
                    if (counter < profile.SelectedExportFormats.Count)
                    {
                        exportFormats += ";";
                    }
                }

                requestModel.OutputSettings = new OutputSettingsModel()
                {
                    ExportFormat = exportFormats
                };

                string serverPath = SettingsHelper.GetSettingValueByName("MainPath");
                string url = SettingsHelper.GetSettingValueByName("ServerPathApi");
                foreach (var document in documents)
                {
                    string filePath = Path.Combine(serverPath, document.Path);
                    string dUrl = Path.Combine(url, document.Path);
                    dUrl = dUrl.Replace("\\", "/");
                    string fileString = ConvertFileToBase64(filePath);
                    //LogHelper.AddLog("DownloadUrl: "+dUrl);
                    InputFileModel model = new InputFileModel()
                    {
                        Name = document.OriginalFileName,
                        Password = "",
                        //InputUrl = "",
                        //InputBlob = fileString,
                        InputUrl = dUrl,
                        InputBlob = "",
                        InputType = document.DocumentTypes.Name,
                        PostFix = ""
                    };
                    requestModel.InputFiles.Add(model);

                }

                var json = JsonConvert.SerializeObject(requestModel);
                LogHelper.AddLog("JSON: " + json);
                LogHelper.AddLog("JSON: " + json);
                return json;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}