//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.EmailAgent.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailStates
    {
        public EmailStates()
        {
            this.Emails = new HashSet<Emails>();
        }
    
        public int Id { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<Emails> Emails { get; set; }
    }
}