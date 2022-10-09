using Newtonsoft.Json;

namespace stockInfoApi.Models.ErrorDtos
{
    public class SuccessMessageDto
    {
        const object _data = null;

        [JsonProperty("status")]
        public string Status { get; } = "success";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
        public Object Data { get; set; }

        public SuccessMessageDto(string message, object data = _data)
        {
            Message = message;
            Data = data;
        }
    }
}
