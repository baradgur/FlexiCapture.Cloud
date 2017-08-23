using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.SettingsModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.FTPAccessTestHelpers
{
    public static class FTPAccessTestHelper
    {
        public static int TestFtpAccess(FTPSettingsAggregateModel model)
        {
            // Какое по счету соединение тестируется
            int currentConnectionFlag = 0;

            try
            {
                FtpWebResponse response = null;

                Uri uriResult;

                //---------

                currentConnectionFlag = 1;
                bool result = Uri.TryCreate("ftp://" + model.InputFtpSettingsModel.Host,
                                  UriKind.Absolute, out uriResult) &&
                                  uriResult.Scheme == Uri.UriSchemeFtp;


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriResult);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(model.InputFtpSettingsModel.UserName, model.InputFtpSettingsModel.Password);

                response = (FtpWebResponse)request.GetResponse();
                response.Close();

                //---------

                currentConnectionFlag = 2;
                result = Uri.TryCreate("ftp://" + model.OutputFtpSettingsModel.Host,
                                  UriKind.Absolute, out uriResult) &&
                                  uriResult.Scheme == Uri.UriSchemeFtp;


                request = (FtpWebRequest)WebRequest.Create(uriResult);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(model.OutputFtpSettingsModel.UserName, model.OutputFtpSettingsModel.Password);

                response = (FtpWebResponse)request.GetResponse();
                response.Close();

                //---------

                currentConnectionFlag = 3;
                result = Uri.TryCreate("ftp://" + model.InputFtpSettingsModel.Host,
                                  UriKind.Absolute, out uriResult) &&
                                  uriResult.Scheme == Uri.UriSchemeFtp;


                request = (FtpWebRequest)WebRequest.Create(uriResult);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(model.ExceptionFtpSettingsModel.UserName, model.ExceptionFtpSettingsModel.Password);

                response = (FtpWebResponse)request.GetResponse();
                response.Close();

                //---------

                return 0;


                //response.ResponseUri

            }
            catch (Exception e)
            {
                return currentConnectionFlag;
            }
        }
    }
}