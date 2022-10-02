

using Newtonsoft.Json;

namespace stockInfoApi.Models.ErrorDtos
{
    public class ErrorMessageDto
    {
        [JsonProperty("status")]
        public string Status { get; } = "error";
        [JsonProperty("message")]
        public string Message { get; set; } = "";

        public ErrorMessageDto(string message)
        {
            Message = message;
        }
    }
}
