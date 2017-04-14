using System;
using System.IO;
using System.Linq;
using FlexiCapture.Cloud.OCR.Assist;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.OCR.AssistTester.Helpers;
using FlexiCapture.Cloud.OCR.AssistTester.Model;
using Newtonsoft.Json;

namespace FlexiCapture.Cloud.OCR.AssistTester
{
    internal class Program
    {

        
        /// <summary>
        ///     input in program
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            try
            {

                SettingsModel settings = new SettingsModel();
                OcrRequestModel requestModel = new OcrRequestModel();
                string lang = "English";
                //init model
                requestModel.ApiKey = settings.ApiKey;

                requestModel.CleanupSettings = new CleanupSettingsModel()
                {
                    Deskew = true,
                    RemoveGarbage = true,
                    RemoveTexture = true,
                    SplitDualPage = true,
                    RotationType = "NoRotation",
                    JpegQuality = "",
                    OutputFormat = "",
                    Resolution = ""
                };
                requestModel.OcrSettings = new OcrSettingsModel()
                {
                    SpeedOcr = false,
                    LookForBarcodes = false,
                    AnalysisMode = "MixedDocument",
                    PrintType = "Print",
                    OcrLanguage = lang
                };

                requestModel.OutputSettings = new OutputSettingsModel()
                {
                    ExportFormat = "Text;PDF"
                };

                requestModel.InputFiles.Add(new InputFileModel()
                {
                    Name = Path.GetFileName(settings.TestEnglishFile),
                    Password = "",
                    InputUrl = "",
                    InputBlob = FileConverter.ConvertFileToBase64(settings.TestEnglishFile),
                    InputType = "JPG",
                    PostFix = ""
                });

                //end init
                String url = "http://api.ocr-it.com:40000/api/jobs";
                var json = JsonConvert.SerializeObject(requestModel);

                AssistProcessor processor = new AssistProcessor();
                //processor.MakeOcr(url,json);


                string baseUrl = "http://api.ocr-it.com:40000/api/Jobs?JobId=";
                string jobId = "12dea613-f6f3-4982-baa0-60e4f452ae4b";

                string gUrl = baseUrl + jobId;

                string jobStatus = processor.GetJobStatus(gUrl);

               OcrResponseModel desModel = JsonConvert.DeserializeObject<OcrResponseModel>(jobStatus);
                string downloadDir = "d://OCRDownload//";
                foreach (var file in desModel.Download)
                {
                    string sName = DateTime.Now.ToString();
                    
                    sName = sName.Replace(":", string.Empty);
                    sName = sName.Replace("-", string.Empty);
                    sName = sName.Replace(".", string.Empty);
                    sName = sName.Replace(",", string.Empty);
                    sName = sName.Replace(";", string.Empty);
                    sName = sName.Replace(" ", string.Empty);

                    string ext = "";

                    switch (file.OutputFormat)
                    {
                        case "PDF":
                            ext = "pdf";
                            break;

                        case "Text":
                            ext = "txt";
                            break;
                    }

                    sName = sName + "." + ext;

                    string downloadPath = Path.Combine(downloadDir, sName);

                 //   processor.DownloadFile(file.Uri,downloadPath);
                }
                //string outReq = JsonConvert.SerializeObject(model);
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: "+exception.Message);
                Console.ReadKey();

            }
        }
    }
}