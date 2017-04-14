using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCapture.Cloud.ServiceAssist.Interfaces
{
    public interface FileExtensionInterface
    {
        /// <summary>
        /// get to file extension by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetToFileExtension(string type);
    }
}
