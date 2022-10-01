using Newtonsoft.Json;
using stockInfoApi.Models.YFDto;

namespace stockInfoApi.Helpers
{
    public class ServicesHelper
    {
        private readonly IConfiguration _config;
        public ServicesHelper(IConfiguration config)
        {
            _config = config;
        }
        public async Task<QuoteDto> NewQuote(string ticker)
        {
            using var req = new HttpClient();
            req.DefaultRequestHeaders.Add("x-api-key", _config["YF_API_KEY"]);
            HttpResponseMessage response = await req.GetAsync($"{_config["YF_BASE_URL"]}/quote?region=US&lang=en&symbols={ticker.ToUpper()}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject<QuoteDto>(result);
                return json;
            }
            else
            {
                return new QuoteDto();
            }
        }
    }
}
