using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using stockInfoApi.Models.DboModels;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.Helpers.Enums;

namespace stockInfoApi.Models.StockDtos.ResponseDtos
{
    public class StockTransaction
    {
        [Key]
        [JsonProperty("transactionId")]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [JsonProperty("accountId")]
        public Guid AccountId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonProperty("numShares")]
        public int NumShares { get; set; } = 0;

        [Required]
        [JsonProperty("tranType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType TranType { get; set; }

        [JsonProperty("sharePrice")]
        public double SharePrice { get; set; } = 0;

        [JsonProperty("transactionAmount")]
        public double TransactionAmount { get; }

        public StockTransaction( Guid accountId, string symbol, int numShares, TransactionType tranType, double sharePrice)
        {
            AccountId = accountId;
            Symbol = symbol;
            NumShares = numShares;
            TranType = tranType;
            SharePrice = sharePrice;
            TransactionAmount = Math.Round((numShares * sharePrice), 2, MidpointRounding.AwayFromZero);
        }
    }
}
