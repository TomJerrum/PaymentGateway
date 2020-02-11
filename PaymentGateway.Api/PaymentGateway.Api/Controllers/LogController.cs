using Microsoft.AspNetCore.Mvc;
using PaymentGateway.CommandQuery.Queries;
using PaymentGateway.Mvc.ViewModelBuilders;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        readonly GetAllLogsQuery getAllLogsQuery;
        readonly LogViewModelBuilder logViewModelBuilder;

        public LogController(GetAllLogsQuery getAllLogsQuery, LogViewModelBuilder logViewModelBuilder)
        {
            this.getAllLogsQuery = getAllLogsQuery;
            this.logViewModelBuilder = logViewModelBuilder;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var logs = await getAllLogsQuery.ExecuteAsync();

            if (!logs.Any())
            {
                return NotFound();
            }

            var logViewModels = logViewModelBuilder.Build(logs);
            return Ok(logViewModels);
        }
    }
}