using PaymentGateway.Domain.Enums;
using System;

namespace PaymentGateway.Specs.Transforms
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime ProcessedDate { get; set; }

        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CVV { get; set; }
    }
}
