﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.DboModels;
using stockInfoApi.Models.ResponseDtos;
using stockInfoApi.Models.StockDtos;
using stockInfoApi.Models.YFDto;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;
        //private readonly DbHelper _dbHelper;

        public StocksController(DevDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            //_dbHelper = dbHelper;
        }

        // Get Stocks for account
        [HttpGet()]
        public async Task<IActionResult> GetStocks(GetStocksDto getStocksDto)
        {
           var stocks = await _context.Stocks.Where(
                s => s.AccountId!.ToString() == getStocksDto.AccountId.ToString()).ToListAsync();
            return Ok(new ResponseMessageDto<IEnumerable<StockDbo>>("success", "success", stocks));
        }

        // Get quote for a stock
        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockById(string symbol)
        {
            // Inject this as a depency and register it in the IOC
            var request = new ServicesHelper(_config);
            var details = await request.NewQuote(symbol);
            var quote = details.QuoteResponse.Result[0];
            return Ok(new ResponseMessageDto<Result>("success", "success", quote));
        }

        // Purchase or Sell a stock
        [HttpPost()]
        public async Task<IActionResult> StockTrans(PostStockDto postStockDto)
        {
            if (postStockDto.TranType == Enums.TransactionType.Buy)
            {
                // Get data
                var request = new ServicesHelper(_config);
                var details = await request.NewQuote(postStockDto.Symbol);
                var quote = details.QuoteResponse.Result[0];
                var ask = quote.Ask;
                // Add stock to account
                var stock = new StockDbo(
                    postStockDto.AccountId,
                    postStockDto.Symbol,
                    ask,
                    postStockDto.NumShares,
                    ask
                );

                _context.Stocks.Add(stock);
                _context.SaveChanges();
                // Format and send response
                var transactionResult = new StockTransactionDbo(
                    postStockDto.AccountId,
                    postStockDto.Symbol,
                    postStockDto.NumShares,
                    postStockDto.TranType,
                    ask
                );
                // subract the amount from the account total
                // add the current value of all shares
                // update the stock in the db
                return Ok(new ResponseMessageDto<StockTransactionDbo>("success", "success", transactionResult));
            }
            else
            {
                return BadRequest("Invalid transaction type, im getting there!");
            }
        }
    }
}
