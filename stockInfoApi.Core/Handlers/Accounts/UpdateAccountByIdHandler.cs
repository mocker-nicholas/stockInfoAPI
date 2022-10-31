using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Accounts;

namespace stockInfoApi.DAL.Handlers.Accounts
{
    public class UpdateAccountByIdHandler : IRequestHandler<UpdateAccountByIdQuery, AccountDbo>
    {
        private readonly DevDbContext _context;
        public UpdateAccountByIdHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDbo> Handle(UpdateAccountByIdQuery request, CancellationToken cancellationToken)
        {
            List<AccountDbo> existingAccounts = await _context.Accounts.Where(x => x.EmailAddress == request.PutAccountDto.EmailAddress).ToListAsync();
            foreach (AccountDbo existingAccount in existingAccounts)
            {
                if (existingAccount.AccountId != request.Id)
                    return existingAccount;
            }

            AccountDbo account = await _context.Accounts.FindAsync(request.Id);
            if (account == null)
            {
                return account;
            }

            account.AccountType = request.PutAccountDto.AccountType;
            account.FirstName = request.PutAccountDto.FirstName;
            account.LastName = request.PutAccountDto.LastName;
            account.EmailAddress = request.PutAccountDto.EmailAddress;
            account.Nickname = request.PutAccountDto.Nickname;
            account.ModifiedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDboExists(request.Id))
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

        private bool AccountDboExists(Guid id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
