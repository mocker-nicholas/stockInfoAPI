using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;
using stockInfoApi.Models.ResponseDtos;

namespace stockInfoApi.DAL.ControllerFeatures
{
    public class AccountFeatures : IAccountFeatures
    {
        private readonly DevDbContext _context;

        public AccountFeatures(DevDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Get all currently existing accounts
        // </summary>
        public async Task<IEnumerable<AccountDbo>> GetAllAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        // <summary>
        // Get an account by it's account Id
        // </summary>
        public async Task<AccountDbo> GetAccountById(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
                return null;
            return null;
        }

        // <summary>
        // Update an existing account
        // </summary>
        public async Task<AccountDbo> UpdateAccount(Guid id, PutAccountDto body)
        {
            var validDto = DtoValidations.ValidPutAccountDto(body);
            if (validDto.Error)
            {
                return null;
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
            return account;
            }

            account.AccountType = body.AccountType;
            account.FirstName = body.FirstName;
            account.LastName = body.LastName;
            account.EmailAddress = body.EmailAddress;
            account.Nickname = body.Nickname;
            account.ModifiedAt = DateTime.UtcNow;

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDboExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return account;
        }

        // <summary>
        // Create a new account
        // </summary>
        public async Task<AccountDbo> CreateAccount(PostAccountDto account)
        {
            var validDto = DtoValidations.ValidPostAccountDto(account);
            if (validDto.Error)
            {
                return null;
            }

            var existingAccount = AccountAlreadyExists(account.EmailAddress);
            if (existingAccount)
            {
                return null;
            }
            var newAccount = new AccountDbo(
                account.AccountType,
                account.FirstName,
                account.LastName,
                account.EmailAddress,
                account.Nickname
             );
            _context.Accounts.Add(newAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return newAccount;
        }

        // <summary>
        // Delete an account from the Accounts Table
        // </summary>
        public async Task<AccountDbo> DeleteAccount(Guid id)
        {
            var accountDbo = await _context.Accounts.FindAsync(id);
            if (accountDbo == null)
            {
                return null;
            }

            _context.Accounts.Remove(accountDbo);
            await _context.SaveChangesAsync();

            return null;
        }

        // <summary>
        // Methods used in accounts features
        // </summary>
        private bool AccountDboExists(Guid id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        private bool AccountAlreadyExists(string email)
        {
            var existingAccount = _context.Accounts
                .Where(a => a.EmailAddress.Equals(email));
            if (existingAccount.Any())
                return true;
            return false;
        }
    }
}
