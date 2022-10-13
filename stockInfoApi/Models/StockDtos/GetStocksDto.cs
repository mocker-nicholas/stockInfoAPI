using Newtonsoft.Json;

namespace stockInfoApi.Models.StockDtos
{
    public class GetStocksDto
    {
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }
    }
}
