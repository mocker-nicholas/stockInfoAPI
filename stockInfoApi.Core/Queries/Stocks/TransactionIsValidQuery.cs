using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;

namespace stockInfoApi.DAL.Queries.Stocks
{
    public record TransactionIsValidQuery(
            PostStockDto PostStockDto,
            AccountDbo Account,
            Result QuoteData,
            StockDbo ExistingStock
        ) : IRequest<ValidationCheck>;
}
