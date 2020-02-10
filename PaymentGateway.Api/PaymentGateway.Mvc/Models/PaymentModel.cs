using System;

namespace PaymentGateway.Mvc.Models
{
    public class PaymentModel
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
