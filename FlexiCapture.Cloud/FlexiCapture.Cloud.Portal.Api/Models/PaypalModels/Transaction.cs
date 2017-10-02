using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.PaypalModels
{
    public class Transaction
    {
        /// <summary>
        /// Identity
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of user (from FCCPortal db)
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Transaction number from PP API
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Amount of money we pay
        /// </summary>
        public decimal PaymentValue { get; set; }
        /// <summary>
        /// Status of transaction 0 - pending, 1 - success, 2 - failed
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// True, if we make recurring transaction
        /// </summary>
        public bool IsRecurring { get; set; }
        /// <summary>
        /// When payment created
        /// </summary>
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// Freqency type, 1 - monthly
        /// </summary>
        public int FrequencyType { get; set; }
        /// <summary>
        /// Frequency. Always 1. Maybe.
        /// </summary>
        public int Frequency { get; set; } = 1;
        /// <summary>
        /// I don't have any idea what is this. Always 1.
        /// </summary>
        public int PaymentType { get; set; } = 1;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="number"></param>
        /// <param name="paymentValue"></param>
        /// <param name="status"></param>
        /// <param name="frequencyType"></param>
        /// <param name="isRecurring"></param>
        public Transaction(int id, int customerId, string number, decimal paymentValue,
            int status, int frequencyType, bool isRecurring = false)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            PaymentValue = paymentValue;
            Status = status;
            IsRecurring = isRecurring;
            PaymentDate = DateTime.Today;
            FrequencyType = frequencyType;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Transaction()
        {
            PaymentDate = DateTime.Today;
        }

    }
}