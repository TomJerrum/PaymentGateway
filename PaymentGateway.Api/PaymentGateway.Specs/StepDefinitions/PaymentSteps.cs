using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
        ActionResult returnedActionResult;

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
            returnedActionResult = await paymentController.Get(paymentId);
        }

        [When(@"I get all payments")]
        public async Task WhenIGetAllPayments()
        {
            returnedActionResult = await paymentController.Get();
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
            returnedActionResult.Should().BeOfType<OkObjectResult>();

            var actionResultValue = (returnedActionResult as OkObjectResult).Value;
            actionResultValue.Should().BeOfType<PaymentViewModel>();

            AssertPaymentViewModelIsCorrect(actionResultValue as PaymentViewModel, expectedPaymentViewModel);
        }

        [Then(@"the payment view models with the following details are returned")]
        public void ThenThePaymentViewModelsWithTheFollowingDetailsAreReturned(IEnumerable<PaymentViewModelDto> expectedPaymentViewModels)
        {
            returnedActionResult.Should().BeOfType<OkObjectResult>();

            var actionResultValue = (returnedActionResult as OkObjectResult).Value;
            actionResultValue.Should().BeOfType<List<PaymentViewModel>>();

            var returnedPaymentViewModels = actionResultValue as List<PaymentViewModel>;
            returnedPaymentViewModels.Should().HaveCount(expectedPaymentViewModels.Count());

            foreach (var expectedPaymentViewModel in expectedPaymentViewModels)
            {
                var returnedViewModel = returnedPaymentViewModels.SingleOrDefault(pvm => pvm.Id == expectedPaymentViewModel.Id);
                AssertPaymentViewModelIsCorrect(returnedViewModel, expectedPaymentViewModel);
            }
        }

        [Then(@"the NotFound HTTP status code is returned")]
        public void ThenTheNotFoundHTTPStatusCodeIsReturned()
        {
            returnedActionResult.Should().BeOfType<NotFoundResult>();
        }

        void AssertPaymentViewModelIsCorrect(PaymentViewModel paymentViewModel, PaymentViewModelDto expectedPaymentViewModel)
        {
            paymentViewModel.Id.Should().Be(expectedPaymentViewModel.Id);
            paymentViewModel.Amount.Should().Be(expectedPaymentViewModel.Amount);
            paymentViewModel.CardNumber.Should().Be(expectedPaymentViewModel.CardNumber);
            paymentViewModel.Currency.Should().Be(expectedPaymentViewModel.Currency);
            paymentViewModel.CVV.Should().Be(expectedPaymentViewModel.CVV);
            paymentViewModel.ExpiryDate.Should().Be(expectedPaymentViewModel.ExpiryDate);
            paymentViewModel.ProcessedDate.Should().Be(expectedPaymentViewModel.ProcessedDate);
            paymentViewModel.Status.Should().Be(expectedPaymentViewModel.Status);
        }
    }
}
