using PaymentGateway.Domain;
using PaymentGateway.Mvc.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PaymentGateway.Mvc.ViewModelBuilders
{
    public class LogViewModelBuilder
    {
        public List<LogViewModel> Build(List<Log> logs)
        {
            return logs.Select(Build).ToList();
        }

        LogViewModel Build(Log log)
        {
            return new LogViewModel
            {
                Id = log.Id,
                RequestMethod = log.RequestMethod,
                RequestPath = log.RequestPath,
                ResponseStatusCode = log.ResponseStatusCode,
                TimeStamp = log.TimeStamp
            };
        }
    }
}
