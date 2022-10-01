using Microsoft.EntityFrameworkCore;
using stockInfoApi.Models.DboModels;

namespace stockInfoApi.Data
{
    public class DevDbContext : DbContext
    {
        public DevDbContext(DbContextOptions<DevDbContext> options) : base(options)
        {

        }

        public DbSet<AccountDbo> Accounts { get; set; }
        public DbSet<StockDbo> Stocks { get; set; }
        public DbSet<TransactionDbo> Transactions { get; set; }
    }
}
