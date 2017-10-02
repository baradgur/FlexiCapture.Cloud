using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexiCapture.Cloud.Portal.Api.DB;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;
using FlexiCapture.Cloud.Portal.Api.Models.PaypalModels;

namespace FlexiCapture.Cloud.Portal.Api.Helpers.PaymentAndTransactionsHelpers
{
    public static class PaymentAndTransactionsHelper
    {
        /// <summary>
        /// Check if vault ID exists by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Models.PaypalModels.VaultUserPair CheckVaultIdExistance(int userId)
        {
            try
            {
                using (var db = new FCCPortalEntities())
                {
                    var response = db.Vault.Where(x => x.UserId == userId)
                        .DefaultIfEmpty(null)
                            .Single();

                    return new VaultUserPair(response.UserId, response.VaultId);
                        //(x => x.UserId == userId);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Adding new vaultID-userID pair
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vaultId"></param>
        public static Models.PaypalModels.VaultUserPair AddNewVaultId(VaultUserPair model)
        {
            try
            {
                using (FCCPortalEntities db = new FCCPortalEntities())
                {
                    try
                    {
                        var vault = new Vault()
                        {
                            UserId = model.UserId,
                            VaultId = model.VaultId
                        };

                        db.Vault.Add(vault);
                        db.SaveChanges();

                        using (PaymentAgentEntities db2 = new PaymentAgentEntities())
                        {
                            db2.Card.Add(new Card()
                            {
                                VaultID = model.VaultId,
                                CustomerID = model.UserId,
                                payerId = Guid.NewGuid().ToString()
                            });

                            db2.SaveChanges();
                        }

                        return model;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Models.PaypalModels.Transaction AddNewTransaction(Models.PaypalModels.Transaction model)
        {
            try
            {
                using (PaymentAgentEntities db = new PaymentAgentEntities())
                {
                    var merchantObject = db.MerchantDetail.ToList().LastOrDefault();  
                    int merchantId = merchantObject?.MerchantID ?? 0;


                    if (!model.IsRecurring)
                    {
                        var transactionNew = new DB_PaymentAgent.Transaction()
                        {
                            CustomerID = model.CustomerId,
                            IsOneTime = true,
                            MerchantID = merchantId,
                            PaymentDate = DateTime.Today,
                            TransactionNumber = model.Number,
                            Status = 0,
                            TransactionCurrency = "USD"
                        };
                        db.Transaction.Add(transactionNew);
                        db.SaveChanges();

                        model.Id = transactionNew.TransactionID;
                        model.Status = transactionNew.Status;

                        return model;
                    }
                    else
                    {
                        var recurringTransactionNew = new RecurringTransaction()
                        {
                            CustomerID = model.CustomerId,
                            MerchantID = merchantId,
                            AgreementPaymentDate = model.PaymentDate.AddDays(1),
                            Frequency = model.Frequency,
                            FrequencyType = model.FrequencyType,
                            PaymentType = model.PaymentType
                        };
                        db.RecurringTransaction.Add(recurringTransactionNew);
                        db.SaveChanges();

                        model.Id = recurringTransactionNew.TransactionID;
                        model.Status = recurringTransactionNew.Status;

                        return model;
                    }
                    
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