using PaymentGateway.Domain;
using PaymentGateway.EntityFramework;
using System.Threading.Tasks;

namespace PaymentGateway.CommandQuery.Command
{
    public class AddEntityCommand
    {
        readonly DataContext dataContext;

        public AddEntityCommand(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task ExecuteAsync<T>(T entity) where T : Entity
        {
            await dataContext.Set<T>().AddAsync(entity);
        }
    }
}
