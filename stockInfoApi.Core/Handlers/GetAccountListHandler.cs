using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries;

namespace stockInfoApi.DAL.Handlers
{
    public class GetAccountListHandler : IRequestHandler<GetAccountListQuery, IEnumerable<AccountDbo>>
    {
        private readonly DevDbContext _context;
        public GetAccountListHandler(DevDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AccountDbo>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
        {
            List<AccountDbo> accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }
    }
}
