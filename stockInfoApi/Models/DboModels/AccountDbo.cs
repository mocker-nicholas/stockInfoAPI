using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.Helpers.Enums;

namespace stockInfoApi.Models.DboModels
{
    public class AccountDbo
    {
        [Key]
        [Required]
        [JsonProperty("accountId")]
        public Guid AccountId { get; } = Guid.NewGuid();

        [Required]
        [JsonProperty("accountType")]
        public AccountType AccountType { get; set; }

        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = "";

        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; } = "";

        [Required]
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; } = "";

        [JsonProperty("nickname")]
        public string Nickname { get; set; } = "";

        [JsonProperty("balance")]
        public double Balance { get; } = 10000;

        [JsonProperty("stocks")]
        public List<StockDbo>? Stocks { get; set; }

        [JsonProperty("transactions")]
        public List<TransactionDbo>? Transactions { get; set; }
    }
}
