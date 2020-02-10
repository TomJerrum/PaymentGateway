using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;

namespace PaymentGateway.Services
{
    public interface IBankService
    {
        BankResponse SubmitPayment(PaymentModel model);
    }
}
