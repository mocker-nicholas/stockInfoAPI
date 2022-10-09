using Newtonsoft.Json.Linq;
using stockInfoApi.Models.HelperModels;

namespace stockInfoApi.Interfaces
{
    public interface IRequestValidation
    {
        public ValidationCheck PostValidation(Object obj);
        public ValidationCheck PutValidation(Object obj);
        public ValidationCheck PatchValidation(Object obj);
    }
}
