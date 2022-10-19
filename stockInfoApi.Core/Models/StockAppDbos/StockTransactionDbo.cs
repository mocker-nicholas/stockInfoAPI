using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using stockInfoApi.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.Helpers.Enums;

namespace stockInfoApi.Models.DboModels
{
    public class StockTransactionDbo : IDateCreateable
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

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public StockTransactionDbo(
            Guid accountId, 
            string symbol, 
            int numShares, 
            TransactionType tranType, 
            double sharePrice
         )
        {
            AccountId = accountId;
            Symbol = symbol;
            NumShares = numShares;
            TranType = tranType;
            SharePrice = sharePrice;
            TransactionAmount = Math.Round(numShares * sharePrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}
