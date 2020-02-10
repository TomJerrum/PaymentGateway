using PaymentGateway.Domain.Enums;
using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;
using System;

namespace PaymentGateway.Services
{
    public class FakeBankService : IBankService
    {
        public BankResponseDto SubmitPayment(PaymentModel model)
        {
            return new BankResponseDto
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentStatus = PaymentStatus.Successful,
                ProcessedDate = DateTime.Now
            };
        }
    }
}
