using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using FlexiCapture.Cloud.FTPService.Models;
using FlexiCapture.Cloud.OCR.Assist.Models;
using FlexiCapture.Cloud.ServiceAssist.DB;
using FlexiCapture.Cloud.ServiceAssist.Helpers;
using FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels;
using DocumentTypes = FlexiCapture.Cloud.ServiceAssist.DB.DocumentTypes;
using UserServiceSubscribes = FlexiCapture.Cloud.ServiceAssist.DB.UserServiceSubscribes;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public static class FTPHelper
    {
        private const int FtpProfileId = 3;
        private const int FtpSProfileActiveState = 1;
        private static List<DocumentTypes> AcceptableTypes;


        /// <summary>
        /// Получаем список ФТП настроек Input
        /// </summary>
        /// <returns></returns>
        public static List<FTPSetting> GetFtpInputSettings()
        {
            using (var db = new FCCPortalEntities2())
            {
                try
                {
                    List<FTPSetting> settings = new List<FTPSetting>();

                    db.FTPSettings.Where(x => x.FtpServiceType == 1).ToList().ForEach(x =>
                    {
                        settings.Add(new FTPSetting(x.Id, x.UserId, x.UserName, x.Host, x.Port ?? 0, x.Password, x.Path,
                            x.UseSSL, (bool)x.DeleteFile, x.Enabled, x.FtpServiceType));
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


        /// <summary>
        /// Получаем Аутпут настройку по айдишнику инпут настройки
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static FTPSetting GetFtpOutputSettings(int parentId)
        {
            using (var db = new FCCPortalEntities2())
            {
                try
                {
                    List<FTPSetting> settings = new List<FTPSetting>();

                    var setting = db.FTPSettings.SingleOrDefault(x =>
                    x.ParentId.Value == parentId && x.FtpServiceType == 2);
                    return new FTPSetting(setting.Id, setting.UserId, setting.UserName,
                        setting.Host, setting.Port ?? 0, setting.Password, setting.Path,
                            setting.UseSSL, (bool)setting.DeleteFile, setting.Enabled,
                            setting.FtpServiceType);
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

        /// <summary>
        /// Получаем Эксепшон настройку по айдишнику инпут настройки
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static FTPSetting GetFtpExceptionSettings(int parentId)
        {
            using (var db = new FCCPortalEntities2())
            {
                try
                {
                    List<FTPSetting> settings = new List<FTPSetting>();

                    var setting = db.FTPSettings.SingleOrDefault(x =>
                    x.ParentId.Value == parentId && x.FtpServiceType == 3);
                    return new FTPSetting(setting.Id, setting.UserId, setting.UserName,
                        setting.Host, setting.Port ?? 0, setting.Password, setting.Path,
                            setting.UseSSL, (bool)setting.DeleteFile, setting.Enabled,
                            setting.FtpServiceType);
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

        public static string PutFileOnFtpServer(FileInfo file, string newName, FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels.FTPSetting setting, string pathToPut)
        {
            try
            {

                // CONVERSION SETING MODEL!!!
                FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels.FTPConversionSettingModel conversionSetting =
                 FtpConversionSettingsHelper.GetSettingsByUserId(setting.UserId);


                string bbaseUri = "ftp://" + setting.Host;
                string uri = Path.Combine(pathToPut, newName);
                Uri baseUri = new Uri(bbaseUri);
                Uri serverUri = new Uri(baseUri, uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return "";
                }

                CheckPathToPut(pathToPut, setting);

                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(serverUri.AbsoluteUri);
                ftpClient.Credentials = new System.Net.NetworkCredential(setting.UserName, PasswordHelper.Crypt.DecryptString(setting.Password));
                ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                ftpClient.UseBinary = true;
                ftpClient.KeepAlive = true;

                ftpClient.ContentLength = file.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)file.Length;
                System.IO.FileStream fs = file.OpenRead();
                System.IO.Stream rs = ftpClient.GetRequestStream();
                while (total_bytes > 0)
                {
                    bytes = fs.Read(buffer, 0, buffer.Length);
                    rs.Write(buffer, 0, bytes);
                    total_bytes = total_bytes - bytes;
                }
                //fs.Flush();
                fs.Close();
                rs.Close();
                FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
                var value = uploadResponse.StatusDescription;
                uploadResponse.Close();

                return value;

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

        private static bool CheckPathToPut(string path, FlexiCapture.Cloud.ServiceAssist.Models.SettingsModels.FTPSetting setting)
        {
            string bbaseUri = "ftp://" + setting.Host;
            string uri = "";
            Uri baseUri = new Uri(bbaseUri);
            Uri serverUri = new Uri(baseUri, uri);


            string[] pathFolders = path.Split('/');



            foreach (var pathItem in pathFolders)
            {
                try
                {

                    if (string.IsNullOrEmpty(pathItem) ||
                        string.IsNullOrWhiteSpace(pathItem))
                        continue;

                    uri = Path.Combine(uri, "/", pathItem);

                    serverUri = new Uri(baseUri, uri);

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(setting.UserName,
                        PasswordHelper.Crypt.DecryptString(setting.Password));

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        var st = response.StatusCode;
                    }
                }
                catch (WebException ex)
                {
                    continue;
                    //if (ex.Response != null)
                    //{
                    //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                    //    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    //    request.Credentials = new NetworkCredential(setting.UserName,
                    //        PasswordHelper.Crypt.DecryptString(setting.Password));
                    //    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    //    {
                    //        if (response.StatusCode != FtpStatusCode.CommandOK)
                    //        {
                    //            return false;
                    //        }
                    //        else
                    //        {
                    //            CheckPathToPut(path, setting);
                    //        }
                    //    }
                    //}
                }


                return true;

            }

            return true;
        }
        public static FTPConversionSettingModel GetFtpConersionSettings(int userId)
        {
            using (var db = new FCCPortalEntities2())
            {
                try
                {

                    var dbSetting = db.FTPConversionSettings.SingleOrDefault(x => x.UserId == userId);

                    return new FTPConversionSettingModel(dbSetting.Id,
                        dbSetting.AddProcessed,
                        dbSetting.ReturnResults,
                        dbSetting.MirrorInput,
                        dbSetting.MoveProcessed,
                        dbSetting.UserId);
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
            string userPassword, string localPath, int userId, int serviceType)
        {
            try
            {
                FtpWebResponse response = null;

                if (IsPrivilegedEnough(userId) && serviceType == 1)
                {
                    Uri uriResult;
                    bool result = Uri.TryCreate("ftp://" + url + localPath,
                                      UriKind.Absolute, out uriResult) &&
                                      uriResult.Scheme == Uri.UriSchemeFtp;

                    if (!result)
                        return response;

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriResult);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;

                    request.Timeout = 20000;
                    request.Credentials = new NetworkCredential(userName, userPassword);
                    request.KeepAlive = false;
                    request.ServicePoint.ConnectionLeaseTimeout = 20000;
                    request.ServicePoint.MaxIdleTime = 20000;

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

        public static List<Tuple<string, string>> ExtractFiles(FtpWebResponse response, FTPSetting ftpSettings)
        {
            FlexiCapture.Cloud.ServiceAssist.DB.FTPConversionSettings ftpConvSettings;

            using (var db = new FCCPortalEntities2())
            {
                if (AcceptableTypes == null)
                {

                    AcceptableTypes = db.DocumentTypes
                        .Select(x => x).ToList();
                }

                ftpConvSettings = db.FTPConversionSettings
                        .SingleOrDefault(x => x.UserId == ftpSettings.UserId);
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
                string storedFilename = DownloadFile(ftpSettings.Host, ftpSettings.Path,
                    x.Item1, x.Item2, ftpSettings.UserName, ftpSettings.Password, ftpConvSettings, ftpSettings);
                newFileNameExtensionTuples.Add(Tuple.Create(storedFilename, x.Item2));
            });

            return newFileNameExtensionTuples;
        }

        private static string DownloadFile(string serverUrl, string localPath, string fileName, string fileExtension,
            string userName,
            string userPassword,
            FlexiCapture.Cloud.ServiceAssist.DB.FTPConversionSettings ftpConvSettings, FTPSetting ftpSettings)
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
                reqFTP.Credentials = new NetworkCredential(userName, PasswordHelper.Crypt.DecryptString(userPassword));
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
                    new FileStream(Path.Combine(SettingsHelper.GetSettingValueByName("MainPath"), "data", "uploads", fileName),
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
                string pathToPut = "/.";
                if (ftpConvSettings.MoveProcessed)
                {
                    pathToPut = "/DCC_Processed/.";
                }

                PutFileOnFtpServer(
                    new FileInfo(Path.Combine(
                        SettingsHelper.GetSettingValueByName("MainPath"), "data", "uploads",
                        fileName)), fileName, ftpSettings, pathToPut);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(serverUri.AbsoluteUri);
                reqFTP.Credentials = new NetworkCredential(userName,
                    PasswordHelper.Crypt.DecryptString(userPassword));
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



