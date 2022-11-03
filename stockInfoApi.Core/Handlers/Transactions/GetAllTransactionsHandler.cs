using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Transactions;

namespace stockInfoApi.DAL.Handlers.Transactions
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<StockTransactionDbo>>
    {
        private readonly DevDbContext _context;
        public GetAllTransactionsHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockTransactionDbo>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<StockTransactionDbo> transactions = await (from Transaction in _context.Transactions
                                                                   where Transaction.AccountId == request.GetTransactionsDto.AccountId
                                                                   select Transaction).ToListAsync();
            // await _context.Transactions.Where(x => x.AccountId == accountId).ToListAsync();
            return transactions;
        }
    }
}
