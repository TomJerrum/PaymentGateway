using Microsoft.AspNetCore.Mvc.Filters;
using PaymentGateway.EntityFramework;

namespace PaymentGateway.Api.Attributes
{
    public sealed class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context != null && context.Exception == null)
            {
                var dataContext = ApplicationContext.Resolve<DataContext>();
                dataContext.SaveChanges();
            }

            base.OnActionExecuted(context);
        }
    }
}
