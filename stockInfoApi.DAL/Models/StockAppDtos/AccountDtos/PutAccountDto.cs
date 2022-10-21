using static stockInfoApi.DAL.Enums.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace stockInfoApi.DAL.Models.AccountDtos
{
    public class PutAccountDto
    {
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
    }
}
