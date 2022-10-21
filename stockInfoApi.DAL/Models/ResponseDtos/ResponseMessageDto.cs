using Newtonsoft.Json;

namespace stockInfoApi.DAL.ResponseDtos
{
    public class ResponseMessageDto<T>
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
        [JsonProperty("data")]
        public T? Data { get; set; }

        public ResponseMessageDto(string message, string status, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResponseMessageDto(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
