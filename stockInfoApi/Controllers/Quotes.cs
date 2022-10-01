using Microsoft.AspNetCore.Mvc;
using stockInfoApi.Models.YFDto;
using stockInfoApi.Helpers;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Quotes : ControllerBase
    {
        private readonly IConfiguration _config;
        public Quotes(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{symbol}")]
        public async Task<QuoteResultDto> GetQuote(string symbol)
        {
            try
            {
            var request = new ServicesHelper(_config);
            var result = await request.NewQuote(symbol);
            var response = new QuoteResultDto("success", "success", result);
            return response;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
