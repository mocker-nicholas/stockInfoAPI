using MediatR;
using stockInfoApi.DAL.Models.YFDto;

namespace stockInfoApi.DAL.Queries.Stocks
{
    public record GetStockBySymbolQuery(string Symbol) : IRequest<Result>;
}
