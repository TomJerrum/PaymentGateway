using System;

namespace PaymentGateway.Mvc.ViewModels
{
    public class LogViewModel
    {
        public string Id { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string ResponseStatusCode { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
