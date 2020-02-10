using PaymentGateway.Domain;
using PaymentGateway.Mvc.Models;

namespace PaymentGateway.Mvc.Mappers
{
    public class PaymentMapper
    {
        public Payment Map(PaymentModel model, BankResponse bankResponse)
        {
            return new Payment 
            {
                Id = bankResponse.PaymentId,
                Status = bankResponse.PaymentStatus,
                ProcessedDate = bankResponse.ProcessedDate,
                CardNumber = model.CardNumber,
                Amount = model.Amount,
                Currency = model.Currency,
                CVV = model.CVV,
                ExpiryDate = model.ExpiryDate
            };
        }
    }
}
