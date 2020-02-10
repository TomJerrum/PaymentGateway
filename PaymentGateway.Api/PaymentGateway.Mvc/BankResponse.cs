using PaymentGateway.Domain.Enums;
using System;

namespace PaymentGateway.Mvc
{
    public class BankResponse
    {
        public string PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime ProcessedDate { get; set; }
    }
}
