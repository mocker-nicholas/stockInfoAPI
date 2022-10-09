

using Newtonsoft.Json;

namespace stockInfoApi.Models.ErrorDtos
{
    public class ErrorMessageDto
    {
        const object _data = null;

        [JsonProperty("status")]
        public string Status { get; } = "error";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
        public Object Data { get; set; } = new object();

        public ErrorMessageDto(string message, object data = _data)
        {
            Message = message;
            Data = data;
        }
    }
}
