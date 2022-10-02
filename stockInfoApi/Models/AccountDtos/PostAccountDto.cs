using Newtonsoft.Json;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.Helpers.Enums;

namespace stockInfoApi.Models.AccountDtos
{
    public class PostAccountDto
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
