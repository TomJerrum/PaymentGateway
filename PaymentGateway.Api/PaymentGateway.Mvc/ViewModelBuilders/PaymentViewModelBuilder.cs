using PaymentGateway.Domain;
using PaymentGateway.Mvc.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PaymentGateway.Mvc.ViewModelBuilders
{
    public class PaymentViewModelBuilder
    {
        public PaymentViewModel Build(Payment payment)
        {
            return new PaymentViewModel
            {
                Id = payment.Id,
                Amount = payment.Amount,
                CardNumber = GenerateMaskedCardNumber(payment.CardNumber),
                Currency = payment.Currency, 
                CVV = payment.CVV,
                ExpiryDate = payment.ExpiryDate,
                ProcessedDate = payment.ProcessedDate,
                Status = payment.Status.ToString()
            };
        }

        public List<PaymentViewModel> Build(List<Payment> payments)
        {
            return payments.Select(Build).ToList();
        }

        string GenerateMaskedCardNumber(string cardNumber)
        {
            var maskedCardNumber = "";

            for (int x = 0; x < cardNumber.Length - 4; x++) 
            {
                maskedCardNumber += "*";
            }

            maskedCardNumber += cardNumber.Substring(cardNumber.Length - 4, 4);
            return maskedCardNumber;
        }
    }
}
