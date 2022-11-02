using MediatR;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Queries.Stocks;
using static stockInfoApi.DAL.Enums.Enums;

namespace stockInfoApi.DAL.Handlers.Stocks
{
    public class CreateStockTransactionHandler : IRequestHandler<CreateStockTransactionQuery, StockTransactionDbo>
    {
        private readonly DevDbContext _context;
        public CreateStockTransactionHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<StockTransactionDbo> Handle(CreateStockTransactionQuery request, CancellationToken cancellationToken)
        {
            StockTransactionDbo transaction = request.PostStockDto.TranType == TransactionType.Buy ?
                await BuyStock(
                    request.PostStockDto,
                    request.Account,
                    request.QuoteData,
                    request.ExistingStock
                ) :
                await SellStock(
                     request.PostStockDto,
                    request.Account,
                    request.QuoteData,
                    request.ExistingStock
            );
            return transaction;
        }

        public async Task<StockTransactionDbo> BuyStock(
           PostStockDto postStockDto,
           AccountDbo account,
           Result quoteData,
           StockDbo existingStock
           )
        {
            account.Cash -= Math.Round((quoteData.Ask * postStockDto.NumShares), 2, MidpointRounding.AwayFromZero);
            if (existingStock == null)
            {
                StockDbo newStock = new(
                    account.AccountId,
                    postStockDto.Symbol,
                    quoteData.Ask * postStockDto.NumShares,
                    postStockDto.NumShares
                );
                _context.Stocks.Add(newStock);
                StockTransactionDbo transaction = new(
                    newStock.AccountId,
                    newStock.Symbol,
                    newStock.NumShares,
                    postStockDto.TranType,
                    quoteData.Ask
                );
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return transaction;
            }
            else
            {
                existingStock.NumShares += postStockDto.NumShares;
                existingStock.TotalHoldings += postStockDto.NumShares * quoteData.Ask;
                StockTransactionDbo transaction = new(
                    existingStock.AccountId,
                    existingStock.Symbol,
                    postStockDto.NumShares,
                    postStockDto.TranType,
                    quoteData.Ask
                );
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return transaction;
            }
        }

        public async Task<StockTransactionDbo> SellStock(
                    PostStockDto postStockDto,
                    AccountDbo account,
                    Result quoteData,
                    StockDbo existingStock
            )
        {
            account.Cash += Math.Round((quoteData.Ask * postStockDto.NumShares), 2, MidpointRounding.AwayFromZero);
            existingStock.NumShares -= postStockDto.NumShares;
            existingStock.TotalHoldings -= postStockDto.NumShares * quoteData.Ask;
            if (existingStock.NumShares == 0)
            {
                _context.Stocks.Remove(existingStock);
            }
            StockTransactionDbo transaction = new(
                existingStock.AccountId,
                existingStock.Symbol,
            postStockDto.NumShares,
                postStockDto.TranType,
                quoteData.Ask
            );
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
