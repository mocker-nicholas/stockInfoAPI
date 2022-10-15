using Newtonsoft.Json;
using stockInfoApi.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stockInfoApi.Models.DboModels
{
    public class StockDbo : IDateCreateable, IModifiable
    {
        [Key]
        [JsonProperty("stockId")]
        public Guid StockId { get; set; } = Guid.NewGuid();

        public AccountDbo? AccountDbo { get; set; }

        [Required]
        [JsonProperty("accountId")]
        [ForeignKey("AccountDbo")]
        public Guid AccountId { get; set; }

        [Required]
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [JsonProperty("purchasePrice")]
        public double PurchasePrice { get; set; } = 0;

        [Required]
        [JsonProperty("numShares")]
        public int NumShares { get; set; } = 0;

        [Required]
        [JsonProperty("sharePrice")]
        public double SharePrice { get; set; } = 0;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public StockDbo(Guid accountId, string symbol, double purchasePrice, int numShares, double sharePrice)
        {
            AccountId = accountId;
            Symbol = symbol;
            PurchasePrice = purchasePrice;
            NumShares = numShares;
            SharePrice = sharePrice;
        }
    }
}
