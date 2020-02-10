using PaymentGateway.Domain;
using PaymentGateway.Mvc.ViewModels;

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
                CardNumber = payment.CardNumber,
                Currency = payment.Currency, 
                CVV = payment.CVV,
                ExpiryDate = payment.ExpiryDate,
                ProcessedDate = payment.ProcessedDate,
                Status = payment.Status.ToString()
            };
        }
    }
}
