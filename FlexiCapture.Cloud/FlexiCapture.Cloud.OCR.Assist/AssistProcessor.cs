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
        public void MakeOcr(string url, string json)
        {
            try
            {
                PostHelper.MakePostRequest(url,json);
            }
            catch (Exception)
            {
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
        public void DownloadFile(string url, string downloadPath)
        {
            try
            {
                PostHelper.DownloadFile(url,downloadPath);
            }
            catch (Exception)
            {
            }
        }
    }
}
