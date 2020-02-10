using System;

namespace PaymentGateway.Specs.Transforms
{
    public class PaymentModelDto
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
    }
}
