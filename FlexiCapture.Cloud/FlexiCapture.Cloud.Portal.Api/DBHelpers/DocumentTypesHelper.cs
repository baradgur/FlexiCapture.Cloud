using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers
{
    /// <summary>
    /// document types helper 
    /// </summary>
    public class DocumentTypesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetToDocumentFileType(string extension)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    List<DocumentTypes> types = db.DocumentTypes.Select(x => x).ToList();
                    foreach (var type in types)
                    {
                        List<string> elements = type.Extension.Split(';').ToList();

                        foreach (var element in elements)
                        {
                            if (!string.IsNullOrEmpty(element))
                            {
                                if (element.Equals(extension.ToLower())) return type.Id;
                            }
                        }
                    }
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}