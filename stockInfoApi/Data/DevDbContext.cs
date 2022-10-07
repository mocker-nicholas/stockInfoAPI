using Microsoft.EntityFrameworkCore;
using stockInfoApi.Models.DboModels;

namespace stockInfoApi.Data
{
    public class DevDbContext : DbContext
    {
        public DevDbContext(DbContextOptions<DevDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AccountDbo>()
                .Property(accountDbo => accountDbo.AccountType)
                .HasConversion<string>();
        }

        public DbSet<AccountDbo> Accounts { get; set; }
        public DbSet<StockDbo> Stocks { get; set; }
        public DbSet<TransactionDbo> Transactions { get; set; }
    }
}
