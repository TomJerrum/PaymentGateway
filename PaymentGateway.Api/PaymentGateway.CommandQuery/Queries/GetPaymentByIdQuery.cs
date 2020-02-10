using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using System.Threading.Tasks;

namespace PaymentGateway.CommandQuery.Queries
{
    public class GetPaymentByIdQuery
    {
        readonly DataContext dataContext;

        public GetPaymentByIdQuery(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Payment> ExecuteAsync(string paymentId)
        {
            return await dataContext.Payments.SingleOrDefaultAsync(p => p.Id == paymentId);
        }
    }
}
