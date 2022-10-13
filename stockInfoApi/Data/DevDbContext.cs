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
            builder.Entity<StockTransactionDbo>()
               .Property(stockTransaction => stockTransaction.TranType)
               .HasConversion<string>();
        }

        public DbSet<AccountDbo> Accounts { get; set; }
        public DbSet<StockDbo> Stocks { get; set; }
        public DbSet <StockTransactionDbo> Transactions { get; set; }
}
}
