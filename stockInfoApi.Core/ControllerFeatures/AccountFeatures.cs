using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Services;
using stockInfoApi.Models.AccountDtos;

namespace stockInfoApi.DAL.ControllerFeatures
{
    public class AccountFeatures : IAccountFeatures
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;
        private readonly StockQuotes _request;
        public AccountFeatures(DevDbContext context, IConfiguration config, StockQuotes request)
        {
            _context = context;
            _config = config;
            _request = request;
        }

        // <summary>
        // Get all currently existing accounts
        // </summary>
        public async Task<IEnumerable<AccountDbo>> GetAllAccounts()
        {
            List<AccountDbo> accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        // <summary>
        // Get an account by it's account Id
        // </summary>
        public async Task<AccountDbo> GetAccountById(Guid id)
        {
            AccountDbo account = await _context.Accounts.FirstOrDefaultAsync(x => id == x.AccountId);

            if (account == null)
                return null;
            await GetUpToDateData(id);
            return account;
        }

        // <summary>
        // Update an existing account
        // </summary>
        public async Task<AccountDbo> UpdateAccount(Guid id, PutAccountDto body)
        {
            // find account by email to ensure you arent using duplicated emails
            List<AccountDbo> existingAccounts = await _context.Accounts.Where(x => x.EmailAddress == body.EmailAddress).ToListAsync();
            foreach (AccountDbo existingAccount in existingAccounts)
            {
                if (existingAccount.AccountId != id)
                    return existingAccount;
            }

            AccountDbo account = await _context.Accounts.FindAsync(id);
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
            AccountDbo account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return null;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
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

        private async Task GetUpToDateData(Guid id)
        {
            AccountDbo account = await _context.Accounts.FirstOrDefaultAsync(x => id == x.AccountId);
            List<StockDbo> stocks = await (from Stock in _context.Stocks
                                           where Stock.AccountId == id
                                           select Stock).ToListAsync();
            if (stocks.Any())
            {
                foreach (StockDbo stock in stocks)
                {
                    QuoteDto details = await _request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], stock.Symbol);
                    Result result = details.QuoteResponse.Result[0];
                    stock.TotalHoldings = result.Ask * stock.NumShares;
                }

                account.StockHoldings = Math.Round(stocks.Aggregate((double)0, (curr, accum) => curr + accum.TotalHoldings), 2, MidpointRounding.AwayFromZero);
                await _context.SaveChangesAsync();
            }
        }
    }
}
