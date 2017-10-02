using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.PaypalModels
{
    public class VaultUserPair
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string VaultId { get; set; }
        public VaultUserPair(int id, int userId, string vaultId)
        {
            Id = id;
            UserId = userId;
            VaultId = vaultId;
        }
        public VaultUserPair(int userId, string vaultId)
        {
            Id = 0;
            UserId = userId;
            VaultId = vaultId;
        }

        public VaultUserPair()
        {
            
        }
    }
}