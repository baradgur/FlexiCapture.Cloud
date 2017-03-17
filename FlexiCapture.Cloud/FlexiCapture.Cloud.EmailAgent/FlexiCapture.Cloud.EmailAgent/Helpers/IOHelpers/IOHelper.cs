using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;

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
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return "";
            }

        }
    }
}
