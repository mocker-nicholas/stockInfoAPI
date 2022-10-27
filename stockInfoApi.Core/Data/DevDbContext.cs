using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Data
{
    public class DevDbContext : DbContext, IDevDbContext
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
        public DbSet<StockTransactionDbo> Transactions { get; set; }

        void IDevDbContext.Add<TEntity>(TEntity entity)
        {
            Add(entity);
        }

        TEntity IDevDbContext.GetById<TEntity, TId>(TId id)
        {
            var entity = Set<TEntity>().Find(id);
            return entity;
        }

        void IDevDbContext.Entry<TEntity>(TEntity entity)
        {
            Update(entity);
        }

        void IDevDbContext.Remove<TEntity>(TEntity entity)
        {
            Remove(entity);
        }

        IQueryable<TEntity> IDevDbContext.Query<TEntity>()
        {
            return Set<TEntity>();
        }
    }
}
