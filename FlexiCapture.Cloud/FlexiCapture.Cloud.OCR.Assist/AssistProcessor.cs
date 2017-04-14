using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.OCR.Assist.Helpers;
using FlexiCapture.Cloud.OCR.Assist.Models;

namespace FlexiCapture.Cloud.OCR.Assist
{
    public class AssistProcessor
    {
        /// <summary>
        /// make ocr
        /// </summary>
        public string MakeOcr(string url, string json)
        {
            try
            {
                return PostHelper.MakePostRequest(url,json);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// get to job status
        /// </summary>
        /// <returns></returns>
        public string GetJobStatus(string url)
        {
            try
            {
                return PostHelper.GetJobStatus(url);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// download fiel
        /// </summary>
        public void DownloadFile(string url, string downloadPath, ref string error)
        {
            try
            {
                PostHelper.DownloadFile(url,downloadPath,ref error);
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string exc = exception.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                error = "Exception: " + exc + "; Inner Exception: " + innerException + "; Method Name: " + methodName;

            }
        }
    }
}
