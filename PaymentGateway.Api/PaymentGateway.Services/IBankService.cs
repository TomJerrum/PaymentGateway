using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;

namespace PaymentGateway.Services
{
    public interface IBankService
    {
        BankResponseDto SubmitPayment(PaymentModel model);
    }
}
