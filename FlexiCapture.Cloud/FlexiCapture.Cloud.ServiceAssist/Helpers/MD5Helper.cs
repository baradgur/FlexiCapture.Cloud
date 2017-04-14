using System;
using System.IO;
using System.Security.Cryptography;

namespace FlexiCapture.Cloud.ServiceAssist.Helpers
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