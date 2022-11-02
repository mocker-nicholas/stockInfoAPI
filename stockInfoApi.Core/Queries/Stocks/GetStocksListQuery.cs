using MediatR;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockDtos;

namespace stockInfoApi.DAL.Queries.Stocks
{
    public record GetStocksListQuery(GetStocksDto GetStocksDto) : IRequest<IEnumerable<StockDbo>>;
}
