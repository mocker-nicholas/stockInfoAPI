using Microsoft.AspNetCore.Mvc;
using stockInfoApi.Models.ErrorDtos;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
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
        public IActionResult BuyStock()
        {
            // Take in a stock post dto
            // reach out for a quote
            // do the math on number of shares
            // subract the amount from the account total
            // add the current value of all shares
            // update the stock in the db
            return Ok();
        }

        [HttpDelete("{symbol}")]
        public IActionResult SellStock(string symbol)
        {
            return Ok();
        }
    }
}
