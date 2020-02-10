using Microsoft.AspNetCore.Mvc;
using PaymentGateway.CommandQuery.Command;
using PaymentGateway.Mvc.Mappers;
using PaymentGateway.Mvc.Models;
using PaymentGateway.Services;
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

        public PaymentController(IBankService bankService, PaymentMapper paymentMapper, AddEntityCommand addEntityCommand)
        {
            this.bankService = bankService;
            this.paymentMapper = paymentMapper;
            this.addEntityCommand = addEntityCommand;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]PaymentModel model)
        {
            var bankResponse = bankService.SubmitPayment(model);
            var payment = paymentMapper.Map(model, bankResponse);
            await addEntityCommand.ExecuteAsync(payment);
            return payment.Id;
        }
    }
}
