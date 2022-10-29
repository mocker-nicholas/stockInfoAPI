using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos;

namespace stockInfoApi.DAL.Interfaces
{
    public interface ITransactionsFeatures
    {
        Task<List<StockTransactionDbo>> GetAllTransactionsForAccount(Guid accountId);
        Task<StockTransactionDbo?> GetTransactionById(Guid id, GetTransactionDto getTransactionDto);
    }
}
