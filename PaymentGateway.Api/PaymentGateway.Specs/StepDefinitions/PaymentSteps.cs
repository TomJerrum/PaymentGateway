using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Api.Controllers;
using PaymentGateway.EntityFramework;
using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;
using PaymentGateway.Services;
using PaymentGateway.Specs.Fakes;
using PaymentGateway.Specs.Transforms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PaymentGateway.Specs.StepDefinitions
{
    [Binding]
    public class PaymentSteps : Steps
    {
        string returnedPaymentId;

        readonly TestBankService bankService;
        readonly PaymentController paymentController;
        readonly DataContext dataContext;

        public PaymentSteps()
        {
            bankService = ApplicationContext.Resolve<IBankService>() as TestBankService;
            paymentController = ApplicationContext.Resolve<PaymentController>();
            dataContext = ApplicationContext.Resolve<DataContext>();
        }

        [Given(@"I receive the following response from the bank service")]
        public void GivenIReceiveTheFollowingResponseFromTheBankService(BankResponseDto bankResponseDto)
        {
            var bankResponse = new BankResponse
            {
                PaymentId = bankResponseDto.PaymentId,
                PaymentStatus = bankResponseDto.PaymentStatus,
                ProcessedDate = bankResponseDto.ProcessedDate
            };

            bankService.SetBankResponse(bankResponse);
        }

        [When(@"I submit the following payment")]
        public async Task WhenISubmitTheFollowingPayment(PaymentModelDto paymentModelDto)
        {
            var model = new PaymentModel
            {
                Amount = paymentModelDto.Amount,
                CardNumber = paymentModelDto.CardNumber,
                Currency = paymentModelDto.Currency,
                CVV = paymentModelDto.CVV,
                ExpiryDate = paymentModelDto.ExpiryDate
            };

            returnedPaymentId = await paymentController.Post(model);
        }

        [Then(@"the payment id '(.*)' is returned")]
        public void ThenThePaymentIdIsReturned(string expectedPaymentId)
        {
            returnedPaymentId.Should().Be(expectedPaymentId);
        }

        [Then(@"the following payments are stored")]
        public async Task ThenTheFollowingPaymentsAreStored(IEnumerable<PaymentDto> expectedPayments)
        {
            var storedPayments = await dataContext.Payments.ToListAsync();
            storedPayments.Should().HaveCount(expectedPayments.Count());

            foreach (var expectedPayment in expectedPayments)
            {
                storedPayments.Should().ContainSingle(p =>
                    p.Id == expectedPayment.Id &&
                    p.Status == expectedPayment.Status &&
                    p.ProcessedDate == expectedPayment.ProcessedDate &&
                    p.CardNumber == expectedPayment.CardNumber &&
                    p.ExpiryDate == expectedPayment.ExpiryDate &&
                    p.Amount == expectedPayment.Amount &&
                    p.Currency == expectedPayment.Currency &&
                    p.CVV == expectedPayment.CVV);
            }
        }
    }
}
