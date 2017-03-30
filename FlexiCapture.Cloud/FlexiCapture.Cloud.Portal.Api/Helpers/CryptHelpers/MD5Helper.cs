using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers
{
    public static class MD5Helper
    {
        /// <summary>
        /// get to md5 file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}