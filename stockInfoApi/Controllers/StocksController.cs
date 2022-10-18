using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.DboModels;
using stockInfoApi.Models.ResponseDtos;
using stockInfoApi.Models.StockDtos;
using stockInfoApi.Models.YFDto;
using static stockInfoApi.Helpers.Enums;

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
            var request = new StockQuotes();
            var details = await request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], symbol);
            var quote = details.QuoteResponse.Result[0];
            return Ok(new ResponseMessageDto<Result>("success", "success", quote));
        }

        // Purchase or Sell a stock
        [HttpPost()]
        public async Task<IActionResult> StockTrans(PostStockDto postStockDto)
        {
            if (postStockDto.TranType == Enums.TransactionType.Buy)
            {
                // Check for existing stock for user
                var existingStock = StockExists(postStockDto.TranType, postStockDto.AccountId, postStockDto.Symbol);
                // Get data
                var request = new StockQuotes();
                var details = await request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], postStockDto.Symbol);
                var quote = details.QuoteResponse.Result[0];
                var ask = quote.Ask;
                var totalHoldings = ask * postStockDto.NumShares;

                if (existingStock.Result)
                {
                    /////// YOU ARE HERE
                    var stockUpdate = _context.Stocks.Where(x => x.Symbol == postStockDto.Symbol && x.AccountId == postStockDto.AccountId);
                    /////// YOU ARE HERE
                }
                // Add stock to account
                var stock = new StockDbo(
                    postStockDto.AccountId,
                    postStockDto.Symbol,
                    totalHoldings,
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

        private async Task<bool> StockExists(TransactionType TransactionType, Guid accountId, string symbol)
        {
            var ownedStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol && x.AccountId == accountId);
            if (ownedStock != null)
                return true;
            return false;
        }
    }
}
