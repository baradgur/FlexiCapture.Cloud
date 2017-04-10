using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.OCR.AssistTester.Helpers
{
    public static class FileConverter
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
    }
}
