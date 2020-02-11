using System;

namespace PaymentGateway.Specs.Transforms
{
    public class LogViewModelDto
    {
        public string Id { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string ResponseStatusCode { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
