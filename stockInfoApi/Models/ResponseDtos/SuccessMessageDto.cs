﻿using Newtonsoft.Json;
using stockInfoApi.Interfaces;

namespace stockInfoApi.Models.ErrorDtos
{
    public class SuccessMessageDto : IResponseBody
    {
        const object _data = null;

        [JsonProperty("status")]
        public string Status { get; } = "success";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
        [JsonProperty("data")]
        public Object Data { get; set; } = new object();

        public SuccessMessageDto(string message, object data = _data)
        {
            Message = message;
            Data = data;
        }
    }
}
