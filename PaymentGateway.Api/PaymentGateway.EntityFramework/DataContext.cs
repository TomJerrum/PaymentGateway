using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;

namespace PaymentGateway.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
