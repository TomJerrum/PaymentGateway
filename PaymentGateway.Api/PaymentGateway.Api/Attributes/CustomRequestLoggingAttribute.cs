using Microsoft.AspNetCore.Mvc.Filters;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using System;

namespace PaymentGateway.Api.Attributes
{
    public sealed class CustomRequestLoggingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var log = new Log
            {
                Id = Guid.NewGuid().ToString(),
                RequestMethod = context.HttpContext.Request.Method.ToString(),
                RequestPath = context.HttpContext.Request.Path.ToString(),
                ResponseStatusCode = context.Result.GetType().GetProperty("StatusCode").GetValue(context.Result).ToString(),
                TimeStamp = DateTime.Now
            };

            var dataContext = ApplicationContext.Resolve<DataContext>();
            dataContext.Logs.Add(log);
            dataContext.SaveChanges();
        }
    }
}
