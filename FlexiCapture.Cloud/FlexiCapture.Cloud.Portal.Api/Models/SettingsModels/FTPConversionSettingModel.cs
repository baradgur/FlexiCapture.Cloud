using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    /// <summary>
    /// Дополнительная модель настроек 
    /// конвертирования с FTP
    /// </summary>
    public class FTPConversionSettingModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Add processed documents to Conversion Library
        /// </summary>
        public bool AddProcessed { get; set; }
        /// <summary>
        /// Return conversion results back to FTP 
        /// (FTP Output and FTP Exceptions locations must be specified)
        /// </summary>
        public bool ReturnResults { get; set; }
        /// <summary>
        /// Mirror Input location sub-folder structure in 
        /// Output/Exceptions locations (otherwise only root location is processed)
        /// </summary>
        public bool MirrorInput { get; set; }
        /// <summary>
        /// Move processed images to “DCC_Processed” sub-folder 
        /// on FTP location (otherwise processed images are deleted from FTP)
        /// </summary>
        public bool MoveProcessed { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addProcessed"></param>
        /// <param name="returnResults"></param>
        /// <param name="mirrorInput"></param>
        /// <param name="moveProcessed"></param>
        public FTPConversionSettingModel(int id, bool addProcessed,
            bool returnResults, bool mirrorInput, bool moveProcessed, int userId)
        {
            Id = id;
            AddProcessed = addProcessed;
            ReturnResults = returnResults;
            MirrorInput = mirrorInput;
            MoveProcessed = moveProcessed;
            UserId = userId;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public FTPConversionSettingModel()
        {
           
        }
    }
}