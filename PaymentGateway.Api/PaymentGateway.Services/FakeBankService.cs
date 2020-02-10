using PaymentGateway.Domain.Enums;
using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;
using System;

namespace PaymentGateway.Services
{
    public class FakeBankService : IBankService
    {
        public BankResponse SubmitPayment(PaymentModel model)
        {
            return new BankResponse
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentStatus = PaymentStatus.Successful,
                ProcessedDate = DateTime.Now
            };
        }
    }
}
