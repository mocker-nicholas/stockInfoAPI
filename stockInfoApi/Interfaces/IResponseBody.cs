using stockInfoApi.Models.HelperModels;

namespace stockInfoApi.Interfaces
{
    public interface IResponseBody
    {
        string Status { get; }
        string Message { get; }
        Object Data { get; }
    }
}
