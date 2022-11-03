using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos;

namespace stockInfoApi.DAL.Queries.Transactions
{
    public record GetAllTransactionsQuery(GetTransactionsDto GetTransactionsDto) : IRequest<IEnumerable<StockTransactionDbo>>;
}
