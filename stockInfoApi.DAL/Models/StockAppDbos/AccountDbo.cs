using Newtonsoft.Json;
using stockInfoApi.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.DAL.Enums.Enums;

namespace stockInfoApi.DAL.Models.DboModels
{
    public class AccountDbo : IDateCreateable, IModifiable
    {
        [JsonProperty("accountId")]
        [Key]
        public Guid AccountId { get; set; } = Guid.NewGuid();

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

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public AccountDbo(
            AccountType accountType, 
            string firstName, 
            string lastName, 
            string emailAddress, 
            string nickname
            )
        {
            AccountType = accountType;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Nickname = nickname;
        }
    }
}
