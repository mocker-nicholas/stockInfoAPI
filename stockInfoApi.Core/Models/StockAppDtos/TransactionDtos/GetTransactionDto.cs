using Newtonsoft.Json;

namespace stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos
{
    public class GetTransactionDto
    {
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }
    }
}
