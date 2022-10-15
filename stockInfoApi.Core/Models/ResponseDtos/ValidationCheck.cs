namespace stockInfoApi.Core.Models.ResponseDtos
{
    public class ValidationCheck
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; } = "";

        public ValidationCheck(bool error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}
