using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.CommandQuery.Queries
{
    public class GetAllLogsQuery
    {
        readonly DataContext dataContext;

        public GetAllLogsQuery(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Log>> ExecuteAsync()
        {
            return await dataContext.Logs.ToListAsync();
        }
    }
}
