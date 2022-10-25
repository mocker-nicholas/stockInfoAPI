using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Services;
using static stockInfoApi.DAL.Enums.Enums;

namespace stockInfoApi.DAL.ControllerFeatures
{
    public class StocksFeatures : IStocksFeatures
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;
        private readonly StockQuotes _request;

        public StocksFeatures(DevDbContext context, IConfiguration config, StockQuotes request)
        {
            _context = context;
            _config = config;
            _request = request;
        }

        public async Task<IEnumerable<StockDbo>> GetAllStocks(GetStocksDto getStocksDto)
        {
            IEnumerable<StockDbo> stocks = await _context.Stocks.Where(
                 s => s.AccountId!
                 .ToString() == getStocksDto.AccountId
                 .ToString())
                 .ToListAsync();
            if (!stocks.Any())
            {
                return null
;
            }
            return stocks;
        }

        public async Task<Result> GetStockBySymbol(string Symbol)
        {
            QuoteDto details = await _request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], Symbol);
            Result result = details.QuoteResponse.Result[0];
            return result;
        }

        public async Task<StockTransactionDbo> CreateStockTransaction(
            PostStockDto postStockDto,
            AccountDbo account,
            Result quoteData,
            StockDbo existingStock
        )
        {
            StockTransactionDbo transaction = postStockDto.TranType == TransactionType.Buy ?
                await BuyStock(
                    postStockDto,
                    account,
                    quoteData,
                    existingStock
                ) :
                await SellStock(
                    postStockDto,
                    account,
                    quoteData,
                    existingStock
                 );
            return transaction;
            //if (postStockDto.TranType == TransactionType.Buy)
            //{
            //    StockTransactionDbo transaction = await BuyStock(
            //        postStockDto,
            //        account,
            //        quoteData,
            //        existingStock
            //    );
            //    return transaction;
            //}
            //if (postStockDto.TranType == TransactionType.Sell)
            //{
            //    StockTransactionDbo transaction = await SellStock(
            //        postStockDto,
            //        account,
            //        quoteData,
            //        existingStock
            //     );
            //    return transaction;
            //}
            //return null;
        }

        /// <summary>
        /// Helper methods for Stock Features
        /// </summary>
        public ValidationCheck TransactionValidation(
            PostStockDto postStockDto,
            AccountDbo account,
            Result quoteData,
            StockDbo existingStock
        )
        {
            if (account == null)
            {
                return new ValidationCheck(true, "No account found");
            }
            if (quoteData == null)
            {
                return new ValidationCheck(true, "Data found for stock symbol");
            }
            if (
                postStockDto.TranType == TransactionType.Buy &&
                account.Balance < (quoteData.Ask * postStockDto.NumShares)
            )
            {
                return new ValidationCheck(true, "Insufficient funds");
            }
            if (
                (postStockDto.TranType == TransactionType.Sell &&
                existingStock == null) ||
                (postStockDto.TranType == TransactionType.Sell &&
                existingStock.NumShares < postStockDto.NumShares)
            )
            {
                return new ValidationCheck(true, "Not enough shares available");
            }
            return new ValidationCheck(false, "");
        }
        public async Task<StockDbo> StockExists(Guid accountId, string symbol)
        {
            List<StockDbo> stock = await _context.Stocks.Where(
                x => x.Symbol == symbol && x.AccountId == accountId
            ).ToListAsync();
            if (stock.Count == 0)
            {
                return null;
            }
            return stock[0];
        }

        public async Task<StockTransactionDbo> BuyStock(
            PostStockDto postStockDto,
            AccountDbo account,
            Result quoteData,
            StockDbo existingStock
            )
        {
            if (existingStock == null)
            {
                StockDbo newStock = new StockDbo(
                    account.AccountId,
                    postStockDto.Symbol,
                    quoteData.Ask * postStockDto.NumShares,
                    postStockDto.NumShares
                );
                _context.Stocks.Add(newStock);
                StockTransactionDbo transaction = new StockTransactionDbo(
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
                StockTransactionDbo transaction = new StockTransactionDbo(
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
            existingStock.NumShares -= postStockDto.NumShares;
            existingStock.TotalHoldings -= postStockDto.NumShares * quoteData.Ask;
            if (existingStock.NumShares == 0)
            {
                _context.Stocks.Remove(existingStock);
            }
            StockTransactionDbo transaction = new StockTransactionDbo(
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
