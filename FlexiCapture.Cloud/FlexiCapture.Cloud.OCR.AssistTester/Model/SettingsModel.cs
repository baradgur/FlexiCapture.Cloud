using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.OCR.AssistTester.Model
{
    /// <summary>
    /// settings
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// constructor
        /// </summary>
        public SettingsModel()
        {
            try
            {
                ApiKey = "791B3548-76E4-4CFB-94B3-B7B26FB01E82";
                string mainPath = AppDomain.CurrentDomain.BaseDirectory;
                DataFolder = Path.Combine(mainPath, "data");
                ImagesFolder = Path.Combine(DataFolder, "images");
                OutputFolder = Path.Combine(DataFolder, "output");

                TestRussianFile = Path.Combine(ImagesFolder, "test_rus.jpg");
                TestEnglishFile = Path.Combine(ImagesFolder, "test_eng.jpg");


            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// api key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// data folder
        /// </summary>
        public string DataFolder { get; set; }

        /// <summary>
        /// images folder
        /// </summary>
        public string ImagesFolder { get; set; }

        /// <summary>
        /// output folder
        /// </summary>
        public string OutputFolder { get; set; }

        /// <summary>
        /// test eng
        /// </summary>
        public string TestRussianFile { get; set; }
        public string TestEnglishFile { get; set; }
    }
}
