using stockInfoApi.DAL.Models.AccountDtos;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Interfaces
{
    public interface IAccountFeatures
    {
        Task<AccountDbo> DeleteAccount(Guid id);
        Task<AccountDbo> CreateAccount(PostAccountDto account);
        Task<AccountDbo> UpdateAccount(Guid id, PutAccountDto body);
        Task<AccountDbo> GetAccountById(Guid id);
        Task<IEnumerable<AccountDbo>> GetAllAccounts();
    }
}
