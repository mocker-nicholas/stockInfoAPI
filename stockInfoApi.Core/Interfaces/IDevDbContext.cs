using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Interfaces
{
    public interface IDevDbContext : IDisposable
    {
        DbSet<AccountDbo> Accounts { get; set; }
        DbSet<StockDbo> Stocks { get; set; }
        DbSet<StockTransactionDbo> Transactions { get; set; }
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Entry<TEntity>(TEntity entity) where TEntity : class;
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        TEntity GetById<TEntity, TId>(TId id) where TEntity : class;
        int SaveChanges();
    }
}
