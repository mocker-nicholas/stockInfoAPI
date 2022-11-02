using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Stocks;

namespace stockInfoApi.DAL.Handlers.Stocks
{
    public class GetStocksListHandler : IRequestHandler<GetStocksListQuery, IEnumerable<StockDbo>>
    {
        private readonly DevDbContext _context;
        public GetStocksListHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockDbo>> Handle(GetStocksListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<StockDbo> stocks = await _context.Stocks.Where(
            s => s.AccountId!
                 .ToString() == request.GetStocksDto.AccountId
                 .ToString())
                 .ToListAsync();
            if (!stocks.Any())
            {
                return null
;
            }
            return stocks;
        }
    }
}
