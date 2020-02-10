using System;

namespace PaymentGateway.Mvc.ViewModels
{
    public class PaymentViewModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
    }
}
