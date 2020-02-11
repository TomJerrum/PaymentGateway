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
                PaymentStatus = GenerateRandomStatus(),
                ProcessedDate = DateTime.Now
            };
        }

        PaymentStatus GenerateRandomStatus()
        {
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 1)
            {
                return PaymentStatus.Successful;
            }
            else
            {
                return PaymentStatus.Unsuccessful;
            }
        }
    }
}
