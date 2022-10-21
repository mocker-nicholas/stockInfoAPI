using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace stockInfoApi.DAL.YFDto
{
    public class QuoteDto
    {
        [JsonProperty("quoteResponse")]
        public QuoteResponse QuoteResponse { get; set; } = new QuoteResponse();
    }

    public class QuoteResponse
    {
        [JsonProperty("result")]
        public Result[] Result { get; set; } = Array.Empty<Result>();

        [JsonProperty("error")]
        public string Error { get; set; } = "There was a problem getting your quote";
    }

    public class Result
    {
        [Key]
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";
        [JsonProperty("longName")]
        public string LongName { get; set; } = "";
        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; } = 0.00;
        [JsonProperty("ask")]
        public double Ask { get; set; } = 0.00;
        [JsonProperty("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; } = 0.00;
        [JsonProperty("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; } = 0.00;
    }
}
