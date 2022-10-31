using MediatR;
using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockDtos;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Queries.Accounts;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksFeatures _stockFeatures;
        private readonly IMediator _mediator;

        public StocksController(
            IStocksFeatures stockFeatures,
            IMediator mediator

        )
        {
            _stockFeatures = stockFeatures;
            _mediator = mediator;
        }

        /// <summary>
        /// Get all owned stocks for an account
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetStocks(GetStocksDto getStocksDto)
        {
            IEnumerable<StockDbo> stocks = await _stockFeatures.GetAllStocks(getStocksDto);
            if (stocks == null)
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
                await _mediator.Send(new GetAccountByIdQuery(postStockDto.AccountId));
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
