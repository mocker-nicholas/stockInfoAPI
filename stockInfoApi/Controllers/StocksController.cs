using Microsoft.AspNetCore.Mvc;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.ErrorDtos;
using stockInfoApi.Models.StockDto;

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
        public IActionResult GetStock()
        {
            return Ok(new SuccessMessageDto($"Hey you got me!"));
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById(string id)
        {
            return Ok(new SuccessMessageDto($"Hey you got me {id}!"));
        }


        [HttpPost()]
        public async Task<IActionResult> BuyStock( PostStockDto postStockDto)
        {
            var request = new ServicesHelper(_config);
            var details = await request.NewQuote(postStockDto.Symbol);
            // reach out for a quote
            // do the math on number of shares
            // subract the amount from the account total
            // add the current value of all shares
            // update the stock in the db
            var test = details.QuoteResponse;
            return Ok(test);
        }

        [HttpDelete("{symbol}")]
        public IActionResult SellStock(string symbol)
        {
            return Ok();
        }
    }
}
