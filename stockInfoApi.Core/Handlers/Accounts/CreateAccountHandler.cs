using MediatR;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Accounts;

namespace stockInfoApi.DAL.Handlers.Accounts
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountQuery, AccountDbo>
    {
        private readonly DevDbContext _context;
        public CreateAccountHandler(DevDbContext context)
        {
            _context = context;
        }
        public async Task<AccountDbo> Handle(CreateAccountQuery request, CancellationToken cancellationToken)
        {
            var existingAccount = AccountAlreadyExists(request.PostAccountDto.EmailAddress);
            if (existingAccount)
            {
                return null;
            }
            var newAccount = new AccountDbo(
            request.PostAccountDto.AccountType,
            request.PostAccountDto.FirstName,
            request.PostAccountDto.LastName,
                request.PostAccountDto.EmailAddress,
            request.PostAccountDto.Nickname
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
