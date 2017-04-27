using System;
using System.Collections.Generic;
using System.Linq;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
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
                using (var db = new FCCPortalEntities2())
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