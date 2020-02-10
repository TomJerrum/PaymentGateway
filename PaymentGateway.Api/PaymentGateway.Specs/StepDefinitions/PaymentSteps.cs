using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using PaymentGateway.Mvc;
using PaymentGateway.Mvc.Models;
using PaymentGateway.Mvc.ViewModels;
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
        PaymentViewModel returnedPaymentViewModel;

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

        [Given(@"I have the following payments stored")]
        public async Task GivenIHaveTheFollowingPaymentsStored(IEnumerable<PaymentDto> paymentDtos)
        {
            foreach (var paymentDto in paymentDtos)
            {
                var payment = new Payment
                {
                    Id = paymentDto.Id,
                    Amount = paymentDto.Amount,
                    CardNumber = paymentDto.CardNumber,
                    Currency = paymentDto.Currency,
                    CVV = paymentDto.CVV,
                    ExpiryDate = paymentDto.ExpiryDate,
                    ProcessedDate = paymentDto.ProcessedDate, 
                    Status = paymentDto.Status
                };

                await dataContext.Payments.AddAsync(payment);
            }
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

        [When(@"I get the payment with the id '(.*)'")]
        public async Task WhenIGetThePaymentWithTheId(string paymentId)
        {
            returnedPaymentViewModel = await paymentController.Get(paymentId);
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

        [Then(@"the payment view model with the following details is returned")]
        public void ThenThePaymentViewModelWithTheFollowingDetailsIsReturned(PaymentViewModelDto expectedPaymentViewModel)
        {
            returnedPaymentViewModel.Id.Should().Be(expectedPaymentViewModel.Id);
            returnedPaymentViewModel.Amount.Should().Be(expectedPaymentViewModel.Amount);
            returnedPaymentViewModel.CardNumber.Should().Be(expectedPaymentViewModel.CardNumber);
            returnedPaymentViewModel.Currency.Should().Be(expectedPaymentViewModel.Currency);
            returnedPaymentViewModel.CVV.Should().Be(expectedPaymentViewModel.CVV);
            returnedPaymentViewModel.ExpiryDate.Should().Be(expectedPaymentViewModel.ExpiryDate);
            returnedPaymentViewModel.ProcessedDate.Should().Be(expectedPaymentViewModel.ProcessedDate);
            returnedPaymentViewModel.Status.Should().Be(expectedPaymentViewModel.Status);
        }
    }
}
