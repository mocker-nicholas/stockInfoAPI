using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Transactions;

namespace stockInfoApi.DAL.Handlers.Transactions
{
    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, StockTransactionDbo>
    {
        private readonly DevDbContext _context;
        public GetTransactionByIdHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<StockTransactionDbo> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            StockTransactionDbo? transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.AccountId == request.GetTransactionDto.AccountId && x.TransactionId == request.Id);
            return transaction;
        }
    }
}
