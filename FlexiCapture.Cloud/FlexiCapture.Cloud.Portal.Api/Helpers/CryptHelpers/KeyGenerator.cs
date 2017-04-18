using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.CryptHelpers
{
    public static class KeyGenerator
    {
        /// <summary>
        /// get to unique assword
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string GetUniqueKey(int maxSize)
        {
            try
            {

            
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
            }
            catch (Exception exception)
            {
                return "";
            }
        }
    }
}