using FlexiCapture.Cloud.Portal.Api.Models.Errors;

namespace FlexiCapture.Cloud.Portal.Api.Models.SettingsModels
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailResponseSettingsModel
    {   
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// show conversion jobs in the Library
        /// </summary>
        public bool ShowJob { get; set; }
        /// <summary>
        /// Reply to Sender’s ‘from’ e-mail address with conversion results
        /// </summary>
        public bool SendReply { get; set; }
        /// <summary>
        /// Include conversion result as attachment
        /// </summary>
        public bool AddAttachment { get; set; }
        /// <summary>
        /// Include conversion result link in e-mail body
        /// </summary>
        public bool AddLink { get; set; }
        /// <summary>
        /// CC the response with conversion result to the following e-mail address(es)
        /// </summary>
        public bool CCResponse { get; set; }
        /// <summary>
        /// e-mail address(es).  Separate multiple addresses with a comma.
        /// </summary>
        public string Addresses { get; set; }
    }
}