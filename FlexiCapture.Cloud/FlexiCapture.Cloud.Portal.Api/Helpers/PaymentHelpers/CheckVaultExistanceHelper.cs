using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.PaymentHelpers
{
    public static class CheckVaultExistanceHelper
    {
        /// <summary>
        /// Check if user have paypal already
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckVaultExistance(int userId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    return db.Vault.Any(x => x.UserId == userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Adding new VaultId for recent user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vaultId"></param>
        public static void AddToVault(int userId, int vaultId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    db.Vault.Add(new Vault()
                    {
                        VaultId = vaultId,
                        UserId = userId
                    });

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}