using MediatR;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Accounts;

namespace stockInfoApi.DAL.Handlers.Accounts
{
    public class DeleteAccountByIdHandler : IRequestHandler<DeleteAccountByIdQuery, AccountDbo>
    {
        private readonly DevDbContext _context;
        public DeleteAccountByIdHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDbo> Handle(DeleteAccountByIdQuery request, CancellationToken cancellationToken)
        {
            AccountDbo account = await _context.Accounts.FindAsync(request.Id);
            if (account == null)
            {
                return null;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }
    }
}
