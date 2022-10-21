using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using static stockInfoApi.DAL.Enums.Enums;

namespace stockInfoApi.Models.AccountDtos
{
    public class PostAccountDto
    {
        [Required]
        [JsonProperty("accountType")]
        [JsonConverter(typeof(StringEnumConverter))] // Tell the route this is coming in as a string
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
