using MediatR;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Queries.Stocks;
using static stockInfoApi.DAL.Enums.Enums;

namespace stockInfoApi.DAL.Handlers.Stocks
{
    internal class TransactionIsValidHandler : IRequestHandler<TransactionIsValidQuery, ValidationCheck>
    {
        public async Task<ValidationCheck> Handle(TransactionIsValidQuery request, CancellationToken cancellationToken)
        {
            {
                if (request.Account == null)
                {
                    return new ValidationCheck(true, "No account found");
                }
                if (request.QuoteData == null)
                {
                    return new ValidationCheck(true, "Data found for stock symbol");
                }
                if (
                request.PostStockDto.TranType == TransactionType.Buy &&
                    request.Account.Cash < (request.QuoteData.Ask * request.PostStockDto.NumShares)
                )
                {
                    return new ValidationCheck(true, "Insufficient funds");
                }
                if (
                    (request.PostStockDto.TranType == TransactionType.Sell &&
                    request.ExistingStock == null) ||
                    (request.PostStockDto.TranType == TransactionType.Sell &&
                    request.ExistingStock.NumShares < request.PostStockDto.NumShares)
                )
                {
                    return new ValidationCheck(true, "Not enough shares available");
                }
                return new ValidationCheck(false, "");
            }
        }
    }
}
