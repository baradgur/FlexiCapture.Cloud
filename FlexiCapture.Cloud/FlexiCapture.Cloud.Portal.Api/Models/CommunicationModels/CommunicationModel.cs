using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.Models.Users;

namespace FlexiCapture.Cloud.Portal.Api.Models.CommunicationModels
{
    /// <summary>
    /// model for sending notifications
    /// </summary>
    public class CommunicationModel
    {
        /// <summary>
        /// Id of communication model
        /// </summary>
        public int  Id { get; set; }
        /// <summary>
        /// user-sender info
        /// </summary>
        public UserModel Sender { get; set; }
        /// <summary>
        /// 1 - Important, 2 - MonthlyUsePayment, 3- PortalUpdatesReleases
        /// </summary>
        public int NotificationTypeId { get; set; }
        
        /// <summary>
        /// 0 - all users, -1 - single user, and other user RoleTypes
        /// </summary>
        public int UserRoleId { get; set; }
        
        /// <summary>
        /// if single there would be UserModel for single user
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        /// subject of the notification
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// body of the notification
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// creation date
        /// </summary>
        public DateTime Date { get; set; }
    }
}