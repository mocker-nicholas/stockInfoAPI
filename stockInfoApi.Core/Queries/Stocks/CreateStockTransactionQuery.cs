using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;

namespace stockInfoApi.DAL.Queries.Stocks
{
    public record CreateStockTransactionQuery(
            PostStockDto PostStockDto,
            AccountDbo Account,
            Result QuoteData,
            StockDbo ExistingStock
        ) : IRequest<StockTransactionDbo>;
}
