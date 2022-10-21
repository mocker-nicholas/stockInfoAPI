using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;
using stockInfoApi.Models.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
