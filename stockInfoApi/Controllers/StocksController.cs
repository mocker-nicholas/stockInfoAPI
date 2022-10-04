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
    }
}
