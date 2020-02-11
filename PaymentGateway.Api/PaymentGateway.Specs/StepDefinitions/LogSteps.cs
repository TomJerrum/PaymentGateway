using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using PaymentGateway.Mvc.ViewModels;
using PaymentGateway.Specs.Transforms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PaymentGateway.Specs.StepDefinitions
{
    [Binding]
    public class LogSteps : Steps
    {
        ActionResult returnedActionResult;

        readonly DataContext dataContext;
        readonly LogController logController;

        public LogSteps()
        {
            dataContext = ApplicationContext.Resolve<DataContext>();
            logController = ApplicationContext.Resolve<LogController>();
        }

        [Given(@"I have the following logs stored")]
        public async Task GivenIHaveTheFollowingLogsStored(IEnumerable<LogDto> logDtos)
        {
            foreach (var logDto in logDtos)
            {
                var log = new Log
                {
                    Id = logDto.Id,
                    RequestMethod = logDto.RequestMethod,
                    RequestPath = logDto.RequestPath,
                    ResponseStatusCode = logDto.ResponseStatusCode,
                    TimeStamp = logDto.TimeStamp
                };

                await dataContext.Logs.AddAsync(log);
            }
        }

        [When(@"I get all logs")]
        public async Task WhenIGetAllLogs()
        {
            returnedActionResult = await logController.Get();
        }

        [Then(@"the log view models with the following details are returned")]
        public void ThenTheLogViewModelsWithTheFollowingDetailsAreReturned(IEnumerable<LogViewModelDto> expectedLogViewModels)
        {
            returnedActionResult.Should().BeOfType<OkObjectResult>();

            var actionResultValue = (returnedActionResult as OkObjectResult).Value;
            actionResultValue.Should().BeOfType<List<LogViewModel>>();

            var returnedLogViewModels = actionResultValue as List<LogViewModel>;
            returnedLogViewModels.Should().HaveCount(expectedLogViewModels.Count());

            foreach (var expectedLogViewModel in expectedLogViewModels)
            {
                var returnedViewModel = returnedLogViewModels.SingleOrDefault(lvm => lvm.Id == expectedLogViewModel.Id);
                AssertLogViewModelIsCorrect(returnedViewModel, expectedLogViewModel);
            }
        }

        [Then(@"the NotFound HTTP status code is returned with no logs")]
        public void ThenTheNotFoundHTTPStatusCodeIsReturnedWithNoLogs()
        {
            returnedActionResult.Should().BeOfType<NotFoundResult>();
        }

        void AssertLogViewModelIsCorrect(LogViewModel logViewModel, LogViewModelDto expectedLogViewModel)
        {
            logViewModel.Id.Should().Be(expectedLogViewModel.Id);
            logViewModel.RequestMethod.Should().Be(expectedLogViewModel.RequestMethod);
            logViewModel.RequestPath.Should().Be(expectedLogViewModel.RequestPath);
            logViewModel.ResponseStatusCode.Should().Be(expectedLogViewModel.ResponseStatusCode);
            logViewModel.TimeStamp.Should().Be(expectedLogViewModel.TimeStamp);
        }
    }
}
