using Newtonsoft.Json;
using stockInfoApi.DAL.YFDto;

namespace stockInfoApi.DAL.Models.YFDto
{
    public class QuoteResultDto
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";
        [JsonProperty("message")]
        public string Message { get; set; } = "";

        [JsonProperty("data")]
        public QuoteDto Data { get; set; }

        public QuoteResultDto(string status, string message, QuoteDto quote)
        {
            Status = status;
            Message = message;
            Data = quote;
        }
    }
}
