using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stockInfoApi.DAL.Models.DboModels
{
    public class StockDbo
    {
        [Key]
        [JsonProperty("stockId")]
        public Guid StockId { get; set; } = Guid.NewGuid();

        [Required]
        [JsonProperty("accountId")]
        [ForeignKey("AccountDbo")]
        public Guid AccountId { get; set; }

        [Required]
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [JsonProperty("totalHoldings")]
        public double TotalHoldings { get; set; } = 0;

        [Required]
        [JsonProperty("numShares")]
        public int NumShares { get; set; } = 0;

        public StockDbo(Guid accountId, string symbol, double totalHoldings, int numShares)
        {
            AccountId = accountId;
            Symbol = symbol;
            TotalHoldings = Math.Round(totalHoldings, 2, MidpointRounding.AwayFromZero);
            NumShares = numShares;
        }
    }
}
