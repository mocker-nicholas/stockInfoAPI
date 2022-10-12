using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.Helpers.Enums;

namespace stockInfoApi.Models.StockDtos
{
    public class PostStockDto
    {
        //[Key]
        //[JsonProperty("stockId")]
        //public Guid StockId { get; set; } = Guid.NewGuid();

        [Required]
        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }

        [Required]
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [JsonProperty("tranType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType TranType { get; set; }

        //[Required]
        //[JsonProperty("purchasePrice")]
        //public double PurchasePrice { get; set; } = 0;

        [Required]
        [JsonProperty("numShares")]
        public int NumShares { get; set; } = 0;

        //[Required]
        //[JsonProperty("sharePrice")]
        //public double SharePrice { get; set; } = 0;
    }
}
