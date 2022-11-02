using MediatR;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Queries.Stocks;

namespace stockInfoApi.DAL.Handlers.Stocks
{
    public class StockExistsHandler : IRequestHandler<StockExistsQuery, StockDbo>
    {
        public readonly DevDbContext _context;
        public StockExistsHandler(DevDbContext context)
        {
            _context = context;
        }

        public async Task<StockDbo> Handle(StockExistsQuery request, CancellationToken cancellationToken)
        {
            List<StockDbo> stock = await _context.Stocks.Where(
                x => x.Symbol == request.Symbol && x.AccountId == request.AccountId
            ).ToListAsync();
            if (stock.Count == 0)
            {
                return null;
            }
            return stock[0];
        }
    }
}
