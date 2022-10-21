using Newtonsoft.Json;
using stockInfoApi.DAL.YFDto;

namespace stockInfoApi.Helpers
{
    public class StockQuotes
    {
        public async Task<QuoteDto> NewQuote(string baseUrl, string apiKey, string ticker)
        {
            using var req = new HttpClient();
            req.DefaultRequestHeaders.Add("x-api-key", apiKey);
            HttpResponseMessage response = await req.GetAsync($"{baseUrl}/quote?region=US&lang=en&symbols={ticker.ToUpper()}");
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
