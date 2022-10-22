using Newtonsoft.Json;

namespace stockInfoApi.DAL.Models.StockDtos
{
    public class GetStocksDto
    {
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }
    }
}
