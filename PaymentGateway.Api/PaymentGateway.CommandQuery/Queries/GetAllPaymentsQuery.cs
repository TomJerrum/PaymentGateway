using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.CommandQuery.Queries
{
    public class GetAllPaymentsQuery
    {
        readonly DataContext dataContext;

        public GetAllPaymentsQuery(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Payment>> ExecuteAsync()
        {
            return await dataContext.Payments.ToListAsync();
        }
    }
}
