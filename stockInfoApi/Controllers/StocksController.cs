using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.ErrorDtos;
using stockInfoApi.Models.StockDto;
using stockInfoApi.Models.StockDtos;
using stockInfoApi.Models.StockDtos.ResponseDtos;
using System.Threading.Tasks;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;

        public StocksController(DevDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet()]
        public async Task<IActionResult> GetStock(string symbol)
        {
            return Ok();
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockById(string symbol)
        {
            var request = new ServicesHelper(_config);
            var details = await request.NewQuote(symbol);
            var quote = details.QuoteResponse.Result[0];
            return Ok(new SuccessMessageDto("success", quote));
        }


        [HttpPost()]
        public async Task<IActionResult> BuyStock(PostStockDto postStockDto)
        {
            var request = new ServicesHelper(_config);
            var details = await request.NewQuote(postStockDto.Symbol);
            var quote = details.QuoteResponse.Result[0];
            var ask = quote.Ask;

            var transactionResult = new StockTransaction(
                postStockDto.AccountId,
                postStockDto.Symbol,
                postStockDto.NumShares,
                postStockDto.TranType,
                ask
            );
            // subract the amount from the account total
            // add the current value of all shares
            // update the stock in the db
            return Ok(new SuccessMessageDto("success", transactionResult));
        }

        [HttpDelete("{symbol}")]
        public IActionResult SellStock(string symbol)
        {
            return Ok();
        }
    }
}
