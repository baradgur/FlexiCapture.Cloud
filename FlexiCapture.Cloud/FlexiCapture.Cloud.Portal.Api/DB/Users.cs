//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.Portal.Api.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.Tasks = new HashSet<Tasks>();
            this.UserLogins = new HashSet<UserLogins>();
            this.UserServiceSubscribes = new HashSet<UserServiceSubscribes>();
            this.UserSettings = new HashSet<UserSettings>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
    
        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<UserLogins> UserLogins { get; set; }
        public virtual ICollection<UserServiceSubscribes> UserServiceSubscribes { get; set; }
        public virtual ICollection<UserSettings> UserSettings { get; set; }
    }
}
