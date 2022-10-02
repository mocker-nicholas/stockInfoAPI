using Newtonsoft.Json;

namespace stockInfoApi.Models.ErrorDtos
{
    public class SuccessMessageDto
    {
        [JsonProperty("status")]
        public string Status { get; } = "success";
        [JsonProperty("message")]
        public string Message { get; set; } = "";

        public SuccessMessageDto(string message)
        {
            Message = message;
        }
    }
}
