using System;
using FlexiCapture.Cloud.OCR.Assist.Models;
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

//                var model = new OcrRequestModel {Name = "John Smith", Language = "English"};
//               
//                var r = JsonConvert.SerializeObject(model);
//               
//               Console.WriteLine("Newton: " + r);
//
//                OcrRequestModel mdl = JsonConvert.DeserializeObject<OcrRequestModel>(r);
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