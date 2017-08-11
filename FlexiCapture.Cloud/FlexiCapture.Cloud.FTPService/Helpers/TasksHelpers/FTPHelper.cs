using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.FTPService.Models;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.Portal.Api.DBHelpers;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.FTPService.Helpers.TasksHelpers
{
    public static class FTPHelper
    {
        private const int FtpProfileId = 3;
        private const int FtpSProfileActiveState = 1;
        private static List<DocumentTypes> AcceptableTypes;
        public static List<FTPSetting> GetFtpSettings()
        {
            using (var db = new FCCPortalEntities2())
            {
                try
                {
                    List<FTPSetting> settings = new List<FTPSetting>();

                    db.FTPSettings.Select(x => x).ToList().ForEach(x =>
                    {
                        settings.Add(new FTPSetting(x.Id, x.UserId, x.UserName, x.Host, x.Port ?? 0, x.Password, x.Path,
                            x.UseSSL, (bool)x.DeleteFile));
                    });

                    return settings;
                }
                catch (Exception exception)
                {
                    string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                       innerException);
                    return null;
                }
            }
        }

        public static FtpWebResponse TryLoginToFtp(string url, string userName,
            string userPassword, string localPath, int userId)
        {
            try
            {
                FtpWebResponse response = null;

                if (IsPrivilegedEnough(userId))
                {
                    Uri uriResult;
                    bool result = Uri.TryCreate("ftp://" + url + localPath,
                                      UriKind.Absolute, out uriResult) &&
                                      uriResult.Scheme == Uri.UriSchemeFtp;

                    if (!result)
                        return response;

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriResult);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    request.Credentials = new NetworkCredential(userName, userPassword);
                    response = (FtpWebResponse)request.GetResponse();
                }

                //response.ResponseUri

                return response;
                /********************************/

            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                   innerException);
                return null;
            }
        }

        public static List<Tuple<string, string>> ExtractFiles(FtpWebResponse response, string serverUrl, string localPath, string userName,
            string userPassword)
        {

            if (AcceptableTypes == null)
            {
                using (var db = new FCCPortalEntities2())
                {
                    AcceptableTypes = db.DocumentTypes
                        .Select(x => x).ToList();
                }
            }

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<Tuple<string, string>> fileNameExtensionTuples =
                new List<Tuple<string, string>>();

            List<Tuple<string, string>> newFileNameExtensionTuples =
                new List<Tuple<string, string>>();

            string line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                var fileName = line.Split(' ').LastOrDefault();

                foreach (var type in AcceptableTypes)
                {
                    foreach (var splittedType in type.Extension.Split(';'))
                    {
                        if (fileName.Contains(splittedType))
                        {
                            fileNameExtensionTuples
                                .Add(Tuple.Create(fileName, splittedType));
                            break;
                        }
                    }

                }
                line = reader.ReadLine();
            }

            reader.Close();
            response.Close();

            fileNameExtensionTuples.ForEach(x =>
            {
                string storedFilename = DownloadFile(serverUrl, localPath, x.Item1, x.Item2, userName, userPassword);
                newFileNameExtensionTuples.Add(Tuple.Create(storedFilename, x.Item2));
            });

            return newFileNameExtensionTuples;
        }

        private static string DownloadFile(string serverUrl, string localPath, string fileName, string fileExtension,
            string userName, string userPassword)
        {
            try
            {
                string bbaseUri = "ftp://" + serverUrl;
                string uri = Path.Combine(localPath, fileName);
                Uri baseUri = new Uri(bbaseUri);
                Uri serverUri = new Uri(baseUri, uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return "";
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(serverUri.AbsoluteUri);
                reqFTP.Credentials = new NetworkCredential(userName, userPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.Timeout = 10000;
                reqFTP.KeepAlive = false;
                reqFTP.ServicePoint.ConnectionLeaseTimeout = 20000;
                reqFTP.ServicePoint.MaxIdleTime = 20000;
                //reqFTP.UseBinary = true;
                //reqFTP.Proxy = null;
                //reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream =
                    new FileStream(Path.Combine(SettingsHelper.GetSettingsValueByName("MainPath"), "data", "uploads", fileName),
                        FileMode.Create);

                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(serverUri.AbsoluteUri);
                reqFTP.Credentials = new NetworkCredential(userName, userPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                reqFTP.KeepAlive = false;
                reqFTP.ServicePoint.ConnectionLeaseTimeout = 20000;
                reqFTP.ServicePoint.MaxIdleTime = 20000;
                reqFTP.Timeout = 10000;
                response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();

                return fileName;

            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException == null ? "" : ex.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + ex.Message + " Innner Exception: " +
                                   innerException);
                return "";
            }

        }

        /// <summary>
        /// Проверка на наличие доступа пользователя к сервису FTP
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static bool IsPrivilegedEnough(int userId)
        {
            try
            {
                bool isPrivelegedEnough = false;

                if (userId <= 0)
                    throw new ArgumentOutOfRangeException();

                UserServiceSubscribes query;

                using (var db = new FCCPortalEntities2())
                {
                    query = (from ss in db.UserServiceSubscribes
                             where ss.UserId.Equals(userId) && ss.ServiceId == FtpProfileId
                             && ss.SubscribeStateId == FtpSProfileActiveState
                             select ss).FirstOrDefault();
                }

                if (query == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                string innerException = e.InnerException == null ? "" : e.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + e.Message + " Innner Exception: " +
                                   innerException);
                return false;
            }
        }
    }
}



