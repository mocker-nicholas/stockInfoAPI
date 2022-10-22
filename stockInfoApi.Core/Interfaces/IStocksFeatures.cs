using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;

namespace stockInfoApi.DAL.Interfaces
{
    public interface IStocksFeatures
    {
        Task<IEnumerable<StockDbo>> GetAllStocks(GetStocksDto getStocksDto);
        Task<Result> GetStockBySymbol(string symbol);
        Task<StockTransactionDbo> CreateStockTransaction(
            PostStockDto postStockDto,
            AccountDbo account,
            Result quoteData,
            StockDbo existingStock
        );
        Task<StockDbo> StockExists(Guid accountId, string symbol);
        ValidationCheck TransactionValidation(
            PostStockDto postStockDto,
            AccountDbo account,
            Result quoteData,
            StockDbo existingStock
        );
    }
}
