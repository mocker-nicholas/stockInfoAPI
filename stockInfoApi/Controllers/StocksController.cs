using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStocksFeatures _stockFeatures;
        private readonly IAccountFeatures _accountFeatures;

        public StocksController(
            DevDbContext context,
            IConfiguration config,
            IStocksFeatures stockFeatures,
            IAccountFeatures accountFeatures

        )
        {
            _context = context;
            _config = config;
            _stockFeatures = stockFeatures;
            _accountFeatures = accountFeatures;
        }

        /// <summary>
        /// Get all owned stocks for an account
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetStocks(GetStocksDto getStocksDto)
        {
            IEnumerable<StockDbo> stocks = await _stockFeatures.GetAllStocks(getStocksDto);
            if (!stocks.Any())
            {
                return NotFound(new ResponseMessageDto<StockDbo>("error", "no stocks found"));
            }
            return Ok(new ResponseMessageDto<IEnumerable<StockDbo>>("success", "success", stocks));
        }

        /// <summary>
        /// Get quote data for a stock by symbol
        /// </summary>
        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockBySymbol(string symbol)
        {
            Result result = await _stockFeatures.GetStockBySymbol(symbol);
            if (result != null)
            {
                return NotFound(new ResponseMessageDto<StockDbo>("error", "no stocks found"));
            }
            return Ok(new ResponseMessageDto<Result>("success", "success", result));
        }

        /// <summary>
        /// Create a new stock transaction
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> StockTrans(PostStockDto postStockDto)
        {
            AccountDbo account =
                await _accountFeatures.GetAccountById(postStockDto.AccountId);
            Result quoteData =
                await _stockFeatures.GetStockBySymbol(postStockDto.Symbol);
            StockDbo existingStock =
                await _stockFeatures
                .StockExists(postStockDto.AccountId, postStockDto.Symbol);

            ValidationCheck transactionIsValid =
                 _stockFeatures.TransactionValidation(
                postStockDto,
                account,
                quoteData,
                existingStock
            );
            if (transactionIsValid.Error)
            {
                return BadRequest(new ResponseMessageDto<ValidationCheck>("error", transactionIsValid.Message));
            }

            StockTransactionDbo transaction =
                await _stockFeatures.CreateStockTransaction(
                postStockDto,
                account,
                quoteData,
                existingStock
            );
            return Ok(new ResponseMessageDto<StockTransactionDbo>("success", "success", transaction));
        }
    }
}
