using MediatR;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.Queries.Stocks
{
    public record StockExistsQuery(Guid AccountId, string Symbol) : IRequest<StockDbo>;
}
