using System;

namespace PaymentGateway.Domain
{
    public class Log : Entity
    {
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string ResponseStatusCode { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
