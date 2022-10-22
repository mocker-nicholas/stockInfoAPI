using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Enums;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
           /// <summary>
           /// Buy a new stock or add existing shares
           /// </summary>
            if (postStockDto.TranType == Enums.Enums.TransactionType.Buy)
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
                    await _context.SaveChangesAsync();
                    StockTransactionDbo transaction = new StockTransactionDbo(
                        newStock.AccountId,
                        newStock.Symbol,
                        newStock.NumShares,
                        postStockDto.TranType,
                        quoteData.Ask
                    );
                    return transaction;
                }
                else
                {
                    existingStock.NumShares += postStockDto.NumShares;
                    existingStock.TotalHoldings += postStockDto.NumShares * quoteData.Ask;
                    await _context.SaveChangesAsync();
                    StockTransactionDbo transaction = new StockTransactionDbo(
                        existingStock.AccountId,
                        existingStock.Symbol,
                        postStockDto.NumShares,
                        postStockDto.TranType,
                        quoteData.Ask
                    );
                    return transaction;
                }
            }
            /// <summary>
            /// Sell a stock and subtract from existing shares
            /// </summary>
            if (postStockDto.TranType == TransactionType.Sell)
            {
                existingStock.NumShares -= postStockDto.NumShares;
                existingStock.TotalHoldings -= postStockDto.NumShares * quoteData.Ask;
                if(existingStock.NumShares == 0)
                {
                    _context.Stocks.Remove(existingStock);
                }
                await _context.SaveChangesAsync();
                StockTransactionDbo transaction = new StockTransactionDbo(
                    existingStock.AccountId,
                    existingStock.Symbol,
                    postStockDto.NumShares,
                    postStockDto.TranType,
                    quoteData.Ask
                );
                return transaction;
            }
            return null;
        }

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
            if(stock.Count == 0)
            {
                return null;
            }
            return stock[0];
        }
    }
}
