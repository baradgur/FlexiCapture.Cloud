using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    public static class ExportFormatHelper
    {
        /// <summary>
        /// get to file extension
        /// </summary>
        /// <returns></returns>
        public static string GetFileExtensionByExportType(string type)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var t = db.ExportFormatsCatalog.FirstOrDefault(x => x.Name.ToLower().Equals(type.ToLower()));
                    if (t!=null)
                    return t.Extension;
                }

                return "";
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
