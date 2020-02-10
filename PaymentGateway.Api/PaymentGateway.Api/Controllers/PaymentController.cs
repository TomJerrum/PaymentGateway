using Microsoft.AspNetCore.Mvc;
using PaymentGateway.CommandQuery.Command;
using PaymentGateway.CommandQuery.Queries;
using PaymentGateway.Mvc.Mappers;
using PaymentGateway.Mvc.Models;
using PaymentGateway.Mvc.ViewModelBuilders;
using PaymentGateway.Mvc.ViewModels;
using PaymentGateway.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        readonly IBankService bankService;
        readonly PaymentMapper paymentMapper;
        readonly AddEntityCommand addEntityCommand;
        readonly GetPaymentByIdQuery getPaymentByIdQuery;
        readonly PaymentViewModelBuilder paymentViewModelBuilder;
        readonly GetAllPaymentsQuery getAllPaymentsQuery;
        
        public PaymentController(
            IBankService bankService, 
            PaymentMapper paymentMapper, 
            AddEntityCommand addEntityCommand, 
            GetPaymentByIdQuery getPaymentByIdQuery,
            PaymentViewModelBuilder paymentViewModelBuilder,
            GetAllPaymentsQuery getAllPaymentsQuery)
        {
            this.bankService = bankService;
            this.paymentMapper = paymentMapper;
            this.addEntityCommand = addEntityCommand;
            this.getPaymentByIdQuery = getPaymentByIdQuery;
            this.paymentViewModelBuilder = paymentViewModelBuilder;
            this.getAllPaymentsQuery = getAllPaymentsQuery;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]PaymentModel model)
        {
            var bankResponse = bankService.SubmitPayment(model);
            var payment = paymentMapper.Map(model, bankResponse);
            await addEntityCommand.ExecuteAsync(payment);
            return payment.Id;
        }

        [HttpGet("{id}")]
        public async Task<PaymentViewModel> Get(string id)
        {
            var payment = await getPaymentByIdQuery.ExecuteAsync(id);
            return paymentViewModelBuilder.Build(payment);
        }

        [HttpGet]
        public async Task<List<PaymentViewModel>> Get()
        {
            var payments = await getAllPaymentsQuery.ExecuteAsync();
            return paymentViewModelBuilder.Build(payments);
        }
    }
}
