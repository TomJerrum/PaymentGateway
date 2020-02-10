using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;
using PaymentGateway.Services;

namespace PaymentGateway.Specs.Fakes
{
    public class TestBankService : IBankService
    {
        public BankResponse BankResponse { get; private set; }

        public void SetBankResponse(BankResponse bankResponse)
        {
            BankResponse = bankResponse;
        }

        public BankResponse SubmitPayment(PaymentModel model)
        {
            return BankResponse;
        }
    }
}
