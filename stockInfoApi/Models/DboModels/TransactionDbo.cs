using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace stockInfoApi.Models.DboModels
{
    public class TransactionDbo
    {
        [Key]
        [JsonProperty("transactionId")]
        public Guid TransactionId { get; } = Guid.NewGuid();

        [Required]
        [JsonProperty("accountId")]
        public AccountDbo? AccountId { get; set; }

        [Required]
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        [Required]
        [JsonProperty("numShares")]
        public int NumShares { get; set; } = 0;

        [Required]
        [JsonProperty("sharePrice")]
        public double SharePrice { get; set; } = 0;

        [Required]
        [JsonProperty("total")]
        public double Total { get; set; } = 0;
    }
}
