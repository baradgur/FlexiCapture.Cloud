namespace FlexiCapture.Cloud.Portal.Api.Models.Documents
{
    /// <summary>
    /// list of errors in document
    /// </summary>
    public class DocumentError
    {   
        /// <summary>
        /// Name
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// error text
        /// </summary>
        public string ErrorText { get; set; }
    }
}