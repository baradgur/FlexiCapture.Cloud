using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.IOHelpers
{
    /// <summary>
    /// IO Helper
    /// </summary>
    public static class IOHelper
    {
        /// <summary>
        /// read filePath
        /// </summary>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
