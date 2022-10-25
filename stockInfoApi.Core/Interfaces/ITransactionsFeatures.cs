using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Interfaces
{
    public interface ITransactionsFeatures
    {
        Task<List<StockTransactionDbo>> GetAllTransactions(Guid id);
    }
}
